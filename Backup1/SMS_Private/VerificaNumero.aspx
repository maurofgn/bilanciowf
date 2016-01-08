<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMS.Master" CodeBehind="VerificaNumero.aspx.vb" Inherits="ServerSMS.VerificaNumero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<center>
    <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Size="14pt">Verifica Numeri</asp:Label>
    <br /><br /><br /><br /><br />
    Inserire uno o piu' numeri da verificare (separando i numeri con la virgola), il numero va indicato senza prefisso internazionale, sono ammessi solo caratteri numerici. 
    <br /><br />
    <b>Il servizio scalera' 2 crediti per ogni numero inserito.</b> 
    <br /><br /><br /><br />
    Numero Cellulare:&nbsp;<asp:TextBox ID="txtListaNumeri" runat="server" Width="400px"></asp:TextBox>
    <br /><br /><br /><br />
    <asp:Label ID="lblErrore" runat="server" CssClass="failureNotification"></asp:Label>
    <br /><br />
    <asp:LinkButton ID="cmdCercaVerifiche" runat="server">Richiedi verifica numeri</asp:LinkButton>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/SMS_Private/CercaSpedizioni.aspx">Annulla</asp:HyperLink>    
</center>
</asp:Content>
