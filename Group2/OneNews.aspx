<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OneNews.aspx.cs" Inherits="OneNews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Styles/OneNews.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainContent">
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="返回" />
        <br />
        <asp:Label ID="titleLable" runat="server"></asp:Label>
        <br /><br />
        <asp:Label ID="contentLabel" runat="server"></asp:Label>
        <br /><br />
    </div>
    </form>
</body>
</html>
