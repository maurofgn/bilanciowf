Imports System.Text

'Imports Bilancio.Models
'Imports System.ComponentModel.DataAnnotations

Namespace ViewModels

    Public Enum HeadFood As Integer
        BODY = 0
        HEAD_WIDTH_FOOD = 1
        HEAD_WIDTHOUT_FOOD = 2
        FOOD = 3
    End Enum

    Public Class CreditDebitAccount

        '<Display(Name:="Codice")>
        Public Property Code As String

        '<Display(Name:="Descrizione")>
        Public Property Name As String

        Public Property creditDebit As CreditDebit

        Public Property headFoot As HeadFood

        Public Overrides Function toString() As String

            Dim cd As String = ""

            If (Not IsNothing(creditDebit)) Then
                cd = creditDebit.toString
            End If

            Return String.Format("{0} {1} {2}", Code, Name, cd)

        End Function

    End Class

End Namespace