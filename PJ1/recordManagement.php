<?php
	session_start();
	header("Content-Type: text/html; charset=utf-8");
	if(!isset($_SESSION['accountType']) || ($_SESSION['accountType'] != 3 && $_SESSION['accountType'] != 0))
	{
		header('Location: index.php');
	}

	include("loginConfigure.php");
	$result = mysql_query("SELECT * FROM pj1.recordmanagement");
	$ret[] = array();
	$num = 0;
  	while($row = mysql_fetch_array($result))
  	{
  		$temp[] = array();
  		
  		$temp['recordID'] = $row['recordID'];
  		$temp['ownerID'] = $row['ownerID'];
  		$temp['carID'] = $row['carID'];
  		$temp['engineID'] = $row['engineID'];
  		$temp['recordType'] = $row['recordType'];
  		$temp['recordDescription'] = $row['recordDescription'];
  		$temp['points'] = $row['points'];
  		$temp['fine'] = $row['fine'];
  		$temp['finePayDue'] = $row['finePayDue'];
  		$temp['recordPic'] = $row['recordPic'];
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
			<table class="table" border="3">
				<tr>
					<td><input type="checkbox" id="deleteWho" value="-1"></input></td>
					<td>recordID</td>
					<td>ownerID</td>
					<td>carID</td>
					<td>engineID</td>
					<td>recordType</td>
					<td>recordDescription</td>
					<td>points</td>
					<td>fine</td>
					<td>finePayDue</td>
					<td>recordPic</td>
					<td>operation</td>
				</tr>
				<?php
					for($i = 0; $i < count($ret); $i++)
					{
				?>
						<tr>
							<td><input type="checkbox" name="deleteIt" value="<?php echo $ret[$i]['recordID'];?>"></input></td>
							<td><?php echo $ret[$i]['recordID'];?></td>
							<td><?php echo $ret[$i]['ownerID'];?></td>
							<td><?php echo $ret[$i]['carID'];?></td>
							<td><?php echo $ret[$i]['engineID'];?></td>
							<td><?php echo $ret[$i]['recordType'];?></td>
							<td><?php echo $ret[$i]['recordDescription'];?></td>
							<td><?php echo $ret[$i]['points'];?></td>
							<td><?php echo $ret[$i]['fine'];?></td>
							<td><?php echo $ret[$i]['finePayDue'];?></td>
							<td>
								<img src="<?php echo $ret[$i]['recordPic'];?>" alt="RecordPic"></img>
							</td>
							<td>
							<form action="recordUpdate.php" method="post">
								<input type="hidden" name="recordID" value="<?php echo $ret[$i]['recordID'];?>"></input>
								<input type="submit" value="update"></input>
							</form>
							<form action="recordDelete.php" method="post">
								<input type="hidden" name="recordID" value="<?php echo $ret[$i]['recordID'];?>"></input>
								<input type="submit" value="delete"></input>
							</form>
							</td>
						</tr>
				<?php
					}
				?>
			</table>
			<input type="button" value="insert" class="insertRecord"></input>
			<input type="button" value="deleteMore" class="deleteMoreRecord"></input>
		</div>
		<div class="backBottomDiv">
		</div>
	</div>
</body>
</html>