using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    private static int sampleCount = 10 ; // 默认的测试样本数

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string output = "";
        output += "DAL层整体简略测试结果：\n";
        output += TestArticleManager.generalTest(sampleCount);
        output += TestArticle2GroupManager.generalTest(sampleCount, 0);
        output += TestGlobalParseManager.generalTest(sampleCount);
        output += TestLocalParseManager.generalTest(sampleCount);
        output += TestPrimaryGroupManager.generalTest(sampleCount);
        output += TestPrimaryGroupKeyWordsManager.generalTest(sampleCount,0);
        output += TestTagManager.generalTest(sampleCount ,1 );
        output += TestUserManager.generalTest(sampleCount);
        output += TestUser2TagManager.generalTest(sampleCount);
        TextArea1.Value = output;
    }
    
    protected void Button4_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestArticleManager.test(sampleCount);    }
    protected void Button3_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestArticle2GroupManager.test(sampleCount, 0);    }
    protected void Button5_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestGlobalParseManager.test(sampleCount);    }
    protected void Button6_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestLocalParseManager.test(sampleCount);    }
    protected void Button8_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestPrimaryGroupManager.test(sampleCount);    }
    protected void Button7_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestPrimaryGroupKeyWordsManager.test(sampleCount,0);    }
    protected void Button9_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestTagManager.test(sampleCount ,1);     }
    protected void Button11_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestUserManager.test(sampleCount);    }
    protected void Button10_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestUser2TagManager.test(sampleCount);    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string output = "";
        output += "BLL层整体简略测试结果：\n";
        output += TestGroupQuery.generalTest(sampleCount);
        output += TestAccountAssist.generalTest(sampleCount);
        output += TestSearch.generalTest(sampleCount);
        output += TestFocusAndCreateTag.generalTest(sampleCount);
        output += TestNewsAssist.generalTest(sampleCount);
        output += TestManagerAssist.generalTest(sampleCount);
        TextArea1.Value = output;
    }
    protected void Button14_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestGroupQuery.test(sampleCount);    }
    protected void Button12_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestAccountAssist.test(sampleCount);    }
    protected void Button17_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestSearch.test(sampleCount);     }
    protected void Button13_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestFocusAndCreateTag.test(sampleCount);    }
    protected void Button16_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestNewsAssist.test(sampleCount);    }
    protected void Button15_Click(object sender, EventArgs e)
    {        TextArea1.Value = TestManagerAssist.test(sampleCount);    }
}