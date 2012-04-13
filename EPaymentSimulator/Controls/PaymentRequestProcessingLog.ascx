<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentRequestProcessingLog.ascx.cs" Inherits="Controls_PaymentRequestProcessingLog" %>

<%--
  Copyright 2009 MONOGRAM Technologies
  
  This file is part of MONOGRAM EPayment libraries
  
  MONOGRAM EPayment libraries is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  MONOGRAM EPayment libraries is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with MONOGRAM EPayment libraries.  If not, see <http://www.gnu.org/licenses/>.
--%>

<div>
Výsledok validácie: <asp:Literal ID="lValidationResult" runat="server" />
<div runat="server" id="dValidationFailureMessages">
  <asp:Repeater ID="rValidationFailureMessages" runat="server">
    <ItemTemplate>
      <span class="validationError"><%# HttpUtility.HtmlEncode((string)Container.DataItem) %></span><br />
    </ItemTemplate>
  </asp:Repeater>
</div>
<br />

Výsledok verifikácie integrity: <asp:Literal ID="lVerificationResult" runat="server" />
<div id="dVerificationLog" runat="server">
  Nekryptovaný odtlačok: <span class="verificationUncryptedSignature"><asp:Literal runat="server" ID="lVerificationUncryptedSignature" /></span><br />
  Správny podpis správy: <span class="verificationSignature"><asp:Literal runat="server" ID="lVerificationSignature" /></span><br />
  Prijatý podpis správy: <span class="verificationReceivedSignature"><asp:Literal runat="server" ID="lVerificationReceivedSignature" /></span>
</div>
</div>