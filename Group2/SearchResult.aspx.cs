using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchResult : System.Web.UI.Page
{
    List<Article> finalArticleList;
    protected void Page_Load(object sender, EventArgs e)
    {
        finalArticleList = new List<Article>();
        int primaryId = Int32.Parse(Session["primaryGroupId"].ToString());
        string keyword = Session["keyword"].ToString();
        if (primaryId != -1)
            searchArticle(keyword, primaryId);
        else
        {
            List<GroupNode> primaryGroupNodeList = GroupQuery.getAllPrimaryGroups();
            foreach (GroupNode gn in primaryGroupNodeList)
            {
                int tempPrimaryId = gn.PrimaryGroupId;
                searchArticle(keyword, tempPrimaryId);
            }
        }
        showSearchResult();
    }
    private void searchArticle(string keyword, int primaryId)
    {
        GroupNode gn = new GroupNode("", primaryId, true, primaryId, false);
        List<Article> articleList = NewsAssist.getArticleListOfCertainPrimaryGroup(gn);
        if (articleList == null)
            return;
        foreach (Article a in articleList)
        {
            string abstractString = a.Abstrct;
            if (abstractString.Contains(keyword))
                finalArticleList.Add(a);
        }

    }
    private void showSearchResult()
    {
        for (int i = 0; i < finalArticleList.Count; i++)
        {
            TableCell titleCell = new TableCell();
            Article a = finalArticleList[i];
            HyperLink titleLink = new HyperLink();
            titleLink.Font.Size = 16;
            titleLink.Text = a.Title;
            titleLink.NavigateUrl = "OneNews.aspx?articleId=" + a.ArticleId;
            titleCell.Controls.Add(titleLink);

            TableRow row = new TableRow();
            row.Cells.Add(titleCell);

            SearchTable.Rows.Add(row);

            TableRow row2 = new TableRow();
            Label abstractlabel = new Label();
            abstractlabel.Font.Size = 10;
            abstractlabel.Text = a.Abstrct;//"这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！这是 abstract ！";
            TableCell abstractCell = new TableCell();
            abstractCell.Controls.Add(abstractlabel);

            row2.Controls.Add(abstractCell);

            SearchTable.Rows.Add(row2);
        }
    }
}