<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 4 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
  
  $fineID = $_POST['fineID'];

  include("loginConfigure.php");
  $result = mysql_query("DELETE FROM pj1.finemanagement WHERE fineID = '$fineID'");
  mysql_close($con);
  header('Location: fineManagement.php');
?>