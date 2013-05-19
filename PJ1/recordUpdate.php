<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 3 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
	if(isset($_POST['recordID']))
	{
		$recordID = $_POST['recordID'];
		include("loginConfigure.php");
		$result = mysql_query("SELECT * FROM pj1.recordmanagement WHERE recordID = '$recordID'");
  		if($row = mysql_fetch_array($result))
  		{
  			$temp[] = array();
  		
  			$temp['ownerID'] = $row['ownerID'];
  			$temp['carID'] = $row['carID'];
  			$temp['engineID'] = $row['engineID'];
  			$temp['recordType'] = $row['recordType'];
  			$temp['recordDescription'] = $row['recordDescription'];
  			$temp['points'] = $row['points'];
  			$temp['fine'] = $row['fine'];
  			$temp['finePayDue'] = $row['finePayDue'];
  			$temp['recordPic'] = $row['recordPic'];
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
	<script language="javascript" type="text/javascript" src="js/Record.js"> </script>
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
		<form action="recordUpdateSQL.php" method="post" enctype="multipart/form-data">
			<div>
				<input type="hidden" name="recordID" value="<?php echo $recordID;?>"></input>
			</div>
			<div>
				<span>ownerID</span>
				<input name="ownerID" value="<?php echo $temp['ownerID'];?>"></input>
			</div>
			<div>
				<span>carID</span>
				<input name="carID" value="<?php echo $temp['carID'];?>"></input>
			</div>
			<div>
				<span>engineID</span>
				<input name="engineID" value="<?php echo $temp['engineID'];?>"></input>
			</div>
			<div>
				<span>recordType</span>
				<input name="recordType" value="<?php echo $temp['recordType'];?>"></input>
			</div>
			<div>
				<span>recordDescription</span>
				<input name="recordDescription" value="<?php echo $temp['recordDescription'];?>"></input>
			</div>
			<div>
				<span>points</span>
				<input name="points" value="<?php echo $temp['points'];?>"></input>
			</div>
			<div>
				<span>fine</span>
				<input name="fine" value="<?php echo $temp['fine'];?>"></input>
			</div>
			<div>
				<span>finePayDue</span>
				<input name="finePayDue" value="<?php echo $temp['finePayDue'];?>"></input>
			</div>
			<div>
				<span>recordPic</span>
				<input name="file" type="file"></input>
				<input name="recordPic" type="hidden" value="<?php echo $temp['recordPic'];?>"></input>
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