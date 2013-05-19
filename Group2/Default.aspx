<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/Default.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="mainContent">
    <h2>
        软工 PJ2!
    </h2>
    <p>
        News 页面请戳这里： <a href="News.aspx">News</a>。
    </p>
    <p>
         Manager 页面请戳这里： <a href="Manager.aspx">Manager</a>。
    </p>
    <p>
         Test 页面请戳这里： <a href="Test.aspx">Test</a>。
    </p>
    <br />
    </div>
    </asp:Content>
