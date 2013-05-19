<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddGroup.aspx.cs" Inherits="AddGroup" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/AddGroup.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="mainContent">
    <fieldset>
    <legend>
        添加主分类
    </legend>
    <p>
        <asp:Label ID="Label2" runat="server" Text="分类名："></asp:Label>
        <asp:TextBox ID="TextBox0" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label3" runat="server" Text="关键词："></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="（请以空格隔开）"></asp:Label>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="确定添加" />
    </p>
    </fieldset>
    <asp:Table ID="HolderTable" runat="server">
    </asp:Table>
    <fieldset>
    <legend>
        添加二级分类
    </legend>
    <p>
        现有主分类列表&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
    </p>
    <p>
        该分类下属的二级分类&nbsp&nbsp<asp:DropDownList ID="DropDownList2" runat="server">
        </asp:DropDownList>
    </p>
    <p>
        添加该主分类的二级分类</p>
    <p>
        <asp:Label ID="Label5" runat="server" Text="分类名："></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label6" runat="server" Text="关键词："></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="确定添加" />
    </p>
    </fieldset>
    </div>
</asp:Content>