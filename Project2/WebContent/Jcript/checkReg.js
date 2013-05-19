// JavaScript Document
/**
* 检查邮箱的输入有效性
**/
function checkMail(){
	var mailValue = document.getElementById("TxtUsername").value;
	//var mailFormat = /^([a-z0-9_]){1,}@([a-z0-9_]){1,}\.([a-z0-9_]){1,}$/;
	var mailFormat = /^([a-z0-9_]){1,}$/;
	var mailMes = document.getElementById("mailMes");
	if(mailValue.length == 0)
	{
		mailMes.innerHTML = "用户名不能为空";
		mailMes.style.color = "red";
	}
	else if(!mailFormat.test(mailValue))
	{
		mailMes.innerHTML="用户名格式错误";
		mailMes.style.color = "red";
	}
	else if(mailValue.length<2||mailValue.length>14)
	{
		mailMes.innerHTML="用户名长度为2~14位";
		mailMes.style.color = "red";
	}
	else
	{
		mailMes.innerHTML="正确";
		mailMes.style.color = "lime";
		return 1;
	}
	return 0;
}
/**
* 检查昵称的输入有效性
**/
function checkNickName(){
	var nickValue = document.getElementById("TxtNickname").value;
	var nickMes = document.getElementById("nameMes");
	if(nickValue.length == 0)
	{
		nickMes.innerHTML = "昵称不能为空";
		nickMes.style.color = "red";
	}
	else
	{
		nickMes.innerHTML="正确";
		nickMes.style.color = "lime";
		return 1;
	}
	return 0;
}
/**
* 检查密码的输入有效性
**/
function checkPass(){
	var passValue = document.getElementById("TxtPassword1").value;
	var passMes = document.getElementById("passwdMes");
	var passFormat = /^([0-9]){1,}$/;
	if(passValue.length == 0)
	{
		passMes.innerHTML = "密码不能为空";
		passMes.style.color = "red";
	}
	else if(passValue.length<7)
	{
		passMes.innerHTML="密码长度不能小于等于6位";
		passMes.style.color = "red";
	}
	else if(passFormat.test(passValue))
	{
		passMes.innerHTML="密码不得为纯数字";
		passMes.style.color = "red";
	}
	else
	{
		checkPass1();
		return 1;
	}
	return 0;
}
/**
* 实时检查检查密码强度
**/
function checkPass1(){
	var passValue = document.getElementById("TxtPassword1").value;
	var passMes = document.getElementById("passwdMes");
	if(passValue.length>=7&&passValue.length<=10)
	{
		passMes.innerHTML="<span id='text'>密码强度：</span><span id='text2'>弱</span>";
		document.getElementById("text").style.color = "lime";
		document.getElementById("text2").style.color = "yellow";
	}
	else if(passValue.length>=11&&passValue.length<=14)
	{
		passMes.innerHTML="<span id='text'>密码强度：</span><span id='text2'>中</span>";
		document.getElementById("text").style.color = "lime";
		document.getElementById("text2").style.color = "green";
	}
	else if(passValue.length>=15)
	{
		passMes.innerHTML="<span id='text'>密码强度：</span><span id='text2'>强</span>";
		document.getElementById("text").style.color = "lime";
		document.getElementById("text2").style.color = "lime";
	}
}
/**
* 检查确认密码
**/
function checkConfirm(){
	var passValue = document.getElementById("TxtPassword1").value;
	var passValue2 = document.getElementById("TxtPassword2").value;
	var passMes = document.getElementById("confirmwdMes");
	if(passValue2.length == 0)
	{
		passMes.innerHTML = "确定密码不能为空";
		passMes.style.color = "red";
	}
	else if(!(passValue2 == passValue))
	{
		passMes.innerHTML = "两次密码不一致";
		passMes.style.color = "red";
	}
	else
	{
		passMes.innerHTML="正确";
		passMes.style.color = "lime";
		return 1;
	}
	return 0;
}
/**
* 检查来处
**/
function checkFrom(){
	var fromValue = document.getElementById("TxtComeFrom").value;
	var fromMes = document.getElementById("fromMes");
	if(fromValue.length == 0)
	{
		fromMes.innerHTML = "来自不能为空";
		fromMes.style.color = "red";
	}
	else
	{
		fromMes.innerHTML="正确";
		fromMes.style.color = "lime";
		return 1;
	}
	return 0;
}
/**
* 检查所有文本输入框的有效性
**/
function checkAll(){
	if(checkMail()==1&&checkNickName()==1&&checkPass()==1&&checkConfirm()==1&&checkFrom()==1)
		//alert("注册成功");
		return true;
	else
		return false;
}