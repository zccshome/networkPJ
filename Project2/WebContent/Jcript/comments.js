$(document).ready(function()
{
	$("[pinglun]").click(function()
	{
		var a = $(this).attr("pinglun");
		if($(this).attr("close")=="close")
		{
			$(this).attr("close","open");
			var $textarea = $("<textarea name='fabu' class='fabuText' focushuifu="+a+"></textarea>");
			$("#"+a).show().prepend($textarea);
			$("[focushuifu="+a+"]").css("height","50px");
			$("[focushuifu="+a+"]").focus().select();
		}
		else
		{
			$(this).attr("close","close");
			$("[focushuifu="+a+"]").remove();
		}
		$("[bbutton^="+a+"]").toggle();
		$("[otherhuifu^="+a+"]").toggle(500);
	});
	$("[focushuifu]").click(function()
	{
		var a = $(this).attr("focushuifu");
		/*$("[jiahuifu="+a+"]").hide();*/
		$("[focushuifu="+a+"]").css("height","50px");
		$("[focushuifu="+a+"]").focus().select();
	});
	$("[focushuifu]").blur(function()
	{
		var a = $(this).attr("focushuifu");
		if($("[focushuifu="+a+"]").val().length == 0)
		{
			$("[focushuifu="+a+"]").css("height","25px");
			/*$("[jiahuifu="+a+"]").show();*/
		}
	});
	$("[zhuanfa]").click(function()
	{
		var a = $(this).attr("zhuanfa");
		var width = $(window).width();
		var height = $(window).height();
		var windowH = $(".zhuanfawindow").height();
		var windowW = $(".zhuanfawindow").width();
		$(".zhuanfawindow").css("top",height/2-windowH/2+"px");
		$(".zhuanfawindow").css("left",width/2-windowW/2+"px");
		$(".zhuanfawindow").show(500);
		$("#zhuanfazhuanfa").attr("value",a);
		$("#zhuanfacheck").focus().select();
		var that = $("[beizhuanfaweibo="+a+"]");
		if($("[beizhuanfaweibo="+a+"]").length > 0)
		{
			var b = that.attr("beizhuanfa");
			$("#zhuanfazhuanfa").attr("value",b);
			$("#zhuanfacheck").val("//转自"+that.attr("nickname")+":"+that.attr("txt"));
			check2();
		}
	});
	$("#zhuanfacheck").blur(function()
	{
		var a = $(this).val().length;
		if(a==0)
		{
			$(".zhuanfawindow").hide(500);
		}
	});
	/*$("[pinglunbutton]").click(function()
	{
		var a = $(this).attr("pinglun");
		$("#focushuifu").val("回复"+$(".huifuWeibo a").html());
		$(".jiahuifu").hide();
		$(".huifu").show();
		$("#focushuifu").focus().select();
	});*/
});
var cancel = function()
{
	$("#zhuanfacheck").val("");
	$(".zhuanfawindow").hide(500);
}