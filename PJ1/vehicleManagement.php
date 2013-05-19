<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}

	include("loginConfigure.php");
	$result = mysql_query("SELECT * FROM pj1.vehiclemanagement");
	$ret[] = array();
	$num = 0;
  	while($row = mysql_fetch_array($result))
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
  		$ret[$num] = $temp;
  		$num++;
  	}
  	mysql_close($con);
?>
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title>ZCC's Home</title>
	<!--<meta http-equiv="refresh" content="6000" />-->
	<meta name="security" content="low" />
	<link href="css/VMSelect.css" rel="stylesheet" type="text/css" />
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
			<table class="table" border="3">
				<tr>
					<td><input type="checkbox" id="deleteWho" value="-1"></input></td>
					<td>carID</td>
					<td>engineID</td>
					<td>ownerID</td>
					<td>instituteID</td>
					<td>carNumber</td>
					<td>carType</td>
					<td>carColor</td>
					<td>carFrom</td>
					<td>buyTime</td>
					<td>vehiclePic</td>
					<td>operation</td>
				</tr>
				<?php
					for($i = 0; $i < count($ret); $i++)
					{
				?>
						<tr>
							<td><input type="checkbox" name="deleteIt" value="<?php echo $ret[$i]['carID'];?>"></input></td>
							<td><?php echo $ret[$i]['carID'];?></td>
							<td><?php echo $ret[$i]['engineID'];?></td>
							<td><?php echo $ret[$i]['ownerID'];?></td>
							<td><?php echo $ret[$i]['instituteID'];?></td>
							<td><?php echo $ret[$i]['carNumber'];?></td>
							<td><?php echo $ret[$i]['carType'];?></td>
							<td><?php echo $ret[$i]['carColor'];?></td>
							<td><?php echo $ret[$i]['carFrom'];?></td>
							<td><?php echo $ret[$i]['buyTime'];?></td>
							<td>
								<img src="<?php echo $ret[$i]['vehiclePic'];?>" alt="CarPic"></img>
							</td>
							<td>
							<form action="vehicleUpdate.php" method="post">
								<input type="hidden" name="carID" value="<?php echo $ret[$i]['carID'];?>"></input>
								<input type="submit" value="update"></input>
							</form>
							<form action="vehicleDelete.php" method="post">
								<input type="hidden" name="carID" value="<?php echo $ret[$i]['carID'];?>"></input>
								<input type="submit" value="delete"></input>
							</form>
							</td>
						</tr>
				<?php
					}
				?>
			</table>
			<input type="button" value="insert" class="insertVehicle"></input>
			<input type="button" value="deleteMore" class="deleteMoreVehicle"></input>
		</div>
		<div class="backBottomDiv">
		</div>
	</div>
</body>
</html>