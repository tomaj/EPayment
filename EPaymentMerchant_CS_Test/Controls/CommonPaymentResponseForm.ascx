<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonPaymentResponseForm.ascx.cs" Inherits="Controls_CommonPaymentResponseForm" %>
<%@ Register Src="~/Controls/SLSPSporoPayRequestForm.ascx" TagName="SLSPSporoPayRequestForm" TagPrefix="uc" %>
<%@ Register Src="~/Controls/TBCardPayRequestForm.ascx" TagName="TBCardPayRequestForm" TagPrefix="uc" %>
<%@ Register Src="~/Controls/TBTatraPayRequestForm.ascx" TagName="TBTatraPayRequestForm" TagPrefix="uc" %>
<%@ Register Src="~/Controls/VUBEPlatbaRequestForm.ascx" TagName="VUBEPlatbaRequestForm" TagPrefix="uc" %>

<%--
  Copyright 2009 MONOGRAM Technologies
  
  This file is part of MONOGRAM EPayment libraries
  
  MONOGRAM EPayment libraries is free software: you can redistribute it and/or modify
  it under the terms of the GNU Lesser General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  MONOGRAM EPayment libraries is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public License
  along with MONOGRAM EPayment libraries.  If not, see <http://www.gnu.org/licenses/>.
--%>

Typ platby: <asp:Literal ID="lPaymentType" runat="server" /><br /><br />

Výsledok validácie: <asp:Literal ID="lValidationResult" runat="server" /><br />
Výsledok verifikácie integrity: <asp:Literal ID="lVerificationResult" runat="server" /><br />
<div>
  Nekryprovaný podpis správy: <asp:Literal ID="lSignatureBase" runat="server" /><br />
  Očakávaný podpis správy: <asp:Literal ID="lSignature" runat="server" /><br />
  Prijatý podpis správy: <asp:Literal ID="lReceivedSignature" runat="server" />
</div>
<br />
<asp:MultiView ID="mvPaymentInfo" runat="server">
  <asp:View runat="server" ID="vFailure">
    Prijatá správa nieje valídna alebo verifikovaná, preto je nutné ju ignorovať.
  </asp:View>
  <asp:View runat="server" ID="vResponseInfo">
    Výsledok platby: <asp:Literal ID="lPaymentResult" runat="server" /><br />
    Variabilný symbol: <asp:Literal ID="lVariableSymbol" runat="server" /><br />
    <asp:Repeater ID="rAdditionalInformation" runat="server">
      <ItemTemplate>
        <asp:Literal runat="server" Text="<%# ((System.Collections.Generic.KeyValuePair<string,string>)Container.DataItem).Key %>" />: <asp:Literal runat="server" Text="<%# ((System.Collections.Generic.KeyValuePair<string,string>)Container.DataItem).Value %>" /><br />
      </ItemTemplate>
    </asp:Repeater>
  </asp:View>
</asp:MultiView>