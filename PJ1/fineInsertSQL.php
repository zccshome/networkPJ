<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 4 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
  	
  	$ownerID = $_POST['ownerID'];
  	$fineDate = $_POST['fineDate'];
  	$fineAmount = $_POST['fineAmount'];
  	$finePaid = $_POST['finePaid'];
  	$finePaidDate = $_POST['finePaidDate'];
  	include("loginConfigure.php");
  	$result = mysql_query("INSERT INTO pj1.finemanagement(ownerID, fineDate, fineAmount, finePaid, finePaidDate) VALUES('$ownerID','$fineDate','$fineAmount','$finePaid','$finePaidDate')");
  	mysql_close($con);
  	header('Location: fineManagement.php');
?>