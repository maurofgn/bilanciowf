<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountCeeFrm
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
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxCode = New System.Windows.Forms.TextBox()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxSeqNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckBoxActive = New System.Windows.Forms.CheckBox()
        Me.CheckBoxParent = New System.Windows.Forms.CheckBox()
        Me.CheckBoxTotal = New System.Windows.Forms.CheckBox()
        Me.CheckBoxDebit = New System.Windows.Forms.CheckBox()
        Me.ComboBoxParent = New System.Windows.Forms.ComboBox()
        Me.ButtonNew = New System.Windows.Forms.Button()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonSearch = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.Location = New System.Drawing.Point(3, 3)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(445, 444)
        Me.TreeView1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(456, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Codice"
        '
        'TextBoxCode
        '
        Me.TextBoxCode.Location = New System.Drawing.Point(508, 12)
        Me.TextBoxCode.Name = "TextBoxCode"
        Me.TextBoxCode.Size = New System.Drawing.Size(134, 20)
        Me.TextBoxCode.TabIndex = 1
        '
        'TextBoxName
        '
        Me.TextBoxName.Location = New System.Drawing.Point(507, 41)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(252, 20)
        Me.TextBoxName.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(456, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Nome"
        '
        'TextBoxSeqNo
        '
        Me.TextBoxSeqNo.Location = New System.Drawing.Point(508, 67)
        Me.TextBoxSeqNo.Name = "TextBoxSeqNo"
        Me.TextBoxSeqNo.Size = New System.Drawing.Size(134, 20)
        Me.TextBoxSeqNo.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(456, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Sequenz."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(456, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Padre"
        '
        'CheckBoxActive
        '
        Me.CheckBoxActive.AutoSize = True
        Me.CheckBoxActive.Checked = True
        Me.CheckBoxActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxActive.Location = New System.Drawing.Point(631, 148)
        Me.CheckBoxActive.Name = "CheckBoxActive"
        Me.CheckBoxActive.Size = New System.Drawing.Size(53, 17)
        Me.CheckBoxActive.TabIndex = 8
        Me.CheckBoxActive.Text = "Attivo"
        Me.CheckBoxActive.UseVisualStyleBackColor = True
        '
        'CheckBoxParent
        '
        Me.CheckBoxParent.AutoSize = True
        Me.CheckBoxParent.Enabled = False
        Me.CheckBoxParent.Location = New System.Drawing.Point(454, 148)
        Me.CheckBoxParent.Name = "CheckBoxParent"
        Me.CheckBoxParent.Size = New System.Drawing.Size(54, 17)
        Me.CheckBoxParent.TabIndex = 5
        Me.CheckBoxParent.Text = "Padre"
        Me.CheckBoxParent.UseVisualStyleBackColor = True
        '
        'CheckBoxTotal
        '
        Me.CheckBoxTotal.AutoSize = True
        Me.CheckBoxTotal.Enabled = False
        Me.CheckBoxTotal.Location = New System.Drawing.Point(514, 148)
        Me.CheckBoxTotal.Name = "CheckBoxTotal"
        Me.CheckBoxTotal.Size = New System.Drawing.Size(56, 17)
        Me.CheckBoxTotal.TabIndex = 6
        Me.CheckBoxTotal.Text = "Totale"
        Me.CheckBoxTotal.UseVisualStyleBackColor = True
        '
        'CheckBoxDebit
        '
        Me.CheckBoxDebit.AutoSize = True
        Me.CheckBoxDebit.Enabled = False
        Me.CheckBoxDebit.Location = New System.Drawing.Point(576, 148)
        Me.CheckBoxDebit.Name = "CheckBoxDebit"
        Me.CheckBoxDebit.Size = New System.Drawing.Size(49, 17)
        Me.CheckBoxDebit.TabIndex = 7
        Me.CheckBoxDebit.Text = "Dare"
        Me.CheckBoxDebit.UseVisualStyleBackColor = True
        '
        'ComboBoxParent
        '
        Me.ComboBoxParent.FormattingEnabled = True
        Me.ComboBoxParent.Location = New System.Drawing.Point(508, 98)
        Me.ComboBoxParent.Name = "ComboBoxParent"
        Me.ComboBoxParent.Size = New System.Drawing.Size(251, 21)
        Me.ComboBoxParent.TabIndex = 4
        '
        'ButtonNew
        '
        Me.ButtonNew.Enabled = False
        Me.ButtonNew.Location = New System.Drawing.Point(459, 400)
        Me.ButtonNew.Name = "ButtonNew"
        Me.ButtonNew.Size = New System.Drawing.Size(63, 24)
        Me.ButtonNew.TabIndex = 9
        Me.ButtonNew.Text = "Nuovo"
        Me.ButtonNew.UseVisualStyleBackColor = True
        '
        'ButtonSave
        '
        Me.ButtonSave.Enabled = False
        Me.ButtonSave.Location = New System.Drawing.Point(528, 400)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(62, 24)
        Me.ButtonSave.TabIndex = 10
        Me.ButtonSave.Text = "Salva"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'ButtonDel
        '
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(596, 400)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(62, 24)
        Me.ButtonDel.TabIndex = 11
        Me.ButtonDel.Text = "Elimina"
        Me.ButtonDel.UseVisualStyleBackColor = True
        '
        'ButtonSearch
        '
        Me.ButtonSearch.Location = New System.Drawing.Point(664, 400)
        Me.ButtonSearch.Name = "ButtonSearch"
        Me.ButtonSearch.Size = New System.Drawing.Size(62, 24)
        Me.ButtonSearch.TabIndex = 12
        Me.ButtonSearch.Text = "Cerca"
        Me.ButtonSearch.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 459)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(779, 22)
        Me.StatusStrip1.TabIndex = 13
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'AccountCeeFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(779, 481)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ButtonSearch)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.ButtonNew)
        Me.Controls.Add(Me.ComboBoxParent)
        Me.Controls.Add(Me.CheckBoxDebit)
        Me.Controls.Add(Me.CheckBoxTotal)
        Me.Controls.Add(Me.CheckBoxParent)
        Me.Controls.Add(Me.CheckBoxActive)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxSeqNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TreeView1)
        Me.Name = "AccountCeeFrm"
        Me.Text = "Conti cee"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCode As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxSeqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxActive As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxParent As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxTotal As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxDebit As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxParent As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonNew As System.Windows.Forms.Button
    Friend WithEvents ButtonSave As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonSearch As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
End Class
