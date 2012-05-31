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
header('Content-Type: text/html; charset=utf8');
?>
<!DOCTYPE html>
<html>
<head>
	<title>MONOGRAM EPayment merchant libraries for PHP</title>
	<link href="resources/css/bootstrap.css" rel="stylesheet">
	<link href="resources/css/docs.css" rel="stylesheet">
</head>
<body data-offset="50">
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
		<header class="jumbotron subhead">
			<div class="inner">
				<h1>MONOGRAM EPayment merchant libraries for PHP</h1>
			</div>
		</header>

		<hr class="soften" />

		<ul class="nav nav-pills nav-stacked">
			<li><a href="start_slsp_sporopay.php">SLSP SporoPay</a></li>
			<li><a href="start_tb_tatrapay.php">TB TatraPay</a></li>
			<li><a href="start_tb_cardpay.php">TB CardPay</a></li>
			<li><a href="start_ucb_uniplatba.php">UCB UniPlatba</a></li>
			<li><a href="start_vub_eplatba2_hmac.php">VÚB EPlatba2 HMAC-SHA256</a></li>
			<li><a href="start_vub_eplatba.php">VÚB EPlatba (pre-2010)</a></li>
			<li><a href="start_vb_vebpay.php">VB VeBpay</a></li>
		</ul>

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