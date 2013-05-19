using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["miao"] = "wu";
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        User trylog = new User();
        trylog.UserName = Login1.UserName;
        trylog.UserPass = Login1.Password;

        User user;
        if ((user = AccountAssist.login(trylog)) != null)
        {
            //ClientScript.RegisterStartupScript(GetType(), "message", @"<script>alert('登录成功！');</script>");
            Session["user"] = user;
            Response.Redirect("News.aspx");
        }
        else
        {
            //ClientScript.RegisterStartupScript(GetType(), "message", @"<script>alert('登录失败！');</script>");
        }
    }
}