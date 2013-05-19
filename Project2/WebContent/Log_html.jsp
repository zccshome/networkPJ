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
<%@ page language="java" import="java.util.*, Project2.bll.*" pageEncoding="UTF-8"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
	<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
	<meta name="name" content="Zhu Chengchun"/>
	<meta name="security" content="low"/>
	<title>Login</title>
	<link rel="stylesheet" type="text/css" href="CSS/Log_CSS.css" />
	<script type="text/javascript" src="Jcript/jquery.js"> </script>
	<script src="Jcript/login.js" language="javascript" > </script>
	<script type="text/javascript">
	if(1==<%=wrong%>)
	{
		alert("用户名或密码错！");
		wrong = 0;
	}
	$(document).ready(function()
	{
	$("[throwout]").mouseover(function()
	{
		var that = $(this);
		var id = that.attr("throwout");
		var nickname = that.attr("nickname");
		var fans = that.attr("fans");
		var offset=that.offset();
		var width=offset.left+that.width();
		var height=offset.top;
		$(".nameSpace").show(500);
		$(".nameSpace").css("left",width);
		$(".nameSpace").css("top",height);
		$(".nameSpacet img").attr("src","Pic/"+id+".png");
		$(".nameSpacen").html(nickname);
		$(".nameSpacej").html(fans+"级");
		$.post("getFriendServlet",{id: id},
		function(data)
		{
			$(".friend").empty().html(data);
		})
	});
	$(".nameSpace").mouseout(function()
	{
		$(".nameSpace").hide(500);
	});
	});
	</script>
</head>
<body class="body">
<%
	ArrayList<String[]> hotList = UserAction.getHot();
	ArrayList<String[]> newList = UserAction.getNew();
