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

	$returnUrl = $protocol.'://'.$_SERVER['SERVER_NAME'].dirname($_SERVER['PHP_SELF']).'/return_slsp_sporopay.php';

	$sporopayFields = array(
		'suma' => '100.00',
		'vs' => str_pad(mt_rand(0, 9999999999), 10, '0', STR_PAD_LEFT),
		'pu_predcislo' => SLSP_SPOROPAY_PU_PREDCISLO,
		'pu_cislo' => SLSP_SPOROPAY_PU_CISLO,
		'ss' => str_pad(mt_rand(0, 9999999999), 10, '0', STR_PAD_LEFT),
		'param' => 'myParam='.mt_rand(),
		'url' => $returnUrl,
		'acc_prefix' => null,
		'acc_number' => null,
		'mail_notify_att' => null,
		'email_adr' => null,
		'client_login' => null,
		'auth_tool_type' => null
	);
?>
<!DOCTYPE html>
<html>
<head>
	<title>SKLSP Sporopay / MONOGRAM EPayment Libraries for PHP</title>
	<link href="resources/css/bootstrap.css" rel="stylesheet">
	<link href="resources/css/docs.css" rel="stylesheet">
</head>
<body>
	<div class="navbar navbar-fixed-top">
		<div class="navbar-inner">
			<div class="container">
				<a class="brand" href="http://www.monogram.sk/">MONOGRAM</a>
				<ul class="nav">
					<li>
						<a href="index.php">Home</a>
					</li>
				</ul>
			</div>
		</div>
	</div>

	<div class="container">
		<div class="page-header">
			<h1>SKLSP Sporopay <small> MONOGRAM EPayment Libraries for PHP</small></h1>
		</div>

		<form action="?" method="post" class="well form-horizontal">
			<fieldset>

<?php
	foreach ($sporopayFields as $key => $value) {
		echo '<div class="control-group">';
		$outValue = $value;
		if (isset($_POST['fields'][$key]['value'])) {
			$outValue = $_POST['fields'][$key]['value'];
		}
		$outValue = htmlentities($outValue, ENT_QUOTES);
    
		echo "<label class=\"control-label\">{$key}</label>";
		echo '<div class="controls">';
		if ($value == null) {
			echo "<input type=\"checkbox\" name=\"fields[$key][set]\" value=\"1\"".(empty($outValue) ? '' : ' checked="checked"')." /> ";
		}
		echo "<input type=\"text\" class=\"input-xxlarge\" name=\"fields[{$key}][value]\" value=\"{$outValue}\" /></td></tr>\n";
		echo "</div></div>";
	}
?>

				<div class="form-actions">
					<input type="submit" class="btn btn-large btn-primary" name="dopayment" value="Create payment request" /></td></tr>
				</div>
			</fieldset>
		</form>

<?php
	if (isset($_POST['dopayment'])) {
		require_once dirname(dirname(__FILE__)).'/EPaymentMerchant_PHP/SLSP_SporoPay/SporoPayPaymentRequest.class.php';
    
		$pr = new SporoPayPaymentRequest();
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
    
		$pr->SetRedirectUrlBase(SLSP_SPOROPAY_REDIRECTURLBASE);
    
		$validationResult = $pr->validate();
		if ($validationResult) {
			echo "<div class=\"alert alert-success\">";
			echo "<strong>Validation OK</strong><br />";

			$pr->SignMessage(SLSP_SPOROPAY_SHAREDSECRET);

			echo "Message signed<br />";

			$prurl = $pr->GetRedirectUrl();

			echo "Payment request: <a href=\"{$prurl}\">{$prurl}</a>";
			echo "</div>";
		} else {
			echo "<div class=\"alert alert-error\"><strong>Validation failed</strong></div>";
		}
	}
?>
		<p>
			<a href="index.php" class="btn">New payment</a>
		</p>

		<footer class="footer">
			<p class="pull-right">
				<a href="#">Back to top</a>
			</p>
			<p>
				<a href="http://epayment.monogram.sk/" target="_blank">MONOGRAM EPayment</a> libraries is distributed in the hope that it will be useful
			</p>
		</footer>
	</div>
</body>
</html>