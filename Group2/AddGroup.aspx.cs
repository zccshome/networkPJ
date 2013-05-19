using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddGroup : System.Web.UI.Page
{
    List<GroupNode> pgs;
    List<GroupNode> sgs;

    protected void Page_Load(object sender, EventArgs e)
    {
        //pgs = getPrimaryGroups();
        pgs = GroupQuery.getAllPrimaryGroups();
        sgs = GroupQuery.getAllSecondaryGroups();
        if (!IsPostBack)
        {
            foreach (GroupNode gn in pgs)
            {
                if (gn.NodeName == "其他")
                    continue;
                ListItem li = new ListItem(gn.NodeName, gn.Id.ToString());
                DropDownList1.Items.Add(li);
            }
        }
        setDropDownList();
    }


    /*
    private List<GroupNode> getPrimaryGroups()
    {
        List<GroupNode> nodes = new List<GroupNode>();
        GroupNode gn0 = new GroupNode("军事", -1, true, 0, false);
        GroupNode gn1 = new GroupNode("政治", -1, true, 1, false);
        GroupNode gn2 = new GroupNode("体育", -1, true, 2, false);

        nodes.Add(gn0);
        nodes.Add(gn1);
        nodes.Add(gn2);

        return nodes;
    }

    private List<GroupNode> getSecondGroups()
    {
        List<GroupNode> nodes = new List<GroupNode>();
        GroupNode gn00 = new GroupNode("朝鲜", 0, true, 11, false);
        GroupNode gn01 = new GroupNode("导弹", 0, true, 12, false);
        GroupNode gn02 = new GroupNode("舰队", 0, true, 13, false);
        nodes.Add(gn00);
        nodes.Add(gn01);
        nodes.Add(gn02);

        GroupNode gn10 = new GroupNode("党委", 1, true, 21, false);
        GroupNode gn11 = new GroupNode("中共", 1, true, 22, false);
        GroupNode gn12 = new GroupNode("和谐", 1, true, 23, false);
        nodes.Add(gn10);
        nodes.Add(gn11);
        nodes.Add(gn12);

        GroupNode gn20 = new GroupNode("足球", 2, true, 31, false);
        GroupNode gn21 = new GroupNode("篮球", 2, true, 32, false);
        GroupNode gn22 = new GroupNode("赛车", 2, true, 33, false);
        nodes.Add(gn20);
        nodes.Add(gn21);
        nodes.Add(gn22);

        return nodes;
    }*/


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        setDropDownList();
    }

    private void setDropDownList()
    {
        //先清空原来的
        DropDownList2.Items.Clear();
        //取得当前选择的主分类
        int pgn = Int32.Parse(DropDownList1.SelectedItem.Value);


        //取得分组
        if (sgs == null)
            return;

        foreach (GroupNode gn in sgs)
        {
            if (pgn == gn.PrimaryGroupId)
                DropDownList2.Items.Add(new ListItem(gn.NodeName, gn.Id.ToString()));
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (TextBox2.Text.Trim() == "" || TextBox3.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "message", @"<script>alert('不能输入空内容！');</script>");
            return;
        }
        ManagerAssist.addSecondaryGroup(Int32.Parse(DropDownList1.SelectedItem.Value), TextBox2.Text.Trim(), TextBox3.Text);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox0.Text.Trim() == "" || TextBox1.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "message", @"<script>alert('不能输入空内容！');</script>");
            return;
        }
        List<Article> articles = ManagerAssist.addPrimaryGroup(TextBox0.Text.Trim(), TextBox1.Text.Trim());

        foreach (Article a in articles)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            Label label = new Label();
            label.Text = a.Title;
            tc.Controls.Add(label);
            tr.Controls.Add(tc);
            HolderTable.Rows.Add(tr);
        }
    }
}