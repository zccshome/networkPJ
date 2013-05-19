<!-- 这个是中右的个人信息区的大div框-->
<div class="personWindow">
	<!-- 这个是个人信息区的div框,包括头像，一些追随数之类的信息-->
	<div class="personInfo">
		<div class="personPic">
			<img src="Pic/<%=id%>.png" class="personPic"/>
		</div>
		<div class="stat">
			<span>
			<%if(id==id2){ %>
				我追随
				<%}else{ %>
				TA追随
				<%} %>
			</span>
			<span>
				<%=friendNumber %>
			</span>
		</div>
		<div class="stat">
			<span>
				<%if(id==id2){ %>
				追随我
				<%}else{ %>
				追随TA
				<%} %>
			</span>
			<span>
				<%=fanNumber %>
			</span>
		</div>
		<div class="stat">
			<span>
				微博数
			</span>
			<span>
				<%=weiboNumber %>
			</span>
		</div>
	</div>
	<!-- 这个是个人信息区下方的更多信息区，有地域，名字和一些便捷按钮，以及可能感兴趣的人-->
	<div class="moreInfo">
		<div class="name">
			<%=nickname %>
		</div>
		<div class="from">
			来自：<%=comefrom %>
		</div>
		<%if(id==id2){ %>
		<div class="otherInfo">
			<form name="imageform" action="addPicServlet" enctype= "multipart/form-data" method="post" onsubmit="return check5()">
				<a href="javascript:document.imageform.submit();">修改头像</a>
				<input type="file" name="picture" id="addPicture"/>
				<!-- <input type="hidden" name="path" id="APath"/> -->
			</form>
		</div>
		<div class="otherInfo">
		<form name="myFriend" action="ffServlet" method="post">
			<a href="javascript:document.myFriend.submit();">我的好友</a>
			<input type="hidden" name="id" value="<%=id %>"/>
			<input type="hidden" name="type" value="1"/>
		</form>
		</div>
		<div class="otherInfo">
		<form name="myFan" action="ffServlet" method="post">
			<a href="javascript:document.myFan.submit();">我的粉丝</a>
			<input type="hidden" name="id" value="<%=id %>"/>
			<input type="hidden" name="type" value="2"/>
		</form>
		</div>
		<%}else{
		if(isFriend==true){ %>
		<div class="otherInfo">
		<form name="addfriend" action="haoyouServlet" method="post">
			<a href="javascript:document.addfriend.submit();">解除好友</a>
			<input type="hidden" name="id2" value="<%=id2 %>"/>
			<input type="hidden" name="id" value="<%=id %>"/>
			<input type="hidden" name="type" value="1"/>
		</form>
		</div>
		<%}else{ %>
		<div class="otherInfo">
		<form name="addfriend" action="haoyouServlet" method="post">
			<a href="javascript:document.addfriend.submit();">加为好友</a>
			<input type="hidden" name="id2" value="<%=id2 %>"/>
			<input type="hidden" name="id" value="<%=id %>"/>
			<input type="hidden" name="type" value="2"/>
		</form>
		</div>
		<%} %>
		<div class="otherInfo">
		<form name="hisFriend" action="ffServlet" method="post">
			<a href="javascript:document.hisFriend.submit();">TA的好友</a>
			<input type="hidden" name="id" value="<%=id %>"/>
			<input type="hidden" name="type" value="1"/>
		</form>
		</div>
		<div class="otherInfo">
		<form name="hisFan" action="ffServlet" method="post">
			<a href="javascript:document.hisFan.submit();">TA的粉丝</a>
			<input type="hidden" name="id" value="<%=id %>"/>
			<input type="hidden" name="type" value="2"/>
		</form>
		</div>
		<%} %>
		<div class="more">
			可能感兴趣的人
		</div>
		<!-- 这个具体可能感兴趣的人，一行两个，与登录界面的布局大致相同-->
		<%
			for(int i = 0; i < newList.size(); i+=2)
			{
				//System.out.println(i);
		%>
		<div class="newPerson">
		<div class="newBlock">
			<div class="newBlock2">
				<a href="Main.jsp?id=<%=newList.get(i)[1]%>&type=2&page=1"><img src="Pic/<%=newList.get(i)[1]%>.png"></a>
				<div><a href="Main.jsp?id=<%=newList.get(i)[1]%>&type=2&page=1"><%=newList.get(i)[0]%></a></div>
			</div>
		</div>
		<%
			if((i+1) == newList.size())
			{%>
		</div>
		<%
				break;
			}
		%>
		<div class="newBlock">
			<div class="newBlock2">
				<a href="Main.jsp?id=<%=newList.get(i+1)[1]%>&type=2&page=1"><img src="Pic/<%=newList.get(i+1)[1]%>.png"></a>
				<div><a href="Main.jsp?id=<%=newList.get(i+1)[1]%>&type=2&page=1"><%=newList.get(i+1)[0]%></a></div>
			</div>
		</div>
	</div>
	<br>&nbsp </br>
	<%} %>
	<!--<div class="newPerson">
		<div class="newBlock">
			<div class="newBlock2">
				<a href=""><img src="New/none.png"></a>
				<div><a href="">xy</a></div>
			</div>
		</div>
		<div class="newBlock">
			<div class="newBlock2">
				<a href=""><img src="New/4.png"></a>
				<div><a href="">荡剑回枫</a></div>
			</div>
		</div>
	</div>
	<br>&nbsp </br>
	<div class="newPerson">
		<div class="newBlock">
			<div class="newBlock2">
				<a href=""><img src="New/none.png"></a>
				<div><a href="">anonymous</a></div>
			</div>
		</div>
		<div class="newBlock">
			<div class="newBlock2">
				<a href=""><img src="New/none.png"></a>
				<div><a href="">李弋大帝</a></div>
			</div>
		</div>
	</div>
	<br>&nbsp </br>
	<div class="newPerson">
		<div class="newBlock">
			<div class="newBlock2">
				<a href=""><img src="New/none.png"></a>
				<div><a href="">fishhan</a></div>
			</div>
		</div>
		<div class="newBlock">
			<div class="newBlock2">
				<a href=""><img src="New/8.png"></a>
				<div><a href="">Iris</a></div>
			</div>
		</div>
	</div>-->
	</div>
</div>