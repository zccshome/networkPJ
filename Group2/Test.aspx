<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        #TextArea1
        {
            height: 415px;
            width: 916px;
        }
        .auto-style1
        {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <span class="auto-style1">DAL层测试&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="height: 21px" Text="总测试" />
    </div>
    <asp:Button ID="Button4" runat="server" Text="Article" OnClick="Button4_Click" />
    <asp:Button ID="Button3" runat="server" Text="Article2Group" OnClick="Button3_Click" />
    <asp:Button ID="Button5" runat="server" Text="GlobalParse" OnClick="Button5_Click" />
    <asp:Button ID="Button6" runat="server" Text="LocalParse" OnClick="Button6_Click" />
    <asp:Button ID="Button8" runat="server" Text="PrimaryGroup" OnClick="Button8_Click" />
    <asp:Button ID="Button7" runat="server" Text="PrimaryGroupKeyWords" OnClick="Button7_Click" />
    <asp:Button ID="Button9" runat="server" Text="Tag" OnClick="Button9_Click" />
    <asp:Button ID="Button11" runat="server" Text="User" OnClick="Button11_Click" />
    <asp:Button ID="Button10" runat="server" Text="User2Tag" OnClick="Button10_Click" />
    <br />
    <hr />
    <br />
    <div class="auto-style1">
        BLL层测试&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="Button2" runat="server" Text="总测试" OnClick="Button2_Click" />
    </div>
    <asp:Button ID="Button14" runat="server" Text="GroupQuery" OnClick="Button14_Click" />
    <asp:Button ID="Button12" runat="server" Text="AccountAssit" OnClick="Button12_Click" />
    <asp:Button ID="Button17" runat="server" Text="Search" OnClick="Button17_Click" />
    <asp:Button ID="Button13" runat="server" Text="FocusAndCreate" OnClick="Button13_Click" />
    <asp:Button ID="Button16" runat="server" Text="NewsAssist" OnClick="Button16_Click" />
    <asp:Button ID="Button15" runat="server" Text="ManagerAssist" OnClick="Button15_Click" />
    <br />
    <hr />
    <br />
    <textarea id="TextArea1" name="S1" runat="server" readonly="readonly" style="float: left; clip: rect(auto, 10px, 10px, auto); height: 400px; width: 910px; margin-bottom: 20px;" ></textarea>
</asp:Content>

