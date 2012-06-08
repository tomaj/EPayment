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
<!DOCTYPE html>
<html>
<head>
	<title>SLSP Sporopay / MONOGRAM EPayment Libraries for PHP</title>
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
			<h1>SLSP Sporopay <small>MONOGRAM EPayment Libraries for PHP</small></h1>
		</div>
<?php
	require_once dirname(__FILE__).'/constants.php';
	$fields = array('u_predcislo', 'u_cislo', 'u_kbanky', 'pu_predcislo', 'pu_cislo', 'pu_kbanky', 'suma', 'mena', 'vs', 'ss', 'url', 'param', 'result', 'real', 'SIGN2');

	require_once dirname(dirname(__FILE__)).'/EPaymentMerchant_PHP/SLSP_SporoPay/SporoPayPaymentHttpResponse.class.php';
	$pres = new SporoPayPaymentHttpResponse();

	if ($pres->Validate()) {
		echo '<div class="alert alert-success">Received message is valid.</div>';

		if ($pres->VerifySignature(SLSP_SPOROPAY_SHAREDSECRET)) {
			echo '<div class="alert alert-success">Received message passed integrity and authenticity check.</div>';

			$result = $pres->GetPaymentResponse();

			echo '<p>Payment result: <strong>';
			if ($result == IEPaymentHttpPaymentResponse::RESPONSE_SUCCESS) {
				echo '<span class="label label-success">SUCCESS</span>';
			} else if ($result == IEPaymentHttpPaymentResponse::RESPONSE_FAIL) {
				echo '<span class="label label-important">FAIL</span>';
			} else if ($result == IEPaymentHttpPaymentResponse::RESPONSE_TIMEOUT) {
				echo '<span class="label label-warning">TIMEOUT</span>';
			}
			echo '</strong></p>';

			echo '<table class="table table-bordered table-striped"><tbody>';
			foreach ($fields as $fname) {
				echo "<tr><td>{$fname}</td><td>{$pres->$fname}</td></tr>\n";
			}
			echo '</tbody></table>';
		} else {
			echo '<div class="alert alert-error">Received message failed integrity and authenticity check.</div>';
		}
	} else {
		echo '<div class="alert alert-success">Received message failed validation.</div>';
	}
?>
		<p>
			<a class="btn" href="index.php">New payment</a>
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