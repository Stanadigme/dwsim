<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EditingForm_Recompletor
    Inherits SharedClasses.ObjectEditorForm

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTipChangeTag = New System.Windows.Forms.ToolTip(Me.components)
        Me.LabelObjet = New System.Windows.Forms.Label()
        Me.LabelStatut = New System.Windows.Forms.Label()
        Me.LabelLinkedTo = New System.Windows.Forms.Label()
        Me.LabelStatutObjet = New System.Windows.Forms.Label()
        Me.lblTag = New System.Windows.Forms.TextBox()
        Me.LabelEdMIn = New System.Windows.Forms.Label()
        Me.LabelEdMFlashLiq = New System.Windows.Forms.Label()
        Me.LabelXFlashVapor = New System.Windows.Forms.Label()
        Me.LabelXCoolerOut = New System.Windows.Forms.Label()
        Me.ComboBoxEdMIn = New System.Windows.Forms.ComboBox()
        Me.ComboBoxXEdMFlashLiq = New System.Windows.Forms.ComboBox()
        Me.ComboBoxXFlashVapor = New System.Windows.Forms.ComboBox()
        Me.ComboBoxXCoolerOut = New System.Windows.Forms.ComboBox()
        Me.ComboBoxXHeaterOut = New System.Windows.Forms.ComboBox()
        Me.LabelXHeaterOut = New System.Windows.Forms.Label()
        Me.TextBoxTSec = New System.Windows.Forms.TextBox()
        Me.LabelTSec = New System.Windows.Forms.Label()
        Me.ComboBoxRejet = New System.Windows.Forms.ComboBox()
        Me.LabelRejet = New System.Windows.Forms.Label()
        Me.ComboBoxRecirculation = New System.Windows.Forms.ComboBox()
        Me.LabelRecirculation = New System.Windows.Forms.Label()
        Me.ComboBoxXFlashLiq = New System.Windows.Forms.ComboBox()
        Me.LabelXFlashLiq = New System.Windows.Forms.Label()
        Me.ComboBoxDistillatOut = New System.Windows.Forms.ComboBox()
        Me.LabelDistillatOut = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ToolTipChangeTag
        '
        Me.ToolTipChangeTag.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTipChangeTag.ToolTipTitle = "Info"
        '
        'LabelObjet
        '
        Me.LabelObjet.AutoSize = True
        Me.LabelObjet.Location = New System.Drawing.Point(13, 13)
        Me.LabelObjet.Name = "LabelObjet"
        Me.LabelObjet.Size = New System.Drawing.Size(32, 13)
        Me.LabelObjet.TabIndex = 0
        Me.LabelObjet.Text = "Objet"
        '
        'LabelStatut
        '
        Me.LabelStatut.AutoSize = True
        Me.LabelStatut.Location = New System.Drawing.Point(13, 35)
        Me.LabelStatut.Name = "LabelStatut"
        Me.LabelStatut.Size = New System.Drawing.Size(35, 13)
        Me.LabelStatut.TabIndex = 1
        Me.LabelStatut.Text = "Statut"
        '
        'LabelLinkedTo
        '
        Me.LabelLinkedTo.AutoSize = True
        Me.LabelLinkedTo.Location = New System.Drawing.Point(13, 62)
        Me.LabelLinkedTo.Name = "LabelLinkedTo"
        Me.LabelLinkedTo.Size = New System.Drawing.Size(51, 13)
        Me.LabelLinkedTo.TabIndex = 2
        Me.LabelLinkedTo.Text = "Linked to"
        '
        'LabelStatutObjet
        '
        Me.LabelStatutObjet.AutoSize = True
        Me.LabelStatutObjet.Location = New System.Drawing.Point(69, 35)
        Me.LabelStatutObjet.Name = "LabelStatutObjet"
        Me.LabelStatutObjet.Size = New System.Drawing.Size(51, 13)
        Me.LabelStatutObjet.TabIndex = 3
        Me.LabelStatutObjet.Text = "Linked to"
        '
        'lblTag
        '
        Me.lblTag.Location = New System.Drawing.Point(72, 13)
        Me.lblTag.Name = "lblTag"
        Me.lblTag.Size = New System.Drawing.Size(100, 20)
        Me.lblTag.TabIndex = 4
        '
        'LabelEdMIn
        '
        Me.LabelEdMIn.AutoSize = True
        Me.LabelEdMIn.Location = New System.Drawing.Point(16, 86)
        Me.LabelEdMIn.Name = "LabelEdMIn"
        Me.LabelEdMIn.Size = New System.Drawing.Size(41, 13)
        Me.LabelEdMIn.TabIndex = 5
        Me.LabelEdMIn.Text = "EdM In"
        '
        'LabelEdMFlashLiq
        '
        Me.LabelEdMFlashLiq.AutoSize = True
        Me.LabelEdMFlashLiq.Location = New System.Drawing.Point(262, 86)
        Me.LabelEdMFlashLiq.Name = "LabelEdMFlashLiq"
        Me.LabelEdMFlashLiq.Size = New System.Drawing.Size(75, 13)
        Me.LabelEdMFlashLiq.TabIndex = 6
        Me.LabelEdMFlashLiq.Text = "XEdMFlashLiq"
        '
        'LabelXFlashVapor
        '
        Me.LabelXFlashVapor.AutoSize = True
        Me.LabelXFlashVapor.Location = New System.Drawing.Point(16, 117)
        Me.LabelXFlashVapor.Name = "LabelXFlashVapor"
        Me.LabelXFlashVapor.Size = New System.Drawing.Size(67, 13)
        Me.LabelXFlashVapor.TabIndex = 7
        Me.LabelXFlashVapor.Text = "XFlashVapor"
        '
        'LabelXCoolerOut
        '
        Me.LabelXCoolerOut.AutoSize = True
        Me.LabelXCoolerOut.Location = New System.Drawing.Point(16, 169)
        Me.LabelXCoolerOut.Name = "LabelXCoolerOut"
        Me.LabelXCoolerOut.Size = New System.Drawing.Size(61, 13)
        Me.LabelXCoolerOut.TabIndex = 8
        Me.LabelXCoolerOut.Text = "XCoolerOut"
        '
        'ComboBoxEdMIn
        '
        Me.ComboBoxEdMIn.FormattingEnabled = True
        Me.ComboBoxEdMIn.Location = New System.Drawing.Point(106, 83)
        Me.ComboBoxEdMIn.Name = "ComboBoxEdMIn"
        Me.ComboBoxEdMIn.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxEdMIn.TabIndex = 9
        '
        'ComboBoxXEdMFlashLiq
        '
        Me.ComboBoxXEdMFlashLiq.FormattingEnabled = True
        Me.ComboBoxXEdMFlashLiq.Location = New System.Drawing.Point(352, 83)
        Me.ComboBoxXEdMFlashLiq.Name = "ComboBoxXEdMFlashLiq"
        Me.ComboBoxXEdMFlashLiq.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxXEdMFlashLiq.TabIndex = 10
        '
        'ComboBoxXFlashVapor
        '
        Me.ComboBoxXFlashVapor.FormattingEnabled = True
        Me.ComboBoxXFlashVapor.Location = New System.Drawing.Point(106, 114)
        Me.ComboBoxXFlashVapor.Name = "ComboBoxXFlashVapor"
        Me.ComboBoxXFlashVapor.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxXFlashVapor.TabIndex = 11
        '
        'ComboBoxXCoolerOut
        '
        Me.ComboBoxXCoolerOut.FormattingEnabled = True
        Me.ComboBoxXCoolerOut.Location = New System.Drawing.Point(106, 166)
        Me.ComboBoxXCoolerOut.Name = "ComboBoxXCoolerOut"
        Me.ComboBoxXCoolerOut.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxXCoolerOut.TabIndex = 12
        '
        'ComboBoxXHeaterOut
        '
        Me.ComboBoxXHeaterOut.FormattingEnabled = True
        Me.ComboBoxXHeaterOut.Location = New System.Drawing.Point(106, 229)
        Me.ComboBoxXHeaterOut.Name = "ComboBoxXHeaterOut"
        Me.ComboBoxXHeaterOut.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxXHeaterOut.TabIndex = 14
        '
        'LabelXHeaterOut
        '
        Me.LabelXHeaterOut.AutoSize = True
        Me.LabelXHeaterOut.Location = New System.Drawing.Point(16, 232)
        Me.LabelXHeaterOut.Name = "LabelXHeaterOut"
        Me.LabelXHeaterOut.Size = New System.Drawing.Size(63, 13)
        Me.LabelXHeaterOut.TabIndex = 13
        Me.LabelXHeaterOut.Text = "XHeaterOut"
        '
        'TextBoxTSec
        '
        Me.TextBoxTSec.Location = New System.Drawing.Point(106, 277)
        Me.TextBoxTSec.Name = "TextBoxTSec"
        Me.TextBoxTSec.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxTSec.TabIndex = 15
        '
        'LabelTSec
        '
        Me.LabelTSec.AutoSize = True
        Me.LabelTSec.Location = New System.Drawing.Point(16, 280)
        Me.LabelTSec.Name = "LabelTSec"
        Me.LabelTSec.Size = New System.Drawing.Size(33, 13)
        Me.LabelTSec.TabIndex = 16
        Me.LabelTSec.Text = "TSec"
        '
        'ComboBoxRejet
        '
        Me.ComboBoxRejet.FormattingEnabled = True
        Me.ComboBoxRejet.Location = New System.Drawing.Point(352, 198)
        Me.ComboBoxRejet.Name = "ComboBoxRejet"
        Me.ComboBoxRejet.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxRejet.TabIndex = 18
        '
        'LabelRejet
        '
        Me.LabelRejet.AutoSize = True
        Me.LabelRejet.Location = New System.Drawing.Point(262, 201)
        Me.LabelRejet.Name = "LabelRejet"
        Me.LabelRejet.Size = New System.Drawing.Size(32, 13)
        Me.LabelRejet.TabIndex = 17
        Me.LabelRejet.Text = "Rejet"
        '
        'ComboBoxRecirculation
        '
        Me.ComboBoxRecirculation.FormattingEnabled = True
        Me.ComboBoxRecirculation.Location = New System.Drawing.Point(352, 226)
        Me.ComboBoxRecirculation.Name = "ComboBoxRecirculation"
        Me.ComboBoxRecirculation.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxRecirculation.TabIndex = 20
        '
        'LabelRecirculation
        '
        Me.LabelRecirculation.AutoSize = True
        Me.LabelRecirculation.Location = New System.Drawing.Point(262, 229)
        Me.LabelRecirculation.Name = "LabelRecirculation"
        Me.LabelRecirculation.Size = New System.Drawing.Size(69, 13)
        Me.LabelRecirculation.TabIndex = 19
        Me.LabelRecirculation.Text = "Recirculation"
        '
        'ComboBoxXFlashLiq
        '
        Me.ComboBoxXFlashLiq.FormattingEnabled = True
        Me.ComboBoxXFlashLiq.Location = New System.Drawing.Point(352, 114)
        Me.ComboBoxXFlashLiq.Name = "ComboBoxXFlashLiq"
        Me.ComboBoxXFlashLiq.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxXFlashLiq.TabIndex = 22
        '
        'LabelXFlashLiq
        '
        Me.LabelXFlashLiq.AutoSize = True
        Me.LabelXFlashLiq.Location = New System.Drawing.Point(262, 117)
        Me.LabelXFlashLiq.Name = "LabelXFlashLiq"
        Me.LabelXFlashLiq.Size = New System.Drawing.Size(53, 13)
        Me.LabelXFlashLiq.TabIndex = 21
        Me.LabelXFlashLiq.Text = "XFlashLiq"
        '
        'ComboBoxDistillatOut
        '
        Me.ComboBoxDistillatOut.FormattingEnabled = True
        Me.ComboBoxDistillatOut.Location = New System.Drawing.Point(352, 171)
        Me.ComboBoxDistillatOut.Name = "ComboBoxDistillatOut"
        Me.ComboBoxDistillatOut.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxDistillatOut.TabIndex = 24
        '
        'LabelDistillatOut
        '
        Me.LabelDistillatOut.AutoSize = True
        Me.LabelDistillatOut.Location = New System.Drawing.Point(262, 174)
        Me.LabelDistillatOut.Name = "LabelDistillatOut"
        Me.LabelDistillatOut.Size = New System.Drawing.Size(57, 13)
        Me.LabelDistillatOut.TabIndex = 23
        Me.LabelDistillatOut.Text = "DistillatOut"
        '
        'EditingForm_Recompletor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(520, 336)
        Me.Controls.Add(Me.ComboBoxDistillatOut)
        Me.Controls.Add(Me.LabelDistillatOut)
        Me.Controls.Add(Me.ComboBoxXFlashLiq)
        Me.Controls.Add(Me.LabelXFlashLiq)
        Me.Controls.Add(Me.ComboBoxRecirculation)
        Me.Controls.Add(Me.LabelRecirculation)
        Me.Controls.Add(Me.ComboBoxRejet)
        Me.Controls.Add(Me.LabelRejet)
        Me.Controls.Add(Me.LabelTSec)
        Me.Controls.Add(Me.TextBoxTSec)
        Me.Controls.Add(Me.ComboBoxXHeaterOut)
        Me.Controls.Add(Me.LabelXHeaterOut)
        Me.Controls.Add(Me.ComboBoxXCoolerOut)
        Me.Controls.Add(Me.ComboBoxXFlashVapor)
        Me.Controls.Add(Me.ComboBoxXEdMFlashLiq)
        Me.Controls.Add(Me.ComboBoxEdMIn)
        Me.Controls.Add(Me.LabelXCoolerOut)
        Me.Controls.Add(Me.LabelXFlashVapor)
        Me.Controls.Add(Me.LabelEdMFlashLiq)
        Me.Controls.Add(Me.LabelEdMIn)
        Me.Controls.Add(Me.lblTag)
        Me.Controls.Add(Me.LabelStatutObjet)
        Me.Controls.Add(Me.LabelLinkedTo)
        Me.Controls.Add(Me.LabelStatut)
        Me.Controls.Add(Me.LabelObjet)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "EditingForm_Recompletor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTipChangeTag As ToolTip
    Friend WithEvents LabelObjet As Label
    Friend WithEvents LabelStatut As Label
    Friend WithEvents LabelLinkedTo As Label
    Friend WithEvents LabelStatutObjet As Label
    Friend WithEvents lblTag As TextBox
    Friend WithEvents LabelEdMIn As Label
    Friend WithEvents LabelEdMFlashLiq As Label
    Friend WithEvents LabelXFlashVapor As Label
    Friend WithEvents LabelXCoolerOut As Label
    Friend WithEvents ComboBoxEdMIn As ComboBox
    Friend WithEvents ComboBoxXEdMFlashLiq As ComboBox
    Friend WithEvents ComboBoxXFlashVapor As ComboBox
    Friend WithEvents ComboBoxXCoolerOut As ComboBox
    Friend WithEvents ComboBoxXHeaterOut As ComboBox
    Friend WithEvents LabelXHeaterOut As Label
    Friend WithEvents TextBoxTSec As TextBox
    Friend WithEvents LabelTSec As Label
    Friend WithEvents ComboBoxRejet As ComboBox
    Friend WithEvents LabelRejet As Label
    Friend WithEvents ComboBoxRecirculation As ComboBox
    Friend WithEvents LabelRecirculation As Label
    Friend WithEvents ComboBoxXFlashLiq As ComboBox
    Friend WithEvents LabelXFlashLiq As Label
    Friend WithEvents ComboBoxDistillatOut As ComboBox
    Friend WithEvents LabelDistillatOut As Label
End Class
