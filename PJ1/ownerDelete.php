<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 2 && $_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
  
  $ownerID = $_POST['ownerID'];

  include("loginConfigure.php");
  $result = mysql_query("DELETE FROM pj1.ownermanagement WHERE ownerID = '$ownerID'");
  mysql_close($con);
  header('Location: ownerManagement.php');
?>