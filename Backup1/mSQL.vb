Module mSQL

    Public Const SQL_AND As String = " AND "
    Public Const SQL_OR As String = " OR "

    Public Enum enum_Confronto
        CLAUSE_EQUALS = 1
        CLAUSE_LIKE = 2
        CLAUSE_GREATERTHAN = 3
        CLAUSE_LESSTHAN = 4
        CLAUSE_GREATERTHANOREQUAL = 5
        CLAUSE_LESSTHANOREQUAL = 6
        CLAUSE_DOESNOTEQUAL = 7
        CLAUSE_DOESNOTLIKE = 8
        CLAUSE_STARTWITH = 9
        CLAUSE_ENDWITH = 10
        CLAUSE_ISNULL = 11
        CLAUSE_ISNOTNULL = 12
    End Enum

    Public Function fnWhere(ByVal sNomeCampo As String, _
                            ByVal eConfronto As enum_Confronto, _
                            ByVal vValore As Object, _
                            Optional ByVal bConsideraNull As Boolean = True, _
                            Optional ByVal iTipoDataSql As Integer = 0) As String
        '** bConsideraNull = True -> che nel confronto tra data i nul vengono presi
        '** in considerazione

        Dim sAppo As String

        fnWhere = ""

        If vValore.Equals(DBNull.Value) Then
            vValore = ""
        End If

        If eConfronto = enum_Confronto.CLAUSE_ISNULL Then
            fnWhere = " (" & sNomeCampo & " IS NULL) "
            Exit Function
        End If

        If eConfronto = enum_Confronto.CLAUSE_ISNOTNULL Then
            fnWhere = " (" & sNomeCampo & " IS NOT NULL) "
            Exit Function
        End If

        If VarType(vValore) = VariantType.String Then
            Select Case eConfronto
                Case enum_Confronto.CLAUSE_DOESNOTLIKE : fnWhere = "(" & sNomeCampo & " NOT LIKE " & StringWhere("%" & vValore & "%") & ")"
                Case enum_Confronto.CLAUSE_DOESNOTEQUAL : fnWhere = "(" & sNomeCampo & " <> " & StringWhere(vValore) & ")"
                Case enum_Confronto.CLAUSE_ENDWITH : fnWhere = "(" & sNomeCampo & " LIKE " & StringWhere("%" & vValore) & ")"
                Case enum_Confronto.CLAUSE_EQUALS
                    If CStr(vValore) = "" Then
                        If bConsideraNull Then
                            fnWhere = "(" & _
                                      "(" & sNomeCampo & " = '') OR " & _
                                      "(" & sNomeCampo & " IS NULL)" & _
                                      ")"
                        Else
                            fnWhere = "(" & sNomeCampo & " = '')"
                        End If
                    Else
                        fnWhere = "(" & sNomeCampo & " = " & StringWhere(vValore) & ")"
                    End If

                Case enum_Confronto.CLAUSE_GREATERTHAN : fnWhere = "(" & sNomeCampo & " > " & StringWhere(vValore) & ")"
                Case enum_Confronto.CLAUSE_GREATERTHANOREQUAL : fnWhere = "(" & sNomeCampo & " >= " & StringWhere(vValore) & ")"
                Case enum_Confronto.CLAUSE_LESSTHAN : fnWhere = "(" & sNomeCampo & " < " & StringWhere(vValore) & ")"
                Case enum_Confronto.CLAUSE_LESSTHANOREQUAL : fnWhere = "(" & sNomeCampo & " <= " & StringWhere(vValore) & ")"
                Case enum_Confronto.CLAUSE_LIKE : fnWhere = "(" & sNomeCampo & " LIKE " & StringWhere("%" & vValore & "%") & ")"
                Case enum_Confronto.CLAUSE_STARTWITH : fnWhere = "(" & sNomeCampo & " LIKE " & StringWhere(vValore & "%") & ")"
            End Select

        ElseIf VarType(vValore) = VariantType.Boolean Then
            sAppo = CStr(System.Math.Abs(CInt(vValore)))
            Select Case eConfronto
                Case enum_Confronto.CLAUSE_DOESNOTEQUAL : fnWhere = "(" & sNomeCampo & " <> " & sAppo & ")"
                Case enum_Confronto.CLAUSE_EQUALS : fnWhere = "(" & sNomeCampo & " = " & sAppo & ")"
            End Select

        ElseIf (VarType(vValore) = VariantType.Integer) Or (VarType(vValore) = VariantType.Long) Or (VarType(vValore) = VariantType.Byte) Then
            Select Case eConfronto
                Case enum_Confronto.CLAUSE_DOESNOTEQUAL : fnWhere = "(" & sNomeCampo & " <> " & vValore & ")"
                Case enum_Confronto.CLAUSE_EQUALS : fnWhere = "(" & sNomeCampo & " = " & vValore & ")"
                Case enum_Confronto.CLAUSE_GREATERTHAN : fnWhere = "(" & sNomeCampo & " > " & vValore & ")"
                Case enum_Confronto.CLAUSE_GREATERTHANOREQUAL : fnWhere = "(" & sNomeCampo & " >= " & vValore & ")"
                Case enum_Confronto.CLAUSE_LESSTHAN : fnWhere = "(" & sNomeCampo & " < " & vValore & ")"
                Case enum_Confronto.CLAUSE_LESSTHANOREQUAL : fnWhere = "(" & sNomeCampo & " <= " & vValore & ")"
            End Select

        ElseIf (VarType(vValore) = VariantType.Double) Then
            Select Case eConfronto
                Case enum_Confronto.CLAUSE_DOESNOTEQUAL : fnWhere = "(" & sNomeCampo & " <> " & Single2StringDecimal(vValore) & ")"
                Case enum_Confronto.CLAUSE_EQUALS : fnWhere = "(" & sNomeCampo & " = " & Single2StringDecimal(vValore) & ")"
                Case enum_Confronto.CLAUSE_GREATERTHAN : fnWhere = "(" & sNomeCampo & " > " & Single2StringDecimal(vValore) & ")"
                Case enum_Confronto.CLAUSE_GREATERTHANOREQUAL : fnWhere = "(" & sNomeCampo & " >= " & Single2StringDecimal(vValore) & ")"
                Case enum_Confronto.CLAUSE_LESSTHAN : fnWhere = "(" & sNomeCampo & " < " & Single2StringDecimal(vValore) & ")"
                Case enum_Confronto.CLAUSE_LESSTHANOREQUAL : fnWhere = "(" & sNomeCampo & " <= " & Single2StringDecimal(vValore) & ")"
            End Select

        ElseIf (VarType(vValore) = VariantType.Date) Then
            Select Case eConfronto
                Case enum_Confronto.CLAUSE_DOESNOTEQUAL : fnWhere = "(" & sNomeCampo & " <> " & DateWhere(vValore, iTipoDataSql) & ")"
                Case enum_Confronto.CLAUSE_EQUALS : fnWhere = "(" & sNomeCampo & " = " & DateWhere(vValore, iTipoDataSql) & ")"
                Case enum_Confronto.CLAUSE_GREATERTHAN : fnWhere = "(" & sNomeCampo & " > " & DateWhere(vValore, iTipoDataSql) & ")"
                Case enum_Confronto.CLAUSE_GREATERTHANOREQUAL : fnWhere = "(" & sNomeCampo & " >= " & DateWhere(vValore, iTipoDataSql) & ")"
                Case enum_Confronto.CLAUSE_LESSTHAN
                    If bConsideraNull Then
                        fnWhere = "((" & sNomeCampo & " < " & DateWhere(vValore, iTipoDataSql) & ") OR (" & sNomeCampo & " is null))"
                    Else
                        fnWhere = "(" & sNomeCampo & " < " & DateWhere(vValore, iTipoDataSql) & ")"
                    End If
                Case enum_Confronto.CLAUSE_LESSTHANOREQUAL
                    If bConsideraNull Then
                        fnWhere = "((" & sNomeCampo & " <= " & DateWhere(vValore, iTipoDataSql) & ") OR (" & sNomeCampo & " is null))"
                    Else
                        fnWhere = "(" & sNomeCampo & " <= " & DateWhere(vValore, iTipoDataSql) & ")"
                    End If

            End Select
        ElseIf vValore Is Nothing Then
            Select Case eConfronto
                Case enum_Confronto.CLAUSE_DOESNOTEQUAL : fnWhere = "(" & sNomeCampo & " is not null)"
                Case enum_Confronto.CLAUSE_EQUALS : fnWhere = "(" & sNomeCampo & " is null)"
            End Select
        Else
            Stop
        End If

        If fnWhere <> "" Then fnWhere = " " & fnWhere & " "
    End Function

    '** Converte il vTesto in una espressione valida per una istruzione WHERE
    Private Function StringWhere(ByVal vTesto As Object) As String
        Dim sAppo As String

        If vTesto Is Nothing Then
            StringWhere = "Null"
        Else
            sAppo = CStr(vTesto)
            sAppo = Replace(sAppo, "'", "''")
            StringWhere = "'" & sAppo & "'"
        End If
    End Function

    Public Function InsertString(ByVal vTesto As Object) As String
        Dim sAppo As String

        If String.IsNullOrEmpty(vTesto) Then
            InsertString = "Null"
        Else
            If vTesto = "" Then
                InsertString = "Null"
            Else
                sAppo = CStr(vTesto)
                sAppo = Replace(sAppo, "'", "''")
                InsertString = "'" & sAppo & "'"
            End If
        End If
    End Function

    Public Function InsertBit(ByVal bValore As Boolean) As String
        InsertBit = Trim$(IIf((bValore = False), 0, 1))
    End Function

    Public Function InsertData(ByVal vValore As Object, _
                               Optional ByVal iTipoDataSql As Integer = 0) As String
        If myIsDate(vValore) Then
            If iTipoDataSql = 0 Then
                iTipoDataSql = 121
            End If
            InsertData = "convert(datetime, '" & _
                         Replace(Format$(FormattaData_Db2Db(vValore, ""), "yyyy-MM-dd HH.mm.ss"), ".", ":") & ".000', " & iTipoDataSql & ")"
        Else
            InsertData = "null"
        End If
    End Function

    Public Function InsertInteger(ByVal vNumero As Object) As String

        If String.IsNullOrEmpty(vNumero) Then
            InsertInteger = "Null"
        Else
            InsertInteger = Trim$(vNumero)
        End If
    End Function

    Public Function InsertFloat(ByVal vNumero As Object) As String
        If String.IsNullOrEmpty(vNumero) Then
            InsertFloat = "Null"
        Else
            InsertFloat = Replace(Trim$(vNumero), ",", ".")
        End If
    End Function

    Public Function DateWhere(ByVal vValore As Object, _
                              Optional ByVal iTipoDataSql As Integer = 0) As String
        If myIsDate(vValore) Then
            If iTipoDataSql = 0 Then
                iTipoDataSql = 121
            End If
            DateWhere = "convert(datetime, '" & _
                            Replace(Format$(vValore, "yyyy-MM-dd HH.mm.ss"), ".", ":") & ".000', " & iTipoDataSql & ")"
        Else
            DateWhere = "null"
        End If
    End Function

    Public Function fnBetweenData(ByVal sNomeCampo As String, _
                                  ByVal vDataIni As Object, _
                                  ByVal vDataFine As Object) As String
        Dim vDataIniN As Date, vDataFineN As Date

        If myIsDate(vDataIni) And myIsDate(vDataFine) Then
            vDataIniN = CDate(vDataIni)
            vDataFineN = CDate(vDataFine)
            vDataFineN = DateAdd(DateInterval.Day, 1, vDataFineN)
            fnBetweenData = "(    " & fnWhere(sNomeCampo, enum_Confronto.CLAUSE_GREATERTHANOREQUAL, vDataIniN, False) & _
                            SQL_AND & fnWhere(sNomeCampo, enum_Confronto.CLAUSE_LESSTHAN, vDataFineN, False) & ")"

        ElseIf myIsDate(vDataIni) Then
            fnBetweenData = fnWhere(sNomeCampo, enum_Confronto.CLAUSE_GREATERTHANOREQUAL, vDataIni, False)

        ElseIf myIsDate(vDataFine) Then
            vDataFineN = CDate(vDataFine)
            vDataFineN = DateAdd(DateInterval.Day, 1, vDataFineN)
            fnBetweenData = fnWhere(sNomeCampo, enum_Confronto.CLAUSE_LESSTHAN, vDataFineN)

        Else
            fnBetweenData = ""
        End If
    End Function

    Public Function fnBetweenDataOra(ByVal sNomeCampo As String, _
                                     ByVal vDataIni As DateTime, _
                                     ByVal vDataFine As DateTime) As String
        Dim vDataIniN As DateTime, vDataFineN As DateTime

        vDataIniN = CDate(vDataIni)
        vDataFineN = CDate(vDataFine)
        fnBetweenDataOra = "(" & fnWhere(sNomeCampo, enum_Confronto.CLAUSE_GREATERTHANOREQUAL, vDataIniN, False) & SQL_AND & _
                                 fnWhere(sNomeCampo, enum_Confronto.CLAUSE_LESSTHANOREQUAL, vDataFineN, False) & ")"
    End Function

    Public Function Single2StringDecimal(ByVal rNumero As Single) As String
        Single2StringDecimal = Replace(Trim$(rNumero), ",", ".")
    End Function

    Public Function getConnectionString(ByVal sUserID As String, _
                                        ByVal sPassword As String, _
                                        ByVal sDataSource As String, _
                                        ByVal sInitialCatalog As String) As String

        getConnectionString = "Password=" & sPassword & ";" & _
                              "User ID=" & sUserID & ";" & _
                              "Initial Catalog=" & sInitialCatalog & ";" & _
                              "Data Source=" & sDataSource & ";" & _
                              "MultipleActiveResultsets=True"
    End Function

    Public Function ServerSMS_ApriConnessione(ByRef cnServer As SqlClient.SqlConnection) As Boolean

        ServerSMS_ApriConnessione = False
        Try
            cnServer = New System.Data.SqlClient.SqlConnection
            cnServer.ConnectionString = getConnectionString(g_tyApplicazione.sUtenteSA, _
                                                            g_tyApplicazione.sPswSA, _
                                                            g_tyApplicazione.sDataSource, _
                                                            g_tyApplicazione.sInitialCatalog)
            cnServer.Open()
            ServerSMS_ApriConnessione = True

        Catch ex As SqlClient.SqlException
            'MsgBox(ex.Message)
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Function

    Public Function ServerSMS_ChiudiConnessione(ByRef cnServer As SqlClient.SqlConnection) As Boolean
        Try
            cnServer.Close()
            cnServer.Dispose()
            cnServer = Nothing
            ServerSMS_ChiudiConnessione = True
        Catch ex As Exception
            ServerSMS_ChiudiConnessione = False
        End Try
    End Function

    Public Sub Chiudi_Rs_DataReader(ByRef rsRead As SqlClient.SqlDataReader, _
                                    ByRef rsCmd As SqlClient.SqlCommand)
        Try
            rsRead.Close()
            rsCmd.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Public Function Apri_Rs_DataSet(ByRef cnServer As SqlClient.SqlConnection, _
                                    ByRef rsDa As SqlClient.SqlDataAdapter, _
                                    ByRef rsDS As DataSet, _
                                    ByVal sSql As String) As String

        Apri_Rs_DataSet = "Apri_Rs_DataSet - "
        'Try
        rsDa = New SqlClient.SqlDataAdapter(sSql, cnServer)
        rsDS = New DataSet()
        rsDa.Fill(rsDS)
        Apri_Rs_DataSet = ""

        'Catch ex As SqlClient.SqlException
        '    Apri_Rs_DataSet = Apri_Rs_DataSet & "SqlException: " & ex.Message

        'Catch ex As Exception
        '    Apri_Rs_DataSet = Apri_Rs_DataSet & "Exception: " & ex.Message
        'End Try
    End Function

    Public Sub Chiudi_Rs_DataSet(ByVal rsDataSet As DataSet, _
                                 ByVal rsDataAdapter As SqlClient.SqlDataAdapter)
        Try
            rsDataSet.Dispose()
            rsDataAdapter.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetColumnWidth(ByRef myCn As SqlClient.SqlConnection, _
                                   ByVal sTabella As String, _
                                   ByVal sCampo As String) As String
        Dim sAppo As String
        Dim schema As String = "dbo"

        Dim restrictions() As String = {g_tyApplicazione.sInitialCatalog, schema, sTabella, sCampo}
        Dim schemaInfo As DataTable = myCn.GetSchema("COLUMNS", restrictions)
        Dim colWidth As Integer = schemaInfo.Rows(0)("CHARACTER_MAXIMUM_LENGTH")
        sAppo = colWidth.ToString()
        GetColumnWidth = sAppo
    End Function

    Public Sub Parameter_Aggiungi(ByVal sNome As String, _
                                  ByVal vTipo As SqlDbType, _
                                  ByVal vValore As Object, _
                                  ByRef cdParam As SqlClient.SqlParameter)

        Select Case vTipo
            Case SqlDbType.VarChar, SqlDbType.Char  'adVarChar, adLongVarChar
                If vValore = "" Then
                    cdParam = New SqlClient.SqlParameter(sNome, vTipo, 1)
                    cdParam.Value = DBNull.Value
                Else
                    cdParam = New SqlClient.SqlParameter(sNome, vTipo, Len(vValore))
                    cdParam.Value = CStr(vValore)
                End If

            Case SqlDbType.Int, SqlDbType.SmallInt
                cdParam = New SqlClient.SqlParameter(sNome, vTipo)
                cdParam.Value = CInt(vValore)

            Case SqlDbType.BigInt  'adLong
                cdParam = New SqlClient.SqlParameter(sNome, vTipo)
                cdParam.Value = CLng(vValore)

            Case SqlDbType.Date, SqlDbType.DateTime, SqlDbType.SmallDateTime  'adDBTimeStamp '** SmallDateTime
                If IsDate(vValore) Then
                    cdParam = New SqlClient.SqlParameter(sNome, vTipo)
                    cdParam.Value = CDate(vValore)
                Else
                    cdParam = New SqlClient.SqlParameter(sNome, vTipo)
                    cdParam.Value = DBNull.Value
                End If

            Case SqlDbType.Bit 'adBoolean
                cdParam = New SqlClient.SqlParameter(sNome, vTipo)
                cdParam.Value = IIf(vValore, 1, 0)

            Case SqlDbType.TinyInt
                cdParam = New SqlClient.SqlParameter(sNome, vTipo)
                cdParam.Value = CInt(vValore)

            Case SqlDbType.SmallMoney, SqlDbType.Money
                cdParam = New SqlClient.SqlParameter(sNome, vTipo)
                cdParam.Value = vValore

            Case SqlDbType.Decimal
                cdParam = New SqlClient.SqlParameter(sNome, vTipo)
                cdParam.Value = CDbl(vValore)
                cdParam.Scale = 4
                cdParam.Precision = 18

            Case SqlDbType.Float
                cdParam = New SqlClient.SqlParameter(sNome, vTipo, 8)
                cdParam.Value = CDbl(vValore)

            Case Else
                'MsgBox("ERR")
        End Select
    End Sub

    Public Function Esegui_Sp_Command(ByRef myCn As SqlClient.SqlConnection, _
                                      ByRef cd As SqlClient.SqlCommand, _
                                      ByVal sNomeSP As String, _
                                      ByRef arrParam() As SqlClient.SqlParameter) As String
        Dim iParam As Integer

        Esegui_Sp_Command = "ERR"
        Try
