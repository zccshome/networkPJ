$(document).ready(function()
{
	$(".insertFine").click(function()
	{
		window.location.href = "fineInsert.php";
	});
	$(".back").click(function()
	{
		window.location.href = "fineManagement.php";
	});
	$(".deleteMoreFine").click(function()
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
		$.post("fineDeleteMore.php",{ids:$hehe},function()
			{
				window.location.href = "fineManagement.php";
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