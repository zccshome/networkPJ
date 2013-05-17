<%@ Page Title="关于我们" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
<h2>项目概述</h2>
<p style="text-indent:2em">每天读者都会阅读大量的新闻、文章，而且读者更希望有针对性的阅读自己感兴趣领域的文章，因此希望有一个系统，能够对新闻、文章进行分类，供读者阅读。该系统通过对各个新闻的标题和内容进行分析，将新闻分别归于“军事”、“经济”等类别，从而让对某一类别感兴趣的读者可以很方便地阅读该类别的新闻和文章。</p>
<h2>功能列表</h2>
<p style="text-indent:2em">对指定的文章进行分词，通过词频统计、TF·IDF计算、余弦相似性判断等策略，自动对文章和新闻进行尽可能正确的分类。</p>
<p style="text-indent:2em">在自动分类结果欠佳的情况下，允许管理员对分类进行手动调整。</p>
<p style="text-indent:2em">建立一个数据库存储新闻和文章的标题、链接、分类结果等信息。</p>
<p style="text-indent:2em">普通用户可以点击标签查看某个分类下的全部文章。</p>
<p style="text-indent:2em">界面美观，布局合理。</p>
<h2>开发语言和平台</h2>
<p style="text-indent:2em">我们选择了ASP.NET作为网站的开发语言和平台。从技术的角度来说，本网站的开发既可以使用JSP，也可以使用ASP，甚至也可以使用PHP等各种不同的语言，但考虑到ASP开发的快捷性、前后台交互的简便性，以及本小组的部分在自学ASP的组员需要一个实践练习的机会，我们最终选定ASP作为开发语言。</p>
<h2>总体架构</h2>
<p style="text-indent:2em">本次项目开发我们在迭代开发的每一个阶段都做到了严格的函数接口预定义和分层架构。</p>

</asp:Content>
