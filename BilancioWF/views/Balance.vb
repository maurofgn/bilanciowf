Imports BilancioWF.Models
Imports BilancioWF.ViewModels

Public Class Balance

    Private Sub Balance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.ComboBoxBalanceType.Tag = True
        Me.ComboBoxYear.Tag = True

        ListView1.View = View.Details
        ListView1.GridLines = True
        ListView1.MultiSelect = False

        PopulateCombo(ComboBoxBalanceType, AccountCeeController.baseBalance(), "nodeType", "Name")
        PopulateCombo(ComboBoxYear, AccountCeeController.baseYears())

        Me.ComboBoxBalanceType.Tag = False
        Me.ComboBoxYear.Tag = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        loadGrid()

    End Sub

    'carica la listView con le righe di bilancio
    Private Sub loadGrid()

        Dim year As Integer = ComboBoxYear.SelectedItem
        Dim nt As NodeType = ComboBoxBalanceType.SelectedValue

        Dim cec = New AccountCeeController()
        Dim balance As List(Of CreditDebitAccount) = cec.balance(nt, year)

        ListView1.Columns.Clear()
        ListView1.Columns.Add("Codice", 60, HorizontalAlignment.Center)             'Column 1
        ListView1.Columns.Add("Nome", 200, HorizontalAlignment.Left)                'Column 2
        ListView1.Columns.Add(year.ToString, 60, HorizontalAlignment.Right)         'Column 3
        ListView1.Columns.Add((year - 1).ToString, 60, HorizontalAlignment.Right)   'Column 4
        ListView1.Columns.Add("Delta", 60, HorizontalAlignment.Right)               'Column 5

        ListView1.Items.Clear()

        'Trace.WriteLine("Nr records x balance: " & balance.Count)

        For Each r In balance
            ListView1.Items.Add(getListViewItem(r))
        Next

    End Sub

    'Formattazione di una singola riga di bilancio
    Private Function getListViewItem(r As CreditDebitAccount) As ListViewItem

        Dim cd As String = ""

        Dim row() As String = {r.Code, r.Name, "", "", ""}

        If (Not IsNothing(r.creditDebit) AndAlso Not r.creditDebit.isEmpty) Then
            row(2) = r.creditDebit.balanceYear.ToString
            row(3) = r.creditDebit.balanceYearPrev.ToString
            row(4) = r.creditDebit.delta.ToString
        End If

        Return New ListViewItem(row)

    End Function

End Class