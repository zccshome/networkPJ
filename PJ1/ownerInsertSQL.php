<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 2 && $_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
  	
  	$ownerID = $_POST['ownerID'];
  	$ownerName = $_POST['ownerName'];
  	$nationality = $_POST['nationality'];
  	$ownerAddress = $_POST['ownerAddress'];
  	$licenseID = $_POST['licenseID'];
  	$points = $_POST['points'];
  	include("loginConfigure.php");
  	$result = mysql_query("INSERT INTO pj1.ownermanagement VALUES('$ownerID','$ownerName','$nationality','$ownerAddress','$licenseID','$points')");
  	mysql_close($con);
  	header('Location: ownerManagement.php');
?>