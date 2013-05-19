<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
  
  $carID = $_POST['ids'];

  include("loginConfigure.php");
  for($i=0;$i<count($carID);$i++)
  {
  	$result = mysql_query("DELETE FROM pj1.vehiclemanagement WHERE carID = '$carID[$i]'");
  }
  mysql_close($con);
  //header('Location: vehicleManagement.php');
?>