%>
	<!-- 这个是整体的DIV框-->
	<div class="wholeWindow">
		<%@ include file="header.jsp"%>
		<!-- 这个是主界面的框，它和背景框是重合的-->
		<div class="mainWindow">
			<!-- 这个是登录的DIV框-->
			<div class="loginWindow">
				<!-- fieldset里面就是所有的登录的组件 -->
				<fieldset>
					<legend>
						<span class="legend">登录界面：</span>
					</legend>
					<br/>
					<form name="input" action="LoginServlet" method="post" onsubmit="return checkValid();">
						<span class="word">账号</span>
						<input type="text" name="account" class="textfield" id="account" value="请输入账号"/>
						<br>&nbsp </br>
						<span class="word">密码</span>
						<input type="text" name="password1" class="textfield" id="password" value="请输入密码"/>
						<input type="password" name="password" class="textfield2" id="password2"/>
						<br>&nbsp </br>
						<div class="word">
							<input type="checkbox" name="isPassword"/>
							<span>下次自动登录</span>
						</div>
						<a href="http://blog.sina.com.cn/zhuchengchun"/>
							<span name="forget" class="forget">忘记密码？</span>
						</a>
						<br>&nbsp </br>
						<div>
							<img src="Reg_image/hungeline.gif" class="login_pic1"/>
							<img src="Reg_image/hungeline.gif" class="login_pic2"/>
							<input name="submit" class="loginButton" type="image" src="Reg_image/ok.png" value="">
						</div>
					</form>
				</fieldset>
				<br>&nbsp </br>
			</div>
			<!-- 这个是注册块的div框-->
			<div class="regWindow">
				<img src="Reg_image/man.png" class="regImageLeft"/>
				<img src="Reg_image/girl.png" class="regImageRight"/>
				<div>
					<form name="input" action="" method="get">
						<p class="noAccount">还没有账号？</p>
						<a href="Reg_html.jsp"/>
							<img src="Reg_image/reg.png" class="registerImage"/>
						</a>
					</form>
				</div>
			</div>
			<!-- 这个是下部所有的框，用于放热门用户、新用户以及新鲜事 -->
			<div class="nameWindow">
				<!-- 这个是热门用户的DIV框-->
				<div class="hot">
					<!-- 热门用户的标题 -->
					<div class="hotTitle">
						<div class="hotT1">
							热门用户
						</div>
						<div class="hotT2">
							粉丝数
						</div>
					</div>
					<br> &nbsp </br>
					<!-- 具体的一行的热门用户信息，包括排名、头像、名字和粉丝数-->
					<%
						if (hotList != null)
						{
						for(int i = 0; i < hotList.size(); i++)
						{
					%>
					<div class="hotOne" id="hotOne">
						<span class="hot1">
							<img src="Reg_image/<%=i+1 %>.png"/>
						</span>
						<span class="hot2" id="hotnum1" throwout="<%=hotList.get(i)[2] %>"nickname="<%=hotList.get(i)[0] %>" fans="<%=hotList.get(i)[1] %>">
							<a href="Main.jsp?id=<%=hotList.get(i)[2] %>&type=2&page=1">
								<img src="Pic/<%=hotList.get(i)[2] %>.png"/>
							</a>
						</span>
						<span class="hot3">
							<a href="Main.jsp?id=<%=hotList.get(i)[2] %>&type=2&page=1">
								<%=hotList.get(i)[0] %>
							</a>
						</span>
						<span class="hot4">
							<span><%=hotList.get(i)[1] %></span>
						</span>
					</div>
					<br>&nbsp </br>
					<%
						}}
					%>
					<!-- <div class="hotOne">
						<span class="hot1">
							<img src="Reg_image/2.png"/>
						</span>
						<span class="hot2" id="hotnum2">
							<a href="">
							<img src="Hot/2.png"/>
							</a>
						</span>
						<span class="hot3">
							<a href="">Rain
							</a>
						</span>
						<span class="hot4">
							<span>1270</span>
						</span>
					</div>
					<br>&nbsp </br>
					<div class="hotOne">
						<div class="hot1">
							<img src="Reg_image/3.png"/>
						</div>
						<div class="hot2" id="hotnum3">
							<a href="">
							<img src="Hot/3.png"/>
							</a>
						</div>
						<div class="hot3">
							<a href="">zccshome</a>
						</div>
						<div class="hot4">
							<span>1260</span>
						</div>
					</div>
					<br>&nbsp </br>
					<div class="hotOne">
						<div class="hot1">
							<img src="Reg_image/4.png"/>
						</div>
						<div class="hot2" id="hotnum4">
							<a href="">
							<img src="Hot/4.png"/>
							</a>
						</div>
						<div class="hot3">
							<a href="">aynimuous</a>
						</div>
						<div class="hot4">
							<span>980</span>
						</div>
					</div>
					<br>&nbsp </br>
					<div class="hotOne">
						<div class="hot1">
							<img src="Reg_image/5.png"/>
						</div>
						<div class="hot2" id="hotnum5">
							<a href="">
							<img src="Hot/5.png"/>
							</a>
						</div>
						<div class="hot3">
							<a href="">wangxinalex</a>
						</div>
						<div class="hot4">
							<span>970</span>
						</div>
					</div>
					<br>&nbsp </br>
					<div class="hotOne">
						<div class="hot1">
							<img src="Reg_image/6.png"/>
						</div>
						<div class="hot2" id="hotnum6">
							<a href="">
							<img src="Hot/6.png"/>
							</a>
						</div>
						<div class="hot3">
							<a href="">horz</a>
						</div>
						<div class="hot4">
							<span>960</span>
						</div>
					</div>-->
				</div>
			<!-- 这个是新用户的的DIV框-->
			<div class="newWindow">
			<div class="new">
				<div class="newTitle">
					新注册的用户
				</div>
				<br/>
				<!-- 这是一行新用户的的DIV框，包括两个新用户的div框-->
				<%
					for(int i = 0; i < newList.size(); i+=2)
					{
				%>
				<div class="newPerson">
					<!-- 这是一个新门用户的的DIV框，包括了上面的头像和下面的名字-->
					<div class="newBlock">
						<div class="newBlock2">
							<a href="Main.jsp?id=<%=newList.get(i)[1]%>&type=2&page=1"><img src="Pic/<%=newList.get(i)[1]%>.png"/></a>
							<div><a href="Main.jsp?id=<%=newList.get(i)[1]%>&type=2&page=1"><%=newList.get(i)[0]%></a></div>
						</div>
					</div>
					<%
					if((i+1) == newList.size()){%>
					</div>
					<%
						break;}
					%>
					<div class="newBlock">
						<div class="newBlock2">
							<a href="Main.jsp?id=<%=newList.get(i+1)[1]%>&type=2&page=1"><img src="Pic/<%=newList.get(i+1)[1]%>.png"/></a>
							<div><a href="Main.jsp?id=<%=newList.get(1+i)[1]%>&type=2&page=1"><%=newList.get(i+1)[0]%></a></div>
						</div>
					</div>
				</div>
				<br> &nbsp </br>
				<!--<div class="newPerson">
					<div class="newBlock">
						<div class="newBlock2">
							<a href=""><img src="New/none.png"/></a>
							<div><a href="">xy</a></div>
						</div>
					</div>
					<div class="newBlock">
						<div class="newBlock2">
							<a href=""><img src="New/4.png"/></a>
							<div><a href="">荡剑回枫</a></div>
						</div>
					</div>
				</div>
				<br> &nbsp </br>
				<div class="newPerson">
					<div class="newBlock">
						<div class="newBlock2">
							<a href=""><img src="New/none.png"/></a>
							<div><a href="">anonymous</a></div>
						</div>
					</div>
					<div class="newBlock">
						<div class="newBlock2">
							<a href=""><img src="New/none.png"/></a>
							<div><a href="">李弋大帝</a></div>
						</div>
					</div>
				</div>
				<br> &nbsp </br>
				<div class="newPerson">
					<div class="newBlock">
						<div class="newBlock2">
							<a href=""><img src="New/none.png"/></a>
							<div><a href="">fishhan</a></div>
						</div>
					</div>
					<div class="newBlock">
						<div class="newBlock2">
							<a href=""><img src="New/8.png"/></a>
							<div><a href="">Iris</a></div>
						</div>
					</div>
				</div>  -->
				<%}%>
			</div>
			</div>
				<!-- 这个是中间的新鲜事，每一个下属div都是一条微博的框-->
				<div class="picWindow">
					<div class="titlePic">看看别人都在干什么喵。。。</div>
					<div class="textWeibo">
						<div class="picWeibo">
							<a href="../Project2/Log_html.jsp"><img src="New/2.png"/></a>
						</div>
						<div class="nameWeibo">
							<a href="../Project2/Log_html.jsp">喵喵洋：</a>易大传：“天下一致而百虑，同归而殊途。”
						</div>
						<div class="infoWeibo">
							<span>今天 24:00</span>
							<span class="zhuanfa"><a>转发：</a>10000</span>
							<span class="zhuanfa"><a>评论：</a>10000</span>
						</div>
					</div>
					<div class="textWeibo">
						<div class="picWeibo">
							<a href="../Project2/Log_html.jsp"><img src="New/2.png"/></a>
						</div>
						<div class="nameWeibo">
							<a href="../Project2/Log_html.jsp">喵喵洋：</a>上传了54张照片至相册<a href="../Project2/Log_html.jsp">《BY2》</a>
						</div>
						<div class="infoWeibo">
							<span>今天 24:00</span>
							<span class="zhuanfa"><a>转发：</a>10000</span>
							<span class="zhuanfa"><a>评论：</a>10000</span>
						</div>
						<div class="pic">
							<img src="By2/1.png"/>
							<img src="By2/2.png"/>
							<img src="By2/3.png"/>
							<img src="By2/next.png" id="changePic"/>
						</div>
					</div>
					<div class="textWeibo">
						<div class="picWeibo">
							<a href="../Project2/Log_html.jsp"><img src="New/2.png"/></a>
						</div>
						<div class="nameWeibo">
							<a href="../Project2/Log_html.jsp">喵喵洋：</a>发表了日志<a href="">《喵的108种叫法》</a>
						</div>
						<div class="infoWeibo">
							<span>今天 24:00</span>
							<span class="zhuanfa"><a>转发：</a>10000</span>
							<span class="zhuanfa"><a>评论：</a>10000</span>
						</div>
					</div>
					<div class="textWeibo">
						<div class="picWeibo">
							<a href="../Project2/Log_html.jsp"><img src="New/2.png"/></a>
						</div>
						<div class="nameWeibo">
							<a href="../Project2/Log_html.jsp">喵喵洋：</a>形神骚动，欲与天地长久，非所闻也。
						</div>
						<div class="zhuanfaweibo">
							<div class="nameWeibo2">
								<a href="../Project2/Log_html.jsp">喵喵洋：</a>易大传：“天下一致而百虑，同归而殊途。”
							</div>
						</div>
						<div class="infoWeibo">
							<span class="zhuanfatime">今天 24:00</span>
							<span class="zhuanfa"><a>转发：</a>10000</span>
							<span class="zhuanfa"><a>评论：</a>10000</span>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- 这个是鼠标移到热门用户头像上会弹出的隐藏div框-->
		<div class="nameSpace" id="nameSpace">
			<a href="" class="nameSpacet">
				<img src="Hot/1.png"/>
			</a>
			<span class="nameSpacen">徐天宇爱卖萌</span>
			<span class="nameSpacej">20级</span>
			<div class="mutualFriends">
				TA的好友:
			</div>
			<div class="friend">
			</div>
		</div>
	</div>
</body>
</html>