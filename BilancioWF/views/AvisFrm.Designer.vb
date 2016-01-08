<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AvisFrm
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
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.CheckBoxActive = New System.Windows.Forms.CheckBox()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxAddress = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxCity = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxPostalCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxRegion = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxEmail = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxPhone = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxContactName = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Nome"
        '
        'ButtonSave
        '
        Me.ButtonSave.Location = New System.Drawing.Point(252, 226)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(62, 24)
        Me.ButtonSave.TabIndex = 27
        Me.ButtonSave.Text = "Salva"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'CheckBoxActive
        '
        Me.CheckBoxActive.AutoSize = True
        Me.CheckBoxActive.Checked = True
        Me.CheckBoxActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxActive.Enabled = False
        Me.CheckBoxActive.Location = New System.Drawing.Point(62, 194)
        Me.CheckBoxActive.Name = "CheckBoxActive"
        Me.CheckBoxActive.Size = New System.Drawing.Size(53, 17)
        Me.CheckBoxActive.TabIndex = 25
        Me.CheckBoxActive.Text = "Attivo"
        Me.CheckBoxActive.UseVisualStyleBackColor = True
        '
        'TextBoxName
        '
        Me.TextBoxName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxName.Location = New System.Drawing.Point(62, 12)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxName.TabIndex = 23
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Indirizzo"
        '
        'TextBoxAddress
        '
        Me.TextBoxAddress.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxAddress.Location = New System.Drawing.Point(62, 38)
        Me.TextBoxAddress.Name = "TextBoxAddress"
        Me.TextBoxAddress.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxAddress.TabIndex = 31
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "Città"
        '
        'TextBoxCity
        '
        Me.TextBoxCity.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxCity.Location = New System.Drawing.Point(62, 64)
        Me.TextBoxCity.Name = "TextBoxCity"
        Me.TextBoxCity.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxCity.TabIndex = 33
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 93)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "C.A.P."
        '
        'TextBoxPostalCode
        '
        Me.TextBoxPostalCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxPostalCode.Location = New System.Drawing.Point(62, 90)
        Me.TextBoxPostalCode.Name = "TextBoxPostalCode"
        Me.TextBoxPostalCode.Size = New System.Drawing.Size(56, 20)
        Me.TextBoxPostalCode.TabIndex = 35
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(145, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 13)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "Provincia"
        '
        'TextBoxRegion
        '
        Me.TextBoxRegion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxRegion.Location = New System.Drawing.Point(202, 90)
        Me.TextBoxRegion.Name = "TextBoxRegion"
        Me.TextBoxRegion.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxRegion.TabIndex = 37
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 119)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(34, 13)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "e-mail"
        '
        'TextBoxEmail
        '
        Me.TextBoxEmail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxEmail.Location = New System.Drawing.Point(62, 116)
        Me.TextBoxEmail.Name = "TextBoxEmail"
        Me.TextBoxEmail.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxEmail.TabIndex = 39
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 145)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "Telefono"
        '
        'TextBoxPhone
        '
        Me.TextBoxPhone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxPhone.Location = New System.Drawing.Point(62, 142)
        Me.TextBoxPhone.Name = "TextBoxPhone"
        Me.TextBoxPhone.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxPhone.TabIndex = 41
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 171)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "Contatto"
        '
        'TextBoxContactName
        '
        Me.TextBoxContactName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxContactName.Location = New System.Drawing.Point(62, 168)
        Me.TextBoxContactName.Name = "TextBoxContactName"
        Me.TextBoxContactName.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxContactName.TabIndex = 43
        '
        'AvisFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(326, 262)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBoxContactName)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxPhone)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxEmail)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxRegion)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxPostalCode)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxCity)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxAddress)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.CheckBoxActive)
        Me.Controls.Add(Me.TextBoxName)
        Me.Name = "AvisFrm"
        Me.Text = "Anagrafica Avis"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonSave As System.Windows.Forms.Button
    Friend WithEvents CheckBoxActive As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCity As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPostalCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRegion As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxContactName As System.Windows.Forms.TextBox
End Class
