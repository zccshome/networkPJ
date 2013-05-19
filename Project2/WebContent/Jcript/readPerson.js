var xmlhttp;
function loadXMLDoc(url)
{
	xmlhttp = null;
	if (window.XMLHttpRequest)
	{// code for all new browsers
		xmlhttp = new XMLHttpRequest();
	}
	else if (window.ActiveXObject)
	{// code for IE5 and IE6
		xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
	}
	if (xmlhttp != null)
	{
		xmlhttp.onreadystatechange = onResponse;
		xmlhttp.open("GET",url,true);
		xmlhttp.send(null);
	}
	else
	{
		alert("Your browser does not support XMLHTTP.");
	}
}

function onResponse()
{
	if(xmlhttp.readyState != 4) return;
	if(xmlhttp.status != 200)
	{
		alert("Problem retrieving XML data");
		return;
	}
	txt="";
	x=xmlhttp.responseXML.documentElement.getElementsByTagName("member");
	for (i=0;i<x.length;i++)
	{
		txt = txt + "<li>";
		var xx2=x[i].getElementsByTagName("name");
		var xx3=x[i].getElementsByTagName("hot");
		var xx4=x[i].getElementsByTagName("from");
		try
		{
			txt = txt + "<img src='Pic/" + xx2[0].firstChild.nodeValue + ".png' class='personImage'/>";
			txt = txt + "<div class='personData'>";
			txt = txt + "<div class='personName'>" + xx2[0].firstChild.nodeValue + "</div>";
			txt = txt + "<span class='personFrom'>" + xx4[0].firstChild.nodeValue + "</span>";
			txt = txt + "<span class='fansNum'>" + xx3[0].firstChild.nodeValue + "</span>";
			txt = txt + "<div class='latest'><span class='Fweibo'>";
			txt = txt + "苍茫的天涯是我的爱，你是我天边最美的云彩，让我用心把你留下来。。。（今天12：00）";
			txt = txt + "</span></div></div></li>";
		}
		catch (er)
		{
			txt=txt + "</li>";
		}
	}
	document.getElementById('listPersons').innerHTML=txt;
}