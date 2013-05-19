using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestLocalParseManager 的摘要说明
/// </summary>
public class TestLocalParseManager
{
	public TestLocalParseManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static string[] titles;    // LocalParse测试样例的wordContent列表
    private static int errorCount = 0;  //全局错误数
    private static Article sampleArticle;   // 测试样例文章

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试LocalParseManager过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes)
    {
        count = testTimes;
        errorCount = 0;
        titles = new string[count];

        string output = "";
        output += "开始测试LocalParseManager（测试样本数为" + count + "）\n\n";

        sampleArticle = new Article(0, "测试样例", "测试样例", "", DateTime.Now, 0, 0);
        int aId = ArticleManager.addRecord(sampleArticle);
        if (aId < 0)
        {
            output += "Error! 增加测试样例文章失败！测试无法继续进行。请先解决GlobalParseManager中的错误。\n";
            errorCount++;
            return output;
        }
        else
            sampleArticle.ArticleId = aId;

        output += ">> Case 1 :\n";
        output += addRecord() + "\n";
        output += ">> Case 2 :\n";
        output += selectRecord() + "\n";
        output += ">> Case 3 :\n";
        output += updateRecord() + "\n";
        output += ">> Case 4 :\n";
        output += deleteRecord() + "\n";

        if (!ArticleManager.deleteRecord(sampleArticle))
        {
            output += "Error! 全部测试完成后自动删除测试样例文章失败！请先解决ArticleManager中的错误。\n";
            errorCount++;
        }
        //*/
        return output;
    }

    private static string addRecord()
    {
        string output = "addRecord\n";

        Random r = new Random();

        // add Global Parse        
        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);

            titles[i] = no + "";
            GlobalParse gp = new GlobalParse(titles[i], "n", sampleArticle.ArticleId);
            if (!GlobalParseManager.addRecord(gp))
            {
                output += "Error! 新增GlobalParse记录\"" + titles[i] + "\"失败！测试无法继续进行。请先解决PrimaryGroupManager中的错误。\n";
                errorCount++;
            }
        }


        for (int i = 0; i < count; i++)
        {
            LocalParse lp = new LocalParse( sampleArticle.ArticleId , titles[i], "n", sampleArticle.ArticleId);
            if (!LocalParseManager.addRecord(lp))
            {
                output += "Error! 为id是\"" + sampleArticle.ArticleId + "\"的样例文章新增wordContent为\"" + titles[i] + "\"的记录失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 为id是\"" + sampleArticle.ArticleId + "\"的样例文章新增wordContent为\"" + titles[i] + "\"的记录成功！\n";
        }
        return output;
    }

    private static string selectRecord()
    {
        string output = "selectRecord\n";

        for (int i = 0; i < count; i++)
        {
            LocalParse lp = new LocalParse(sampleArticle.ArticleId, titles[i], "n", sampleArticle.ArticleId);
            //output += sampleArticle.ArticleId + "\t" + titles[i] + "\n";
            lp = LocalParseManager.selectRecord(lp);
            //output += lp.ArticleId + "\n";
            if (lp == null)
            {
                output += "Error! 通过ArticleId和WordContent获取LocalParse记录失败！返回值为空。\n";
                errorCount++;
            }
            else
                output += "Ok! 通过ArticleId和WordContent获取LocalParse记录成功！返回值为空。\n";
        }

        return output;
    }

    private static string updateRecord()
    {
        string output = "updateRecord\n";

        for (int i = 0; i < count; i++)
        {
            LocalParse lp = new LocalParse(sampleArticle.ArticleId, titles[i], "n", sampleArticle.ArticleId + 1);
            if (!LocalParseManager.updateRecord(lp))
            {
                output += "Error! 调用updateRecord对WordContent为\"" + titles[i] + "\"的记录失败！返回值为false。\n";
                errorCount++;
                return output;
            }
            else
                output += "Ok! 调用updateRecord对WordContent为\"" + titles[i] + "\"的记录成功！返回值为true。\n";
            lp = LocalParseManager.selectRecord(lp);
            if (lp == null)
            {
                output += "Error! 通过WordContent\"" + titles[i] + "\"查询记录失败！返回值为空。\n";
                errorCount++;
                continue;
            }
            if (lp.Count != sampleArticle.ArticleId + 1 )
            {
                output += "Error! 对WordContent为\"" + titles[i] + "\"的记录更新Count字段失败！Count被更改为" + lp.Count + "。\n";
                errorCount++;
            }
            else
                output += "Ok! 对WordContent为\"" + titles[i] + "\"的记录更新Count字段成功！Count被更改为" + lp.Count + "。\n";
        }

        return output;
    }

    private static string deleteRecord()
    {
        string output = "deleteRecord\n";

        for (int i = 0; i < count; i++)
        {
            LocalParse lp = new LocalParse(sampleArticle.ArticleId, titles[i], "n", sampleArticle.ArticleId + 1);
            if (!LocalParseManager.deleteRecord(lp))
            {
                output += "Error! 删除WordContent为\"" + titles[i] + "\"的LocalParse记录失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 删除WordContent为\"" + titles[i] + "\"的LocalParse记录成功！\n";
        }

        // delete Global Parse
        for (int i = 0; i < count; i++)
        {
            GlobalParse gp = new GlobalParse();
            gp.WordContent = titles[i];
            if (!GlobalParseManager.deleteRecord(gp))
            {
                output += "Error! 删除WordContent为\"" + titles[i] + "\"的GlobalParse记录失败！测试无法继续进行。请先解决GlobalParseManager中的错误。\n";
                errorCount++;
            }
        }

        return output;
    }
}