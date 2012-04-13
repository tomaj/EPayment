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
?>
<html>
  <head>
    <title>MONOGRAM EPayment Libraries for PHP</title>
  </head>
  <body>
    <h1>MONOGRAM EPayment Libraries for PHP</h1>
<?php
  require_once dirname(__FILE__).'/constants.php';
  $fields = array('SS', 'VS', 'RES', 'SIGN');
  
  require_once dirname(dirname(__FILE__)).'/EPaymentMerchant_PHP/VB_VeBpay/VeBpayPaymentHttpResponse.class.php';
  $pres = new VeBpayPaymentHttpResponse();
  
  if ($pres->Validate()) {
    echo 'Received message is valid.<br />';
    
    if ($pres->VerifySignature(VB_VEBPAY_SHAREDSECRET)) {
      echo 'Received message passed integrity and authenticity check.<br />';
      
      $result = $pres->GetPaymentResponse();
      
      echo '<strong>Payment result: ';
      if ($result == IEPaymentHttpPaymentResponse::RESPONSE_SUCCESS) {
        echo 'SUCCESS';
      } else if ($result == IEPaymentHttpPaymentResponse::RESPONSE_FAIL) {
        echo 'FAIL';
      }
      echo '</strong><br />';
      
      echo '<table>';
      foreach ($fields as $fname) {
        echo "<tr><td>{$fname}</td><td>{$pres->$fname}</td></tr>\n";
      }
      echo '</table>';
    } else {
      echo 'Received message failed integrity and authenticity check.';
    }
  } else {
    echo 'Received message failed validation.';
  }
?>
    <p>
      <a href="index.php">New payment</a>
    </p>
  </body>
</html>
  