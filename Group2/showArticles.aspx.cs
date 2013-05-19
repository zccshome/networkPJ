using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class showArticles : System.Web.UI.Page
{
    private User user;

    protected void Page_Load(object sender, EventArgs e)
    {
        user = (User)Session["user"];
        int primary_flag = -1;
        int groupID = -1;
        int other_flag = -1;
        int pgid = -1;
        try
        {
            primary_flag = Int32.Parse(Request["primary"]);
            groupID = Int32.Parse(Request["gn"]);
            other_flag = Int32.Parse(Request["other"]);
            pgid = Int32.Parse(Request["pgid"]);
        }
        catch (Exception e2)
        {
            TableCell titleCell = new TableCell();
            Label label = new Label();
            label.Text = "请求的参数不正确！";
            label.Font.Size = 18;
            label.ForeColor = Color.Red;
            titleCell.Controls.Add(label);
            TableRow tr = new TableRow();
            tr.Controls.Add(titleCell);
            HolderTable.Rows.Add(tr);
            return;
        }

        /*
        Article a0 = new Article();
        a0.Title = "唐智慧和汤志辉跳舞，多恶心啊！";
        a0.Abstrct = "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊，恶心啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊。真尼玛恶心啊啊啊啊啊啊啊啊！！！！来来来，我们把他揍一顿吧，啊哈哈哈哈！";
        a0.ArticleId = 0;

        Article a1 = new Article();
        a1.Title = "唐智慧和汤志辉跳舞，多恶心啊！";
        a1.Abstrct = "啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊，恶心啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊。真尼玛恶心啊啊啊啊啊啊啊啊";
        a1.ArticleId = 1;
         */

        List<Article> articles;
        /*
        articles = new List<Article>();
        articles.Add(a0);
        articles.Add(a1);
        */

        GroupNode gn = new GroupNode();
        gn.Id = groupID;
        gn.PrimaryGroupId = pgid;

        if (other_flag == 1)
        {
            if (user != null)
            {
                //主分类
                if (primary_flag == 1)
                    articles = NewsAssist.getArticleListOfOthers(user.UserId, null);
                else//子分类
                    articles = NewsAssist.getArticleListOfOthers(user.UserId, gn);
            }
            else
            {
                //主分类
                if (primary_flag == 1)
                    articles = NewsAssist.getArticleListOfOthersForNoLogin(-1, null);
                else//子分类
                    articles = NewsAssist.getArticleListOfOthersForNoLogin(-1, gn);
            }
        }
        else
        {
            if (primary_flag == 1)
                articles = NewsAssist.getArticleListOfCertainPrimaryGroup(gn);
            else
                articles = NewsAssist.getArticleListByDynamicSearch(gn);
        }

        if (articles != null)
        {
            foreach (Article article in articles)
            {
                TableCell titleCell = new TableCell();

                HyperLink titleLink = new HyperLink();
                titleLink.Font.Size = 16;
                titleLink.Text = article.Title;
                titleLink.NavigateUrl = "OneNews.aspx?articleId=" + article.ArticleId;
                titleCell.Controls.Add(titleLink);

                TableRow row = new TableRow();
                row.Cells.Add(titleCell);

                HolderTable.Rows.Add(row);

                TableRow row2 = new TableRow();
                Label abstractlabel = new Label();
                abstractlabel.Font.Size = 10;
                abstractlabel.ForeColor = Color.White;
                abstractlabel.Text = article.Abstrct;
                TableCell abstractCell = new TableCell();
                abstractCell.Controls.Add(abstractlabel);

                row2.Controls.Add(abstractCell);

                HolderTable.Rows.Add(row2);

            }
        }
        else
        {
            TableCell titleCell = new TableCell();
            Label label = new Label();
            label.Text = "该分类暂时不存在文章！";
            label.Font.Size = 18;
            label.ForeColor = Color.Red;
            titleCell.Controls.Add(label);
            TableRow tr = new TableRow();
            tr.Controls.Add(titleCell);
            HolderTable.Rows.Add(tr);
        }
    }
}