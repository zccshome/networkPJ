<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 2 && $_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
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
	<script language="javascript" type="text/javascript" src="js/Owner.js"> </script>
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
		<form action="ownerInsertSQL.php" method="post">
			<div>
				<span>ownerID</span>
				<input name="ownerID" value=""></input>
			</div>
			<div>
				<span>ownerName</span>
				<input name="ownerName" value=""></input>
			</div>
			<div>
				<span>nationality</span>
				<input name="nationality" value=""></input>
			</div>
			<div>
				<span>ownerAddress</span>
				<input name="ownerAddress" value=""></input>
			</div>
			<div>
				<span>licenseID</span>
				<input name="licenseID" value=""></input>
			</div>
			<div>
				<span>points</span>
				<input name="points" value=""></input>
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