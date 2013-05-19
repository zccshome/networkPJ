using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ManagerAssist.stop_list.Count == 0)
        {
            string url = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "App_Data\\StopList\\中文停用词.txt";
            StreamReader objReader = new StreamReader(url, Encoding.GetEncoding("UTF-8"));
            //StringReader SR = new StringReader(url);
            String line = "";
            while ((line = objReader.ReadLine()) != null)
            {
                ManagerAssist.stop_list.Add(line);
            }
            objReader.Close();
        }
        List<GroupNode> primarygroupNodeList = GroupQuery.getAllPrimaryGroups();
        foreach (GroupNode primaryGroupNode in primarygroupNodeList)
        {
            ListItem item = new ListItem(primaryGroupNode.NodeName, ""+primaryGroupNode.PrimaryGroupId);
            SearchDropDownList.Items.Add(item);
        }
    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        string keyword = SearchBox.Text;

        int primaryId = Int32.Parse(SearchDropDownList.SelectedItem.Value);
        /**
         * 下面靠你们了嗯。。。
         */
        Session["keyword"] = keyword;
        Session["primaryGroupId"] = primaryId;
        Response.Redirect("~/SearchResult.aspx");
    }
    
}
