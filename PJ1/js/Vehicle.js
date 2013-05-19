$(document).ready(function()
{
	$(".insertVehicle").click(function()
	{
		window.location.href = "vehicleInsert.php";
	});
	$(".back").click(function()
	{
		window.location.href = "vehicleManagement.php";
	});
	$(".deleteMoreVehicle").click(function()
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
		$.post("vehicleDeleteMore.php",{ids:$hehe},function()
			{
				window.location.href = "vehicleManagement.php";
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