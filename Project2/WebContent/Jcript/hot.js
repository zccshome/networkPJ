$(document).ready(function()
{
    $.get("hotMem.xml", function(callback)
	{
		alert("aa");
		$(callback).find("hotMem").each(function()
		{
			alert("aa");
			var $hotMem = $(this);
            var name = $hotMem.find("name").text();
			$("#asdfg").text(name+"a");
        })
    })
})