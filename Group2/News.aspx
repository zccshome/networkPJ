<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="News" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/News.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="mainContent">
    <div class="leftDiv">
        <asp:Table ID="HolderTable"  runat="server"></asp:Table>
    </div>
    <div class="rightDiv">
        <iframe id="iframe1" runat="server" name="mainframe" frameborder="0" scrolling="yes" height="500px" width="105%" marginheight="0" marginwidth="0"></iframe>
    </div>
    </div>

</asp:Content>