<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FocusSelect.aspx.cs" Inherits="FocusSelect" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/FocusSelect.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="mainContent">
    <fieldset>
    <asp:Label ID="Label1" runat="server" Text="提示内容"></asp:Label>
    <asp:Table ID="HolderTable" runat="server"></asp:Table>
    <asp:Button ID="Button1" runat="server" Text="保存关注" OnClick="Button1_Click" />
        <asp:Button ID="Button3" runat="server" Text="test" OnClick="Button3_Click" />
    </fieldset>
    <br />
    <fieldset>
    <div class="left"><asp:Label ID="Label2" runat="server" Text="选择分组："></asp:Label></div>
        <div class="right smallright"><asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList></div>
    <br />
    <div class="left"><asp:Label ID="Label3" runat="server" Text="兴趣标签名称："></asp:Label></div><div class="right"><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
    <br />
    <div class="left"><asp:Label ID="Label4" runat="server" Text="关键词：（用空格隔开）"></asp:Label></div><div class="right"><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></div>
    <br />
    <div class="left"><asp:Button ID="Button2" runat="server" Text="添加关注" OnClick="Button2_Click" /></div>
    </fieldset>
    </div>
</asp:Content>