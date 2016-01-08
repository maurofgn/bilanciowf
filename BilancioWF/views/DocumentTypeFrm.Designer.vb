<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DocumentTypeFrm
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla nell'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.ButtonNew = New System.Windows.Forms.Button()
        Me.CheckBoxActive = New System.Windows.Forms.CheckBox()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.TextBoxCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonDel
        '
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(258, 97)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(62, 24)
        Me.ButtonDel.TabIndex = 19
        Me.ButtonDel.Text = "Elimina"
        Me.ButtonDel.UseVisualStyleBackColor = True
        '
        'ButtonSave
        '
        Me.ButtonSave.Location = New System.Drawing.Point(190, 97)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(62, 24)
        Me.ButtonSave.TabIndex = 18
        Me.ButtonSave.Text = "Salva"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'ButtonNew
        '
        Me.ButtonNew.Location = New System.Drawing.Point(121, 97)
        Me.ButtonNew.Name = "ButtonNew"
        Me.ButtonNew.Size = New System.Drawing.Size(63, 24)
        Me.ButtonNew.TabIndex = 17
        Me.ButtonNew.Text = "Nuovo"
        Me.ButtonNew.UseVisualStyleBackColor = True
        '
        'CheckBoxActive
        '
        Me.CheckBoxActive.AutoSize = True
        Me.CheckBoxActive.Checked = True
        Me.CheckBoxActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxActive.Location = New System.Drawing.Point(267, 9)
        Me.CheckBoxActive.Name = "CheckBoxActive"
        Me.CheckBoxActive.Size = New System.Drawing.Size(53, 17)
        Me.CheckBoxActive.TabIndex = 16
        Me.CheckBoxActive.Text = "Attivo"
        Me.CheckBoxActive.UseVisualStyleBackColor = True
        '
        'TextBoxName
        '
        Me.TextBoxName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxName.Location = New System.Drawing.Point(68, 44)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxName.TabIndex = 14
        '
        'TextBoxCode
        '
        Me.TextBoxCode.Location = New System.Drawing.Point(68, 9)
        Me.TextBoxCode.Name = "TextBoxCode"
        Me.TextBoxCode.Size = New System.Drawing.Size(134, 20)
        Me.TextBoxCode.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Codice"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Nome"
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Image = Global.BilancioWF.My.Resources.Resources.CercaLente
        Me.ButtonSearch.Location = New System.Drawing.Point(208, 0)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(32, 32)
        Me.ButtonSearch.TabIndex = 20
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'DocumentTypeFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(343, 142)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.ButtonNew)
        Me.Controls.Add(Me.CheckBoxActive)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.TextBoxCode)
        Me.Controls.Add(Me.Label1)
        Me.Name = "DocumentTypeFrm"
        Me.Text = "Tipo Documento"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonSearch As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonSave As System.Windows.Forms.Button
    Friend WithEvents ButtonNew As System.Windows.Forms.Button
    Friend WithEvents CheckBoxActive As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxName As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
