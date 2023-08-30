Imports DWSIM.Thermodynamics
Imports DWSIM.Thermodynamics.Streams
Imports DWSIM.SharedClasses
Imports System.Windows.Forms
Imports DWSIM.UnitOperations.UnitOperations.Auxiliary
Imports DWSIM.Thermodynamics.BaseClasses
Imports DWSIM.Interfaces.Enums
Imports DWSIM.Drawing.SkiaSharp.GraphicObjects.Shapes

Namespace UnitOperations
    <System.Serializable()> Public Class Recompletor

        Inherits UnitOperations.UnitOpBaseClass

        Public Overrides Property ObjectClass As SimulationObjectClass = SimulationObjectClass.Exchangers

        Public Overrides ReadOnly Property SupportsDynamicMode As Boolean = True

        Public Overrides ReadOnly Property HasPropertiesForDynamicMode As Boolean = True

        <NonSerialized> <Xml.Serialization.XmlIgnore> Public f As EditingForm_Recompletor

        Public Overrides ReadOnly Property MobileCompatible As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides Sub DisplayEditForm()
            If f Is Nothing Then
                f = New EditingForm_Recompletor With {.SimObject = Me}
                f.ShowHint = GlobalSettings.Settings.DefaultEditFormLocation
                f.Tag = "ObjectEditor"
                Me.FlowSheet.DisplayForm(f)
            Else
                If f.IsDisposed Then
                    f = New EditingForm_Recompletor With {.SimObject = Me}
                    f.ShowHint = GlobalSettings.Settings.DefaultEditFormLocation
                    f.Tag = "ObjectEditor"
                    Me.FlowSheet.DisplayForm(f)
                Else
                    f.Activate()
                End If
            End If
        End Sub

        Public Overrides Sub UpdateEditForm()
            If f IsNot Nothing Then
                If Not f.IsDisposed Then
                    f.UIThread(Sub() f.UpdateInfo())
                End If
            End If
        End Sub

        Public Overrides Sub CloseEditForm()
            If f IsNot Nothing Then
                If Not f.IsDisposed Then
                    f.Close()
                    f = Nothing
                End If
            End If
        End Sub

        Public Overrides Function GetIconBitmap() As Object
            Return My.Resources.heat_exchanger
        End Function

        Public Overrides Function GetDisplayDescription() As String
            Return ResMan.GetLocalString("RECOMPLETOR_Desc")
        End Function

        Public Overrides Function GetDisplayName() As String
            Return ResMan.GetLocalString("RECOMPLETOR_Name")
        End Function

        Public Overrides Function CloneXML() As Object
            Dim obj As ICustomXMLSerialization = New Recompletor()
            obj.LoadData(Me.SaveData)
            Return obj
        End Function

        Public Overrides Function CloneJSON() As Object
            Return Newtonsoft.Json.JsonConvert.DeserializeObject(Of Heater)(Newtonsoft.Json.JsonConvert.SerializeObject(Me))
        End Function

        Public Sub New(ByVal name As String, ByVal description As String)
            MyBase.CreateNew()
            Me.ComponentName = name
            Me.ComponentDescription = description
            init()
        End Sub

        Public Sub New()
            MyBase.New()
            init()
        End Sub

        Public Function init()
            m_Cooler.CalcMode = Cooler.CalculationMode.OutletVaporFraction
            m_Cooler.OutletVaporFraction = 0
            m_Heater.CalcMode = Heater.CalculationMode.HeatAddedRemoved
        End Function

        Protected m_TSec As Nullable(Of Double) = 2.5
        Protected m_Cooler As Cooler = New Cooler()
        Protected m_Heater As Heater = New Heater()
        Protected m_Splitter As Splitter = New Splitter()

        Public Overrides Sub CreateDynamicProperties()
            AddDynamicProperty("Heater Volume", "Heater Volume", 1, UnitOfMeasure.volume, 1.0.GetType())
            AddDynamicProperty("Cooler Volume", "Cooler Volume", 1, UnitOfMeasure.volume, 1.0.GetType())
            AddDynamicProperty("Initialize using Inlet Stream", "Initializes the volume content with information from the inlet stream, if the content is null.", True, UnitOfMeasure.none, True.GetType())
            AddDynamicProperty("Reset Content", "Empties the volume content on the next run.", False, UnitOfMeasure.none, True.GetType())
        End Sub

        Public Overrides Sub RunDynamicModel()
            Dim Reset As Boolean = GetDynamicProperty("Reset Content")

            Dim edmIn = Me.GetInletMaterialStream(0)
            Dim flashLiq = Me.GetInletMaterialStream(1)
            Dim flashVap = Me.GetInletMaterialStream(2)

            Dim coolerOut = Me.GetOutletMaterialStream(0)
            Dim heaterOut = Me.GetOutletMaterialStream(1)
            Dim rejet = Me.GetOutletMaterialStream(2)
            Dim recirculation = Me.GetOutletMaterialStream(3)

            If Reset Then
                m_Cooler.PropertyPackage = edmIn.PropertyPackage
                m_Cooler.SetDynamicProperty("Volume", GetDynamicProperty("Cooler Volume"))
                m_Cooler.GraphicObject = New CoolerGraphic()

                m_Heater.PropertyPackage = edmIn.PropertyPackage
                m_Heater.SetDynamicProperty("Volume", GetDynamicProperty("Heater Volume"))
                m_Heater.GraphicObject = New HeaterGraphic()

                SetDynamicProperty("Reset Content", 0)
            End If


        End Sub

        Public Function GetInletMaterialStreamGraphic(index As Integer) As MaterialStream
            Dim ms = Me.GetInletMaterialStream(index).Clone
            ms.GraphicObject = New MaterialStreamGraphic()
            ms.GraphicObject.CreateConnectors(0, 0)
            Return ms
        End Function

        Public Function GetOutletMaterialStreamGraphic(index As Integer) As MaterialStream
            Dim ms = Me.GetOutletMaterialStream(index).Clone
            ms.GraphicObject = New MaterialStreamGraphic()
            ms.GraphicObject.CreateConnectors(0, 0)
            Return ms
        End Function



        Public Overrides Sub Calculate(Optional ByVal args As Object = Nothing)
            Dim edmIn = Me.GetInletMaterialStreamGraphic(0)
            Dim flashLiq = Me.GetInletMaterialStreamGraphic(1)
            Dim flashVap = Me.GetInletMaterialStreamGraphic(2)

            Dim coolerOut = Me.GetOutletMaterialStreamGraphic(0)
            Dim heaterOut = Me.GetOutletMaterialStreamGraphic(1)
            Dim rejet = Me.GetOutletMaterialStreamGraphic(2)
            Dim recirculation = Me.GetOutletMaterialStreamGraphic(3)

            'm_Cooler.PropertyPackage = edmIn.PropertyPackage
            m_Cooler.SetDynamicProperty("Volume", GetDynamicProperty("Cooler Volume"))
            m_Cooler.GraphicObject = New CoolerGraphic()
            m_Cooler.GraphicObject.CreateConnectors(0, 0)
            FlowSheet.ConnectObjects(flashVap.GraphicObject, m_Cooler.GraphicObject, 0, 0)
            FlowSheet.ConnectObjects(m_Cooler.GraphicObject, coolerOut.GraphicObject, 0, 0)
            m_Cooler.Calculate()
            Me.GetInletMaterialStream(0).CopyFromMaterial(m_Cooler.GetOutletMaterialStream(0))


            'm_Heater.PropertyPackage = edmIn.PropertyPackage
            m_Heater.SetDynamicProperty("Volume", GetDynamicProperty("Heater Volume"))
            m_Heater.GraphicObject = New HeaterGraphic()

        End Sub

    End Class
End Namespace