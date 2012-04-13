<?php
/*
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
*/

  require_once 'constants.php';
  
  $protocol = 'http';
  if (isset($_SERVER['HTTPS']) && $_SERVER['HTTPS'] == 'on') {
    $protocol = 'https';
  }
  
  $returnUrl = $protocol.'://'.$_SERVER['SERVER_NAME'].dirname($_SERVER['PHP_SELF']).'/return_tb_tatrapay.php';
  
  $sporopayFields = array(
    'MID' => TB_TATRAPAY_MID,
    'AMT' => '100.00',
    'CURR' => '978',
    'VS' => str_pad(mt_rand(0, 9999999999), 10, '0', STR_PAD_LEFT),
    'CS' => '0308',
    'RURL' => $returnUrl,
    'PT' => null,
    'SS' => null,
    'DESC' => null,
    'RSMS' => null,
    'REM' => null,
    'AREDIR' => null,
    'LANG' => null
  );
?>
<html>
  <head>
    <title>MONOGRAM EPayment Libraries for PHP</title>
  </head>
  <body>
    <h1>MONOGRAM EPayment Libraries for PHP</h1>
    <form action="?" method="post">
      <table>
<?php
  foreach ($sporopayFields as $key => $value) {
    $outValue = $value;
    if (isset($_POST['fields'][$key]['value'])) {
      $outValue = $_POST['fields'][$key]['value'];
    }
    $outValue = htmlentities($outValue, ENT_QUOTES);
    
    echo "<tr><td>{$key}</td><td>";
    if ($value == null) {
      echo "<input type=\"checkbox\" name=\"fields[$key][set]\" value=\"1\"".(empty($outValue) ? '' : ' checked="checked"')." /> ";
    }
    echo "<input type=\"text\" name=\"fields[{$key}][value]\" value=\"{$outValue}\" /></td></tr>\n";
    
  }
?>
        <tr><td></td><td><input type="submit" name="dopayment" value="Create payment request" /></td></tr>
      </table>
    </form>
<?php
  if (isset($_POST['dopayment'])) {
    require_once dirname(dirname(__FILE__)).'/EPaymentMerchant_PHP/TB_TatraPay/TatraPayPaymentRequest.class.php';
    
    $pr = new TatraPayPaymentRequest();
    foreach ($sporopayFields as $key => $value) {
      $outValue = $value;
      
      if ($value === null) {
        if (isset($_POST['fields'][$key]['set']) && ($_POST['fields'][$key]['set'])) {
          $outValue = $_POST['fields'][$key]['value'];
        }
      } else {
        $outValue = $_POST['fields'][$key]['value'];
      }
      
      if (!($outValue === null)) {
        $pr->$key = $outValue;
      }
    }
    
    $pr->SetRedirectUrlBase(TB_TATRAPAY_REDIRECTURLBASE);
    
    $validationResult = $pr->validate();
    if ($validationResult) {
      echo "Validation OK<br />";
      
      $pr->SignMessage(TB_TATRAPAY_SHAREDSECRET);
      
      echo "Message signed<br />";
      
      $prurl = $pr->GetRedirectUrl();
      
      echo "Payment request: <a href=\"{$prurl}\">{$prurl}</a><br />";
    } else {
      echo "Validation failed<br />";
    }
  }
?>
    <p>
      <a href="index.php">New payment</a>
    </p>
  </body>
</html>