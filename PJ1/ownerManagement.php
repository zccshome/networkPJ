<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 2 && $_SESSION['accountType'] != 1 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}

	include("loginConfigure.php");
	$result = mysql_query("SELECT * FROM pj1.ownermanagement");
	$ret[] = array();
	$num = 0;
  	while($row = mysql_fetch_array($result))
  	{
  		$temp[] = array();
  		
  		$temp['ownerID'] = $row['ownerID'];
  		$temp['ownerName'] = $row['ownerName'];
  		$temp['nationality'] = $row['nationality'];
  		$temp['ownerAddress'] = $row['ownerAddress'];
  		$temp['licenseID'] = $row['licenseID'];
  		$temp['points'] = $row['points'];
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
			<table class="table" border="3">
				<tr>
					<td><input type="checkbox" id="deleteWho" value="-1"></input></td>
					<td>ownerID</td>
					<td>ownerName</td>
					<td>nationality</td>
					<td>ownerAddress</td>
					<td>licenseID</td>
					<td>points</td>
					<td>operation</td>
				</tr>
				<?php
					for($i = 0; $i < count($ret); $i++)
					{
				?>
						<tr>
							<td><input type="checkbox" name="deleteIt" value="<?php echo $ret[$i]['ownerID'];?>"></input></td>
							<td><?php echo $ret[$i]['ownerID'];?></td>
							<td><?php echo $ret[$i]['ownerName'];?></td>
							<td><?php echo $ret[$i]['nationality'];?></td>
							<td><?php echo $ret[$i]['ownerAddress'];?></td>
							<td><?php echo $ret[$i]['licenseID'];?></td>
							<td><?php echo $ret[$i]['points'];?></td>
							<td>
							<form action="ownerUpdate.php" method="post">
								<input type="hidden" name="ownerID" value="<?php echo $ret[$i]['ownerID'];?>"></input>
								<input type="submit" value="update"></input>
							</form>
							<form action="ownerDelete.php" method="post">
								<input type="hidden" name="ownerID" value="<?php echo $ret[$i]['ownerID'];?>"></input>
								<input type="submit" value="delete"></input>
							</form>
							</td>
						</tr>
				<?php
					}
				?>
			</table>
			<input type="button" value="insert" class="insertOwner"></input>
			<input type="button" value="deleteMore" class="deleteMoreOwner"></input>
		</div>
		<div class="backBottomDiv">
		</div>
	</div>
</body>
</html>