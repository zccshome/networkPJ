<?php
    session_start();
    header("Content-Type: text/html; charset=utf-8");
    if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
    {
		    header('Location: index.php');
    }
    $recordID = $_POST['recordID'];
    $ownerID = $_POST['ownerID'];
    $carID = $_POST['carID'];
    $engineID = $_POST['engineID'];
    $recordType = $_POST['recordType'];
    $recordDescription = $_POST['recordDescription'];
    $points = $_POST['points'];
    $fine = $_POST['fine'];
    $finePayDue = $_POST['finePayDue'];
    $recordPic = $_POST['recordPic'];
    if ((($_FILES["file"]["type"] == "image/gif") || ($_FILES["file"]["type"] == "image/jpeg") || ($_FILES["file"]["type"] == "image/pjpeg")) )//&& ($_FILES["file"]["size"] < 20000))
    {
        if ($_FILES["file"]["error"] > 0)
        {
            //echo "Return Code: " . $_FILES["file"]["error"] . "<br />";
        }
        else
        {
            move_uploaded_file($_FILES["file"]["tmp_name"], "./Pic/Car/" . $_FILES["file"]["name"]);
            $recordPic = "./Pic/Car/" . $_FILES["file"]["name"];
        }
    }
  	include("loginConfigure.php");
  	$result = mysql_query("UPDATE pj1.recordmanagement SET ownerID = 
  		'$ownerID', carID = '$carID', engineID = '$engineID', recordType = '$recordType', recordDescription = '$recordDescription', points = '$points', fine = '$fine', finePayDue = '$finePayDue', recordPic = '$recordPic' WHERE recordID = '$recordID'");
  	mysql_close($con);
  	header('Location: recordManagement.php');
?>