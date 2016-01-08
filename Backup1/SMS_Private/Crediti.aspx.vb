Public Class Crediti
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call Riempi_Finestra()
        End If
    End Sub

    Private Sub Riempi_Finestra()
        Dim sRiepilogo As String = "", sSql As String, sFont As String
        Dim iCreditiResidui As Integer = 0
        Dim myCn As SqlClient.SqlConnection = Nothing
        Dim rsDa_Crediti As SqlClient.SqlDataAdapter = Nothing, rsDs_Crediti As DataSet = Nothing
        Dim myRiga As TableRow, row_Lista As DataRow

        Call GetCreditiResidui(CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis, _
                               iCreditiResidui, sRiepilogo)

        lblCreditiResidui.Text = iCreditiResidui
        lblRiepilogo.Text = sRiepilogo

        If ServerSMS_ApriConnessione(myCn) Then
            sSql = " SELECT *, dbo.fDataIta(Data_Attivazione) AS Data_Attivazione_Ita" & _
                   ", dbo.fDataIta(Data_Scadenza) AS Data_Scadenza_Ita, GetDate() AS DataCorrente " & _
                   " FROM CREDITI " & _
                   " WHERE " & mSQL.fnWhere("Codice_Avis", enum_Confronto.CLAUSE_EQUALS, CType(Session("MeSIS_SMS_myTyUtente"), tyUtente).sCodiceAvis) & _
                   " ORDER BY Data_Attivazione DESC "
            If Apri_Rs_DataSet(myCn, rsDa_Crediti, rsDS_Crediti, sSql) = "" Then
                For Each row_Lista In rsDs_Crediti.Tables(0).Rows
                    myRiga = New System.Web.UI.WebControls.TableRow
                    If (row_Lista("data_scadenza") < row_Lista("DataCorrente") And row_Lista("Crediti_residui") > 0) Then
                        sFont = "tdErrore"
                    ElseIf (row_Lista("data_scadenza") > row_Lista("DataCorrente") And row_Lista("Crediti_residui") > 0) Then
                        sFont = "tdAttivo"
                    Else
                        sFont = "tdStandard"
                    End If

                    Call Table_AggiungiColonna(myRiga, row_Lista("Data_Attivazione_Ita"), HorizontalAlign.Center, sFont)
                    Call Table_AggiungiColonna(myRiga, row_Lista("Data_Scadenza_Ita"), HorizontalAlign.Center, sFont)

                    If row_Lista("TipologiaCredito") = 1 Then
                        Call Table_AggiungiColonna(myRiga, "SMS: " & row_Lista("SMS_Acquistati"), HorizontalAlign.Right, sFont)
                        Call Table_AggiungiColonna(myRiga, "SMS: " & row_Lista("SMS_Residui"), HorizontalAlign.Right, sFont)
                    Else
                        Call Table_AggiungiColonna(myRiga, "" & row_Lista("Crediti_Acquistati"), HorizontalAlign.Right, sFont)
                        Call Table_AggiungiColonna(myRiga, "" & row_Lista("Crediti_Residui"), HorizontalAlign.Right, sFont)
                    End If

                    tblCrediti.Rows.Add(myRiga)
                Next
            End If
            Call Chiudi_Rs_DataSet(rsDs_Crediti, rsDa_Crediti)
        End If
        Call ServerSMS_ChiudiConnessione(myCn)
    End Sub

End Class