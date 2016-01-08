<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DocumentFrm
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBoxAmount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNote = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBoxDocType = New System.Windows.Forms.ComboBox()
        Me.TextBoxNrDoc = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePickerDoc = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimePickerDre = New System.Windows.Forms.DateTimePicker()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ButtonRowDel = New System.Windows.Forms.Button()
        Me.ButtonRowSave = New System.Windows.Forms.Button()
        Me.ButtonRowNew = New System.Windows.Forms.Button()
        Me.LabelDebit = New System.Windows.Forms.Label()
        Me.TextBoxRowNote = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxRowAmount = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ComboBoxAccountChart = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxRowNr = New System.Windows.Forms.TextBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.LabelStatoDoc = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonDel
        '
        Me.ButtonDel.Location = New System.Drawing.Point(443, 117)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(80, 24)
        Me.ButtonDel.TabIndex = 28
        Me.ButtonDel.Text = "Elimina doc"
        Me.ButtonDel.UseVisualStyleBackColor = True
        '
        'ButtonSave
        '
        Me.ButtonSave.Location = New System.Drawing.Point(357, 117)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(80, 24)
        Me.ButtonSave.TabIndex = 27
        Me.ButtonSave.Text = "Salva doc"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'ButtonNew
        '
        Me.ButtonNew.Location = New System.Drawing.Point(271, 117)
        Me.ButtonNew.Name = "ButtonNew"
        Me.ButtonNew.Size = New System.Drawing.Size(80, 24)
        Me.ButtonNew.TabIndex = 26
        Me.ButtonNew.Text = "Nuovo doc"
        Me.ButtonNew.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LabelStatoDoc)
        Me.GroupBox1.Controls.Add(Me.TextBoxAmount)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ButtonDel)
        Me.GroupBox1.Controls.Add(Me.TextBoxNote)
        Me.GroupBox1.Controls.Add(Me.ButtonSave)
        Me.GroupBox1.Controls.Add(Me.ButtonNew)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.ComboBoxDocType)
        Me.GroupBox1.Controls.Add(Me.TextBoxNrDoc)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.DateTimePickerDoc)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.DateTimePickerDre)
        Me.GroupBox1.Controls.Add(Me.ButtonSearch)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(542, 147)
        Me.GroupBox1.TabIndex = 67
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Testata Documento"
        '
        'TextBoxAmount
        '
        Me.TextBoxAmount.Location = New System.Drawing.Point(441, 24)
        Me.TextBoxAmount.Name = "TextBoxAmount"
        Me.TextBoxAmount.Size = New System.Drawing.Size(82, 20)
        Me.TextBoxAmount.TabIndex = 43
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(388, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Importo"
        '
        'TextBoxNote
        '
        Me.TextBoxNote.Location = New System.Drawing.Point(55, 85)
        Me.TextBoxNote.Name = "TextBoxNote"
        Me.TextBoxNote.Size = New System.Drawing.Size(401, 20)
        Me.TextBoxNote.TabIndex = 47
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Nota"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(2, 31)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "Tipo Doc."
        '
        'ComboBoxDocType
        '
        Me.ComboBoxDocType.FormattingEnabled = True
        Me.ComboBoxDocType.Location = New System.Drawing.Point(55, 28)
        Me.ComboBoxDocType.Name = "ComboBoxDocType"
        Me.ComboBoxDocType.Size = New System.Drawing.Size(161, 21)
        Me.ComboBoxDocType.TabIndex = 42
        '
        'TextBoxNrDoc
        '
        Me.TextBoxNrDoc.Location = New System.Drawing.Point(441, 55)
        Me.TextBoxNrDoc.Name = "TextBoxNrDoc"
        Me.TextBoxNrDoc.Size = New System.Drawing.Size(82, 20)
        Me.TextBoxNrDoc.TabIndex = 46
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(388, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "Nr. Doc."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(169, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "Data Doc."
        '
        'DateTimePickerDoc
        '
        Me.DateTimePickerDoc.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDoc.Location = New System.Drawing.Point(228, 56)
        Me.DateTimePickerDoc.Name = "DateTimePickerDoc"
        Me.DateTimePickerDoc.Size = New System.Drawing.Size(98, 20)
        Me.DateTimePickerDoc.TabIndex = 45
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(2, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Data Reg."
        '
        'DateTimePickerDre
        '
        Me.DateTimePickerDre.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDre.Location = New System.Drawing.Point(55, 55)
        Me.DateTimePickerDre.Name = "DateTimePickerDre"
        Me.DateTimePickerDre.Size = New System.Drawing.Size(98, 20)
        Me.DateTimePickerDre.TabIndex = 44
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Image = Global.BilancioWF.My.Resources.Resources.CercaLente
        Me.ButtonSearch.Location = New System.Drawing.Point(232, 17)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(32, 32)
        Me.ButtonSearch.TabIndex = 48
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.ButtonRowDel)
        Me.GroupBox2.Controls.Add(Me.ButtonRowSave)
        Me.GroupBox2.Controls.Add(Me.ButtonRowNew)
        Me.GroupBox2.Controls.Add(Me.LabelDebit)
        Me.GroupBox2.Controls.Add(Me.TextBoxRowNote)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.TextBoxRowAmount)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.ComboBoxAccountChart)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.TextBoxRowNr)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 165)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(542, 308)
        Me.GroupBox2.TabIndex = 68
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Righe documento"
        '
        'ButtonRowDel
        '
        Me.ButtonRowDel.Location = New System.Drawing.Point(443, 269)
        Me.ButtonRowDel.Name = "ButtonRowDel"
        Me.ButtonRowDel.Size = New System.Drawing.Size(80, 24)
        Me.ButtonRowDel.TabIndex = 68
        Me.ButtonRowDel.Text = "Elimina riga"
        Me.ButtonRowDel.UseVisualStyleBackColor = True
        '
        'ButtonRowSave
        '
        Me.ButtonRowSave.Location = New System.Drawing.Point(357, 269)
        Me.ButtonRowSave.Name = "ButtonRowSave"
        Me.ButtonRowSave.Size = New System.Drawing.Size(80, 24)
        Me.ButtonRowSave.TabIndex = 67
        Me.ButtonRowSave.Text = "Salva riga"
        Me.ButtonRowSave.UseVisualStyleBackColor = True
        '
        'ButtonRowNew
        '
        Me.ButtonRowNew.Location = New System.Drawing.Point(271, 269)
        Me.ButtonRowNew.Name = "ButtonRowNew"
        Me.ButtonRowNew.Size = New System.Drawing.Size(80, 24)
        Me.ButtonRowNew.TabIndex = 66
        Me.ButtonRowNew.Text = "Nuova riga"
        Me.ButtonRowNew.UseVisualStyleBackColor = True
        '
        'LabelDebit
        '
        Me.LabelDebit.AutoSize = True
        Me.LabelDebit.Location = New System.Drawing.Point(231, 16)
        Me.LabelDebit.Name = "LabelDebit"
        Me.LabelDebit.Size = New System.Drawing.Size(30, 13)
        Me.LabelDebit.TabIndex = 61
        Me.LabelDebit.Text = "Dare"
        '
        'TextBoxRowNote
        '
        Me.TextBoxRowNote.Location = New System.Drawing.Point(65, 40)
        Me.TextBoxRowNote.Name = "TextBoxRowNote"
        Me.TextBoxRowNote.Size = New System.Drawing.Size(401, 20)
        Me.TextBoxRowNote.TabIndex = 59
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 43)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(30, 13)
        Me.Label10.TabIndex = 60
        Me.Label10.Text = "Nota"
        '
        'TextBoxRowAmount
        '
        Me.TextBoxRowAmount.Location = New System.Drawing.Point(441, 14)
        Me.TextBoxRowAmount.Name = "TextBoxRowAmount"
        Me.TextBoxRowAmount.Size = New System.Drawing.Size(82, 20)
        Me.TextBoxRowAmount.TabIndex = 57
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(388, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 13)
        Me.Label9.TabIndex = 58
        Me.Label9.Text = "Importo"
        '
        'ComboBoxAccountChart
        '
        Me.ComboBoxAccountChart.FormattingEnabled = True
        Me.ComboBoxAccountChart.Location = New System.Drawing.Point(65, 13)
        Me.ComboBoxAccountChart.Name = "ComboBoxAccountChart"
        Me.ComboBoxAccountChart.Size = New System.Drawing.Size(160, 21)
        Me.ComboBoxAccountChart.TabIndex = 56
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 13)
        Me.Label8.TabIndex = 55
        Me.Label8.Text = "Conto"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(277, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 53
        Me.Label7.Text = "Riga"
        '
        'TextBoxRowNr
        '
        Me.TextBoxRowNr.Location = New System.Drawing.Point(312, 13)
        Me.TextBoxRowNr.Name = "TextBoxRowNr"
        Me.TextBoxRowNr.Size = New System.Drawing.Size(44, 20)
        Me.TextBoxRowNr.TabIndex = 58
        '
        'ListView1
        '
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(9, 66)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(519, 182)
        Me.ListView1.TabIndex = 69
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'LabelStatoDoc
        '
        Me.LabelStatoDoc.AutoSize = True
        Me.LabelStatoDoc.ForeColor = System.Drawing.Color.Red
        Me.LabelStatoDoc.Location = New System.Drawing.Point(2, 123)
        Me.LabelStatoDoc.Name = "LabelStatoDoc"
        Me.LabelStatoDoc.Size = New System.Drawing.Size(51, 13)
        Me.LabelStatoDoc.TabIndex = 55
        Me.LabelStatoDoc.Text = "stato doc"
        '
        'DocumentFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 481)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "DocumentFrm"
        Me.Text = "Documenti"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonSave As System.Windows.Forms.Button
    Friend WithEvents ButtonNew As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBoxAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNote As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxDocType As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxNrDoc As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerDoc As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerDre As System.Windows.Forms.DateTimePicker
    Friend WithEvents ButtonSearch As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRowNr As System.Windows.Forms.TextBox
    Friend WithEvents LabelDebit As System.Windows.Forms.Label
    Friend WithEvents TextBoxRowNote As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRowAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxAccountChart As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ButtonRowDel As System.Windows.Forms.Button
    Friend WithEvents ButtonRowSave As System.Windows.Forms.Button
    Friend WithEvents ButtonRowNew As System.Windows.Forms.Button
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents LabelStatoDoc As System.Windows.Forms.Label
End Class
