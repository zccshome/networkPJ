<%@ page language="java" contentType="text/html"  pageEncoding="UTF-8"%>
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
	int type=2;
	try
	{
		type = Integer.parseInt(request.getParameter("type"));
	}
	catch(Exception e)
	{
		
	}
	String wrong = "";
	try
	{
		wrong = ((String)session.getAttribute("wrong1"));
		request.getSession().removeAttribute("wrong1");
	}
	catch(Exception e)
	{
		
	}
	boolean isFriend = UserAction.isFriend(id2, id);
	ArrayList<String[]> newList = UserAction.getFriend(id);
	String nickname = UserAction.getNickNameById(id);
	String comefrom = UserAction.getcomefromById(id);
	//String nickname = ((User)session.getAttribute("user")).getNickname();
	//String comefrom = ((User)session.getAttribute("user")).getComefrom();
	int fanNumber = UserAction.getFanNumber(id);
	int friendNumber = UserAction.getFriendNumber(id);
	//ArrayList<String[]> weiboList = UserAction.getWeibo(id);
	ArrayList<String[]> weiboList = new ArrayList<String[]>();
	if(type==2)
	{
		weiboList = UserAction.getWeibo(id);
	}
	else if(type==1)
	{
		weiboList = UserAction.getWholeWeibo();
	}
	else if(type==4)
	{
		weiboList = UserAction.getGuanzhuWeibo(id);
	}
	else
	{
		weiboList = UserAction.getWeibo(id);
	}
	int weiboNumber = UserAction.getWeiboNumber(id);
	int ssize = weiboList.size();
	int pageNum = 0;
	if(ssize % 3 == 0)
	{
		pageNum = ssize/3;
	}
	else
	{
		pageNum = ssize/3+1;
	}
	int pages = 1;
	int nextPage = 1;
	int prevPage = 1;
	int lastPage = 1;
	try
	{
		pages = Integer.parseInt(request.getParameter("page"));
		if(pageNum > 0)
		{
			if(pages > pageNum)
			{
				pages = pageNum;
			}
			if(pages < 1)
			{
				pages = 1;
			}
			if(pages >= pageNum)
			{
				nextPage = pageNum;
			}
			else
			{
				nextPage = pages+1;
			}
			if(pages <= 1)
			{
				prevPage = 1;
			}
			else
			{
				prevPage = pages-1;
			}
			lastPage = pageNum;
		}
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
	<title>Main</title>
	<link rel="stylesheet" type="text/css" href="CSS/Main_CSS.css" />
	<script type="text/javascript" src="Jcript/jquery.js"> </script>
	<script type="text/javascript" src="Jcript/jquery.js"> </script>
	<script src="Jcript/comments.js" language="javascript" > </script>
	<script src="Jcript/getPath.js" language="javascript" > </script>
	<!-- <script src="Jcript/mainWrong.js" language="javascript" > </script> -->
	<script type="text/javascript">
	$(document).ready(function()
			{
				if("<%=wrong%>"=="zhuanfawrong")
				{
					wrong = "";
					alert("转发微博出现错误！");
				}
				else if("<%=wrong%>"=="addwrong")
				{
					wrong = "";
					alert("加好友出现错误！");
				}
				else if("<%=wrong%>"=="deletewrong")
				{
					wrong = "";
					alert("删好友出现错误！");
				}
				else if("<%=wrong%>"=="fabuwrong")
				{
					wrong = "";
					alert("发布微博出现错误！");
				}
				else if("<%=wrong%>"=="pinglunwrong")
				{
					wrong = "";
					alert("发布评论出现错误！");
				}
				else if("<%=wrong%>"=="picwrong2")
				{
					wrong = "";
					alert("请选择图片！");
				}
				else if("<%=wrong%>"=="picwrong1")
				{
					wrong = "";
					alert("图片格式只能是png！");
				}
				else if("<%=wrong%>"=="picwrong")
				{
					wrong = "";
					alert("上传图片出现错误！");
				}
			});
		var check = function()
		{
			var a=$("#weiboFabu<%=id2 %>").val().length;
			if(a <= 140)
				$("#leaveText<%=id2 %>").html("还能输入"+(140-a)+"字");
			else
			{
				$("#weiboFabu<%=id2 %>").val($("#weiboFabu<%=id2 %>").val().substring(0,140));
				check();
			}
		};
		var check2 = function()
		{
			var a=$("#zhuanfacheck").val().length;
			if(a <= 140)
				$("#zhuanfacheck2").html("还能输入"+(140-a)+"字");
			else
			{
				$("#zhuanfacheck").val($("#weiboFabu<%=id2 %>").val().substring(0,140));
				check();
			}
		};
		var check5 = function()
		{
			alert($("#addPicture").val());
			var a=$("#addPicture").val().length;
			if(a==0)
			{
				alert("图片不能为空！");
				return false;
			}
			else
				return true;
		};
		var checkWeibo = function()
		{
			var a=$("#weiboFabu<%=id2 %>").val().length;
			if(a==0)
			{
				alert("微博不能为空！");
				return false;
			}
			else
				return true;
		};
		var checkpinglun = function(at)
		{
			var a=$("[focushuifu="+at+"]").val().length;
			if(a==0)
			{
				alert("评论不能为空！");
				return false;
			}
			else
				return true;
		};
		var checkzhuanfa = function()
		{
			var a=$("#zhuanfacheck").val().length;
			if(a==0)
			{
				alert("转发内容不能为空！");
				return false;
			}
			else
				return true;
		};
		$(document).ready(function()
		{
			$("#zhuanfacheck").keyup(function()
			{
				check2();
			});
			$("#zhuanfacheck").keydown(function()
			{
				check2();
			});
			$("#zhuanfacheck").focus(function()
			{
				check2();
			});
			$("#weiboFabu<%=id2 %>").keyup(function()
			{
				check();
			});
			$("#weiboFabu<%=id2 %>").keydown(function()
			{
				check();
			});
			$("#weiboFabu<%=id2 %>").focus(function()
			{
				check();
			});
		});
	</script>
</head>
<body class="body">
	<!-- 这个是整体的DIV框-->
	<div class="wholeWindow">
		<%@ include file="header2.jsp"%>
		<%@ include file="search.jsp"%>
		<!-- 这个是主要的大微博区DIV框-->
		<div class="mainWindow">
			<!-- 这个是微博区DIV框-->
			<div class="weiboWindow">
				<!-- 这个是微博区发布框DIV框-->
				<div class="fabuWindow">
				<form action="fabuWeiboServlet" method="post" onsubmit="return checkWeibo();">
					<span>说说你做PJ2的心情吧：</span>
					<br>&nbsp </br>
					<textarea id="weiboFabu<%=id2 %>" maxlength="140" name="fabu" class="fabuText"></textarea>
					<br>&nbsp </br>
					<div class="chooseFile">
						<div class="choose">
							<img src="Reg_image/reg.png" class="fabuP"/>
							<span>照片</span>
						</div>
						<div class="choose">
							<img src="Reg_image/reg.png" class="fabuP"/>
							<span>视频</span>
						</div>
						<div class="choose">
							<img src="Reg_image/reg.png" class="fabuP"/>
							<span>表情</span>
						</div>
						<div>
							<input type="file" value="Browse">
							<!-- <button type="submit">
								上传
							</button> -->
							<input type="hidden" name="id" value="<%=id2 %>"></input>
							<input type="hidden" name="type" value="<%=type %>"></input>
						</div>
					</div>
					<div class="leave">
						<input type="image" src="Reg_image/reg.png" class="ok"/>
						<div id="leaveText<%=id2 %>" class="leaveText">
							<span>还能输入140字</span>
						</div>
					</div>
				</form>
				</div>
				<!-- 这个是微博展示区DIV框-->
				<div class="shejinping">
					<div class="buttonsss">
						<a href = "Main.jsp?id=<%=id%>&type=<%=type%>&page=1">首页</a>
						<a href = "Main.jsp?id=<%=id%>&type=<%=type%>&page=<%=prevPage%>">上一页</a>
						<a href = "Main.jsp?id=<%=id%>&type=<%=type%>&page=<%=nextPage%>">下一页</a>
						<a href = "Main.jsp?id=<%=id%>&type=<%=type%>&page=<%=lastPage%>">末页</a>
					</div>
					<!-- 这个是第一条纯文字微博DIV框-->
					<!--<div class="textWeibo">
						<div class="picWeibo">
							<a href=""><img src="New/2.png"/></a>
						</div>
						<div class="picRight">
						<div class="nameWeibo">
							<a href="">喵喵洋：</a>易大传：“天下一致而百虑，同归而殊途。”
						</div>
						<div class="infoWeibo">
							<span>今天 24:00</span>
							<span class="zhuanfa"><a>转发：</a>10000</span>
							<span class="zhuanfa"><a>讨论：</a>10000</span>
						</div>
						</div>
					</div>-->
					<!-- 这个是第二条纯文字微博DIV框，带有回复框-->
					<!-- <div class="textWeibo">
						<div class="picWeibo">
							<a href=""><img src="New/2.png"/></a>
						</div>
						<div class="picRight">
						<div class="nameWeibo">
							<a href="">喵喵洋：</a>苍茫的天涯是我的爱丫喵。
						</div>
						<div class="infoWeibo">
							<span class="time">今天 24:00</span>
							<span id="zhuanfabutton" class="zhuanfa"><a>转发：</a>10000</span>
							<span id="pinglunbutton" class="zhuanfa"><a>讨论：</a>10000</span>
						</div>
						</div>
						<div class="jiahuifu">
							<textarea name="fabu" class="fabuText"></textarea>
						</div>
						<div class="huifu">
						<textarea name="fabu" class="fabuText" id="focushuifu"></textarea>
						<input type="submit" value="评论"/>
						<span class="tongshizhuanfa">同时转发</span>
						<input type="checkbox" value="转发"/>
						</div>
						<div class="otherhuifu">
							<div class="picWeibo2">
								<a href=""><img src="New/2.png"/></a>
							</div>
							<div class="picRight2">
								<div class="nameWeibo huifuWeibo">
									<a href="">王天翼高富帅：</a>我爱你，我的家，我的家，我的天堂。
								</div>
								<div class="infoWeibo3">
									<span class="time">今天 24:00</span>
									<span id="pinglunbutton" class="zhuanfa"><a>讨论</a></span>
								</div>
							</div>
						</div>
						<div class="otherhuifu">
							<div class="picWeibo2">
								<a href=""><img src="New/2.png"/></a>
							</div>
							<div class="picRight2">
								<div class="nameWeibo huifuWeibo">
									<a href="">顾敬潇大鳕鲅：</a>沙哇嘎嘛　苏杂咩(美) 资档　司(洗)里养　古鲁吽　哈哈哈哈火 班嘎温
								</div>
								<div class="infoWeibo3">
									<span class="time">今天 24:00</span>
									<span id="pinglunbutton" class="zhuanfa"><a>讨论</a></span>
								</div>
							</div>
						</div>
					</div>-->
					<!-- 这个是转发的微博DIV框-->
					<%for(int i = 3*(pages-1); i < 3*(pages-1)+3 && i < weiboList.size(); i++){ %>
					<div class="textWeibo">
						<div class="picWeibo">
							<a href="Main.jsp?id=<%=weiboList.get(i)[3]%>&type=2&page=1"><img src="Pic/<%= weiboList.get(i)[3] %>.png"/></a>
						</div>
						<div class="picRight">
						<div class="nameWeibo">
							<a href="Main.jsp?id=<%=weiboList.get(i)[3]%>&type=2&page=1"><%= weiboList.get(i)[2] %>：</a><%= weiboList.get(i)[0] %>
						</div>
						<%if(!weiboList.get(i)[4].equals("")){ %>
						<!--被转发的微博DIV框-->
						<div class="zhuanfaweibo" beizhuanfaweibo="<%=weiboList.get(i)[7] %>" beizhuanfa="<%=weiboList.get(i)[9]%>"
						nickname="<%= weiboList.get(i)[2] %>" txt="<%= weiboList.get(i)[0] %>">
							<div class="nameWeibo2">
								<a href="Main.jsp?id=<%=weiboList.get(i)[8]%>&type=2&page=1"><%= weiboList.get(i)[5] %>：</a><%= weiboList.get(i)[4] %>
							</div>
						</div>
						<%} 
						int commentNumber = UserAction.getpinglunNumber(Integer.parseInt(weiboList.get(i)[7]));
						%>
						<div class="infoWeibo">
							<span><%= weiboList.get(i)[1] %></span>
							<span zhuanfa="<%=weiboList.get(i)[7] %>" class="zhuanfa"><a>转发：</a><%=weiboList.get(i)[6] %></span>
							<span close="close" pinglun="<%=weiboList.get(i)[7] %>" class="zhuanfa"><a>讨论：</a><%=commentNumber %></span>
						</div>
						</div>
						<%ArrayList<String[]> commentArray = UserAction.getpinglun(Integer.parseInt(weiboList.get(i)[7])); %>				
						<!-- <div class="jiahuifu">
							<textarea name="fabu" class="fabuText" jiahuifu="<%=weiboList.get(i)[7] %>"></textarea>
						</div>-->
						<div class="huifu" huifu="<%=weiboList.get(i)[7] %>">
						<form id="<%=weiboList.get(i)[7] %>" action="fabuPinglunServlet" method="post" id="pl" onsubmit="return checkpinglun(<%=weiboList.get(i)[7] %>);">
						<!-- <textarea name="fabu" class="fabuText" focushuifu="<%=weiboList.get(i)[7] %>"></textarea> -->
						<div bbutton="<%=weiboList.get(i)[7] %>" style="display:none">
						<input type="submit" value="评论"/>
						<span class="tongshizhuanfa">同时转发</span>
						<input type="checkbox" value="转发"/>
						</div>
						<input type="hidden" name="idtargetmblog" value="<%=weiboList.get(i)[7] %>"></input>
						<input type="hidden" name="idpublisher" value="<%=id2%>"></input>
						<input type="hidden" name="returnid" value="<%=id%>"></input>
						<input type="hidden" name="type" value="<%=type%>"></input>
						<input type="hidden" name="page" value="<%=pages%>"></input>
						</form>
						</div>
						<%if(commentArray.size() > 0)
						{
							int j = 0;
							while(j < commentArray.size())
							{
						%>
						<div class="otherhuifu" otherhuifu="<%=weiboList.get(i)[7] %>-<%=commentArray.get(j)[4] %>">
							<div class="picWeibo2">
								<a href="Main.jsp?id=<%=commentArray.get(j)[0]%>&type=2&page=1"><img src="Pic/<%=commentArray.get(j)[0] %>.png"/></a>
							</div>
							<div class="picRight2">
								<div class="nameWeibo huifuWeibo">
									<a href="Main.jsp?id=<%=commentArray.get(j)[0]%>&type=2&page=1"><%=commentArray.get(j)[3] %>：</a><%=commentArray.get(j)[1] %>
								</div>
								<div class="infoWeibo3">
									<span class="time"><%=commentArray.get(j)[2] %></span>
									<span pinglunbutton="<%=weiboList.get(i)[7] %>-<%=commentArray.get(j)[4] %>" class="zhuanfa"><a>讨论</a></span>
								</div>
							</div>
						</div>
						<%j++;}} %>
					</div>
					<%} %>
				</div>
			</div>
			<%@ include file="personInfo.jsp"%>
			<%@ include file="footer.jsp"%>
		</div>
		<div class="zhuanfawindow">
			<div class="fabuWindow">
			<form action="zhuanfaServlet" method="post" onsubmit="return checkzhuanfa()">
				<br>&nbsp </br>
				<span>转发微博：</span>
				<br>&nbsp </br>
				<textarea id="zhuanfacheck" width="600px" maxlength="140" name="fabu" class="fabuText"></textarea>
				<br>&nbsp </br>
				<div class="chooseFile">
					<div class="choose">
						<img src="Reg_image/reg.png" class="fabuP"/>
						<span>照片</span>
					</div>
					<div class="choose">
						<img src="Reg_image/reg.png" class="fabuP"/>
						<span>视频</span>
					</div>
					<div class="choose">
						<img src="Reg_image/reg.png" class="fabuP"/>
						<span>表情</span>
					</div>
					<div>
						<input type="file" value="Browse">
						<!-- <button type="submit">
							上传
						</button> -->
					</div>
					<!-- <input type="submit" class="zzfabu">发布</input>-->
					<input type="hidden" name="idpublisher" value="<%=id2%>"></input>
					<input type="hidden" name="idoriginalmblog" id="zhuanfazhuanfa"></input>
					<input type="hidden" name="type" value=<%=type %>></input>
					<input type="hidden" name="idd" value=<%=id %>></input>
					<!-- <button class="zzcancel">取消</button>-->
				</div>
				<div class="leave">
					<input type="image" src="Reg_image/reg.png" class="ok"/>
					<div class="leaveText">
						<span id="zhuanfacheck2">还能输入140字</span>
					</div>
				</div>
				<img class="close" src="Main/del.png" onclick="cancel();"></img>
			</form>
			</div>
		</div>
	</div>
</body>
