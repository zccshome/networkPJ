<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 3 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
  	
  	$ownerID = $_POST['ownerID'];
  	$carID = $_POST['carID'];
  	$engineID = $_POST['engineID'];
  	$recordType = $_POST['recordType'];
  	$recordDescription = $_POST['recordDescription'];
  	$points = $_POST['points'];
  	$fine = $_POST['fine'];
  	$finePayDue = $_POST['finePayDue'];
  	if ((($_FILES["file"]["type"] == "image/gif") || ($_FILES["file"]["type"] == "image/jpeg") || ($_FILES["file"]["type"] == "image/pjpeg")) )//&& ($_FILES["file"]["size"] < 20000))
    {
        if ($_FILES["file"]["error"] > 0)
        {
            //echo "Return Code: " . $_FILES["file"]["error"] . "<br />";
        }
        else
        {
            move_uploaded_file($_FILES["file"]["tmp_name"], "./Pic/Record/" . $_FILES["file"]["name"]);
            $recordPic = "./Pic/Record/" . $_FILES["file"]["name"];
        }
    }

  	include("loginConfigure.php");
  	$result = mysql_query("INSERT INTO pj1.recordmanagement(ownerID, carID, engineID, recordType, recordDescription, points, fine, finePayDue, recordPic) VALUES('$ownerID','$carID','$engineID','$recordType','$recordDescription','$points','$fine','$finePayDue', '$recordPic')");
  	mysql_close($con);
  	header('Location: recordManagement.php');
?>