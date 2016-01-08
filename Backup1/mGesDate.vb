Module mGesDate

    Public Const FORMATO_DATAITA As String = "dd/MM/yyyy"      '** Fisso non cambiare per versione internazionale

    Public Const FORMATO_DATA As String = "dd/MM/yyyy"
    Public Const FORMATO_DATA1 As String = "dd/MM/yyyy HH.mm"
    Public Const FORMATO_DATA2 As String = "dd/MM/yyyy HH.mm.ss"
    Public Const FORMATO_DATA4 As String = "HH.mm"
    Public Const FORMATO_DATA7 As String = "yyyyMMddHHmmss"
    Public Const FORMATO_DATA50 As String = "dd/MM/yyyy HH.mm.ss.ffff"

    Public g_arrFormatiColonne(2)() As String

    Public Sub CreaArrFormatiColonne()
        ReDim Preserve g_arrFormatiColonne(1)(1)
        ReDim Preserve g_arrFormatiColonne(2)(1)

        'g_arrFormatiColonne(1)(1) = "" : g_arrFormatiColonne(2)(1) = FORMATO_DATA
    End Sub

    Public Function Date2Date(ByVal vDataOra As Object) As Date
        If myIsDate(vDataOra) Then
            Date2Date = DateSerial(Year(vDataOra), Month(vDataOra), Day(vDataOra))
        Else
            Date2Date = #1/1/1753#
        End If
    End Function

    Public Function myIsDate(ByVal vData As Object) As Boolean
        Dim vAppo As Date
        Dim provider As IFormatProvider = New Globalization.CultureInfo("it-IT")

        myIsDate = False
        Try
            If DateTime.TryParse(CStr(vData), vAppo) Then
                If CDate(vAppo) >= #1/1/1753# Then
                    myIsDate = True
                End If
            Else
                vData = Replace(vData, ".", ":")
                If DateTime.TryParse(CStr(vData), vAppo) Then
                    If CDate(vAppo) >= #1/1/1753# Then
                        myIsDate = True
                    End If
                End If
            End If

        Catch ex As Exception
            myIsDate = False
        End Try
    End Function

    Public Function FormattaData_Db2Db(ByVal vDataOra As Object, _
                                       ByVal sNomeCampo As String) As Object
        Dim sAppo As String
        Dim lPos As Long

        If myIsDate(vDataOra) Then
            lPos = Find_In_Array_String(sNomeCampo, g_arrFormatiColonne, 1)
            If lPos >= 0 Then
                FormattaData_Db2Db = DateTime.Parse(Replace(vDataOra, ".", ":"))
                sAppo = FormattaData_2Str(FormattaData_Db2Db, "", g_arrFormatiColonne(2)(lPos))
                FormattaData_Db2Db = DateTime.Parse(Replace(sAppo, ".", ":"))
            Else
                FormattaData_Db2Db = vDataOra
            End If
        Else
            FormattaData_Db2Db = DBNull.Value
        End If
    End Function

    Public Function FormattaData_Str2Db(ByVal vDataOra As String, _
                                        ByVal sNomeCampo As String) As Object
        Dim sAppo As String
        Dim iAnno As Integer, iMese As Integer, iGiorno As Integer
        Dim iOra As Integer, iMinuti As Integer, iSecondi As Integer

        If myIsDate(vDataOra) Then

            iGiorno = Val(Mid(vDataOra, 1, 2))
            iMese = Val(Mid(vDataOra, 4, 2))
            iAnno = Val(Mid(vDataOra, 7, 4))
            iOra = Val(Mid(vDataOra, 12, 2))
            iMinuti = Val(Mid(vDataOra, 15, 2))
            iSecondi = Val(Mid(vDataOra, 17, 2))

            FormattaData_Str2Db = DateSerial(iAnno, iMese, iGiorno)
            If (iOra >= 0) And (iOra <= 23) Then
                FormattaData_Str2Db = DateAdd(DateInterval.Hour, iOra, FormattaData_Str2Db)
            End If
            If (iMinuti >= 0) And (iMinuti <= 59) Then
                FormattaData_Str2Db = DateAdd(DateInterval.Minute, iMinuti, FormattaData_Str2Db)
            End If
            If (iSecondi >= 0) And (iSecondi <= 59) Then
                FormattaData_Str2Db = DateAdd(DateInterval.Second, iSecondi, FormattaData_Str2Db)
            End If

            sAppo = FormattaData_2Str(FormattaData_Str2Db, sNomeCampo)
            FormattaData_Str2Db = DateTime.Parse(Replace(sAppo, ".", ":"))
        Else
            FormattaData_Str2Db = DBNull.Value
        End If
    End Function

    Public Function FormattaData_2Str(ByVal sValore As String, _
                                      ByVal sNomeCampo As String, _
                                      Optional ByVal sFormato As String = FORMATO_DATA2) As String
        Dim lPos As Long

        If myIsDate(sValore) Then
            Dim resultDate As DateTime = DateTime.Parse(Replace(sValore, ".", ":"))
            ' Converto nuovamente la data in stringa usando il nuovo formato
            If sNomeCampo <> "" Then
                lPos = Find_In_Array_String(sNomeCampo, g_arrFormatiColonne, 1)
                If lPos >= 0 Then
                    sFormato = g_arrFormatiColonne(2)(lPos)
                End If
            End If
            FormattaData_2Str = resultDate.ToString(sFormato)
        Else
            FormattaData_2Str = ""
        End If
    End Function

    Public Function FormattaData_2Str(ByVal dValore As DateTime, _
                                      ByVal sNomeCampo As String, _
                                      Optional ByVal sFormato As String = FORMATO_DATA2) As String
        Dim lPos As Long

        If myIsDate(dValore) Then
            Dim resultDate As DateTime = DateTime.Parse(Replace(dValore, ".", ":"))
            ' Converto nuovamente la data in stringa usando il nuovo formato
            If sNomeCampo <> "" Then
                lPos = Find_In_Array_String(sNomeCampo, g_arrFormatiColonne, 1)
                If lPos >= 0 Then
                    sFormato = g_arrFormatiColonne(2)(lPos)
                End If
            End If
            FormattaData_2Str = resultDate.ToString(sFormato)
        Else
            FormattaData_2Str = ""
        End If
    End Function

    Public Function FormattaData_2DtNull(ByVal sValore As String) As Nullable(Of DateTime)
        If myIsDate(sValore) Then
            FormattaData_2DtNull = CDate(sValore)
        Else
            FormattaData_2DtNull = Nothing
        End If
    End Function

    Public Function ConfrontaDate_IsDiverse(ByVal vData1_2Db As Object, _
                                            ByVal vData2_2Db As Object) As Boolean
        Dim bIsDiverso As Boolean

        bIsDiverso = False

        If vData1_2Db.Equals(DBNull.Value) And vData2_2Db.Equals(DBNull.Value) Then
        ElseIf vData1_2Db.Equals(DBNull.Value) Or vData2_2Db.Equals(DBNull.Value) Then
            bIsDiverso = True
        ElseIf vData1_2Db <> vData2_2Db Then
            bIsDiverso = True
        End If

        ConfrontaDate_IsDiverse = bIsDiverso
    End Function
End Module
