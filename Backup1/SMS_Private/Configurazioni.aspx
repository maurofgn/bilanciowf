<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMS.Master" CodeBehind="Configurazioni.aspx.vb" Inherits="ServerSMS.Configurazioni" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="Label1" runat="server" Text="Configura servizio SMS" 
        Font-Bold="True" Font-Size="14pt"></asp:Label>
    <br /><br /><br /><br />
    <table class="tblStandard">
        <tr>
            <td align="right"><asp:Label ID="lblGgViewErrori" runat="server" Text="Numero giorni predefiniti nella ricerca"></asp:Label>:&nbsp;</td>
            <td align="left"><asp:TextBox ID="txtGgViewErrori" runat="server" Width="80px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right">&nbsp;</td>
            <td align="left">&nbsp;</td>
        </tr>
        <tr>
            <td align="right"><asp:Label ID="lblGgStoricoVerificaNum" runat="server" Text="Intervallo di giorni prima di effettuare una nuova&nbsp;&nbsp;<br /> verifica di un numero cellulare già verificato"></asp:Label>:&nbsp;</td>
            <td align="left"><asp:TextBox ID="txtGgStoricoVerificaNum" runat="server" Width="80px"></asp:TextBox></td>
        </tr>
        <tr>
            <td align="right">&nbsp;</td>
            <td align="left">&nbsp;</td>
        </tr>
        <tr>
            <td align="right">Invio mail notifica pacchetti:&nbsp;</td>
            <td align="left">
                <asp:DropDownList ID="cboNotificaInvioPacchetti" runat="server">
                </asp:DropDownList>
            </td>
        </tr>    
    </table>
    <p align="center"><asp:Label ID="lblErrore" runat="server" Text="" CssClass="failureNotification"></asp:Label></p> 
    <asp:LinkButton ID="cmdSalva" runat="server">Salva</asp:LinkButton>
    <label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/SMS_Private/CercaSpedizioni.aspx">Annulla</asp:HyperLink>
</asp:Content>
