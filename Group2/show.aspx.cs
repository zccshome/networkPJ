using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class show : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<GroupNode> list;
        list = GroupQuery.getAllPrimaryGroups();
        //list = generateNodes();

        int count = 0;
        foreach (GroupNode gn in list)
        {
            cbl.Items.Add(new ListItem(gn.NodeName, (count++).ToString()));
        }

        hf1.Value = list.Count.ToString();
        hf2.Value = Request["id"];

        Button1.UseSubmitBehavior = false;
        Button1.OnClientClick = "return sendback()";
    }

    /*
    private List<GroupNode> generateNodes()
    {
        List<GroupNode> nodes = new List<GroupNode>();
        GroupNode n0 = new GroupNode();
        n0.NodeName = "军事";
        GroupNode n1 = new GroupNode();
        n1.NodeName = "政治";
        GroupNode n2 = new GroupNode();
        n2.NodeName = "体育";
        nodes.Add(n0);
        nodes.Add(n1);
        nodes.Add(n2);
        return nodes;
    }
     * 
     * */

}