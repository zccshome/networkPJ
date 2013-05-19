$(document).ready(function()
{
	$(".insertRecord").click(function()
	{
		window.location.href = "recordInsert.php";
	});
	$(".back").click(function()
	{
		window.location.href = "recordManagement.php";
	});
	$(".deleteMoreRecord").click(function()
	{
		$hehe = new Array();
		$i = 0;
		$("[name='deleteIt']").each(function()
				{
					if($(this).attr("checked")=='checked')
					{
						$hehe[$i] = $(this).attr("value");
						$i++;
					}
				});
		$.post("recordDeleteMore.php",{ids:$hehe},function()
			{
				window.location.href = "recordManagement.php";
			});
	});
	$("#deleteWho").click(function()
	{
		if($("#deleteWho").attr("checked")=='checked')
		{
			$("[name='deleteIt']").each(function()
				{
					$(this).attr("checked", 'true');
				});
		}
		else
		{
			$("[name='deleteIt']").each(function()
				{
					$(this).removeAttr("checked");
				});
		}
	});
});