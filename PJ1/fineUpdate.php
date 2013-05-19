<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 4 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}
	if(isset($_POST['fineID']))
	{
		$fineID = $_POST['fineID'];
		include("loginConfigure.php");
		$result = mysql_query("SELECT * FROM pj1.finemanagement WHERE fineID = '$fineID'");
  		if($row = mysql_fetch_array($result))
  		{
  			$temp[] = array();
  		
  			$temp['fineID'] = $row['fineID'];
  			$temp['ownerID'] = $row['ownerID'];
  			$temp['fineDate'] = $row['fineDate'];
  			$temp['fineAmount'] = $row['fineAmount'];
  			$temp['finePaid'] = $row['finePaid'];
  			$temp['finePaidDate'] = $row['finePaidDate'];
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
	<script language="javascript" type="text/javascript" src="js/Fine.js"> </script>
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
		<form action="fineUpdateSQL.php" method="post">
			<div>
				<span>fineID</span>
				<input readonly="true" name="fineID" value="<?php echo $temp['fineID'];?>"></input>
			</div>
			<div>
				<span>ownerID</span>
				<input name="ownerID" value="<?php echo $temp['ownerID'];?>"></input>
			</div>
			<div>
				<span>fineDate</span>
				<input name="fineDate" value="<?php echo $temp['fineDate'];?>"></input>
			</div>
			<div>
				<span>fineAmount</span>
				<input name="fineAmount" value="<?php echo $temp['fineAmount'];?>"></input>
			</div>
			<div>
				<span>finePaid</span>
				<input name="finePaid" value="<?php echo $temp['finePaid'];?>"></input>
			</div>
			<div>
				<span>finePaidDate</span>
				<input name="finePaidDate" value="<?php echo $temp['finePaidDate'];?>"></input>
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