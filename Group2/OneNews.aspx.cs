using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class OneNews : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int articleId = Convert.ToInt32(Request["articleId"]);
        Article a = NewsAssist.getArticleByIdWrapper(articleId);

        /*titleLable.Text = "哈哈";
        titleLable.Font.Size = 16;
        contentLabel.Text = "文件找不到或已损坏！";*/
        titleLable.Text = a.Title;
        titleLable.Font.Size = 16;
        string content = NewsAssist.getArticleContentByArticleModel(a);
        if (content != null)
            contentLabel.Text = content;
        else
        {
            contentLabel.Text = "文件找不到或已损坏！";
            contentLabel.ForeColor = Color.Red;
        }
        contentLabel.Font.Size = 11;
    }

    /**
     * 返回按钮 
     */
    protected void Button1_Click(object sender, EventArgs e)
    {
        // 其中，history.go(-2)，要写为-2，因在按钮事件触发前，已刷新一次页面，所以应是-2。跟直接写脚本的有所不同。
        Response.Write("<script language=javascript>history.go(-2);</script>");
    }
}