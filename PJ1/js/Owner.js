$(document).ready(function()
{
	$(".insertOwner").click(function()
	{
		window.location.href = "ownerInsert.php";
	});
	$(".back").click(function()
	{
		window.location.href = "ownerManagement.php";
	});
	$(".deleteMoreOwner").click(function()
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
		$.post("ownerDeleteMore.php",{ids:$hehe},function()
			{
				window.location.href = "ownerManagement.php";
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