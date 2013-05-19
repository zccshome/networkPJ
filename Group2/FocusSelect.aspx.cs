using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FocusSelect : System.Web.UI.Page
{
    private Hashtable map;
    private Hashtable p2o;//通过勾选的主分类寻找其他分类的GroupNode
    private User user;
    private List<TreeView> tvs;
    private List<TreeNode> alltns;

    protected void Page_Load(object sender, EventArgs e)
    {

        /*
        tv0 = new TreeView();
        TreeNode tn0 = new TreeNode("体育");
        tn0.ShowCheckBox = true;
        TreeNode tn01 = new TreeNode("足球");
        TreeNode tn02 = new TreeNode(@"<span class='iLabel'>篮球</span>");

        tn0.ChildNodes.Add(tn01);
        tn0.ChildNodes.Add(tn02);
        tv0.Nodes.Add(tn0);

        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        tc.Controls.Add(tv0);
        tr.Controls.Add(tc);
        HolderTable.Controls.Add(tr);

        TreeView tv1 = new TreeView();
        TreeNode tn1 = new TreeNode("军事");

        TreeNode tn11 = new TreeNode("亚洲");
        TreeNode tn12 = new TreeNode("中东");
        tn1.ChildNodes.Add(tn11);
        tn1.ChildNodes.Add(tn12);
        tv1.Nodes.Add(tn1);

        TableCell tc1 = new TableCell();
        tc1.Controls.Add(tv1);
        tr.Controls.Add(tc1);
        */


        user = (User)Session["user"];
        
        if (user == null)
        {
            Label1.Text = "对不起，您没有访问该资源的权限";
            Label1.Font.Size = 18;
            Label1.ForeColor = Color.Red;
            Label2.Visible = false;
            DropDownList1.Visible = false;
            Label3.Visible = false;
            Label4.Visible = false;
            TextBox1.Visible = false;
            TextBox2.Visible = false;
            Button1.Visible = false;
            Button2.Visible = false;
            return;
        }

        map = new Hashtable();
        p2o = new Hashtable();
        tvs = new List<TreeView>();
        alltns = new List<TreeNode>();
        

        Label1.Visible = false;

        List<GroupNode> lists = GroupQuery.sortGroupNodeList(new List<GroupNode>[]{GroupQuery.getAllPrimaryGroups(), GroupQuery.getAllSecondaryGroups(), GroupQuery.getAllInterestLabelsByUserId(user.UserId)});
        List<GroupNode> list2 = GroupQuery.sortGroupNodeList(new List<GroupNode>[]{GroupQuery.getFocusedPublicGroupsByUserId(user.UserId), GroupQuery.getAllInterestLabelsByUserId(user.UserId)});

        List<GroupNode> list3 = GroupQuery.getAllPrimaryGroups();

        //DropDownList1.Items.Clear();
        /*
        foreach (TreeView tv in tvs)
        {
            tv.Nodes.Clear();
        }
         * */
        if (!IsPostBack)
            foreach(GroupNode gn in list3)
                if (gn.NodeName != "其他")
                    DropDownList1.Items.Add(new ListItem(gn.NodeName, gn.Id.ToString()));

        TreeNode tmpPrimary = null;

        foreach (GroupNode gn in lists)
        {
            TableRow row = new TableRow();
            if (gn.NodeName == "其他" && gn.IsPrimaryGroup)
                continue;

            TreeNode tn = new TreeNode(gn.NodeName);
            
            map.Add(tn, gn);
            tn.ShowCheckBox = true;
            if (checkDup(list2, gn))
                tn.Checked = true;

            tn.NavigateUrl = "#";
            
            
            
            if (gn.NodeName == "其他")
            {
                p2o.Add(tmpPrimary, gn);
                continue;
            }
            alltns.Add(tn);
            if (gn.IsPrimaryGroup)
            {
                tmpPrimary = tn;
                TreeView tv = new TreeView();
                tv.Nodes.Add(tn);
                tvs.Add(tv);
                TableCell tc = new TableCell();
                tc.Controls.Add(tv);
                row.Controls.Add(tc);
                HolderTable.Rows.Add(row);
                tmpPrimary.ChildNodes.Clear();
            }
            else
            {
                
                //if (!IsPostBack)
                //{
                    tmpPrimary.ChildNodes.Add(tn);
                    if (gn.IsInterestLabel)
                    {
                        tn.Text = @"<span class='iLabel'>" + gn.NodeName + @"</span>";
                    }
                //}
                
            }

        }
        




    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        /*
        List<GroupNode> tempList = new List<GroupNode>();
        GroupNode g1 = new GroupNode("a", 0, true, 5, false);
        GroupNode g2 = new GroupNode("b", 0, false, 2, false);
        GroupNode g3 = new GroupNode("c", 0, false, 1, false);
        GroupNode g4 = new GroupNode("d", 1, false, 3, false);
        GroupNode g5 = new GroupNode("e", 1, true, 1, false);
        GroupNode g6 = new GroupNode("f", 0, false, 4, false);
        GroupNode g7 = new GroupNode("g", 1, false, 0, false);
        tempList.Add(g1);
        tempList.Add(g2);
        tempList.Add(g3);
        tempList.Add(g4);
        tempList.Add(g5);
        tempList.Add(g6);
        tempList.Add(g7);
        tempList.Sort();
        String df = "";
        foreach (GroupNode wu in tempList)
            df = df + wu.PrimaryGroupId + wu.Id;
        TextBox1.Text = df;
         * */

        //把所有打勾的子分类的主分类都打上勾
        foreach (TreeView tv in tvs)
        {
            TreeNode ptn = tv.Nodes[0];
            TreeNodeCollection ptnc = ptn.ChildNodes;
            foreach (TreeNode tn in ptnc)
            {
                if (tn.Checked)
                {
                    ptn.Checked = true;
                    break;
                }
            }
           
        }


        List<int> focuses = new List<int>();

        foreach (TreeNode tn in alltns)
        {
            GroupNode gn = (GroupNode)map[tn];
            if (gn.IsPrimaryGroup && tn.Checked)
            {
                GroupNode pgn = (GroupNode)p2o[tn];
                focuses.Add(pgn.Id);
                continue;
            }
            if (gn.IsInterestLabel && !tn.Checked)
            {
                FocusAndCreateTag.deleteTag(new User2Tag(user.UserId, gn.Id));
                continue;
            }

            if (tn.Checked)
                focuses.Add(gn.Id);
        }

        /*
        foreach (TreeView tv in tvs)
        {
            TreeNode primary = tv.Nodes[0];
            GroupNode primarygp = (GroupNode)map[primary];
            if (primary.Checked)
            {
                GroupNode pgn = (GroupNode)p2o[primary];
                focuses.Add(pgn.Id);
            }


            TreeNodeCollection tnc = primary.ChildNodes;
            foreach (TreeNode tn in tnc)
            {
                GroupNode gn = (GroupNode)map[tn];
                if (gn == null)
                    continue;
                
                if ((!gn.IsInterestLabel) && tn.Checked)
                    focuses.Add(gn.Id);
            }
        }
         * */


        bool answer = FocusAndCreateTag.saveFocus(focuses, user.UserId);
        //Response.Redirect("FocusSelect.aspx");

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        FocusAndCreateTag.addTag(Int32.Parse(DropDownList1.SelectedValue), TextBox1.Text.Trim(), TextBox2.Text.Trim(), user.UserId);
        Response.Redirect("FocusSelect.aspx");
    }

    private bool checkDup(List<GroupNode> list, GroupNode gn)
    {
        if (list == null || list.Count == 0)
            return false;
        foreach (GroupNode node in list)
            if (node.Id == gn.Id && node.IsInterestLabel == gn.IsInterestLabel && node.IsPrimaryGroup == gn.IsPrimaryGroup && node.NodeName == gn.NodeName && node.PrimaryGroupId == gn.PrimaryGroupId)
                return true;
        return false;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        StringBuilder sb = new StringBuilder();
        foreach (TreeNode tn in alltns)
        {
            sb.Append(tn.Text);
            sb.Append(tn.Checked);
        }

        Label3.Text = sb.ToString();

    }
}