<!-- 这个是上部搜索区的DIV框， 里面有输入框和一些标签-->
<div class="searchWindow">
	<div class="search">
	<form name="input" action="searchPersonServlet" method="post">
		<input type="text" name="search"/>
		<input type="image" src="Main/search.png" class="searchPic"/>
		<input type="hidden" name="id2" value="<%=id2%>"/>
	</form>
	</div>
	<div class="search">
		<form name="quit" action="quitServlet" method="post">
			<a href="javascript:document.quit.submit();">退出</a>
		</form>
	</div>
	<div class="search">
		<a href="Main.jsp?id=<%=id2%>&type=4&page=1">我关注的用户微博</a>
	</div>
	<div class="search">
		<a href="Main.jsp?id=<%=id2%>&type=3&page=1">随便看看</a>
	</div>
	<div class="search">
		<a href="Main.jsp?id=<%=id2%>&type=2&page=1">我的微博</a>
	</div>
	<div class="search">
		<a href="Main.jsp?id=<%=id2%>&type=1&page=1">微博广场</a>
	</div>
</div>