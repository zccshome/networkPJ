<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 2 && $_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
	if(isset($_POST['ownerID']))
	{
		$ownerID = $_POST['ownerID'];
		include("loginConfigure.php");
		$result = mysql_query("SELECT * FROM pj1.ownermanagement WHERE ownerID = '$ownerID'");
  		if($row = mysql_fetch_array($result))
  		{
  			$temp[] = array();
  		
  			$temp['ownerID'] = $row['ownerID'];
  			$temp['ownerName'] = $row['ownerName'];
  			$temp['nationality'] = $row['nationality'];
  			$temp['ownerAddress'] = $row['ownerAddress'];
  			$temp['licenseID'] = $row['licenseID'];
  			$temp['points'] = $row['points'];
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
		<form action="ownerUpdateSQL.php" method="post">
			<div>
				<span>ownerID</span>
				<input readonly="true" name="ownerID" value="<?php echo $temp['ownerID'];?>"></input>
			</div>
			<div>
				<span>ownerName</span>
				<input name="ownerName" value="<?php echo $temp['ownerName'];?>"></input>
			</div>
			<div>
				<span>nationality</span>
				<input name="nationality" value="<?php echo $temp['nationality'];?>"></input>
			</div>
			<div>
				<span>ownerAddress</span>
				<input name="ownerAddress" value="<?php echo $temp['ownerAddress'];?>"></input>
			</div>
			<div>
				<span>licenseID</span>
				<input name="licenseID" value="<?php echo $temp['licenseID'];?>"></input>
			</div>
			<div>
				<span>points</span>
				<input readonly="true" name="points" value="<?php echo $temp['points'];?>"></input>
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