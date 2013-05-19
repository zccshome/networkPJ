<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 3 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
  
  $recordID = $_POST['recordID'];

  include("loginConfigure.php");
  $result = mysql_query("DELETE FROM pj1.recordmanagement WHERE recordID = '$recordID'");
  mysql_close($con);
  header('Location: recordManagement.php');
?>