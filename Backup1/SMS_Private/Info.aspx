<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SMS.Master" CodeBehind="Info.aspx.vb" Inherits="ServerSMS.Info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Servizio SMS per AssoAvis5" 
        Font-Bold="True" Font-Size="14pt"></asp:Label>
    <br />
    
    <p style="text-align: justify">
        Tramite il servizio di SMS fornito da Me.S.I.S. srl è possibile inviare SMS direttamente tramite ASSOAVIS 5 a singoli 
        donatori o a gruppi di donatori. Il manuale di ASSOAVIS 5 descrive come generare i modelli di SMS e come sfruttare al 
        meglio il servizio.
    </p>
<p style="text-align: justify">
        Il servizio di invio SMS non è effettuato direttamente dalla MeSIS ma vengono 
        utilizzati alcuni provider presenti nel territorio nazionale quali: Aruba, 
        Aimon, ProgettoSMS, Skebby. Ogni richiesta di invio SMS effettuata dal cliente 
        viene smistata verso uno o più provider in base alla disponibilità, al tipo di 
        servizio, a politiche organizzative della MeSIS.</p>
    <p style="text-align: justify">
        Per usufruire del servizio di invio SMS Me.S.I.S. si deve essere in possesso di contratto di 
        assistenza su ASSOAVIS 5 e si devono acquistare i CREDITI. 
    </p>
    <p style="text-align: justify">
        Il costo degli SMS varia in base alla modalità di invio:<br />
        - La modalità standard (più economica) prevede messaggi di massimo 160 caratteri ad un costo di 4 CREDITI. Non può 
        essere scelto il mittente del messaggio.<br />
        - La modalità "con mittente", prevede messaggi di massimo 160 caratteri ad un costo di 6 crediti. <br />
        Questa modalità permette di specificare un mittente (massimo 11 caratteri)<br />
        - La modalità "con conferma di ricezione", prevede messaggi di massimo 160 caratteri ad un costo di 7 CREDITI. <br />
        Questa modalità oltre a permettere di specificare un mittente (massimo 11 caratteri) permette di verificare sul sito MeSIS 
        l'effettiva consegna del messaggio.
    </p>
    <p style="text-align: justify">
        Può essere scelta una modalità di invio predefinita per ogni modello di SMS generato, si possono anche variare le 
        modalità di invio per ogni singolo SMS. Un blocco (pacchetto) di SMS inviati può contenere SMS con modalità diverse 
        di invio.
    </p>
    <p style="text-align: justify">
        La Me.S.I.S utilizza vari provider per inviare gli SMS, di norma l'addebito dell'invio dell'SMS viene effettuato anche se il 
        numero di cellulare del destinatario è sbagliato, anomalo o inesistente. Per ridurre eventuali addebiti inutili in ASSOAVIS 
        5 è previsto un pre-controllo del numero di cellulare. Verranno inviati solo gli SMS a numeri di cellulare che contengono 
        solamente da un minimo di 9 ad un massimo di 11 caratteri numerici con primo carattere diverso da 0. Devono essere 
        assenti nei numeri di cellulare: spazi, prefissi internazionali, barre, o altri caratteri non numerici.<br />
        Gli SMS inviati a numeri che non hanno le caratteristiche precedentemente indicate rimangono nella cartella "in uscita" 
        di ASSOAVIS senza essere inviati.  
    </p>
    <p style="text-align: justify">
        Per permettere una verifica ed una pulizia dei numeri presenti in ASSOAVIS 5 è stata implementata una funzionalità di 
        "Verifica numeri" (HLR). Questa nuova funzionalità permette di verificare, tramite le funzionalità messe a disposizione 
        degli operatori telefonici, che il numero telefonico sia attivo. Ovviamente non si può verificare che appartenga ad uno 
        specifico individuo ma sicuramente permette di evidenziare numeri inesistenti o disattivati. La nuova funzionalità ha il 
        costo di 2 CREDITI per ogni numero di telefono verificato.
    </p>
    <div style="text-align: center"><b>-- ATTENZIONE --</b></div>
    <div style="text-align: justify">
        Si segnala che alcuni caratteri potrebbero non essere gestiti per l'invio di SMS, in questi casi il carattere viene sostituito con uno spazio o convertito in un carattere equivalente. Per alcuni caratteri particolari il carattere sostituito potrebbe essere conteggiato all'interno del messaggio come carattere doppio.<br />
        Si indica che in linea di massima tutti i caratteri numerici, alfabetici senza accento e la punteggiatura standard vengono normalmente accettati.<br />
        I seguenti caratteri [ \ ] ^ { | } ~ potrebbero essere inviati in base al provider di invio ma essere conteggiati come doppio carattere.<br />
        Il simbolo dell'euro verrà convertito in E.<br />
        Le lettere accentate non dovrebbero essere utilizzate,in caso di utilizzo non se ne garantisce la corretta visualizzazione.
    </div><br /><br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="~/SMS_Private/CercaSpedizioni.aspx">Indietro</asp:HyperLink>
</asp:Content>
