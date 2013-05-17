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
        string url;
        // 如果他们当中有一个是null 则重新读取一遍
        if (ManagerAspxAssist.stop_list.Count == 0 || ManagerAspxAssist.other.Count == 0
            || ManagerAspxAssist.military.Count == 0 || ManagerAspxAssist.gym.Count == 0
            || ManagerAspxAssist.economy.Count == 0)
        {
            url = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "App_Data\\StopList\\中文停用词.txt";
            StreamReader objReader = new StreamReader(url, Encoding.GetEncoding("UTF-8"));
            //StringReader SR = new StringReader(url);
            String line = "";
            while ((line = objReader.ReadLine()) != null)
            {
                ManagerAspxAssist.stop_list.Add(line);
            }
            objReader.Close();
            url = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "App_Data\\StopList\\other.txt";
            objReader = new StreamReader(url, Encoding.GetEncoding("UTF-8"));
            while ((line = objReader.ReadLine()) != null)
            {
                ManagerAspxAssist.other.Add(line);
                Console.WriteLine(line);
            }
            objReader.Close();
            url = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "App_Data\\StopList\\military.txt";
            objReader = new StreamReader(url, Encoding.GetEncoding("UTF-8"));
            while ((line = objReader.ReadLine()) != null)
            {
                ManagerAspxAssist.military.Add(line);
            }
            objReader.Close();
            url = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "App_Data\\StopList\\gym.txt";
            objReader = new StreamReader(url, Encoding.GetEncoding("UTF-8"));
            while ((line = objReader.ReadLine()) != null)
            {
                ManagerAspxAssist.gym.Add(line);
            }
            objReader.Close();
            url = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "App_Data\\StopList\\economy.txt";
            objReader = new StreamReader(url, Encoding.GetEncoding("UTF-8"));
            while ((line = objReader.ReadLine()) != null)
            {
                ManagerAspxAssist.economy.Add(line);
            }
            objReader.Close();
        }
    }
}
