<?php
    session_start();
    header("Content-Type: text/html; charset=utf-8");
    if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
    {
		    header('Location: index.php');
    }

    $carID = $_POST['carID'];
    $engineID = $_POST['engineID'];
    $ownerID = $_POST['ownerID'];
    $instituteID = $_POST['instituteID'];
    $carNumber = $_POST['carNumber'];
    $carType = $_POST['carType'];
    $carColor = $_POST['carColor'];
    $carFrom = $_POST['carFrom'];
    $buyTime = $_POST['buyTime'];
    $vehiclePic = $_POST['vehiclePic'];
    if ((($_FILES["file"]["type"] == "image/gif") || ($_FILES["file"]["type"] == "image/jpeg") || ($_FILES["file"]["type"] == "image/pjpeg")) )//&& ($_FILES["file"]["size"] < 20000))
    {
        if ($_FILES["file"]["error"] > 0)
        {
            //echo "Return Code: " . $_FILES["file"]["error"] . "<br />";
        }
        else
        {
            move_uploaded_file($_FILES["file"]["tmp_name"], "./Pic/Car/" . $_FILES["file"]["name"]);
            $vehiclePic = "./Pic/Car/" . $_FILES["file"]["name"];
        }
    }
  	include("loginConfigure.php");
  	$result = mysql_query("UPDATE pj1.vehiclemanagement SET engineID = 
  		'$engineID', ownerID = '$ownerID', instituteID = '$instituteID', carNumber = '$carNumber', carType = '$carType', carColor = '$carColor', carFrom = '$carFrom', buyTime = '$buyTime', vehiclePic = '$vehiclePic' WHERE carID = '$carID'");
  	mysql_close($con);
  	header('Location: vehicleManagement.php');
?>