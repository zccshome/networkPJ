<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Regist.aspx.cs" Inherits="Regist" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/Reg.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="mainContent">
    <fieldset>
    <asp:Label ID="Label1" runat="server" Font-Size="X-Large" Text="用户注册"></asp:Label>
    <br />
    <asp:Table ID="HolderTable" runat="server">
    </asp:Table>
    <br />
    <asp:Label ID="name1" runat="server" Text="用户名："></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Label ID="tip1" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="name2" runat="server" Text="密码："></asp:Label><asp:TextBox ID="TextBox2" type="password" runat="server"></asp:TextBox>
    <asp:Label ID="tip2" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="name3" runat="server" Text="确认密码："></asp:Label><asp:TextBox ID="TextBox3" type="password" runat="server"></asp:TextBox>
    <asp:Label ID="tip3" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="name4" runat="server" Text="登录邮箱："></asp:Label><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    <asp:Label ID="tip4" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="确定" />
    <asp:Button ID="Button2" runat="server" Text="取消" />
&nbsp;
    </fieldset>
    </div>
</asp:Content>