<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
  
  $carID = $_POST['carID'];

  include("loginConfigure.php");
  $result = mysql_query("DELETE FROM pj1.vehiclemanagement WHERE carID = '$carID'");
  mysql_close($con);
  header('Location: vehicleManagement.php');
?>