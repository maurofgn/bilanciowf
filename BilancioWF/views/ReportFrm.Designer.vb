<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportFrm
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.ButtonNew = New System.Windows.Forms.Button()
        Me.CheckBoxActive = New System.Windows.Forms.CheckBox()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.TextBoxCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxModelName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxOutFileName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxActionName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxControllerName = New System.Windows.Forms.TextBox()
        Me.ComboBoxFormatType = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Nome"
        '
        'ButtonDel
        '
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(287, 224)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(62, 24)
        Me.ButtonDel.TabIndex = 28
        Me.ButtonDel.Text = "Elimina"
        Me.ButtonDel.UseVisualStyleBackColor = True
        '
        'ButtonSave
        '
        Me.ButtonSave.Location = New System.Drawing.Point(219, 224)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(62, 24)
        Me.ButtonSave.TabIndex = 27
        Me.ButtonSave.Text = "Salva"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'ButtonNew
        '
        Me.ButtonNew.Location = New System.Drawing.Point(150, 224)
        Me.ButtonNew.Name = "ButtonNew"
        Me.ButtonNew.Size = New System.Drawing.Size(63, 24)
        Me.ButtonNew.TabIndex = 26
        Me.ButtonNew.Text = "Nuovo"
        Me.ButtonNew.UseVisualStyleBackColor = True
        '
        'CheckBoxActive
        '
        Me.CheckBoxActive.AutoSize = True
        Me.CheckBoxActive.Checked = True
        Me.CheckBoxActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxActive.Location = New System.Drawing.Point(296, 15)
        Me.CheckBoxActive.Name = "CheckBoxActive"
        Me.CheckBoxActive.Size = New System.Drawing.Size(53, 17)
        Me.CheckBoxActive.TabIndex = 25
        Me.CheckBoxActive.Text = "Attivo"
        Me.CheckBoxActive.UseVisualStyleBackColor = True
        '
        'TextBoxName
        '
        Me.TextBoxName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxName.Location = New System.Drawing.Point(98, 47)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxName.TabIndex = 23
        '
        'TextBoxCode
        '
        Me.TextBoxCode.Location = New System.Drawing.Point(97, 15)
        Me.TextBoxCode.Name = "TextBoxCode"
        Me.TextBoxCode.Size = New System.Drawing.Size(134, 20)
        Me.TextBoxCode.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Codice"
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Image = Global.BilancioWF.My.Resources.Resources.CercaLente
        Me.ButtonSearch.Location = New System.Drawing.Point(237, 5)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(32, 32)
        Me.ButtonSearch.TabIndex = 29
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Modello"
        '
        'TextBoxModelName
        '
        Me.TextBoxModelName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxModelName.Location = New System.Drawing.Point(98, 73)
        Me.TextBoxModelName.Name = "TextBoxModelName"
        Me.TextBoxModelName.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxModelName.TabIndex = 31
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "Tipo formato"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 128)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 13)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Nome file Output"
        '
        'TextBoxOutFileName
        '
        Me.TextBoxOutFileName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxOutFileName.Location = New System.Drawing.Point(97, 125)
        Me.TextBoxOutFileName.Name = "TextBoxOutFileName"
        Me.TextBoxOutFileName.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxOutFileName.TabIndex = 35
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 154)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "Azione"
        '
        'TextBoxActionName
        '
        Me.TextBoxActionName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxActionName.Location = New System.Drawing.Point(98, 151)
        Me.TextBoxActionName.Name = "TextBoxActionName"
        Me.TextBoxActionName.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxActionName.TabIndex = 37
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 180)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Controllo"
        '
        'TextBoxControllerName
        '
        Me.TextBoxControllerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxControllerName.Location = New System.Drawing.Point(98, 177)
        Me.TextBoxControllerName.Name = "TextBoxControllerName"
        Me.TextBoxControllerName.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxControllerName.TabIndex = 39
        '
        'ComboBoxFormatType
        '
        Me.ComboBoxFormatType.FormattingEnabled = True
        Me.ComboBoxFormatType.Location = New System.Drawing.Point(98, 99)
        Me.ComboBoxFormatType.Name = "ComboBoxFormatType"
        Me.ComboBoxFormatType.Size = New System.Drawing.Size(251, 21)
        Me.ComboBoxFormatType.TabIndex = 33
        '
        'ReportFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(361, 254)
        Me.Controls.Add(Me.ComboBoxFormatType)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxControllerName)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxActionName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxOutFileName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxModelName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.ButtonNew)
        Me.Controls.Add(Me.CheckBoxActive)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.TextBoxCode)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ReportFrm"
        Me.Text = "Reports"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonSearch As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonSave As System.Windows.Forms.Button
    Friend WithEvents ButtonNew As System.Windows.Forms.Button
    Friend WithEvents CheckBoxActive As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxName As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxCode As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxModelName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxOutFileName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxActionName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxControllerName As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxFormatType As System.Windows.Forms.ComboBox
End Class
