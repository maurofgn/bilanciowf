Module mSMS

    Public Enum enum_Notifica_Invio_Pacchetti
        e_Notifica_Sempre = 0 'Invia sempre le notifiche di spedizione
        e_Notifica_SoloErrori = 1 'Invia le notifiche solo se il pacchetto contiene errori
        e_Notifica_CreditiInsufficienti = 2 'Invia la notifica solo in caso di NoCreditiMeSIS
    End Enum

    Public Enum enum_Verifiche_StatoInivio
        VER_Da_Inviare = 0
        VER_In_Invio = 1
        VER_Inviato = 2
        VER_RicevutoReport = 3
        VER_Errore = 4
    End Enum

    Public Structure tyUtente
        Dim sUtente As String
        Dim sCodiceAvis As String
        Dim sUltimoAccesso As String
        Dim lCreditiResidui As Long
        Dim iGG_View_Errori As Integer
        Dim iGG_Storico_Verifica_Num As Integer
        Dim iNotifica_Invio_Pacchetti As Integer
    End Structure

    Public Structure tyCercaSpedizioni
        Dim bCercaSMS As Boolean
        Dim bEvasa As Boolean
        Dim bErrori As Boolean
        Dim sDallaData As String
        Dim sAllaData As String
    End Structure

    Public Function tyUtente_Azzera() As tyUtente
        Dim myUtenteAppo As New tyUtente
        tyUtente_Azzera = myUtenteAppo
    End Function

    Public Function tyCercaSpedizioni_Azzera(ByVal iGgErrori As Integer, _
                                             Optional ByVal bCercaSMS As Boolean = True) As tyCercaSpedizioni

        Dim myCercaSpedAppo As New tyCercaSpedizioni

        With myCercaSpedAppo
            .bCercaSMS = bCercaSMS
            If bCercaSMS = True Then
                .bEvasa = False
                .bErrori = True
            Else
                .bEvasa = False
                .bErrori = False
            End If
            .sDallaData = FormattaData_2Str(DateAdd(DateInterval.Day, (-iGgErrori), Now()), "", FORMATO_DATA)
            .sAllaData = FormattaData_2Str(Now(), "", FORMATO_DATA)
        End With
        tyCercaSpedizioni_Azzera = myCercaSpedAppo
    End Function

    Public Sub FaiAccesso(ByVal sUtente As String, _
                          ByVal sPassword As String, _
                          ByRef sErrore As String, _
                          ByRef myTyUtente As tyUtente, _
                          Optional ByVal bImpostaData As Boolean = True, _
                          Optional ByVal bControlloPsw As Boolean = True)

        Dim myCn As SqlClient.SqlConnection = Nothing
        Dim rsCmdEx As SqlClient.SqlCommand
        Dim rsDa_Utente As SqlClient.SqlDataAdapter = Nothing, rsDS_Utente As DataSet = Nothing
        Dim sSql As String

        myTyUtente = tyUtente_Azzera()

        If ServerSMS_ApriConnessione(myCn) Then
            sSql = " SELECT * " & _
                   " FROM Utenti" & _
                   " WHERE " & mSQL.fnWhere("Utente", enum_Confronto.CLAUSE_EQUALS, sUtente)
            If Apri_Rs_DataSet(myCn, rsDa_Utente, rsDS_Utente, sSql) = "" Then
                If (rsDS_Utente.Tables(0).Rows.Count) > 0 Then
                    myTyUtente.sUtente = rsDS_Utente.Tables(0).Rows(0)("Utente")

                    'If bControlloPsw And ((CStr(rsDS_Utente.Tables(0).Rows(0)("Password")) <> CStr(sPassword)) Or (CStr(rsDS_Utente.Tables(0).Rows(0)("Utente")) <> CStr(sUtente))) Then
                    If bControlloPsw And ((CStr(rsDS_Utente.Tables(0).Rows(0)("Password")) <> CStr(sPassword))) Then
                        sErrore = "Nome utente e/o password errati ! <br> verificare il corretto inserimento di minuscole, maiuscole e numeri."

                    Else
                        myTyUtente.sCodiceAvis = rsDS_Utente.Tables(0).Rows(0)("Codice_Avis")
                        myTyUtente.sUltimoAccesso = FormattaData_2Str(rsDS_Utente.Tables(0).Rows(0)("Ultimo_Accesso"), "", FORMATO_DATA1)
                        myTyUtente.lCreditiResidui = 0
                        myTyUtente.iGG_View_Errori = rsDS_Utente.Tables(0).Rows(0)("GG_View_Errori")
                        myTyUtente.iGG_Storico_Verifica_Num = rsDS_Utente.Tables(0).Rows(0)("GG_Storico_Verifica_Num")
                        myTyUtente.iNotifica_Invio_Pacchetti = rsDS_Utente.Tables(0).Rows(0)("Notifica_Invio_Pacchetti")

                        Call Chiudi_Rs_DataSet(rsDS_Utente, rsDa_Utente)

                        If bImpostaData Then
                            sSql = " UPDATE [Utenti]" & _
                                   " SET Ultimo_Accesso = GetDate()" & _
                                   " WHERE " & mSQL.fnWhere("Utente", enum_Confronto.CLAUSE_EQUALS, sUtente)
                            rsCmdEx = New SqlClient.SqlCommand
                            rsCmdEx = myCn.CreateCommand()
                            rsCmdEx.CommandText = sSql
                            rsCmdEx.ExecuteNonQuery()
                            rsCmdEx.Dispose()
                        End If

                    End If
                Else
                    sErrore = "Nome utente e/o password errati !! <br> verificare il corretto inserimento di minuscole, maiuscole e numeri."
                End If
            Else
                sErrore = "Errore SQL, impossibile proseguire ! "
            End If
            Call Chiudi_Rs_DataSet(rsDS_Utente, rsDa_Utente)
        Else
            sErrore = "Errore di connessione, impossibile proseguire ! "
        End If
        Call ServerSMS_ChiudiConnessione(myCn)
    End Sub

    Public Function GetCreditiResidui(ByVal sCodiceAvis As String, _
                                      ByRef iCreditiResidui As Integer, _
                                      ByRef sRiepilogo As String) As Boolean

        Dim myCn As SqlClient.SqlConnection = Nothing
        Dim rsDa_Crediti As SqlClient.SqlDataAdapter = Nothing, rsDS_Crediti As DataSet = Nothing
        Dim sSql As String
        Dim iStandard As Integer, iConMitt As Integer, iRicevuta As Integer, iNumVerifiche As Integer

        GetCreditiResidui = False
        iCreditiResidui = 0
        iStandard = 0
        iConMitt = 0
        iRicevuta = 0
        iNumVerifiche = 0

        If ServerSMS_ApriConnessione(myCn) Then
            sSql = " SELECT Codice_Avis, SUM(crediti_residui) AS TotResidui " & _
                   " FROM CREDITI " & _
                   " WHERE (dateadd(day,1,data_scadenza) > getdate())" & _
                     SQL_AND & mSQL.fnWhere("Codice_Avis", enum_Confronto.CLAUSE_EQUALS, sCodiceAvis) & _
                   " GROUP BY Codice_Avis"
            If Apri_Rs_DataSet(myCn, rsDa_Crediti, rsDS_Crediti, sSql) = "" Then
                If rsDS_Crediti.Tables(0).Rows.Count > 0 Then
                    iCreditiResidui = rsDS_Crediti.Tables(0).Rows(0)("TotResidui")

                    iStandard = (iCreditiResidui \ 4)
                    iConMitt = (iCreditiResidui \ 6)
                    iRicevuta = (iCreditiResidui \ 7)
                    iNumVerifiche = (iCreditiResidui \ 2)

                    GetCreditiResidui = True
                End If
            End If
            Call Chiudi_Rs_DataSet(rsDS_Crediti, rsDa_Crediti)
        End If
        Call ServerSMS_ChiudiConnessione(myCn)

        sRiepilogo = "Puoi inviare: " & iStandard & " SMS standard " & _
                     "o " & iConMitt & " SMS con mittente " & _
                     "o " & iRicevuta & " SMS con mittente+ricevuta " & _
                     "o " & iNumVerifiche & " verifiche numeri"
    End Function

    'Public Function InsVerificaNumeri(ByVal sCodiceAvis As String, _
    '                                  ByVal iGG_Storico_Verifica_Num As Integer, _
    '                                  ByVal sTelefono As String) As Long

    '    Dim myCn As SqlClient.SqlConnection = Nothing
    '    Dim rsDa_verifica As SqlClient.SqlDataAdapter = Nothing, rsDs_Verifica As DataSet = Nothing
    '    Dim rsCmdEx As SqlClient.SqlCommand
    '    Dim sSql As String
    '    Dim lAppoID As Long
    '    Dim bDatiEsiste As Boolean

    '    lAppoID = 0

    '    If ServerSMS_ApriConnessione(myCn) Then
    '        bDatiEsiste = False
    '        sSql = " SELECT *, DATEDIFF(DAY,Data_Ricezione_AVIS, GetDate()) AS GgDiff " & _
    '               " FROM Verifica_Numeri " & _
    '               " WHERE " & mSQL.fnWhere("Verifica_Numeri.Codice_Avis", enum_Confronto.CLAUSE_EQUALS, sCodiceAvis) & _
    '                 SQL_AND & mSQL.fnWhere("Verifica_Numeri.Telefono", enum_Confronto.CLAUSE_EQUALS, sTelefono)
    '        If Apri_Rs_DataSet(myCn, rsDa_verifica, rsDs_Verifica, sSql) = "" Then
    '            If rsDs_Verifica.Tables(0).Rows.Count > 0 Then
    '                If rsDs_Verifica.Tables(0).Rows(0)("GgDiff") <= iGG_Storico_Verifica_Num Then
    '                    lAppoID = rsDs_Verifica.Tables(0).Rows(0)("ID")
    '                    bDatiEsiste = True
    '                End If
    '            End If
    '        End If
    '        Call Chiudi_Rs_DataSet(rsDs_Verifica, rsDa_verifica)
    '        If bDatiEsiste = False Then

    '            '???? se non ci sono crediti ?????

    '            sSql = " INSERT INTO Verifica_Numeri (Codice_Avis, Telefono, Data_Ricezione_AVIS)  VALUES (" & _
    '                        "  " & mSQL.InsertString(sCodiceAvis) & _
    '                        ", " & mSQL.InsertString(sTelefono) & _
    '                        ", GetDate()" & _
    '                        "); SELECT SCOPE_IDENTITY() AS 'Ultimo_ID' "
    '            rsCmdEx = New SqlClient.SqlCommand
    '            rsCmdEx = myCn.CreateCommand()
    '            rsCmdEx.CommandText = sSql
    '            lAppoID = rsCmdEx.ExecuteScalar()
    '            rsCmdEx.Dispose()
    '        End If
    '    End If
    '    Call ServerSMS_ChiudiConnessione(myCn)
    '    InsVerificaNumeri = lAppoID
    'End Function

    'Public Sub GetEsitoVerificaNumeri(ByVal sCodiceAvis As String, _
    '                                  ByVal lIDVerifica As Long, _
    '                                  ByVal sTelefono As String, _
    '                                  ByRef sOutDati As String)

    '    Dim myCn As SqlClient.SqlConnection = Nothing
    '    Dim rsDa_verifica As SqlClient.SqlDataAdapter = Nothing, rsDs_Verifica As DataSet = Nothing
    '    Dim sSql As String

    '    sOutDati = ""
    '    If ServerSMS_ApriConnessione(myCn) Then

    '        'Stato_Descr = 1 => Da verificare
    '        'Stato_Descr = 2 => Da ricevere esito o provider non lo invierà per errore nella richiesta
    '        'Stato_Descr = 3 => Errori_Provider o Errore in MeSIS_Server_SMS
    '        'Stato_Descr = 4 => OK
    '        'Stato_Descr = 5 => ErroreNumero

    '        sSql = " SELECT Verifica_Numeri.ID, Verifica_Numeri.Codice_Avis, Verifica_Numeri.Telefono" & _
    '               ", Verifica_Numeri.Data_Ricezione_AVIS, Verifica_Numeri.Data_Invio_Provider" & _
    '               ", Verifica_Numeri_Esito.ID_Provider, Verifica_Numeri_Esito.Telefono As EsitoTelefono" & _
    '               ", Verifica_Numeri_Esito.Codice_Nazione_Operatore" & _
    '               ", Verifica_Numeri_Esito.Codice_Operatore, Verifica_Numeri_Esito.Nome_Operatore" & _
    '               ", CASE IsNull(Verifica_Numeri.Data_Invio_Provider, '') " & _
    '               "     WHEN '' THEN '1 | Da verificare' " & _
    '               "     ELSE CASE Verifica_Numeri.Esito_MeSIS " & _
    '               "             WHEN 'InvioCorretto' THEN " & _
    '               "                      CASE IsNull(Verifica_Numeri_Esito.Stato, '') " & _
    '               "                         WHEN '' THEN '2 | Da ricevere esito' " & _
    '               "                         ELSE Verifica_Numeri_Esito.Valore_AssoAvis + ' | ' + PopUpEsitoVerifica.Descrizione_WEB" & _
    '               "                      END " & _
    '               "             ELSE '3 | '+ (SELECT IsNull(Errori_Provider.Descrizione_WEB, '') " & _
    '               "                           FROM Errori_Provider " & _
    '               "                           WHERE Errori_Provider.Codice_Errore_provider = Verifica_Numeri.Esito_MeSIS) " & _
    '               "          END " & _
    '               "  END AS Stato_Descr" & _
    '               " FROM Verifica_Numeri " & _
    '               "   LEFT JOIN Verifica_Numeri_Esito ON Verifica_Numeri_Esito.Telefono = '39'+Verifica_Numeri.Telefono " & _
    '               "                                  AND Verifica_Numeri_Esito.ID_Provider = Verifica_Numeri.ID_Provider " & _
    '               "   LEFT JOIN (SELECT Provider_SMS, Valore, Descrizione_WEB " & _
    '               "              FROM PopUp " & _
    '               "              WHERE " & mSQL.fnWhere("TipoPopUp", enum_Confronto.CLAUSE_EQUALS, "EsitoVerificaNumeri") & _
    '               "             ) AS PopUpEsitoVerifica ON PopUpEsitoVerifica.Provider_SMS = SubString(IsNull(Verifica_Numeri_Esito.ID_Provider, ''), 1, 1) " & _
    '               "                                    AND PopUpEsitoVerifica.Valore = Verifica_Numeri_Esito.Stato " & _
    '               " WHERE " & mSQL.fnWhere("Verifica_Numeri.Codice_Avis", enum_Confronto.CLAUSE_EQUALS, sCodiceAvis)
    '        If sTelefono <> "" Then
    '            sSql = sSql & _
    '                   SQL_AND & mSQL.fnWhere("Verifica_Numeri.Telefono", enum_Confronto.CLAUSE_EQUALS, sTelefono)
    '        End If
    '        If lIDVerifica > 0 Then
    '            sSql = sSql & _
    '                   SQL_AND & mSQL.fnWhere("Verifica_Numeri.ID", enum_Confronto.CLAUSE_EQUALS, lIDVerifica)
    '        End If
    '        If Apri_Rs_DataSet(myCn, rsDa_verifica, rsDs_Verifica, sSql) = "" Then
    '            If rsDs_Verifica.Tables(0).Rows.Count > 0 Then
    '                sOutDati = "|<" & rsDs_Verifica.Tables(0).Rows(0)("Telefono") & ">|" & _
    '                           "|<" & FormattaData_2Str("" & rsDs_Verifica.Tables(0).Rows(0)("Data_Invio_Provider"), "", FORMATO_DATA2) & ">|" & _
    '                           "|<" & Trim(Mid(rsDs_Verifica.Tables(0).Rows(0)("Stato_Descr"), _
    '                                           1, InStr(rsDs_Verifica.Tables(0).Rows(0)("Stato_Descr"), "|") - 1)) & ">|" & _
    '                           "|<" & Trim(Mid(rsDs_Verifica.Tables(0).Rows(0)("Stato_Descr"), _
    '                                           InStr(rsDs_Verifica.Tables(0).Rows(0)("Stato_Descr"), "|") + 1)) & ">|"
    '            End If
    '        End If
    '        Call Chiudi_Rs_DataSet(rsDs_Verifica, rsDa_verifica)
    '    End If
    '    Call ServerSMS_ChiudiConnessione(myCn)
    'End Sub

    Public Function au_RiattivaSMS(ByRef myCn As SqlClient.SqlConnection, _
                                   ByVal lIdSpedizione As Long, _
                                   ByVal sCodiceAvis As String, _
                                   ByVal sUtente As String, _
                                   ByVal sListaID1 As String, _
                                   ByVal sListaID2 As String, _
                                   ByVal sListaID3 As String, _
                                   ByVal sListaID4 As String) As Long

        Dim cd As SqlClient.SqlCommand
        Dim pm(7) As SqlClient.SqlParameter
        Dim sErr As String

        au_RiattivaSMS = 0
        Try
            Call Parameter_Aggiungi("ID_Spedizione", SqlDbType.Int, lIdSpedizione, pm(0))
            Call Parameter_Aggiungi("Codice_Avis", SqlDbType.VarChar, sCodiceAvis, pm(1))
            Call Parameter_Aggiungi("Utente", SqlDbType.VarChar, sUtente, pm(2))
            Call Parameter_Aggiungi("ListaID1", SqlDbType.VarChar, sListaID1, pm(3))
            Call Parameter_Aggiungi("ListaID2", SqlDbType.VarChar, sListaID2, pm(4))
            Call Parameter_Aggiungi("ListaID3", SqlDbType.VarChar, sListaID3, pm(5))
            Call Parameter_Aggiungi("ListaID4", SqlDbType.VarChar, sListaID4, pm(6))

            pm(7) = New SqlClient.SqlParameter
            pm(7).ParameterName = "RetValue"
            pm(7).SqlDbType = SqlDbType.Int
            pm(7).Direction = ParameterDirection.Output

            sErr = Esegui_Sp_Command(myCn, cd, "[dbo].[au_RiattivaSMS]", pm)
            If sErr = "" Then
                au_RiattivaSMS = cd.Parameters("RetValue").Value
            End If
            cd.Parameters.Clear()
            cd.Dispose()
            cd = Nothing

        Catch ex As Exception
            'Call WriteLogSMS("au_RiattivaSMS", ex.Message)
        End Try
    End Function

    Public Function au_Spedizione_Verifiche_Crea(ByVal iIndex As Integer, _
                                                  ByRef myCn As SqlClient.SqlConnection, _
                                                  ByVal sCodiceAVIS As String, _
                                                  ByVal sIdentificativoSpedizione As String, _
                                                  ByVal dDataModificaFile As DateTime, _
                                                  ByVal iGG_StoricoVerificaNum As Integer) As Long
        Dim cd As SqlClient.SqlCommand
        Dim pm(4) As SqlClient.SqlParameter
        Dim sErr As String

        au_Spedizione_Verifiche_Crea = 0
        Try
            Call Parameter_Aggiungi("CodiceAvis", SqlDbType.VarChar, sCodiceAVIS, pm(0))
            Call Parameter_Aggiungi("IdentificativoSped", SqlDbType.VarChar, sIdentificativoSpedizione, pm(1))
            Call Parameter_Aggiungi("DataModificaFile", SqlDbType.DateTime, dDataModificaFile, pm(2))
            Call Parameter_Aggiungi("GG_StoricoVerificaNum", SqlDbType.SmallInt, iGG_StoricoVerificaNum, pm(3))

            pm(4) = New SqlClient.SqlParameter
            pm(4).ParameterName = "IdOut"
            pm(4).SqlDbType = SqlDbType.Int
            pm(4).Direction = ParameterDirection.Output

            sErr = Esegui_Sp_Command(myCn, cd, "[dbo].[au_Spedizione_Verifiche_Crea]", pm)
            If sErr = "" Then
                au_Spedizione_Verifiche_Crea = cd.Parameters("IdOut").Value
            End If
            cd.Parameters.Clear()
            cd.Dispose()
            cd = Nothing

        Catch ex As Exception
            'Call WriteLogSMS("au_Spedizione_Verifiche_Crea", ex.Message)
        End Try
    End Function

    Public Function au_ListaVerificheNumeri_Crea(ByVal iIndex As Integer, _
                                                  ByRef myCn As SqlClient.SqlConnection, _
                                                  ByVal lIdSpedizione As Long, _
                                                  ByVal sCellulare As String) As Long
        Dim cd As SqlClient.SqlCommand
        Dim pm(3) As SqlClient.SqlParameter
        Dim sErr As String

        au_ListaVerificheNumeri_Crea = 0
        Try
            Call Parameter_Aggiungi("IdSpedizione", SqlDbType.Int, lIdSpedizione, pm(0))
            Call Parameter_Aggiungi("Telefono", SqlDbType.VarChar, sCellulare, pm(1))
            Call Parameter_Aggiungi("Provider_SMS", SqlDbType.SmallInt, CInt(2), pm(2)) 'Provider_Aimon = 2

            pm(3) = New SqlClient.SqlParameter
            pm(3).ParameterName = "IDOUT"
            pm(3).SqlDbType = SqlDbType.Int
            pm(3).Direction = ParameterDirection.Output

            sErr = Esegui_Sp_Command(myCn, cd, "[dbo].[au_ListaVerificheNumeri_Crea]", pm)
            If sErr = "" Then
                au_ListaVerificheNumeri_Crea = cd.Parameters("IDOUT").Value
            End If
            cd.Parameters.Clear()
            cd.Dispose()
            cd = Nothing

        Catch ex As Exception
            'Call WriteLogSMS("au_ListaVerificheNumeri_Crea", ex.Message)
        End Try
    End Function
End Module
