$(document).ready(function()
{
	$("#account").focus(function()
	{
		var a=$("#account").val();
		if(a == "请输入账号")
		{
			$("#account").val("");
		}
	});
	$("#account").blur(function()
	{
		var a=$("#account").val();
		if(a.length == 0)
		{
			$("#account").val("请输入账号");
		}
	});
	$("#password").focus(function()
	{
		var a=$("#password").val();
		if(a == "请输入密码")
		{
			$("#password").hide();
			$("#password2").show();
			$("#password2").focus();
		}
	});
	$("#password2").blur(function()
	{
		var a=$("#password2").val();
		if(a.length == 0)
		{
			$("#password2").hide();
			$("#password").show();
		}
	});
});
var checkValid = function()
	{
		var content = $("#content").val().length;
		var b = $("#password2").val().length;
		if(a==0)
		{
			alert("用户名不得为空！");
			return false;
		}
		else if(b==0)
		{
			alert("密码不得为空！");
			return false;
		}
		else
			return true;
	}