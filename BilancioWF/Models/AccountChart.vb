'Imports System.ComponentModel.DataAnnotations
'Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class AccountChart
        Inherits CodeName

        '<Display(Name:="Segno")>
        '<ScaffoldColumn(False)>
        Public Property Debit As DareAvere

        '<Display(Name:="Riferimento CEE")>
        '<ForeignKey("AccountCee")>
        Public Property AccountCeeID As Integer?

        Public Overridable Property AccountCee As AccountCee
        Public Overridable Property DocumentRows As ICollection(Of DocumentRow)

        Private Property _creditDebit As CreditDebit


        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public ReadOnly Property creditDebit As CreditDebit
            Get
                Return _creditDebit
            End Get
        End Property

        'calcola e ritorna i totali del conto dalle righe documento che hanno l'anno compatibile
        Function calculateCreditDebit(year As Integer) As CreditDebit

            _creditDebit = New CreditDebit(year, Debit)

            If (Not IsNothing(DocumentRows)) Then
                DocumentRows.Where(Function(a) a.Document.dateReg.Year = year Or a.Document.dateReg.Year = year - 1).ToList().ForEach(Sub(a) _creditDebit.addRow(a))
            End If

            Return _creditDebit

        End Function

        Sub addDocumentRow(detail As DocumentRow)
            If (IsNothing(DocumentRows)) Then
                DocumentRows = New List(Of DocumentRow)
            End If
            DocumentRows.Add(detail)
            detail.AccountChart = Me
        End Sub

    End Class

End Namespace