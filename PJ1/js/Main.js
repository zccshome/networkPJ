$(document).ready(function()
{
	var height = $(".foreCenterDiv").height();
	var num = height / 128;
	for(var i = 1; i < num; i++)
	{
		$(".backCenterDiv").append("<img src='./Pic/BackPic/Back_center.jpg' alt='backCenterPic'></img>");
	};
	$(".backTopDiv :first-child").click(function()
	{
		var turn = $(".backTopDiv :first-child").attr("title");
		if(turn == "title")
		{
			$(".backTopDiv :first-child").attr("src","./Pic/BackPic/Back_title_alt.png");
			$(".backTopDiv :first-child").attr("title", "title_alt");
		}
		else
		{
			$(".backTopDiv :first-child").attr("src","./Pic/BackPic/Back_title.png");
			$(".backTopDiv :first-child").attr("title", "title");
		}
	});
});