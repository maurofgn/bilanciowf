Public Class Bilancio

    Private Sub ContoCEEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContoCEEToolStripMenuItem.Click
        Dim f = New AccountCeeFrm()
        f.MdiParent = Me
        f.Show()

    End Sub


    Private Sub TipoDocumentoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TipoDocumentoToolStripMenuItem.Click
        Dim f = New DocumentTypeFrm()
        f.MdiParent = Me
        f.Show()

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        If (MsgBox("Vuoi uscire ?", MsgBoxStyle.OkCancel, "Bilancio") = MsgBoxResult.Ok) Then
            Me.Close()
        End If

    End Sub

    Private Sub ContoPCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContoPCToolStripMenuItem.Click
        Dim f = New AccountChartFrm()
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub AvisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AvisToolStripMenuItem.Click
        Dim f = New AvisFrm()
        f.MdiParent = Me
        f.Show()
    End Sub

    'Private Sub ReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportToolStripMenuItem.Click
    '    Dim f = New ReportFrm()
    '    f.MdiParent = Me
    '    f.Show()
    'End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim f = New DocumentFrm()
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub BilancioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BilancioToolStripMenuItem.Click
        Dim f = New Balance()
        f.MdiParent = Me
        f.Show()
    End Sub

End Class
