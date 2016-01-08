<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountChartFrm
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
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.ButtonNew = New System.Windows.Forms.Button()
        Me.CheckBoxActive = New System.Windows.Forms.CheckBox()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.TextBoxCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelDebit = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboAccountCee = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Nome"
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Image = Global.BilancioWF.My.Resources.Resources.CercaLente
        Me.ButtonSearch.Location = New System.Drawing.Point(210, 14)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(32, 32)
        Me.ButtonSearch.TabIndex = 29
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'ButtonDel
        '
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(260, 183)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(62, 24)
        Me.ButtonDel.TabIndex = 28
        Me.ButtonDel.Text = "Elimina"
        Me.ButtonDel.UseVisualStyleBackColor = True
        '
        'ButtonSave
        '
        Me.ButtonSave.Location = New System.Drawing.Point(192, 183)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(62, 24)
        Me.ButtonSave.TabIndex = 27
        Me.ButtonSave.Text = "Salva"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'ButtonNew
        '
        Me.ButtonNew.Location = New System.Drawing.Point(123, 183)
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
        Me.CheckBoxActive.Location = New System.Drawing.Point(269, 23)
        Me.CheckBoxActive.Name = "CheckBoxActive"
        Me.CheckBoxActive.Size = New System.Drawing.Size(53, 17)
        Me.CheckBoxActive.TabIndex = 25
        Me.CheckBoxActive.Text = "Attivo"
        Me.CheckBoxActive.UseVisualStyleBackColor = True
        '
        'TextBoxName
        '
        Me.TextBoxName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBoxName.Location = New System.Drawing.Point(70, 58)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxName.TabIndex = 23
        '
        'TextBoxCode
        '
        Me.TextBoxCode.Location = New System.Drawing.Point(70, 23)
        Me.TextBoxCode.Name = "TextBoxCode"
        Me.TextBoxCode.Size = New System.Drawing.Size(134, 20)
        Me.TextBoxCode.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Codice"
        '
        'LabelDebit
        '
        Me.LabelDebit.AutoSize = True
        Me.LabelDebit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDebit.Location = New System.Drawing.Point(67, 137)
        Me.LabelDebit.Name = "LabelDebit"
        Me.LabelDebit.Size = New System.Drawing.Size(0, 13)
        Me.LabelDebit.TabIndex = 31
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Conto Cee"
        '
        'ComboAccountCee
        '
        Me.ComboAccountCee.FormattingEnabled = True
        Me.ComboAccountCee.Location = New System.Drawing.Point(70, 93)
        Me.ComboAccountCee.Name = "ComboAccountCee"
        Me.ComboAccountCee.Size = New System.Drawing.Size(252, 21)
        Me.ComboAccountCee.TabIndex = 33
        '
        'AccountChartFrm
        '
        Me.AcceptButton = Me.ButtonSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(342, 234)
        Me.Controls.Add(Me.ComboAccountCee)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LabelDebit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.ButtonNew)
        Me.Controls.Add(Me.CheckBoxActive)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.TextBoxCode)
        Me.Controls.Add(Me.Label1)
        Me.Name = "AccountChartFrm"
        Me.Text = "Piano dei conti"
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
    Friend WithEvents LabelDebit As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboAccountCee As System.Windows.Forms.ComboBox
End Class
