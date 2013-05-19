$(document).ready(function()
{
	if(<%=wrong%>=="zhuanfawrong")
	{
		wrong = "";
		alert("转发微博出现错误！");
	}
	else if(<%=wrong%>=="addwrong")
	{
		wrong = "";
		alert("加好友出现错误！");
	}
	else if(<%=wrong%>=="deletewrong")
	{
		wrong = "";
		alert("删好友出现错误！");
	}
	else if(<%=wrong%>=="fabuwrong")
	{
		wrong = "";
		alert("发布微博出现错误！");
	}
	else if(<%=wrong%>=="pinglunwrong")
	{
		wrong = "";
		alert("发布评论出现错误！");
	}
	else if(<%=wrong%>=="picwrong2")
	{
		wrong = "";
		alert("请选择图片！");
	}
	else if(<%=wrong%>=="picwrong1")
	{
		wrong = "";
		alert("图片格式只能是png！");
	}
	else if(<%=wrong%>=="picwrong")
	{
		wrong = "";
		alert("上传图片出现错误！");
	}
}