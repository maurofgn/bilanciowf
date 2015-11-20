Public Class Bilancio

    Private Sub ContoCEEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContoCEEToolStripMenuItem.Click
        Dim f = New AccountCeeFrm()
        f.MdiParent = Me
        f.Show()

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click

    End Sub
End Class