1:
            cd = New SqlClient.SqlCommand
            cd = myCn.CreateCommand()
            cd.CommandType = CommandType.StoredProcedure
            cd.CommandText = sNomeSP
            For iParam = 0 To UBound(arrParam)
                cd.Parameters.Add(arrParam(iParam))
            Next
            cd.ExecuteNonQuery()
            Esegui_Sp_Command = ""

        Catch ex As Exception
            'Call MsgErroreStoredProcedure(myCn, sNomeSP, 0, ex.Message)
        End Try
    End Function

    Public Function getDataOra(ByRef myCnAppo As SqlClient.SqlConnection) As DateTime
        Dim sSql As String
        Dim rs_Cmd As SqlClient.SqlCommand, rs_Read As SqlClient.SqlDataReader

        sSql = "SELECT getdate() as DataOra"
        rs_Cmd = New SqlClient.SqlCommand
        rs_Cmd = myCnAppo.CreateCommand()
        rs_Cmd.CommandText = sSql
        rs_Read = rs_Cmd.ExecuteReader()
        If rs_Read.HasRows Then
            rs_Read.Read()
            getDataOra = rs_Read("DataOra")
        Else
            getDataOra = #1/1/1753#
        End If

        Call Chiudi_Rs_DataReader(rs_Read, rs_Cmd)
    End Function

End Module
