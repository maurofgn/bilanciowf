<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DocumentSearchFrm
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
        Me.DateTimePickerDre2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerDoc2 = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxNrDoc2 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TextBoxAmount2 = New System.Windows.Forms.TextBox()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxAmount
        '
        Me.TextBoxAmount.Location = New System.Drawing.Point(339, 80)
        Me.TextBoxAmount.Name = "TextBoxAmount"
        Me.TextBoxAmount.Size = New System.Drawing.Size(98, 20)
        Me.TextBoxAmount.TabIndex = 43
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(277, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Importo"
        '
        'TextBoxNote
        '
        Me.TextBoxNote.Location = New System.Drawing.Point(69, 106)
        Me.TextBoxNote.Name = "TextBoxNote"
        Me.TextBoxNote.Size = New System.Drawing.Size(472, 20)
        Me.TextBoxNote.TabIndex = 47
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Nota"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "Tipo Doc."
        '
        'ComboBoxDocType
        '
        Me.ComboBoxDocType.FormattingEnabled = True
        Me.ComboBoxDocType.Location = New System.Drawing.Point(69, 12)
        Me.ComboBoxDocType.Name = "ComboBoxDocType"
        Me.ComboBoxDocType.Size = New System.Drawing.Size(161, 21)
        Me.ComboBoxDocType.TabIndex = 42
        '
        'TextBoxNrDoc
        '
        Me.TextBoxNrDoc.Location = New System.Drawing.Point(69, 80)
        Me.TextBoxNrDoc.Name = "TextBoxNrDoc"
        Me.TextBoxNrDoc.Size = New System.Drawing.Size(98, 20)
        Me.TextBoxNrDoc.TabIndex = 46
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 87)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "Nr. Doc."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(277, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "Data Doc."
        '
        'DateTimePickerDoc
        '
        Me.DateTimePickerDoc.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDoc.Location = New System.Drawing.Point(339, 54)
        Me.DateTimePickerDoc.Name = "DateTimePickerDoc"
        Me.DateTimePickerDoc.Size = New System.Drawing.Size(98, 20)
        Me.DateTimePickerDoc.TabIndex = 45
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Data Reg."
        '
        'DateTimePickerDre
        '
        Me.DateTimePickerDre.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDre.Location = New System.Drawing.Point(69, 54)
        Me.DateTimePickerDre.MaxDate = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.DateTimePickerDre.Name = "DateTimePickerDre"
        Me.DateTimePickerDre.Size = New System.Drawing.Size(98, 20)
        Me.DateTimePickerDre.TabIndex = 44
        '
        'DateTimePickerDre2
        '
        Me.DateTimePickerDre2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDre2.Location = New System.Drawing.Point(173, 54)
        Me.DateTimePickerDre2.MaxDate = New Date(2050, 12, 31, 0, 0, 0, 0)
        Me.DateTimePickerDre2.MinDate = New Date(2015, 1, 1, 0, 0, 0, 0)
        Me.DateTimePickerDre2.Name = "DateTimePickerDre2"
        Me.DateTimePickerDre2.Size = New System.Drawing.Size(98, 20)
        Me.DateTimePickerDre2.TabIndex = 55
        '
        'DateTimePickerDoc2
        '
        Me.DateTimePickerDoc2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDoc2.Location = New System.Drawing.Point(443, 54)
        Me.DateTimePickerDoc2.Name = "DateTimePickerDoc2"
        Me.DateTimePickerDoc2.Size = New System.Drawing.Size(98, 20)
        Me.DateTimePickerDoc2.TabIndex = 57
        '
        'TextBoxNrDoc2
        '
        Me.TextBoxNrDoc2.Location = New System.Drawing.Point(173, 80)
        Me.TextBoxNrDoc2.Name = "TextBoxNrDoc2"
        Me.TextBoxNrDoc2.Size = New System.Drawing.Size(98, 20)
        Me.TextBoxNrDoc2.TabIndex = 59
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(85, 38)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 13)
        Me.Label10.TabIndex = 61
        Me.Label10.Text = "Inizio"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(194, 38)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(27, 13)
        Me.Label11.TabIndex = 62
        Me.Label11.Text = "Fine"
        '
        'TextBoxAmount2
        '
        Me.TextBoxAmount2.Location = New System.Drawing.Point(443, 80)
        Me.TextBoxAmount2.Name = "TextBoxAmount2"
        Me.TextBoxAmount2.Size = New System.Drawing.Size(98, 20)
        Me.TextBoxAmount2.TabIndex = 63
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Location = New System.Drawing.Point(339, 12)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSearch.TabIndex = 65
        Me.ButtonSearch.Text = "Cerca"
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 132)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(529, 332)
        Me.DataGridView1.TabIndex = 64
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(460, 38)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(27, 13)
        Me.Label7.TabIndex = 67
        Me.Label7.Text = "Fine"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(351, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 66
        Me.Label8.Text = "Inizio"
        '
        'DocumentSearchFrm
        '
        Me.AcceptButton = Me.ButtonSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 482)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBoxAmount2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TextBoxNrDoc2)
        Me.Controls.Add(Me.DateTimePickerDoc2)
        Me.Controls.Add(Me.DateTimePickerDre2)
        Me.Controls.Add(Me.TextBoxAmount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxNote)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBoxDocType)
        Me.Controls.Add(Me.TextBoxNrDoc)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DateTimePickerDoc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DateTimePickerDre)
        Me.Name = "DocumentSearchFrm"
        Me.Text = "Ricerca Documenti"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
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
    Friend WithEvents DateTimePickerDre2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerDoc2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxNrDoc2 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAmount2 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSearch As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
