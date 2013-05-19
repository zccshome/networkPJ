<%
	int wrong = 0;
	try
	{
		wrong = (Integer)request.getSession().getAttribute("wrong");
		request.getSession().removeAttribute("wrong");
	}
	catch(Exception e)
	{}
%>
<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
	<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
	<meta name="name" content="Zhu Chengchun"/>
	<meta name="security" content="low"/>
	<title>Reg</title>
	<link rel="stylesheet" type="text/css" href="CSS/Reg_CSS.css" />
	<script type="text/javascript" src="Jcript/checkReg.js"> </script>
	<script type="text/javascript">
	if(1==<%=wrong%>)
	{
		alert("用户名重复，请重新注册！");
		wrong = 0;
	}
	else if(2==<%=wrong%>)
	{
		alert("数据库故障，请稍候再试！");
		wrong = 0;
	}
	</script>
</head>
<body class="static">
	<!-- 这个是整个的DIV框-->
	<div class="wholeWindow">
		<%@ include file="header3.jsp"%>
		<!-- 这个是注册界面的主DIV框，包括两栏，左边是文字右边是输入的input-->
		<div class="mainWindow">
		<div class="regWindow">
			<form class="rWindow" name="input" action="RegServlet" method="post" onsubmit="return checkAll()">
				<div class="headReg">
					<div class="headName">
						用户注册
					</div>
				</div>
				<br/>
				<div>
					<div class="left">
						用户名
					</div>
					<div class="left">
						<input type="text" name="account" maxlength="20" id="TxtUsername" onblur="checkMail()"/>
					</div>
					<div class="right" id="mailMes">
					</div>
				</div>
				<br> &nbsp </br>
				<div>
					<div class="left">
						昵称
					</div>
					<div class="left">
						<input type="text" name="nickname" maxlength="20" id="TxtNickname" onfocus="checkMail()" onblur="checkNickName()"/>
					</div>
					<div class="right" id="nameMes">
					</div>
				</div>
				<br> &nbsp </br>
				<div>
					<div class="left">
						创建密码
					</div>
					<div class="left">
						<input type="password" name="password1" maxlength="20" id="TxtPassword1" onfocus="checkNickName()" onblur="checkPass()" onkeyup="checkPass()" onkeydown="checkPass()"/>
					</div>
					<div class="right" id="passwdMes">
					</div>
				</div>
				<br> &nbsp </br>
				<div>
					<div class="left">
						确认密码
					</div>
					<div class="left">
						<input type="password" name="password2" maxlength="20" id="TxtPassword2" onfocus="checkPass()" onblur="checkConfirm()"/>
					</div>
					<div class="right" id="confirmwdMes">
					</div>
				</div>
				<br> &nbsp </br>
				<div>
					<div class="left">
						来自
					</div>
					<div class="left">
						<input type="text" name="from" maxlength="20" id="TxtComeFrom" onfocus="checkConfirm()" onblur="checkFrom()"/>
					</div>
					<div class="right" id="fromMes">
					</div>
				</div>
				<br> &nbsp </br>
				<div class="checkInBar">
				<div class="leftBox">
					<input type="checkbox" name="checkbox"/>
					<span> 我已阅读并同意 <a href="http://blog.sina.com.cn/zhuchengchun">微博使用协议</a> </span>
				</div>
				<br> &nbsp </br>
				<div class="center">
					<input type="submit" name="submit" value="确认注册" id="BtnSubmit" onfocus="checkAll()" onclick="checkAll()"/>
				</div>
				<div class="gotoLogin">
					<div>
						<a href="Log_html.jsp">已有账号，去登陆？</a>
					</div>
				</div>
				</div>
			</form>
		</div>
	</div>
</body>
</html>