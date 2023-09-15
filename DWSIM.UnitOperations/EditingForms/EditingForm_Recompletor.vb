Imports System.Windows.Forms
Imports DWSIM.Interfaces.Enums.GraphicObjects
Imports DWSIM.SharedClasses.UnitOperations
Imports DWSIM.UnitOperations.UnitOperations.AirCooler2
Imports su = DWSIM.SharedClasses.SystemsOfUnits



Public Class EditingForm_Recompletor
    Inherits SharedClasses.ObjectEditorForm

    Public Property SimObject As UnitOperations.UnitOpBaseClass

    Public Loaded As Boolean = False

    Dim units As SharedClasses.SystemsOfUnits.Units
    Dim nf As String

    Private Sub EditingForm_Recompletor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.ShowHint = GlobalSettings.Settings.DefaultEditFormLocation

        UpdateInfo()

    End Sub

    'Public WithEvents chkActive As System.Windows.Forms.CheckBox
    'Public WithEvents lblTag As System.Windows.Forms.TextBox
    'Public WithEvents lblStatus As System.Windows.Forms.Label
    'Public WithEvents lblConnectedTo As System.Windows.Forms.Label
    'Public WithEvents cbInlet1 As System.Windows.Forms.ComboBox
    'Public WithEvents cbOutlet1 As System.Windows.Forms.ComboBox
    'Public WithEvents rtbAnnotations As Extended.Windows.Forms.RichTextBoxExtended

    Sub UpdateInfo()

        units = SimObject.FlowSheet.FlowsheetOptions.SelectedUnitSystem
        nf = SimObject.FlowSheet.FlowsheetOptions.NumberFormat

        Loaded = False

        If Host.Items.Where(Function(x) x.Name.Contains(SimObject.GraphicObject.Tag)).Count > 0 Then
            If InspReportBar Is Nothing Then
                InspReportBar = New SharedClasses.InspectorReportBar
                InspReportBar.Dock = DockStyle.Bottom
                AddHandler InspReportBar.Button1.Click, Sub()
                                                            Dim iwindow As New Inspector.Window2
                                                            iwindow.SelectedObject = SimObject
                                                            iwindow.Show(DockPanel)
                                                        End Sub
                Me.Controls.Add(InspReportBar)
                InspReportBar.BringToFront()
            End If
        Else
            If InspReportBar IsNot Nothing Then
                Me.Controls.Remove(InspReportBar)
                InspReportBar = Nothing
            End If
        End If

        With SimObject

            'first block

            'chkActive.Checked = .GraphicObject.Active

            Me.Text = .GraphicObject.Tag & " (" & .GetDisplayName() & ")"

            lblTag.Text = .GraphicObject.Tag
            If .Calculated Then
                LabelStatut.Text = .FlowSheet.GetTranslatedString("Calculado") & " (" & .LastUpdated.ToString & ")"
                LabelStatut.ForeColor = System.Drawing.Color.Blue
            Else
                If Not .GraphicObject.Active Then
                    LabelStatut.Text = .FlowSheet.GetTranslatedString("Inativo")
                    LabelStatut.ForeColor = System.Drawing.Color.Gray
                ElseIf .ErrorMessage <> "" Then
                    LabelStatut.Text = .FlowSheet.GetTranslatedString("Erro")
                    LabelStatut.ForeColor = System.Drawing.Color.Red
                Else
                    LabelStatut.Text = .FlowSheet.GetTranslatedString("NoCalculado")
                    LabelStatut.ForeColor = System.Drawing.Color.Black
                End If
            End If

            'lblConnectedTo.Text = ""

            'If .IsSpecAttached Then lblConnectedTo.Text = .FlowSheet.SimulationObjects(.AttachedSpecId).GraphicObject.Tag
            'If .IsAdjustAttached Then lblConnectedTo.Text = .FlowSheet.SimulationObjects(.AttachedAdjustId).GraphicObject.Tag

            'connections

            Dim mslist As String() = .FlowSheet.GraphicObjects.Values.Where(Function(x) x.ObjectType = ObjectType.MaterialStream).Select(Function(m) m.Tag).OrderBy(Function(m) m).ToArray

            ComboBoxEdMIn.Items.Clear()
            ComboBoxEdMIn.Items.AddRange(mslist)
            If .GraphicObject.InputConnectors(0).IsAttached Then ComboBoxEdMIn.SelectedItem = .GraphicObject.InputConnectors(0).AttachedConnector.AttachedFrom.Tag

            ComboBoxXEdMFlashLiq.Items.Clear()
            ComboBoxXEdMFlashLiq.Items.AddRange(mslist)
            If .GraphicObject.InputConnectors(1).IsAttached Then ComboBoxXEdMFlashLiq.SelectedItem = .GraphicObject.InputConnectors(1).AttachedConnector.AttachedFrom.Tag

            ComboBoxXFlashVapor.Items.Clear()
            ComboBoxXFlashVapor.Items.AddRange(mslist)
            If .GraphicObject.InputConnectors(2).IsAttached Then ComboBoxXFlashVapor.SelectedItem = .GraphicObject.InputConnectors(2).AttachedConnector.AttachedFrom.Tag

            ComboBoxXFlashLiq.Items.Clear()
            ComboBoxXFlashLiq.Items.AddRange(mslist)
            If .GraphicObject.InputConnectors(3).IsAttached Then ComboBoxXFlashLiq.SelectedItem = .GraphicObject.InputConnectors(3).AttachedConnector.AttachedFrom.Tag

            ComboBoxXCoolerOut.Items.Clear()
            ComboBoxXCoolerOut.Items.AddRange(mslist)
            If .GraphicObject.OutputConnectors(0).IsAttached Then ComboBoxXCoolerOut.SelectedItem = .GraphicObject.OutputConnectors(0).AttachedConnector.AttachedTo.Tag

            ComboBoxXHeaterOut.Items.Clear()
            ComboBoxXHeaterOut.Items.AddRange(mslist)
            If .GraphicObject.OutputConnectors(1).IsAttached Then ComboBoxXHeaterOut.SelectedItem = .GraphicObject.OutputConnectors(1).AttachedConnector.AttachedTo.Tag

            ComboBoxRejet.Items.Clear()
            ComboBoxRejet.Items.AddRange(mslist)
            If .GraphicObject.OutputConnectors(2).IsAttached Then ComboBoxRejet.SelectedItem = .GraphicObject.OutputConnectors(2).AttachedConnector.AttachedTo.Tag

            ComboBoxRecirculation.Items.Clear()
            ComboBoxRecirculation.Items.AddRange(mslist)
            If .GraphicObject.OutputConnectors(3).IsAttached Then ComboBoxRecirculation.SelectedItem = .GraphicObject.OutputConnectors(3).AttachedConnector.AttachedTo.Tag

            ComboBoxDistillatOut.Items.Clear()
            ComboBoxDistillatOut.Items.AddRange(mslist)
            If .GraphicObject.OutputConnectors(4).IsAttached Then ComboBoxDistillatOut.SelectedItem = .GraphicObject.OutputConnectors(4).AttachedConnector.AttachedTo.Tag

        End With

        Loaded = True

    End Sub


    Private Sub lblTag_TextChanged(sender As Object, e As EventArgs)

        If Loaded Then ToolTipChangeTag.Show("Press ENTER to commit changes.", lblTag, New System.Drawing.Point(0, lblTag.Height + 3), 3000)

    End Sub

    Private Sub ComboBoxEdMIn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdMIn.SelectedIndexChanged
        If Loaded Then

            Dim text As String = ComboBoxEdMIn.Text
            Dim index As Integer = 0

            Dim gobj = SimObject.GraphicObject
            Dim flowsheet = SimObject.FlowSheet

            If text <> "" Then

                If flowsheet.GetFlowsheetSimulationObject(text).GraphicObject.OutputConnectors(0).IsAttached Then
                    MessageBox.Show(flowsheet.GetTranslatedString("Todasasconexespossve"), flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Try
                        If gobj.InputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj.InputConnectors(index).AttachedConnector.AttachedFrom, gobj)
                        flowsheet.ConnectObjects(flowsheet.GetFlowsheetSimulationObject(text).GraphicObject, gobj, 0, index)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                UpdateInfo()
            Else
                If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
            End If

        End If
    End Sub

    Private Sub ComboBoxXFlashLiq_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxXEdMFlashLiq.SelectedIndexChanged
        If Loaded Then

            Dim text As String = ComboBoxXEdMFlashLiq.Text
            Dim index As Integer = 1

            Dim gobj = SimObject.GraphicObject
            Dim flowsheet = SimObject.FlowSheet

            If text <> "" Then
                If flowsheet.GetFlowsheetSimulationObject(text).GraphicObject.OutputConnectors(0).IsAttached Then
                    MessageBox.Show(flowsheet.GetTranslatedString("Todasasconexespossve"), flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Try
                        If gobj.InputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj.InputConnectors(index).AttachedConnector.AttachedFrom, gobj)
                        flowsheet.ConnectObjects(flowsheet.GetFlowsheetSimulationObject(text).GraphicObject, gobj, 0, index)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                UpdateInfo()
            Else
                If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
            End If

        End If

    End Sub

    Private Sub ComboBoxXFlashVapor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxXFlashVapor.SelectedIndexChanged
        If Loaded Then

            Dim text As String = ComboBoxXFlashVapor.Text

            Dim index As Integer = 2

            Dim gobj = SimObject.GraphicObject
            Dim flowsheet = SimObject.FlowSheet

            If text <> "" Then

                If flowsheet.GetFlowsheetSimulationObject(text).GraphicObject.OutputConnectors(0).IsAttached Then
                    MessageBox.Show(flowsheet.GetTranslatedString("Todasasconexespossve"), flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Try
                        If gobj.InputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj.InputConnectors(index).AttachedConnector.AttachedFrom, gobj)
                        flowsheet.ConnectObjects(flowsheet.GetFlowsheetSimulationObject(text).GraphicObject, gobj, 0, index)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                UpdateInfo()
            Else
                If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
            End If

        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxXFlashLiq.SelectedIndexChanged
        If Loaded Then

            Dim text As String = ComboBoxXFlashLiq.Text

            Dim index As Integer = 3

            Dim gobj = SimObject.GraphicObject
            Dim flowsheet = SimObject.FlowSheet

            If text <> "" Then

                If flowsheet.GetFlowsheetSimulationObject(text).GraphicObject.OutputConnectors(0).IsAttached Then
                    MessageBox.Show(flowsheet.GetTranslatedString("Todasasconexespossve"), flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Try
                        If gobj.InputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj.InputConnectors(index).AttachedConnector.AttachedFrom, gobj)
                        flowsheet.ConnectObjects(flowsheet.GetFlowsheetSimulationObject(text).GraphicObject, gobj, 0, index)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                UpdateInfo()
            Else
                If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
            End If

        End If
    End Sub

    Private Sub ComboBoxXCoolerOut_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxXCoolerOut.SelectedIndexChanged
        If Loaded Then

            Dim text As String = ComboBoxXCoolerOut.Text
            Dim index As Integer = 0

            Dim gobj = SimObject.GraphicObject
            Dim flowsheet = SimObject.FlowSheet

            If text <> "" Then



                If flowsheet.GetFlowsheetSimulationObject(text).GraphicObject.InputConnectors(0).IsAttached Then
                    MessageBox.Show(flowsheet.GetTranslatedString("Todasasconexespossve"), flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Try
                        If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
                        flowsheet.ConnectObjects(gobj, flowsheet.GetFlowsheetSimulationObject(text).GraphicObject, 0, 0)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                UpdateInfo()
            Else
                If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
            End If

        End If
    End Sub

    Private Sub ComboBoxXHeaterOut_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxXHeaterOut.SelectedIndexChanged
        If Loaded Then

            Dim text As String = ComboBoxXHeaterOut.Text
            Dim index As Integer = 1

            Dim gobj = SimObject.GraphicObject
            Dim flowsheet = SimObject.FlowSheet

            If text <> "" Then



                If flowsheet.GetFlowsheetSimulationObject(text).GraphicObject.InputConnectors(0).IsAttached Then
                    MessageBox.Show(flowsheet.GetTranslatedString("Todasasconexespossve"), flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Try
                        If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
                        flowsheet.ConnectObjects(gobj, flowsheet.GetFlowsheetSimulationObject(text).GraphicObject, index, 0)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                UpdateInfo()
            Else
                If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
            End If

        End If
    End Sub

    Private Sub ComboBoxRejet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxRejet.SelectedIndexChanged
        If Loaded Then

            Dim text As String = ComboBoxRejet.Text
            Dim index As Integer = 2

            Dim gobj = SimObject.GraphicObject
            Dim flowsheet = SimObject.FlowSheet

            If text <> "" Then
                If flowsheet.GetFlowsheetSimulationObject(text).GraphicObject.InputConnectors(0).IsAttached Then
                    MessageBox.Show(flowsheet.GetTranslatedString("Todasasconexespossve"), flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Try
                        If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
                        flowsheet.ConnectObjects(gobj, flowsheet.GetFlowsheetSimulationObject(text).GraphicObject, index, 0)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                UpdateInfo()
            Else
                If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
            End If

        End If
    End Sub

    Private Sub ComboBoxRecirculation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxRecirculation.SelectedIndexChanged
        If Loaded Then

            Dim text As String = ComboBoxRecirculation.Text
            Dim index As Integer = 3
            Dim gobj = SimObject.GraphicObject
            Dim flowsheet = SimObject.FlowSheet

            If text <> "" Then
                If flowsheet.GetFlowsheetSimulationObject(text).GraphicObject.InputConnectors(0).IsAttached Then
                    MessageBox.Show(flowsheet.GetTranslatedString("Todasasconexespossve"), flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Try
                        If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
                        flowsheet.ConnectObjects(gobj, flowsheet.GetFlowsheetSimulationObject(text).GraphicObject, index, 0)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                UpdateInfo()
            Else
                If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
            End If

        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBoxDistillatOut.SelectedIndexChanged
        If Loaded Then

            Dim text As String = ComboBoxDistillatOut.Text
            Dim index As Integer = 4
            Dim gobj = SimObject.GraphicObject
            Dim flowsheet = SimObject.FlowSheet

            If text <> "" Then
                If flowsheet.GetFlowsheetSimulationObject(text).GraphicObject.InputConnectors(0).IsAttached Then
                    MessageBox.Show(flowsheet.GetTranslatedString("Todasasconexespossve"), flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Try
                        If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
                        flowsheet.ConnectObjects(gobj, flowsheet.GetFlowsheetSimulationObject(text).GraphicObject, index, 0)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                UpdateInfo()
            Else
                If gobj.OutputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(index).AttachedConnector.AttachedTo)
            End If

        End If
    End Sub
End Class