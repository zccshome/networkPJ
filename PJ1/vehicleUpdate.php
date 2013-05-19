<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
	if(isset($_POST['carID']))
	{
		$carID = $_POST['carID'];
		include("loginConfigure.php");
		$result = mysql_query("SELECT * FROM pj1.vehiclemanagement WHERE carID = '$carID'");
  		if($row = mysql_fetch_array($result))
  		{
  			$temp[] = array();
  		
  			$temp['carID'] = $row['carID'];
  			$temp['engineID'] = $row['engineID'];
  			$temp['ownerID'] = $row['ownerID'];
  			$temp['instituteID'] = $row['instituteID'];
  			$temp['carNumber'] = $row['carNumber'];
  			$temp['carType'] = $row['carType'];
  			$temp['carColor'] = $row['carColor'];
  			$temp['carFrom'] = $row['carFrom'];
  			$temp['buyTime'] = $row['buyTime'];
  			$temp['vehiclePic'] = $row['vehiclePic'];
		}
		mysql_close($con);
	}
?>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>ZCC's Home</title>
	<!--<meta http-equiv="refresh" content="6000" />-->
	<meta name="security" content="low" />
	<link href="css/VMUpdate.css" rel="stylesheet" type="text/css" />
	<script language="javascript" type="text/javascript" src="js/jquery.js"> </script>
	<script language="javascript" type="text/javascript" src="js/Main.js"> </script>
	<script language="javascript" type="text/javascript" src="js/Vehicle.js"> </script>
</head>
<body class="body">
	<div class="mainDiv" title="title">
		<div class="backTopDiv">
			<img src="./Pic/BackPic/Back_title.png" alt="title" class="backSnow"></img>
			<img src="./Pic/BackPic/Back_snow.gif" alt="snow" class="backSnow"></img>
			<img src="./Pic/BackPic/Back_snow.gif" alt="snow" class="backSnow"></img>
		</div>
		<div class="backCenterDiv">
			<img src="./Pic/BackPic/Back_center.jpg" alt="backCenterPic"></img>
		</div>
		<div class="foreCenterDiv">
		<form action="vehicleUpdateSQL.php" method="post" enctype="multipart/form-data">
			<div>
				<span>carID</span>
				<input readonly="true" name="carID" value="<?php echo $temp['carID'];?>"></input>
			</div>
			<div>
				<span>engineID</span>
				<input name="engineID" value="<?php echo $temp['engineID'];?>"></input>
			</div>
			<div>
				<span>ownerID</span>
				<input name="ownerID" value="<?php echo $temp['ownerID'];?>"></input>
			</div>
			<div>
				<span>instituteID</span>
				<input name="instituteID" value="<?php echo $temp['instituteID'];?>"></input>
			</div>
			<div>
				<span>carNumber</span>
				<input name="carNumber" value="<?php echo $temp['carNumber'];?>"></input>
			</div>
			<div>
				<span>carType</span>
				<input readonl ="true" name="carType" value="<?php echo $temp['carType'];?>"></input>
			</div>
			<div>
				<span>carColor</span>
				<input name="carColor" value="<?php echo $temp['carColor'];?>"></input>
			</div>
			<div>
				<span>carFrom</span>
				<input readonly="true" name="carFrom" value="<?php echo $temp['carFrom'];?>"></input>
			</div>
			<div>
				<span>buyTime</span>
				<input readonly="true" name="buyTime" value="<?php echo $temp['buyTime'];?>"></input>
			</div>
			<div>
				<span>vehiclePic</span>
				<input name="file" type="file"></input>
				<input name="vehiclePic" type="hidden" value="<?php echo $temp['vehiclePic'];?>"></input>
			</div>
			<div>
				<input type="button" value="cancel" class="back"></input>
				<input type="submit" value="submit"></input>
			</div>
		</form>
		</div>
		<div class="backBottomDiv">
		</div>
	</div>
</body>
</html>