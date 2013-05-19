<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 3 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
  
  $recordID = $_POST['ids'];

  include("loginConfigure.php");
  for($i=0;$i<count($recordID);$i++)
  {
  	$result = mysql_query("DELETE FROM pj1.recordmanagement WHERE recordID = '$recordID[$i]'");
  }
  mysql_close($con);
  //header('Location: vehicleManagement.php');
?>