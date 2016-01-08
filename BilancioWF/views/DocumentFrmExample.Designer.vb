<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DocumentFrmExample
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
        Me.components = New System.ComponentModel.Container()
        Me.masterDataGridView = New System.Windows.Forms.DataGridView()
        Me.detailDataGridView = New System.Windows.Forms.DataGridView()
        Me.masterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.detailBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.masterDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.detailDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.masterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.detailBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'masterDataGridView
        '
        Me.masterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.masterDataGridView.Location = New System.Drawing.Point(12, 12)
        Me.masterDataGridView.Name = "masterDataGridView"
        Me.masterDataGridView.Size = New System.Drawing.Size(504, 150)
        Me.masterDataGridView.TabIndex = 0
        '
        'detailDataGridView
        '
        Me.detailDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.detailDataGridView.Location = New System.Drawing.Point(12, 178)
        Me.detailDataGridView.Name = "detailDataGridView"
        Me.detailDataGridView.Size = New System.Drawing.Size(504, 150)
        Me.detailDataGridView.TabIndex = 1
        '
        'DocumentFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(554, 361)
        Me.Controls.Add(Me.detailDataGridView)
        Me.Controls.Add(Me.masterDataGridView)
        Me.Name = "DocumentFrm"
        Me.Text = "Documenti"
        CType(Me.masterDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.detailDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.masterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.detailBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents masterDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents detailDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents masterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents detailBindingSource As System.Windows.Forms.BindingSource
End Class
