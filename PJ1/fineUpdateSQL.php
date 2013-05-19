<?php
    session_start();
    header("Content-Type: text/html; charset=utf-8");
    if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 2 && $_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
    {
		    header('Location: index.php');
    }

    $fineID = $_POST['fineID'];
    $ownerID = $_POST['ownerID'];
    $fineDate = $_POST['fineDate'];
    $fineAmount = $_POST['fineAmount'];
    $finePaid = $_POST['finePaid'];
    $finePaidDate = $_POST['finePaidDate'];
  	include("loginConfigure.php");
  	$result = mysql_query("UPDATE pj1.finemanagement SET ownerID = 
  		'$ownerID', fineDate = '$fineDate', fineAmount = '$fineAmount', finePaid = '$finePaid', finePaidDate = '$finePaidDate' WHERE fineID = '$fineID'");
  	mysql_close($con);
  	header('Location: fineManagement.php');
?>