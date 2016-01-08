Imports BilancioWF.Models

Public Class DocumentFrm

    Enum errorsCodHead As Integer
        amount = 0
        nrDoc = 1
        docType = 2
        dataReg = 3
    End Enum

    Dim errorsHead As String() = {"Totale doc non valido", "Nr doc. non valido", "Il tipo Documento è obbligatorio", "La Data Reg. non può essere inferiore alla Data Doc."}

    Enum errorsCodRow As Integer
        amount = 0
        account = 1
    End Enum

    Dim errorsRow As String() = {"Importo di riga non valido", "Il conto è obbligatorio"}

    Dim controller As DocumentController = New DocumentController()
    Dim controllerAccountChart As AccountChartController = New AccountChartController()
    Dim current As Document
    Dim currentRow As DocumentRow
    Dim listViewItemIdPos As Integer    'posizione dell'ID nei subItem

    Private Sub DocumentFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim dtc = New DocumentTypeController()
        Dim acc = New AccountChartController()

        Me.ComboBoxDocType.Tag = True
        Me.ComboBoxAccountChart.Tag = True
        PopulateCombo(ComboBoxDocType, dtc.comboItems(), "ID", "Name")
        PopulateCombo(ComboBoxAccountChart, acc.comboItems(), "ID", "Name")
        Me.ComboBoxDocType.Tag = False
        Me.ComboBoxAccountChart.Tag = False

        ListView1.View = View.Details
        ListView1.GridLines = True
        ListView1.MultiSelect = False
        ListView1.Columns.Clear()
        ListView1.Columns.Add("Riga", 40, HorizontalAlignment.Center)           'Column 1
        ListView1.Columns.Add("Conto", 180, HorizontalAlignment.Left)           'Column 2
        ListView1.Columns.Add("D/A", 40, HorizontalAlignment.Center)            'Column 3
        ListView1.Columns.Add("Importo", 75, HorizontalAlignment.Center)        'Column 4
        ListView1.Columns.Add("Nota", 180, HorizontalAlignment.Left)            'Column 5

        ButtonSave.Enabled = False
        ButtonNew.Enabled = False
        current = controller.Edit()
        assignView()

    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click

        Dim sea As DocumentSearchFrm = New DocumentSearchFrm()

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
        'If (IsNothing(current)) Then
        '    current = controller.Edit()
        'End If

        Me.DateTimePickerDre.Value = current.dateReg()
        Me.DateTimePickerDoc.Value = current.dateDoc()
        Me.TextBoxNrDoc.Text = current.docNr
        Me.ComboBoxDocType.SelectedValue = current.DocumentType_ID
        Me.TextBoxAmount.Text = current.amount
        Me.TextBoxNote.Text = current.note

        updStato()

        loadGrid()
        assignRowView()

        ButtonNew.Enabled = current.ID > 0
        ButtonDel.Enabled = current.ID > 0
        ButtonSave.Enabled = current.ID > 0

    End Sub

    Private Sub assignRowView()
        If (IsNothing(currentRow) OrElse currentRow.ID <= 0) Then
            TextBoxRowNr.Text = ""
            ComboBoxAccountChart.SelectedValue = 0
            LabelDebit.Text = ""
            TextBoxRowAmount.Text = ""
            TextBoxRowNote.Text = ""
        Else
            TextBoxRowNr.Text = currentRow.rowNr
            ComboBoxAccountChart.SelectedValue = currentRow.AccountChart_ID
            LabelDebit.Text = currentRow.debit.ToString
            TextBoxRowAmount.Text = currentRow.amount
            TextBoxRowNote.Text = currentRow.note
        End If

    End Sub

    Private Sub loadGrid()

        ListView1.Items.Clear()

        If (current.documentRows.Count >= 1) Then
            currentRow = current.documentRows(0)
            For Each r In current.documentRows
                ListView1.Items.Add(getListViewItem(r))
            Next
        Else
            currentRow = New DocumentRow()
        End If

    End Sub

    Private Function getListViewItem(r As DocumentRow) As ListViewItem
        getListViewItem = New ListViewItem(New String() {r.rowNr, r.AccountChart.ToString, r.debit.ToString, r.amount.ToString, r.note, r.ID})
        listViewItemIdPos = getListViewItem.SubItems.Count() - 1

    End Function



    'assegna oggetto dalla view
    Private Function assigneCurrent() As Boolean

        Dim errors = validateHead()
        If (errors > 0) Then
            MsgBox(String.Join(Environment.NewLine, errorsHeadMsg(errors)))
            Return False
        End If

        'If (IsNothing(current)) Then
        '    current = New Document()
        'End If

        current.dateReg() = Me.DateTimePickerDre.Value
        current.dateDoc() = Me.DateTimePickerDoc.Value
        current.docNr = Me.TextBoxNrDoc.Text
        current.DocumentType_ID = Me.ComboBoxDocType.SelectedValue
        current.amount = Me.TextBoxAmount.Text
        current.note = Me.TextBoxNote.Text

        Return True

    End Function

    Private Function assigneCurrentRow() As Boolean

        Dim errors = validateRows()
        If (errors > 0) Then
            MsgBox(String.Join(Environment.NewLine, errorsRowMsg(errors)))
            Return False
        End If

        If (IsNothing(currentRow)) Then
            currentRow = New DocumentRow()
        End If

        currentRow.rowNr() = parseInteger(Me.TextBoxRowNr.Text)
        currentRow.AccountChart_ID() = Me.ComboBoxAccountChart.SelectedValue
        currentRow.AccountChart = controllerAccountChart.Edit(currentRow.AccountChart_ID())
        currentRow.debit() = currentRow.AccountChart.Debit
        currentRow.amount = Me.TextBoxRowAmount.Text
        currentRow.note = Me.TextBoxRowNote.Text
        currentRow.Document = current
        currentRow.Document_ID = current.ID

        Return True

    End Function

    Private Function validateHead() As Integer

        validateHead = 0

        If (parseDecimal(Me.TextBoxAmount.Text) <= 0) Then
            validateHead = validateHead + 2 ^ errorsCodHead.amount
        End If

        If (String.IsNullOrEmpty(TextBoxNrDoc.Text)) Then
            validateHead = validateHead + 2 ^ errorsCodHead.nrDoc
        End If

        If (IsNothing(ComboBoxDocType.SelectedValue)) Then
            validateHead = validateHead + 2 ^ errorsCodHead.docType
        End If

        If (DateTimePickerDre.Value.CompareTo(DateTimePickerDoc.Value) < 0) Then
            validateHead = validateHead + 2 ^ errorsCodHead.dataReg
        End If

        'If (IsNothing(current) OrElse (Not IsNothing(current) AndAlso current.documentRows.Count < 2)) Then
        '    validateHead = validateHead + 2 ^ errorsCodHead.atLeastTwoRows
        'End If

        ButtonSave.Enabled = validateHead = 0


    End Function

    Private Function validateRows() As Integer
        validateRows = 0
        If (parseDecimal(TextBoxRowAmount.Text) = 0) Then
            validateRows = validateRows + 2 ^ errorsCodRow.amount
        End If

        If (ComboBoxAccountChart.SelectedValue = 0) Then
            validateRows = validateRows + 2 ^ errorsCodRow.account
        End If

    End Function

    Private Function errorsHeadMsg(errors As Integer) As String

        errorsHeadMsg = String.Empty
        For Each err As errorsCodHead In System.Enum.GetValues(GetType(errorsCodHead))
            If (errors And 2 ^ err) Then
                errorsHeadMsg &= IIf(errorsHeadMsg.Length = 0, "", Environment.NewLine) & errorsHead(err)
            End If
        Next

    End Function

    Private Function errorsRowMsg(errors As Integer) As String

        errorsRowMsg = String.Empty
        For Each err As errorsCodRow In System.Enum.GetValues(GetType(errorsCodRow))
            If (errors And 2 ^ err) Then
                errorsRowMsg &= IIf(errorsRowMsg.Length = 0, "", Environment.NewLine) & errorsRow(err)
            End If
        Next

    End Function

    Private Sub ButtonNew_Click(sender As Object, e As EventArgs) Handles ButtonNew.Click
        current = New Document()
        assignView()
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        saveHead()
        updStato()
    End Sub

    Private Function saveHead() As Boolean

        saveHead = assigneCurrent()

        If (saveHead) Then
            saveHead = controller.save(current)
            ButtonNew.Enabled = saveHead
            ButtonDel.Enabled = True

            If (Not saveHead) Then
                MsgBox("Testata non salvata")
            End If
        End If

    End Function

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click

        If (Not IsNothing(current)) Then
            If (controller.remove(current.ID)) Then
                current = New Document()
            Else
                MsgBox("Non è possibile eliminare questo record")
            End If
        End If

        assignView()

    End Sub

    Private Sub ButtonRowNew_Click(sender As Object, e As EventArgs) Handles ButtonRowNew.Click
        currentRow = Nothing
        ButtonRowNew.Enabled = False
        ButtonRowDel.Enabled = True
    End Sub

    Private Sub ButtonRowSave_Click(sender As Object, e As EventArgs) Handles ButtonRowSave.Click

        If (current.ID <= 0) Then
            If (Not saveHead()) Then
                Return
            End If
        End If

        If (assigneCurrentRow()) Then
            Dim isNewRow As Boolean = currentRow.ID <= 0

            Dim successful = controller.save(currentRow)

            ButtonRowNew.Enabled = successful
            ButtonRowDel.Enabled = True

            If (successful) Then
                updListView(isNewRow)
                updStato()
            Else
                MsgBox("Salvataggio della riga non riuscito")
            End If
        End If

    End Sub

    Private Sub updStato()
        LabelStatoDoc.Text = current.totalInfo.stateMsg()
    End Sub

    'aggiorna la listView
    Private Sub updListView(isNewRow As Boolean)
        If (isNewRow) Then
            ListView1.Items.Add(getListViewItem(currentRow))
        Else
            For pos = 0 To ListView1.Items.Count - 1 Step 1
                Dim listItem = ListView1.Items(pos)
                If parseInteger(listItem.SubItems(listViewItemIdPos).Text) = currentRow.ID Then
                    ListView1.Items(pos) = getListViewItem(currentRow)
                    Exit For
                End If
            Next
        End If
    End Sub

    'elimina dalla listView la riga corrispondende a currentRow e poi riassegna il nuovo currentRow
    Private Sub removeItemFromListView()

        Dim newCurrentId As Integer = 0

        For pos = ListView1.Items.Count - 1 To 0 Step -1
            Dim listItem = ListView1.Items(pos)

            If listItem.SubItems(listViewItemIdPos).Text = currentRow.ID Then
                ListView1.Items.Remove(listItem)
                Exit For
            Else
                newCurrentId = listItem.SubItems(listViewItemIdPos).Text    'successivo al record da eliminare, assunto come corrente
            End If
        Next

        If (newCurrentId > 0) Then
            currentRow = currentRow.Document.getRow(newCurrentId)   'successiva riga rispetto a quella eliminata
        ElseIf (currentRow.Document.documentRows.Count > 0) Then
            currentRow = currentRow.Document.documentRows.Last      'ultima riga di quelle rimaste
        Else
            currentRow = New DocumentRow()                          'documento senza righe
        End If

    End Sub

    Private Sub ButtonRowDel_Click(sender As Object, e As EventArgs) Handles ButtonRowDel.Click

        If (IsNothing(currentRow) OrElse currentRow.ID <= 0) Then
            Return
        End If

        If (controller.removeLine(currentRow)) Then
            removeItemFromListView()
            assignRowView()
            updStato()
        Else
            MsgBox("Non è possibile eliminare questa riga")
        End If

    End Sub

    Private Sub TextBoxAmount_LostFocus(sender As Object, e As EventArgs) Handles TextBoxAmount.LostFocus
        validateHead()
    End Sub


    Private Sub TextBoxNrDoc_LostFocus(sender As Object, e As EventArgs) Handles TextBoxNrDoc.LostFocus
        validateHead()
    End Sub
    Private Sub DateTimePickerDre_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerDre.ValueChanged
        validateHead()
    End Sub

    Private Sub DateTimePickerDoc_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePickerDoc.ValueChanged
        validateHead()
    End Sub


    Private Sub ComboBoxAccountChart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxAccountChart.SelectedIndexChanged
        If (ComboBoxAccountChart.Tag) Then
            Return
        End If

        If (IsNothing(ComboBoxAccountChart.SelectedItem)) Then
            LabelDebit.Text = ""
        Else
            Dim c As AccountChart = CType(ComboBoxAccountChart.SelectedItem, AccountChart)
            LabelDebit.Text = c.Debit.ToString
        End If

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

        If (ListView1.SelectedIndices.Count > 0) Then
            currentRow = current.documentRows(ListView1.SelectedIndices(0))
            assignRowView()
        End If

    End Sub

End Class