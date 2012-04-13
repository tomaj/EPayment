<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/MasterPage.master" %>

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

<asp:Content ContentPlaceHolderID="MainContentPlace" runat="server">
  <p>Táto služba pomáha otestovať implementáciu EPayment platobných systémov a ich integráciu do webových projektov.
  Ak teda na Vašom portáli používate niektoré nami podporované internet bankingové platby, môžete náš server používať
  pri testovaní namiesto živého systému banky - úplne zadarmo a s plnou kontrolou nad spätne odosielanými dátami.</p>
  <p>
    <ul>
      <li>validácia príchodzích správ</li>
      <li>verifikácia digitálneho podpisu príchodzích správ</li>
      <li>výber odpovede zaslanej serverom naspäť do Vášho systému</li>
      <li>možnosť manuálne zmeniť už podpísané parametre odchodzej správy</li>
      <li>odoslanie správy do Vášho systému</li>
    </ul>
  </p>
  
  <h2>Používanie EPayment simulátora</h2>
  <p>Aby ste mohli používať EPayment simulátor, musíte vo Vašej implementácii platobných systémov zmeniť url 
  (na ktorú sa z Vášho systému odosielajú platobné dotazy), merchant ID (alebo ekvivalent pre daný protokol)
  a samozrejme aj shared secret, aby náš server mohol správne verifikovať a podpisovať testovacie správy.<br />
  Všetky tieto údaje sú fixne zadané nami a samozrejme sa líšia pre každú službu.</p>
  
  <h3>SLSP SporoPay</h3>
  <p>
    SporoPay používa na identifikáciu obchodníka jeho číslo účtu (spolu s predčíslom a kódom banky).
  </p>
  <table>
    <caption>Parametre pre testovanie SLSP SporoPay</caption>
    <tr><td>Predčíslo účtu (pu_predcislo):</td><td><asp:Label ID="lSLSP_SporoPay_pu_predcislo" runat="server" /></td></tr>
    <tr><td>Číslo účtu (pu_cislo):</td><td><asp:Label ID="lSLSP_SporoPay_pu_cislo" runat="server" /></td></tr>
    <tr><td>Kód banky (pu_kbanky):</td><td><asp:Label ID="lSLSP_SporoPay_pu_kbanky" runat="server" /></td></tr>
    <tr><td>Heslo používané na digitálny podpis:</td><td><asp:Label ID="lSLSP_SporoPay_sharedSecret" runat="server" /></td></tr>
    <tr><td>Url, na ktorú má byť zaslaný payment request:</td><td><asp:Literal ID="lSLSP_SporoPay_UrlBase" runat="server" /></td></tr>
  </table>
  
  <h3>VÚB e-platba</h3>
  <h4>Verzia protokolu z roku 2010, HMAC-SHA256 podpisovanie správ</h4>
  <table>
    <caption>Parametre pre testovanie VÚB e-platby</caption>
    <tr><td>Identifikátor obchodníka (MID):</td><td><asp:Label ID="lVUB_EPlatba2_HMAC_mid" runat="server" /></td></tr>
    <tr><td>Heslo používané na digitálny podpis:</td><td><asp:Label ID="lVUB_EPlatba2_HMAC_sharedSecret" runat="server" /></td></tr>
    <tr><td>Url, na ktorú má byť zaslaný payment request:</td><td><asp:Literal ID="lVUB_EPlatba2_HMAC_UrlBase" runat="server" /></td></tr>
  </table>
  <h4>Staršia verzia protokolu zpred roku 2010</h4>
  <table>
    <caption>Parametre pre testovanie VÚB e-platby</caption>
    <tr><td>Identifikátor obchodníka (MID):</td><td><asp:Label ID="lVUB_EPlatba_mid" runat="server" /></td></tr>
    <tr><td>Heslo používané na digitálny podpis:</td><td><asp:Label ID="lVUB_EPlatba_sharedSecret" runat="server" /></td></tr>
    <tr><td>Url, na ktorú má byť zaslaný payment request:</td><td><asp:Literal ID="lVUB_EPlatba_UrlBase" runat="server" /></td></tr>
  </table>
  
  <h3>TB TatraPay</h3>
  <p></p>
  <table>
    <caption>Parametre pre testovanie TB TatraPay</caption>
    <tr><td>Identifikátor obchodníka (MID):</td><td><asp:Label ID="lTB_TatraPay_mid" runat="server" /></td></tr>
    <tr><td>Heslo používané na digitálny podpis:</td><td><asp:Label ID="lTB_TatraPay_sharedSecret" runat="server" /></td></tr>
    <tr><td>Url, na ktorú má byť zaslaný payment request:</td><td><asp:Literal ID="lTB_TatraPay_UrlBase" runat="server" /></td></tr>
  </table>
  
  <h3>TB CardPay</h3>
  <p></p>
  <table>
    <caption>Parametre pre testovanie TB CardPay</caption>
    <tr><td>Identifikátor obchodníka (MID):</td><td><asp:Label ID="lTB_CardPay_mid" runat="server" /></td></tr>
    <tr><td>Heslo používané na digitálny podpis:</td><td><asp:Label ID="lTB_CardPay_sharedSecret" runat="server" /></td></tr>
    <tr><td>Url, na ktorú má byť zaslaný payment request:</td><td><asp:Literal ID="lTB_CardPay_UrlBase" runat="server" /></td></tr>
  </table>
  
  <h3>UCB UniPlatba</h3>
  <p>Pri UniPlatbe sa v Payment requeste neposiela návratová URL. Systém platieb má túto URL nastavenú pre každého obchodníka a teda ju zisťuje pomocou merchant ID. EPayment simulátor želanú návratovú URL nevie zistiť a preto je pri tomto type platby v EPayment simulátore uvedená aj hodnota RURL, ktorú môžete zmeniť.</p>
  <table>
    <caption>Parametre pre testovanie UCB UniPlatba</caption>
    <tr><td>Identifikátor obchodníka (MID):</td><td><asp:Label ID="lUCB_UniPlatba_mid" runat="server" /></td></tr>
    <tr><td>Heslo používané na digitálny podpis:</td><td><asp:Label ID="lUCB_UniPlatba_sharedSecret" runat="server" /></td></tr>
    <tr><td>Url, na ktorú má byť zaslaný payment request:</td><td><asp:Literal ID="lUCB_UniPlatba_UrlBase" runat="server" /></td></tr>
  </table>
  
  <h3>VB VeBpay</h3>
  <p></p>
  <table>
    <caption>Parametre pre testovanie VB VeBpay</caption>
    <tr><td>Identifikátor obchodníka (MID):</td><td><asp:Label ID="lVB_VeBpay_mid" runat="server" /></td></tr>
    <tr><td>Heslo používané na digitálny podpis:</td><td><asp:Label ID="lVB_VeBpay_sharedSecret" runat="server" /></td></tr>
    <tr><td>Url, na ktorú má byť zaslaný payment request:</td><td><asp:Literal ID="lVB_VeBpay_UrlBase" runat="server" /></td></tr>
  </table>
</asp:Content>
