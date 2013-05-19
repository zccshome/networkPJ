<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
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
		<form action="vehicleInsertSQL.php" method="post" enctype="multipart/form-data">
			<div>
				<span>carID</span>
				<input name="carID" value=""></input>
			</div>
			<div>
				<span>engineID</span>
				<input name="engineID" value=""></input>
			</div>
			<div>
				<span>ownerID</span>
				<input name="ownerID" value=""></input>
			</div>
			<div>
				<span>instituteID</span>
				<input name="instituteID" value=""></input>
			</div>
			<div>
				<span>carNumber</span>
				<input name="carNumber" value=""></input>
			</div>
			<div>
				<span>carType</span>
				<input name="carType" value=""></input>
			</div>
			<div>
				<span>carColor</span>
				<input name="carColor" value=""></input>
			</div>
			<div>
				<span>carFrom</span>
				<input name="carFrom" value=""></input>
			</div>
			<div>
				<span>buyTime</span>
				<input name="buyTime" value=""></input>
			</div>
			<div>
				<span>vehiclePic</span>
				<input name="file" value="" type="file"></input>
			</div>
			<div>
				<input type="button" value="cancel" onclick="back();"></input>
				<input type="submit" value="submit"></input>
			</div>
		</form>
		</div>
		<div class="backBottomDiv">
		</div>
	</div>
</body>
</html>