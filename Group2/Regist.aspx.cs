using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Regist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        clearContent();
        
        
        //第一行
        TableRow tr1 = new TableRow();
        TableCell tc11 = new TableCell();
        tc11.Controls.Add(name1);
        TableCell tc12 = new TableCell();
        tc12.Controls.Add(TextBox1);
        TableCell tc13 = new TableCell();
        tc13.Controls.Add(tip1);
        tr1.Controls.Add(tc11);
        tr1.Controls.Add(tc12);
        tr1.Controls.Add(tc13);
        HolderTable.Rows.Add(tr1);

        //第二行
        TableRow tr2 = new TableRow();
        TableCell tc21 = new TableCell();
        tc21.Controls.Add(name2);
        TableCell tc22 = new TableCell();
        tc22.Controls.Add(TextBox2);
        TableCell tc23 = new TableCell();
        tc23.Controls.Add(tip2);
        tr2.Controls.Add(tc21);
        tr2.Controls.Add(tc22);
        tr2.Controls.Add(tc23);
        HolderTable.Rows.Add(tr2);

        //第三行
        TableRow tr3 = new TableRow();
        TableCell tc31 = new TableCell();
        tc31.Controls.Add(name3);
        TableCell tc32 = new TableCell();
        tc32.Controls.Add(TextBox3);
        TableCell tc33 = new TableCell();
        tc33.Controls.Add(tip3);
        tr3.Controls.Add(tc31);
        tr3.Controls.Add(tc32);
        tr3.Controls.Add(tc33);
        HolderTable.Rows.Add(tr3);

        //第四行
        TableRow tr4 = new TableRow();
        TableCell tc41 = new TableCell();
        tc41.Controls.Add(name4);
        TableCell tc42 = new TableCell();
        tc42.Controls.Add(TextBox4);
        TableCell tc43 = new TableCell();
        tc43.Controls.Add(tip4);
        tr4.Controls.Add(tc41);
        tr4.Controls.Add(tc42);
        tr4.Controls.Add(tc43);
        HolderTable.Rows.Add(tr4);

        //第五行
        TableRow tr5 = new TableRow();
        TableCell tc51 = new TableCell();
        tc51.Controls.Add(Button1);

        TableCell tc52 = new TableCell();
        tc52.Controls.Add(Button2);

        tr5.Controls.Add(tc51);
        tr5.Controls.Add(tc52);

        HolderTable.Rows.Add(tr5);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //这里前台js应该先进行一次合法性检测，这里省略
        if (TextBox1.Text == "")
        {
            clearContent();
            tip1.Text = "请输入用户名";
            return;
        }

        if (TextBox2.Text == "")
        {
            clearContent();
            tip2.Text = "请输入密码";
            return;
        }

        if (TextBox3.Text != TextBox2.Text)
        {
            clearContent();
            tip3.Text = "两次输入的密码不一致";
            return;
        }
        
        string mailRegex = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
        if (!Regex.IsMatch(TextBox4.Text.Trim(), mailRegex))
        {
            clearContent();
            tip4.Text = "请输入有效的邮箱地址！";
            return;
        }


        User user = new User(TextBox1.Text.Trim(), TextBox2.Text, TextBox4.Text.Trim());
        int signal;
        if ((signal = AccountAssist.register(user)) > 0)

        {
            ClientScript.RegisterStartupScript(GetType(), "message", @"<script>alert('注册成功！');</script>");
            clearContent();
            return;
        }
        else
        {
            switch (signal)
            {
                case -1:
                    tip1.Text = "用户名重复";
                    ClientScript.RegisterStartupScript(GetType(), "message", @"<script>alert('注册失败，请重试！');</script>");
                    return;
                case -2:
                    ClientScript.RegisterStartupScript(GetType(), "message", @"<script>alert('数据库操作失败，请重试！');</script>");
                    return;
                case -3:
                    tip4.Text = "邮箱地址重复";
                    ClientScript.RegisterStartupScript(GetType(), "message", @"<script>alert('注册失败，请重试！');</script>");
                    return;
                default:
                    ClientScript.RegisterStartupScript(GetType(), "message", @"<script>alert('注册失败，原因不明，请重试！');</script>");
                    return;
            }
        }



    }

    private void clearContent()
    {
        tip1.Text = tip2.Text = tip3.Text = tip4.Text = "";
    }
}