<?php
  session_start();
  header("Content-Type: text/html; charset=utf-8");
  $account = $_POST["account"];
	$password = $_POST["password"];
	include("loginConfigure.php");
  $result = mysql_query("SELECT * FROM pj1.systemuserrole WHERE account = '$account' and password = '$password'");
  if($row = mysql_fetch_array($result))
  {
  	$userType = $row['accountType'];
    $instituteID = $row['instituteID'];
    $_SESSION['accountType'] = $userType;
    $_SESSION['instituteID'] = $instituteID;
    if($userType == 0)
    {
      header('Location: fineManagement.php');
    }
    if($userType == 1)
    {
      header('Location: vehicleManagement.php');
    }
    if($userType == 2)
    {
      header('Location: ownerManagement.php');
    }
    if($userType == 3)
    {
      header('Location: recordManagement.php');
    }
    if($userType == 4)
    {
      header('Location: fineManagement.php');
    }
  }
  else
  {
  	header('Location: index.php');
  }
	mysql_close($con);
?>