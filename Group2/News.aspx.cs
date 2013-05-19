using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class News : System.Web.UI.Page
{
    //user为null时则为未登录状态
    private User user;
    private Hashtable map;//通过TreeNode寻找相应的GroupNode

    protected void Page_Load(object sender, EventArgs e)
    {
        
        user = (User)Session["user"];
        const string TARGET = "mainframe";

        /*
        TreeView tv0 = new TreeView();
        TreeNode tn0 = new TreeNode("体育");
        tn0.NavigateUrl = "Showarticles.aspx";
        tn0.Target = TARGET;

        TreeNode tn01 = new TreeNode("足球");
        TreeNode tn02 = new TreeNode(@"<span class='iLabel'>篮球</span>");
        tn01.Target = TARGET;
        tn02.Target = TARGET;

        tn0.ChildNodes.Add(tn01);
        tn0.ChildNodes.Add(tn02);
        tv0.Nodes.Add(tn0);

        TableRow tr = new TableRow();
        TableCell tc = new TableCell();
        tc.Controls.Add(tv0);
        tr.Controls.Add(tc);
        HolderTable.Rows.Add(tr);

        TreeView tv1 = new TreeView();
        TreeNode tn1 = new TreeNode("军事");
        tn1.Target = TARGET;
        TreeNode tn11 = new TreeNode("亚洲");
        TreeNode tn12 = new TreeNode("中东");
        tn11.Target = TARGET;
        tn12.Target = TARGET;
        tn1.ChildNodes.Add(tn11);
        tn1.ChildNodes.Add(tn12);
        tv1.Nodes.Add(tn1);

        TableRow tr1 = new TableRow();
        TableCell tc1 = new TableCell();
        tc1.Controls.Add(tv1);
        tr1.Controls.Add(tc1);
        HolderTable.Rows.Add(tr1);
        
        */
        
        //针对为注册用户
        if (user == null)
        {
            //首先建立左边的TreeView
            //取得所有主分类
            List<GroupNode> primaryGroups = GroupQuery.getAllPrimaryGroups();
            List<GroupNode> secondaryGroups = GroupQuery.getAllSecondaryGroups();
            List<GroupNode> list = GroupQuery.sortGroupNodeList(new List<GroupNode>[] { primaryGroups, secondaryGroups });

            

            TreeNode tmpPrimaryNode = null;
            foreach (GroupNode gn in list)
            {
                if (gn.IsPrimaryGroup)
                {

                    TreeView tv = new TreeView();
                    TreeNode tn = new TreeNode(gn.NodeName);//根节点
                    tn.Target = TARGET;
                    tn.NavigateUrl = "Showarticles.aspx?gn=" + gn.Id + "&primary=1&other=0&pgid=-1";
                    tmpPrimaryNode = tn;
                    tv.Nodes.Add(tn);

                    TableCell tc = new TableCell();
                    tc.Controls.Add(tv);
                    TableRow tr = new TableRow();
                    tr.Controls.Add(tc);

                    HolderTable.Rows.Add(tr);
                }
                else
                {
                    TreeNode tn = new TreeNode(gn.NodeName);//子节点
                    tn.Target = TARGET;
                    if (gn.NodeName == "其他")
                        tn.NavigateUrl = "Showarticles.aspx?gn=" + gn.Id + "&primary=0&other=1&pgid=" + gn.PrimaryGroupId;
                    else
                        tn.NavigateUrl = "Showarticles.aspx?gn=" + gn.Id + "&primary=0&other=0&pgid=" + gn.PrimaryGroupId;
                    tmpPrimaryNode.ChildNodes.Add(tn);
                }

                
            }

        }
        else//针对已登录用户
        {
            List<GroupNode> primaryGroups = GroupQuery.getFocusedPublicGroupsByUserId(user.UserId);
            List<GroupNode> secondaryGroups = GroupQuery.getAllInterestLabelsByUserId(user.UserId);
            List<GroupNode> list = GroupQuery.sortGroupNodeList(new List<GroupNode>[] { primaryGroups, secondaryGroups });

            if (list == null || list.Count == 0)
            {
                Label tmpLabel = new Label();
                tmpLabel.Text = "您还没有任何关注哦！";
                TableCell tc = new TableCell();
                TableRow tr = new TableRow();
                tc.Controls.Add(tmpLabel);
                tr.Controls.Add(tc);
                HolderTable.Rows.Add(tr);
                return;


            }

            TreeNode tmpPrimaryNode = null;

            foreach (GroupNode gn in list)
            {
                if (gn.IsPrimaryGroup)
                {
                    TreeView tv = new TreeView();
                    TreeNode tn = new TreeNode(gn.NodeName);//根节点
                    tn.Target = TARGET;
                    tn.NavigateUrl = "Showarticles.aspx?gn=" + gn.Id + "&primary=1&other=0&pgid=-1";
                    tmpPrimaryNode = tn;
                    tv.Nodes.Add(tn);

                    TableCell tc = new TableCell();
                    tc.Controls.Add(tv);
                    TableRow tr = new TableRow();
                    tr.Controls.Add(tc);

                    HolderTable.Rows.Add(tr);

                }
                else
                {
                    //这里还需要判断是否是兴趣标签
                    TreeNode tn;
                    if (gn.IsInterestLabel)
                        tn = new TreeNode(@"<span class='iLabel'>" + gn.NodeName + @"</span>");//子节点
                    else
                        tn = new TreeNode(gn.NodeName);

                    tn.Target = TARGET;
                    if (gn.NodeName == "其他")
                        tn.NavigateUrl = "Showarticles.aspx?gn=" + gn.Id + "&primary=0&other=1&pgid="+gn.PrimaryGroupId;
                    else
                        tn.NavigateUrl = "Showarticles.aspx?gn=" + gn.Id + "&primary=0&other=0&pgid="+gn.PrimaryGroupId;
                    tmpPrimaryNode.ChildNodes.Add(tn);
                }
            }

            //增加其他选项
            TreeView othertv = new TreeView();
            TreeNode othertn = new TreeNode("其他");
            othertn.NavigateUrl = "Showarticles.aspx?gn=-1&primary=1&other=1&pgid=-1";
            othertn.Target = TARGET;
            othertv.Nodes.Add(othertn);
            TableRow othertr = new TableRow();
            TableCell othertc = new TableCell();
            othertr.Controls.Add(othertc);
            othertc.Controls.Add(othertv);
            HolderTable.Rows.Add(othertr);


        }

        /*primaryGroups = new List<GroupNode>();
            List<GroupNode> temp = GroupQuery.getFocusedPublicGroupsByUserId(user.UserId);
            foreach (GroupNode node in temp)
            {
                if (node.IsPrimaryGroup)
                    primaryGroups.Add(node);
            }*/

    }


}