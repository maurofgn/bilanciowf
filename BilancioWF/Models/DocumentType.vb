'Imports System.ComponentModel.DataAnnotations
'Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models
    Public Class DocumentType

        Public Property ID As Integer

        '<Required>
        '<StringLength(20, MinimumLength:=1, ErrorMessage:="{0} deve essere di almeno {2} caratteri e massimo {1}.")>
        '<Display(Name:="Codice")>
        '<Index("codeIndex", IsUnique:=True)>
        Public Property Code As String

        '<Required>
        '<StringLength(60, MinimumLength:=3, ErrorMessage:="{0} deve essere di almeno {2} carattere e massimo {1}.")>
        '<Display(Name:="Descrizione")>
        Public Property Name As String

        Public Property Active As Boolean = True
        Public Overridable Property Document As ICollection(Of Document)

    End Class

End Namespace