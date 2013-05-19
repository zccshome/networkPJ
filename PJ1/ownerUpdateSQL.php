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
  	$result = mysql_query("UPDATE pj1.ownermanagement SET ownerName = 
  		'$ownerName', ownerName = '$ownerName', nationality = '$nationality', ownerAddress = '$ownerAddress', licenseID = '$licenseID', points = '$points' WHERE ownerID = '$ownerID'");
  	mysql_close($con);
  	header('Location: ownerManagement.php');
?>