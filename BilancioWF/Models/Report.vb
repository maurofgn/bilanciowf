
Namespace Models

    Public Enum ReportFormatType
        PDF
        HTML
        XML
        CSV
        XLS
        ODT
        ODS
        DOCX
        XLSX
        PPTX
    End Enum

    Public Class Report
        Inherits CodeName
        Implements EntityBase

        Public Property ModelName As String


        '<DisplayFormat(NullDisplayText:="Non definito")>
        Public Property FormatType As ReportFormatType 'Integer?

        Public Property OutFileName As String

        '<Display(Name:="Azione")>
        Public Property ActionName As String
        '<Display(Name:="Controllo")>
        Public Property ControllerName As String

        '<DataType(DataType.DateTime)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Data Creazione")>
        '<Editable(False)>
        Public Property dateCreated As DateTime Implements EntityBase.dateCreated

        '<DataType(DataType.DateTime)>
        '<DisplayFormat(DataFormatString:="{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode:=True)>
        '<Display(Name:="Ultimo Aggiornamento")>
        '<Editable(False)>
        Public Property lastUpdate As DateTime Implements EntityBase.lastUpdate

    End Class

End Namespace
