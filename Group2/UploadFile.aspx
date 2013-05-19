<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UploadFile.aspx.cs" Inherits="UploadFile" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="Scripts/UploadFile.js" type="text/javascript"></script>
    <link href="Styles/UploadFile.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="mainContent2">
    <asp:HiddenField ID="gnname" runat="server" />
    <asp:HiddenField ID="gnid" runat="server" />
    <asp:Table ID="HolderTable" runat="server"></asp:Table>
    <asp:Button ID="Button1" runat="server" Text="确定上传" OnClick="Button1_Click" style="height: 21px" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Table ID="OutputTable" runat="server"></asp:Table>
    <asp:Button ID="Button2" runat="server" Text="确定更改" OnClick="Button2_Click" style="height: 21px" Visible="False" />
    </div>
    </asp:Content>