using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadFile : System.Web.UI.Page
{
    private const int LIMIT = 10;
    private const string PATH = "App_Data/RawFiles/";
    private FileUpload[] fus;
    private Article[] articles;
    private List<HiddenField> hfs;
    private TextBox[] tbs;

    protected void Page_Load(object sender, EventArgs e)
    {
        fus = new FileUpload[10];
        for (int i = 0; i < LIMIT; i++)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();
            FileUpload fu = new FileUpload();
            fu.ID = "FileUpload" + i;
            fus[i] = fu;
            tc.Controls.Add(fu);
            tr.Controls.Add(tc);
            HolderTable.Rows.Add(tr);
        }

        //List<GroupNode> gns = testGroups();
        List<GroupNode> gns = GroupQuery.getAllPrimaryGroups();

        string namestr, idstr;
        nodelist2str(gns, out namestr, out idstr);

        gnname.Value = namestr;
        gnid.Value = idstr;
        Label1.Visible = false;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //先清除所有的内容
        HolderTable.Visible = false;
        Button1.Visible = false;
        Button2.Visible = true;
        DirectoryInfo rawdi = new DirectoryInfo(Server.MapPath(PATH));
        FileInfo[] rawfis = rawdi.GetFiles();
        foreach (FileInfo fi in rawfis)
        {
            File.Delete(fi.FullName);
        }


        foreach (FileUpload fu in fus)
        {
            string filename = fu.FileName;
            if (filename != "")
            {
                fu.PostedFile.SaveAs(Server.MapPath(PATH + filename));
            }

        }

        DirectoryInfo di = new DirectoryInfo(Server.MapPath(PATH));
        FileInfo[] fis = di.GetFiles();

        hfs = new List<HiddenField>();
        tbs = new TextBox[fis.Length];
        articles = new Article[fis.Length];

        //Session.Add("hfs", hfs);
        //Session.Add("articles", articles);

        var count = 0;
        foreach (FileInfo fi in fis)
        {
            FileStream fs = new FileStream(fi.FullName, FileMode.Open);
            string title = fi.Name.Split('.')[0];
            int length = (int)fs.Length;
            byte[] bytes = new byte[length];
            fs.Read(bytes, 0, length);
            fs.Close();

            string content = Encoding.UTF8.GetString(bytes);

            //需要替换
            //int articleID = testAddArticle(title, content);
            int articleID = ManagerAssist.addArticle(title, content);
            Article art = new Article();
            art.ArticleId = articleID;
            List<int> ids = ManagerAssist.getGroupIdsByArticleWrapper(art);
            //List<int> ids = testGetGroupIdsByArticleWrapper(art);

            List<string> idstrs = ManagerAssist.getPrimaryGroupNamesByPrimaryGroupIds(ids);
            //List<string> idstrs = testGetPrimaryGroupNamesByPrimaryGroupIds(ids);

            articles[count] = art;

            var groupstrs = "";

            foreach (string str in idstrs)
                groupstrs += str + ' ';

            TableRow tr = new TableRow();
            OutputTable.Rows.Add(tr);
            TableCell tc = new TableCell();
            Label label = new Label();
            label.Text = "我们为《" + title + "》选择的默认分类为:" + groupstrs;


            tc.Controls.Add(label);
            tr.Controls.Add(tc);

            TableRow tr2 = new TableRow();
            OutputTable.Rows.Add(tr2);
            TableCell tc2 = new TableCell();

            Label label2 = new Label();
            label2.Text = "如果您对此分类不满意，请您选择分类：";

            Button bt = new Button();

            bt.Text = "选择分类";
            bt.UseSubmitBehavior = true;
            bt.OnClientClick = "return popup(" + count + ")";

            tc2.Controls.Add(label2);
            tc2.Controls.Add(bt);
            tr2.Controls.Add(tc2);

            Label label3 = new Label();
            label3.Text = "您新选择的分类为：";

            TableRow tr3 = new TableRow();
            OutputTable.Rows.Add(tr3);
            TableCell tc3 = new TableCell();
            tr3.Controls.Add(tc3);
            tc3.Controls.Add(label3);

            TextBox tb = new TextBox();
            tb.ReadOnly = true;
            tb.ID = "tb" + count;
            tc3.Controls.Add(tb);

            HiddenField hf = new HiddenField();
            hf.ID = "hf" + count;
            hfs.Add(hf);
            tbs[count] = tb;
            tc3.Controls.Add(hf);
            
            

            count++;
        }

        Session.Add("articles", articles);
        
    }

    
    private List<GroupNode> testGroups()
    {
        List<GroupNode> list = new List<GroupNode>();
        GroupNode node1 = new GroupNode();
        node1.NodeName = "军事";
        node1.Id = 10;
        list.Add(node1);
        GroupNode node2 = new GroupNode();
        node2.NodeName = "政治";
        node2.Id = 11;
        list.Add(node2);
        GroupNode node3 = new GroupNode();
        node3.NodeName = "体育";
        node3.Id = 12;
        list.Add(node3);
        return list;
    }
    

    private void nodelist2str(List<GroupNode> raw, out string namestr, out string idstr)
    {
        int length = raw.Count;
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            GroupNode gn = raw.ElementAt(i);
            sb.Append(gn.NodeName);
            sb2.Append(gn.Id);
            if (i != length - 1)
            {
                sb.Append(",");
                sb2.Append(",");
            }
        }
        namestr = sb.ToString();
        idstr = sb2.ToString();
    }

    private List<int> dealGroupID(string str)
    {
        List<int> list = new List<int>();
        string raw = str.Trim();
        if (raw == "")
            return list;
        string[] idstrs = raw.Split(',');
        foreach (string idstr in idstrs)
            list.Add(Int32.Parse(idstr));
        return list;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        articles = (Article[])Session["articles"];

        Button2.Visible = false;

        int length = articles.Length;
        bool result = true;

        Label1.Visible = true;
        for (int i = 0; i < length; i++)
        {
            List<int> ids = dealGroupID(Request.Form["ctl00$MainContent$hf" + i]);
            //Label1.Text = Request.Form["ctl00$MainContent$hf" + i];
            result &= ManagerAssist.changeGroupRelation(articles[i], ids);
        }

        if (result)
            Label1.Text = "保存成功！将在3秒之后返回...";
        else
            Label1.Text = "保存失败，请重试！";
        Label1.Visible = true;
        
        Response.AddHeader("Refresh", "3");
    }

    
    private int testAddArticle(string title, string content)
    {
        return 1;
    }
    

    private List<int> testGetGroupIdsByArticleWrapper(Article a)
    {
        List<int> list = new List<int>();
        list.Add(0);
        list.Add(1);
        list.Add(2);
        return list;
    }

    
    private List<string> testGetPrimaryGroupNamesByPrimaryGroupIds(List<int> gids)
    {
        List<string> list = new List<string>();
        list.Add("军事");
        list.Add("游戏");
        list.Add("暴力");
        return list;
    }
    
}