<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMS.Master" CodeBehind="DettaglioSpedizione.aspx.vb" Inherits="ServerSMS.DettaglioSpedizione" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function SelezDeselezTutto() {
            elmTutto = document.getElementById('<%= chkTuttoRiattiva.ClientID %>');
            arrayControlli = document.getElementsByTagName('INPUT');
            for (i = 0; i < arrayControlli.length; i++) {
                sAppo = arrayControlli[i].name;
                if (sAppo.indexOf('chkRiattiva') > -1) {
                    arrayControlli[i].checked = elmTutto.checked;
                }
            }
        }

        function ListaSelezionati() {
            iContaSMS = 0;
            sListaSelez = '';
            arrayControlli = document.getElementsByTagName('INPUT');
            for (i = 0; i < arrayControlli.length; i++) {
                sAppo = arrayControlli[i].name;
                if (sAppo.indexOf('chkRiattiva') > -1) {
                    if (arrayControlli[i].checked == true) {
                        iContaSMS = iContaSMS + 1;
                        sListaSelez = sListaSelez + sAppo.substr(sAppo.indexOf('chkRiattiva'), 30) + '|';
                    }
                }
            }
            elmRiattivaID = document.getElementById('<%= hhRiattivaID.ClientID %>');
            elmRiattivaID.value = sListaSelez;
            if (sListaSelez == '') {
                alert('Nessun SMS selezionato per la riattivazione !');
                return false;
            }
            else {
                return window.confirm('Verrano riattivati ' + iContaSMS + ' SMS. \n \n Proseguire ?');
            }
        }
    </script>
    <asp:HiddenField ID="hhIDSpedizione" runat="server" />
    <asp:HiddenField ID="hhIDAssoAvis" runat="server" />
    <center>
    <table style="width:70%;" class="tblStandard" border="1">
        <tr>
            <td class="tdIntestazione" align="left">Dati della spedizione:</td>
        </tr>
        <tr>
            <td align="left"><table style="width:100%;" class="tblStandard" border="0">
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;Data e ora creazione:&nbsp;<asp:Label ID="lblCreazione" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;Data e ora accettazione:&nbsp;<asp:Label ID="lblAccettazione" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;Totale SMS:&nbsp;<asp:Label ID="lblTotSMS" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;Totale SMS inviati correttamente:&nbsp;<asp:Label ID="lblTotOK" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;Totale SMS con errori:&nbsp;<asp:Label ID="lblTotErrori" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table></td>
        </tr>
    </table> 
    <asp:Label ID="lblErrore" runat="server" Text=""></asp:Label>
    </center>
    <br />
    <asp:Table ID="tblSpedizioni" runat="server" Width="100%" BorderStyle="Solid" 
        CssClass="tblStandard" BorderWidth="1" BorderColor="Black">
        <asp:TableHeaderRow ID="trIntestazione">
            <asp:TableHeaderCell CssClass="thIntestazione" Text="&nbsp;"></asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="thIntestazione" Text="ID AssoAvis" Width="90px"></asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="thIntestazione" Text="Data Spedizione" Width="125px"></asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="thIntestazione" Text="Esito" Width="140px"></asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="thIntestazione" Text="Testo"></asp:TableHeaderCell>
            <asp:TableHeaderCell CssClass="thIntestazione" Text="Spedizione" Width="70px"></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="tdRiattiva" CssClass="thIntestazione" Text="Riattiva">
                <asp:CheckBox ID="chkTuttoRiattiva" runat="server" onClick="SelezDeselezTutto();" /></asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <br />
    <table class="tblStandardNoWidth" width="70%"><tr>
        <td valign="top" align="center" width="50%"><asp:PlaceHolder ID="plhRiattivazione" runat="server">
                <asp:LinkButton ID="lkRiattiva" runat="server" OnClientClick="return ListaSelezionati();">Riattiva SMS selezionati</asp:LinkButton>
            </asp:PlaceHolder>
            <asp:HiddenField ID="hhRiattivaID" runat="server" />
        </td>
        <td valign="top" align="center" width="50%">
            <asp:Panel ID="pnlIndietro" runat="server">
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="~/SMS_Private/CercaSpedizioni.aspx">Indietro</asp:HyperLink>    
            </asp:Panel>        
        </td>
    </tr></table>






</asp:Content>


