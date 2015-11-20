<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Bilancio
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContoCEEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContoPCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TipoDocumentoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AvisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ContoCEEToolStripMenuItem, Me.ContoPCToolStripMenuItem, Me.TipoDocumentoToolStripMenuItem, Me.AvisToolStripMenuItem, Me.ReportToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1091, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(78, 20)
        Me.ToolStripMenuItem1.Text = "Documenti"
        '
        'ContoCEEToolStripMenuItem
        '
        Me.ContoCEEToolStripMenuItem.Name = "ContoCEEToolStripMenuItem"
        Me.ContoCEEToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.ContoCEEToolStripMenuItem.Text = "Conto CEE"
        '
        'ContoPCToolStripMenuItem
        '
        Me.ContoPCToolStripMenuItem.Name = "ContoPCToolStripMenuItem"
        Me.ContoPCToolStripMenuItem.Size = New System.Drawing.Size(70, 20)
        Me.ContoPCToolStripMenuItem.Text = "Conto PC"
        '
        'TipoDocumentoToolStripMenuItem
        '
        Me.TipoDocumentoToolStripMenuItem.Name = "TipoDocumentoToolStripMenuItem"
        Me.TipoDocumentoToolStripMenuItem.Size = New System.Drawing.Size(109, 20)
        Me.TipoDocumentoToolStripMenuItem.Text = "Tipo Documento"
        '
        'AvisToolStripMenuItem
        '
        Me.AvisToolStripMenuItem.Name = "AvisToolStripMenuItem"
        Me.AvisToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.AvisToolStripMenuItem.Text = "Avis"
        '
        'ReportToolStripMenuItem
        '
        Me.ReportToolStripMenuItem.Name = "ReportToolStripMenuItem"
        Me.ReportToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
        Me.ReportToolStripMenuItem.Text = "Report"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 559)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1091, 22)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'Bilancio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1091, 581)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Bilancio"
        Me.Text = "Bilancio"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContoCEEToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContoPCToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TipoDocumentoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AvisToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel

End Class
