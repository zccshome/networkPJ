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
  	$result = mysql_query("INSERT INTO pj1.vehiclemanagement VALUES('$carID','$engineID','$ownerID','$instituteID','$carNumber','$carType','$carColor','$carFrom','$buyTime','$vehiclePic')");
  	mysql_close($con);
  	header('Location: vehicleManagement.php');
?>