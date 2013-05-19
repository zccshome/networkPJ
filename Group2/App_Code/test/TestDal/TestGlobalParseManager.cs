using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestGlobalParseManager 的摘要说明
/// </summary>
public class TestGlobalParseManager
{
	public TestGlobalParseManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static string[] titles;    // GlobalParse测试样例的wordContent列表
    private static int errorCount = 0;  //全局错误数
    private static Article sampleArticle;   // 测试样例文章

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试GlobalParseManager过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes)
    {
        count = testTimes;
        errorCount = 0;
        titles = new string[count];

        string output = "";
        output += "开始测试GlobalParseManager（测试样本数为" + count + "）\n\n";

        sampleArticle = new Article(0, "测试样例", "测试样例", "", DateTime.Now, 0, 0);
        int aId = ArticleManager.addRecord(sampleArticle);
        if (aId < 0)
        {
            output += "Error! 增加测试样例文章失败！测试无法继续进行。请先解决ArticleManager中的错误。\n";
            errorCount++;
            return output;
        }
        else
            sampleArticle.ArticleId = aId;

        output += ">> Case 1 :\n";
        output += addRecord() + "\n";
        output += ">> Case 2 :\n";
        output += selectRecordByWordContent() + "\n";
        output += ">> Case 3 :\n";
        output += updateRecord() + "\n";
        output += ">> Case 4 :\n";
        output += deleteRecord() + "\n";

        if (!ArticleManager.deleteRecord(sampleArticle))
        {
            output += "Error! 全部测试完成后自动删除测试样例文章失败！请先解决ArticleManager中的错误。\n";
            errorCount++;
        }

        return output;
    }

    private static string addRecord()
    {
        string output = "addRecord\n";        

        Random r = new Random();

        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);

            titles[i] =  no + "";
            GlobalParse gp = new GlobalParse( titles[i] , "n" , sampleArticle.ArticleId );
            if (!GlobalParseManager.addRecord(gp))
            {
                output += "Error! 新增记录\"" + titles[i] + "\"失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 新增记录\"" + titles[i] + "\"成功！\n";
        }
        return output;
    }

    private static string selectRecordByWordContent()
    {
        string output = "selectRecordByWordContent\n";

        for (int i = 0; i < count; i++)
        {
            GlobalParse gp = new GlobalParse();
            gp.WordContent = titles[i];
            gp = GlobalParseManager.selectRecordByWordContent(gp);
            if (gp == null)
            {
                output += "Error! 通过WordContent获取GlobalParse记录失败！返回值为空。\n";
                errorCount++;
            }
            else
                output += "Ok! 通过WordContent获取GlobalParse记录成功！\n";
        }

        return output;
    }

    private static string updateRecord()
    {
        string output = "updateRecord\n";

        for (int i = 0; i < count; i++)
        {
            GlobalParse gp = new GlobalParse(titles[i], "v", sampleArticle.ArticleId);
            if (!GlobalParseManager.updateRecord(gp))
            {
                output += "Error! 调用updateRecord对WordContent为\"" + titles[i] + "\"的记录失败！返回值为false。\n";
                errorCount++;
                return output;
            }
            else
                output += "Ok! 调用updateRecord对WordContent为\"" + titles[i] + "\"的记录成功！返回值为true。\n";
            gp = GlobalParseManager.selectRecordByWordContent(gp);
            if (gp == null)
            {
                output += "Error! 通过WordContent\"" + titles[i] + "\"查询记录失败！返回值为空。\n";
                errorCount++;
                continue;
            }
            if (!gp.Type.Equals("v"))
            {
                output += "Error! 对WordContent为\"" + titles[i] + "\"的记录更新Type字段失败！Type被更改为" + gp.Type + "。\n";
                errorCount++;
            }
            else
                output += "Ok! 对WordContent为\"" + titles[i] + "\"的记录更新Type字段成功！Type被更改为" + gp.Type + "。\n";
        }

        return output;
    }

    private static string deleteRecord()
    {
        string output = "deleteRecord\n";

        for (int i = 0; i < count; i++)
        {
            GlobalParse gp = new GlobalParse();
            gp.WordContent = titles[i];
            if (!GlobalParseManager.deleteRecord(gp))
            {
                output += "Error! 删除WordContent为\"" + titles[i] + "\"的GlobalParse记录失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 删除WordContent为\"" + titles[i] + "\"的GlobalParse记录成功！\n";
        }

        return output;
    }

}