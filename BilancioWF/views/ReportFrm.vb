Imports BilancioWF.Models

Public Class ReportFrm

    Private controller As ReportController = New ReportController()
    Private current As Report


    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click

        Dim sea As Search = New Search("Report", "code", "name", "id", "active", TextBoxCode.Text, TextBoxName.Text)

        Try
            If (DialogResult.OK = sea.ShowDialog(Me)) Then
                current = controller.Edit(sea.selectedId())
                assignView()
            End If

        Finally
            sea.Dispose()
        End Try

    End Sub

    'Assegna il record alla view
    Private Sub assignView()
        If (IsNothing(current)) Then
            current = New Report()
        End If

        TextBoxCode.Text = current.Code
        TextBoxName.Text = current.Name
        CheckBoxActive.Checked = current.Active

        TextBoxModelName.Text = current.ModelName

        ComboBoxFormatType.Text = current.FormatType.ToString

        If (current.FormatType > 0) Then
            ComboBoxFormatType.Text = current.FormatType.ToString()
        Else
            ComboBoxFormatType.Text = ""
            ComboBoxFormatType.SelectedIndex = 0
        End If

        TextBoxOutFileName.Text = current.OutFileName
        TextBoxActionName.Text = current.ActionName
        TextBoxControllerName.Text = current.ControllerName
        'TextBoxModelName.Text = current.dateCreated
        'TextBoxModelName.Text = current.lastUpdate

        ButtonNew.Enabled = current.ID > 0
        ButtonDel.Enabled = current.ID > 0 AndAlso controller.keyNotUsed(current.ID)

    End Sub

    'assegna oggetto dalla view
    Private Function assigneCurrent() As Boolean

        Dim errors = validateErrors()
        If (errors.Count > 0) Then
            MsgBox(String.Join(Environment.NewLine, errors.ToArray()))
            Return False
        End If

        If (IsNothing(current)) Then
            current = New Report()
        End If

        current.Code = TextBoxCode.Text
        current.Name = TextBoxName.Text
        current.Active = CheckBoxActive.Checked

        current.ModelName = TextBoxModelName.Text

        If (IsNothing(ComboBoxFormatType.SelectedItem)) Then
            current.FormatType = 0
        Else
            current.FormatType = DirectCast([Enum].Parse(GetType(ReportFormatType), ComboBoxFormatType.SelectedItem), ReportFormatType)
        End If

        current.OutFileName = TextBoxOutFileName.Text
        current.ActionName = TextBoxActionName.Text
        current.ControllerName = TextBoxControllerName.Text
        'current.dateCreated = TextBoxModelName.Text
        'current.lastUpdate = TextBoxModelName.Text

        Return True

    End Function

    Private Function validateErrors() As IEnumerable(Of String)
        Dim id As Integer = 0
        If (Not IsNothing(current)) Then
            id = current.ID
        End If

        Dim errors As List(Of String) = controller.validateErrors(TextBoxCode.Text, TextBoxName.Text, id)

        Return errors

    End Function

    Private Sub ButtonNew_Click(sender As Object, e As EventArgs) Handles ButtonNew.Click
        current = Nothing
        ButtonNew.Enabled = False
        ButtonDel.Enabled = True
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        If (assigneCurrent()) Then

            Dim successful As Boolean = controller.save(current)
            ButtonNew.Enabled = successful
            ButtonDel.Enabled = True

            If (Not successful) Then
                MsgBox("Salvataggio non riuscito")
            End If

        End If
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        If (controller.remove(current)) Then
            'MsgBox("Eliminato")
            current = Nothing
            assignView()
        Else
            MsgBox("Non è possibile eliminare questo record")
        End If

    End Sub

    Private Sub ReportFrm_Load(sender As Object, e As EventArgs) Handles Me.Load
        ButtonNew.Enabled = False
        ComboBoxFormatType.DataSource = [Enum].GetNames(GetType(ReportFormatType))

        'ComboBoxFormatType.Items.Add("----")
        'For Each i In [Enum].GetValues(GetType(ReportFormatType))
        '    ComboBoxFormatType.Items.Add(i)
        'Next


    End Sub

End Class
