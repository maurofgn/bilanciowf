Public Class CodeName

    Public Shared VOID = New CodeName() With {.ID = 0, .Code = "", .Name = "---"}

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

    '<Timestamp>
    'Public Property RowVersion As Byte()
    '<Display(Name:="[Codice]Nome")>
    Public ReadOnly Property CodeName() As String
        Get
            If (ID > 0) Then
                Return String.Format("[{0}]{1}", Code, Name)
            Else
                Return "---"
            End If

        End Get

    End Property



End Class
