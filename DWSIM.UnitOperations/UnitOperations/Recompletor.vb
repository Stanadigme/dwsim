Imports DWSIM.Thermodynamics
Imports DWSIM.Thermodynamics.Streams
Imports DWSIM.SharedClasses
Imports System.Windows.Forms
Imports DWSIM.UnitOperations.UnitOperations.Auxiliary
Imports DWSIM.Thermodynamics.BaseClasses
Imports DWSIM.Interfaces.Enums
Imports DWSIM.Drawing.SkiaSharp.GraphicObjects.Shapes
Imports IronPython.Modules.PythonWeakRef

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
            m_Cooler.SetFlowsheet(FlowSheet)
            m_Cooler.CalcMode = Cooler.CalculationMode.OutletVaporFraction
            m_Cooler.OutletVaporFraction = 0
            m_Heater.CalcMode = Heater.CalculationMode.HeatAddedRemoved
            m_Heater.SetFlowsheet(FlowSheet)
            m_Mixer.PressureCalculation = Mixer.PressureBehavior.Minimum
            m_Mixer.SetFlowsheet(FlowSheet)
        End Function

        Protected m_TSec As Nullable(Of Double) = 2.5
        Protected m_Cooler As Cooler = New Cooler()
        Protected m_Heater As Heater = New Heater()
        Protected m_Splitter As Splitter = New Splitter()
        Protected m_Mixer As Mixer = New Mixer()

        Public Overrides Sub CreateDynamicProperties()
            AddDynamicProperty("Heater Volume", "Heater Volume", 1, UnitOfMeasure.volume, 1.0.GetType())
            AddDynamicProperty("Cooler Volume", "Cooler Volume", 1, UnitOfMeasure.volume, 1.0.GetType())
            AddDynamicProperty("Initialize using Inlet Stream", "Initializes the volume content with information from the inlet stream, if the content is null.", True, UnitOfMeasure.none, True.GetType())
            AddDynamicProperty("Reset Content", "Empties the volume content on the next run.", False, UnitOfMeasure.none, True.GetType())
        End Sub

        Public Overrides Sub RunDynamicModel()
            init()
            Dim Reset As Boolean = GetDynamicProperty("Reset Content")

            'Inputs
            Dim edmIn = Me.GetInletMaterialStream(0)
            Dim edmFlashLiq = Me.GetInletMaterialStream(1)
            Dim flashVap = Me.GetInletMaterialStream(2)
            Dim flashLiq = Me.GetInletMaterialStream(3)

            'Outputs
            Dim coolerOut = Me.GetOutletMaterialStream(0)
            Dim heaterOut = Me.GetOutletMaterialStream(1)
            Dim rejet = Me.GetOutletMaterialStream(2)
            Dim recirculation = Me.GetOutletMaterialStream(3)
            Dim distillatOut = Me.GetOutletMaterialStream(4)

            'edmIn.SetPressure(edmFlashLiq.GetPressure)


            m_Cooler.GraphicObject = New CoolerGraphic()
            m_Cooler.metaIms = flashVap
            m_Cooler.metaOms = coolerOut
            m_Cooler.PropertyPackage = m_Cooler.metaIms.PropertyPackage

            m_Heater.GraphicObject = New HeaterGraphic()
            m_Heater.metaIms = edmIn
            m_Heater.metaOms = heaterOut
            m_Heater.PropertyPackage = edmIn.PropertyPackage
            m_Heater.SetDynamicProperty("Pressure Control", False)

            m_Mixer.GraphicObject = New MixerGraphic()
            m_Mixer.PropertyPackage = flashLiq.PropertyPackage


            rejet.CopyFromMaterial(edmFlashLiq)
            recirculation.CopyFromMaterial(edmFlashLiq)


            If Reset Then
                m_Cooler.PropertyPackage = edmIn.PropertyPackage
                m_Cooler.DeltaQ = 0
                m_Cooler.SetDynamicProperty("Volume", GetDynamicProperty("Cooler Volume"))
                m_Cooler.GraphicObject = New CoolerGraphic()
                m_Cooler.AccumulationStream = Nothing

                m_Heater.PropertyPackage = edmIn.PropertyPackage
                m_Heater.SetDynamicProperty("Volume", GetDynamicProperty("Heater Volume"))
                m_Heater.GraphicObject = New HeaterGraphic()
                m_Heater.AccumulationStream = Nothing

                SetDynamicProperty("Reset Content", 0)
            End If

            m_Cooler.RunDynamicModel()
            Dim Pcond As Double = m_Cooler.DeltaQ.GetValueOrDefault
            'Console.WriteLine(String.Format("Puiss condensation : {0}", Pcond))

            m_Mixer.metaMS = {coolerOut, flashLiq}
            m_Mixer.metaMSOut = distillatOut
            m_Mixer.Calculate()

            Dim W As Double = 0
            Dim T2 = coolerOut.GetTemperature - m_TSec
            edmIn.SetMassFlow(W)



            If Pcond > 0 And flashVap.GetMassFlow > 0 Then
                edmIn.PropertyPackage.CurrentMaterialStream = edmIn
                Dim tmp = edmIn.PropertyPackage.CalculateEquilibrium2(FlashCalculationType.PressureTemperature, edmIn.GetPressure, T2, 0)
                Dim H2 = tmp.CalculatedEnthalpy
                W = Pcond / (H2 - edmIn.GetMassEnthalpy)

                If W <= distillatOut.GetMassFlow Then
                    ' On a besoin de moins d'eau que nécessaire pour condenser
                    ' T2 sera < Tdistillat - Tsec
                    W = distillatOut.GetMassFlow
                    edmIn.SetMassFlow(W)
                    m_Heater.CalcMode = Heater.CalculationMode.HeatAddedRemoved
                    m_Heater.DeltaQ = Pcond

                    'Pas de rejet pour conserver le max de chaleur
                    recirculation.SetMassFlow(edmFlashLiq.GetMassFlow)
                    'rejet.SetMassFlow(0)
                    'recirculation.SetMassFlow(rejet.GetMassFlow)
                Else
                    ' Qdistillat < QEdmin
                    ' On fait rentrer plus d'eau pour pouvoir condenser à T2
                    edmIn.SetMassFlow(W)
                    m_Heater.CalcMode = Heater.CalculationMode.OutletTemperature
                    m_Heater.OutletTemperature = T2

                    'Il faut évacuer le surplus
                    ' La qté à intégrer peut être plus grande que la Qté de circulation
                    If W <= (edmFlashLiq.GetMassFlow + distillatOut.GetMassFlow) Then
                        recirculation.SetMassFlow((edmFlashLiq.GetMassFlow + distillatOut.GetMassFlow) - W)

                    End If
                    ' La Qté à rejeter peut être supérieure au flux disponible !

                End If

                rejet.SetMassFlow(edmFlashLiq.GetMassFlow - recirculation.GetMassFlow)

            ElseIf distillatOut.GetMassFlow > 0 Then
                ' On produit du distillat déjà condensé (re-démarrage)
                W = distillatOut.GetMassFlow
                edmIn.SetMassFlow(W)
                m_Heater.CalcMode = Heater.CalculationMode.HeatAddedRemoved
                m_Heater.DeltaQ = 0
                rejet.SetMassFlow(edmFlashLiq.GetMassFlow - recirculation.GetMassFlow)
                ' TODO : Il faudrait que le distillat chaud puisse transférer de la chaleur
            ElseIf distillatOut.GetMassFlow Then
                'Chauffage sans production distillat
                edmIn.SetMassFlow(W)
                recirculation.SetMassFlow(edmFlashLiq.GetMassFlow)
                rejet.SetMassFlow(0)
            End If

            m_Heater.RunDynamicModel()

            Dim edm_hx_in As MaterialStream = FlowSheet.GetObject("edm_hx_in")

            If W > (edmFlashLiq.GetMassFlow + distillatOut.GetMassFlow) Then

                recirculation.SetMassFlow(0)
                heaterOut.SetMassFlow(edmFlashLiq.GetMassFlow + distillatOut.GetMassFlow)
                Dim tempHeaterOut As MaterialStream = heaterOut.Clone
                tempHeaterOut.SetMassFlow(W - heaterOut.GetMassFlow)
                m_Mixer.metaMS = {tempHeaterOut, edmFlashLiq}
                m_Mixer.metaMSOut = rejet
                m_Mixer.Calculate()
                Console.WriteLine("W > (edmFlashLiq.GetMassFlow + distillatOut.GetMassFlow)")

            End If

            If Math.Abs(edm_hx_in.GetMassFlow - (recirculation.GetMassFlow + heaterOut.GetMassFlow)) > 0.001 Then
                Console.WriteLine("Math.Abs(edm_hx_in.GetMassFlow - (recirculation.GetMassFlow + heaterOut.GetMassFlow)) > 0.001")
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
            'Inputs
            Dim edmIn = Me.GetInletMaterialStream(0)
            Dim edmFlashLiq = Me.GetInletMaterialStream(1)
            Dim flashVap = Me.GetInletMaterialStream(2)
            Dim flashLiq = Me.GetInletMaterialStream(3)

            'Outputs
            Dim coolerOut = Me.GetOutletMaterialStream(0)
            Dim heaterOut = Me.GetOutletMaterialStream(1)
            Dim rejet = Me.GetOutletMaterialStream(2)
            Dim recirculation = Me.GetOutletMaterialStream(3)
            Dim distillatOut = Me.GetOutletMaterialStream(4)

            m_Cooler.GraphicObject = New CoolerGraphic()
            m_Cooler.metaIms = flashVap
            m_Cooler.metaOms = coolerOut
            m_Cooler.PropertyPackage = m_Cooler.metaIms.PropertyPackage

            m_Heater.GraphicObject = New HeaterGraphic()
            m_Heater.metaIms = edmIn
            m_Heater.metaOms = heaterOut
            m_Heater.PropertyPackage = edmIn.PropertyPackage

            m_Mixer.GraphicObject = New MixerGraphic()
            m_Mixer.PropertyPackage = flashLiq.PropertyPackage


            rejet.CopyFromMaterial(edmFlashLiq)
            recirculation.CopyFromMaterial(edmFlashLiq)

            m_Cooler.Calculate()
            Dim Pcond As Double = m_Cooler.DeltaQ.GetValueOrDefault
            Console.WriteLine(String.Format("Puiss condensation : {0}", Pcond))

            m_Mixer.metaMS = {coolerOut, flashLiq}
            m_Mixer.metaMSOut = distillatOut
            m_Mixer.Calculate()

            Dim W As Double = 0
            Dim T2 = coolerOut.GetTemperature - m_TSec
            edmIn.SetMassFlow(W)



            If Pcond > 0 Then
                edmIn.PropertyPackage.CurrentMaterialStream = edmIn
                Dim tmp = edmIn.PropertyPackage.CalculateEquilibrium2(FlashCalculationType.PressureTemperature, edmIn.GetPressure, T2, 0)
                Dim H2 = tmp.CalculatedEnthalpy
                W = Pcond / (H2 - edmIn.GetMassEnthalpy)
                If W <= distillatOut.GetMassFlow Then
                    ' On a besoin de moins d'eau que nécessaire pour condenser
                    ' T2 sera < Tdistillat - Tsec
                    W = distillatOut.GetMassFlow
                    edmIn.SetMassFlow(W)
                    m_Heater.CalcMode = Heater.CalculationMode.HeatAddedRemoved
                    m_Heater.DeltaQ = Pcond

                    'Pas de rejet pour conserver le max de chaleur
                    rejet.SetMassFlow(0)
                    'recirculation.SetMassFlow(rejet.GetMassFlow)
                Else
                    ' Qdistillat < QEdmin
                    ' On fait rentrer plus d'eau pour pouvoir condenser à T2
                    edmIn.SetMassFlow(W)
                    m_Heater.CalcMode = Heater.CalculationMode.OutletTemperature
                    m_Heater.OutletTemperature = T2

                    'Il faut évacuer le surplus
                    ' La qté à intégrer peut être plus grande que la Qté de circulation
                    If W <= (edmFlashLiq.GetMassFlow + distillatOut.GetMassFlow) Then
                        recirculation.SetMassFlow((edmFlashLiq.GetMassFlow + distillatOut.GetMassFlow) - W)
                        rejet.SetMassFlow(edmFlashLiq.GetMassFlow - recirculation.GetMassFlow)

                    End If
                    ' La Qté à rejeter peut être supérieure au flux disponible ! 
                End If
            ElseIf distillatOut.GetMassFlow > 0 Then
                ' On produit du distillat déjà condensé (re-démarrage)
                W = distillatOut.GetMassFlow
                edmIn.SetMassFlow(W)
                m_Heater.CalcMode = Heater.CalculationMode.HeatAddedRemoved
                m_Heater.DeltaQ = 0
                ' TODO : Il faudrait que le distillat chaud puisse transférer de la chaleur

            End If

            m_Heater.Calculate()

            If W > (edmFlashLiq.GetMassFlow + distillatOut.GetMassFlow) Then

                recirculation.SetMassFlow(0)
                heaterOut.SetMassFlow(edmFlashLiq.GetMassFlow + distillatOut.GetMassFlow)
                Dim tempHeaterOut As MaterialStream = heaterOut.Clone
                tempHeaterOut.SetMassFlow(W - heaterOut.GetMassFlow)
                m_Mixer.metaMS = {tempHeaterOut, edmFlashLiq}
                m_Mixer.metaMSOut = rejet
                m_Mixer.Calculate()
            End If

        End Sub

    End Class
End Namespace