<%@ page language="java" import="java.util.*" pageEncoding="UTF-8"%>
<%@ page import="Project2.bll.*, java.util.*" %>
<%
	User user = (User)request.getSession().getAttribute("user");
	if(user == null)
	{
		response.sendRedirect("Log_html.jsp");
		return;
	}
%>
<%
	int id2 = ((User)session.getAttribute("user")).getId();
	int id = id2;
	try
	{
		id = Integer.parseInt(request.getParameter("id"));
		if(UserAction.getNickNameById(id).equals(""))
			id = id2;
	}
	catch(Exception e)
	{
	
	}
	boolean isFriend = UserAction.isFriend(id2, id);
	ArrayList<String[]> newList = UserAction.getFriend(id);
	String nickname = UserAction.getNickNameById(id);
	String comefrom = UserAction.getcomefromById(id);
	int friendNumber = UserAction.getFriendNumber(id);
	int fanNumber = UserAction.getFanNumber(id);
	int weiboNumber = UserAction.getWeiboNumber(id);
	ArrayList<String[]> personInfos = new ArrayList<String[]>();
	try
	{
		personInfos = (ArrayList<String[]>)session.getAttribute("personInfo");
		session.removeAttribute("personInfo");
	}
	catch(Exception e)
	{
		
	}
	
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
	<meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
	<meta name="name" content="Zhu Chengchun"/>
	<meta name="security" content="low"/>
	<title>Person</title>
	<link rel="stylesheet" type="text/css" href="CSS/Person_CSS.css" />
<!-- 	<script type="text/javascript" src="Jcript/jquery.js"> </script> -->
	<script type="text/javascript" src="Jcript/readPerson.js"> </script>
</head>
<body class="body" id="body">
	<!-- 这个是整体的DIV框-->
	<div class="wholeWindow">
		<%@ include file="header2.jsp"%>
		<%@ include file="search.jsp"%>
		<!-- 这个是主要的大微博区DIV框-->
		<div class="mainWindow">
			<!-- 这个是微博区DIV框-->
			<div class="weiboWindow">
				<div class="paixu">
					<ul>
						<li>排序方式:</li>
						<li><a href="Person.jsp?id=<%=id%>">关注时间</a></li>
						<li><a href="Person.jsp?id=<%=id%>">最近联系</a></li>
						<li><a href="Person.jsp?id=<%=id%>">粉丝数</a></li>
					</ul>
				</div>
				<div class="listPerson">
					<ul id="listPersons">
					<%if(personInfos != null){
					for(int i = 0; i < personInfos.size(); i++){%>
						<li>
							<a href="Main.jsp?id=<%=personInfos.get(i)[0] %>&type=2&page=1"><img src="Pic/<%=personInfos.get(i)[0] %>.png" class="personImage"/></a>
							<div class="personData">
								<a href="Main.jsp?id=<%=personInfos.get(i)[0] %>&type=2&page=1"><div class="personName"><%=personInfos.get(i)[1] %></div></a>
								<span class="personFrom"><%=personInfos.get(i)[2] %></span>
								<span class="fansNum"><%=personInfos.get(i)[3] %></span>
								<div class="latest">
									<span class="Fweibo">
									<%=personInfos.get(i)[4] %>
									</span>
								</div>
							</div>
						</li>
						<%}} %>
						<!-- <li>
							<img src="Hot/4.png" class="personImage"/>
							<div class="personData">
								<div class="personName">hackerqian</div>
								<span class="personFrom">云南市</span>
								<span class="fansNum">100</span>
								<div class="latest">
									<span class="Fweibo">
									当我不在你身边，答应我用心去飞，生命若有新体验，你别拒绝；当我不在你身边，寂寞若是让你累，
回头看一下一切也是安慰（今天12：00）
									</span>
								</div>
							</div>
						</li>
						<li>
							<img src="New/none.png" class="personImage"/>
							<div class="personData">
								<div class="personName">hackersun</div>
								<span class="personFrom">南充市</span>
								<span class="fansNum">100</span>
								<div class="latest">
									<span class="Fweibo">
									We are one!（今天12：00）
									</span>
								</div>
							</div>
						</li>
						<li>
							<img src="Hot/5.png" class="personImage"/>
							<div class="personData">
								<div class="personName">hackerli</div>
								<span class="personFrom">乌鲁木齐市</span>
								<span class="fansNum">100</span>
								<div class="latest">
									<span class="Fweibo">
									2002年的第一场雪，比以往时候来得更晚一些。。。（今天12：00）
									</span>
								</div>
							</div>
						</li>-->
					</ul>
				</div>
			</div>
			<%@ include file="personInfo.jsp"%>
			<%@ include file="footer.jsp"%>
		</div>
	</div>
	<div id="zcc"></div>
</body>
