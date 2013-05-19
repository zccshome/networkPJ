using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestPrimaryGroupKeyWordsManager 的摘要说明
/// </summary>
public class TestPrimaryGroupKeyWordsManager
{
	public TestPrimaryGroupKeyWordsManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static string[] keys;    // 测试样例的keyWord字段值列表
    private static int errorCount = 0;  //全局错误数
    private static int primaryGroupId = 0; // 默认将样例关键词关联到的主分类

    // 返回简略测试结果报告
    public static string generalTest(int testTimes ,int groupId )
    {
        test(testTimes ,groupId);
        string output = "测试PrimaryGroupKeyWordsManager过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes, int groupId)
    {
        count = testTimes;
        errorCount = 0;
        keys = new string[count];
        primaryGroupId = groupId;

        string output = "";
        output += "开始测试PrimaryGroupKeyWordsManager（测试样本数为" + count + "）\n\n";
        output += ">> Case 1 :\n";
        output += addRecord() + "\n";
        output += ">> Case 2 :\n";
        output += getKeyWordsOfCertainPrimaryGroup() + "\n";
        output += ">> Case 3 :\n";
        output += deleteRecord() + "\n";

        return output;
    }

    private static string addRecord()
    {
        string output = "addRecord\n";

        Random r = new Random();

        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);
            keys[i] = "测试样例" + no;
            PrimaryGroupKeyWords kw = new PrimaryGroupKeyWords(primaryGroupId,keys[i]);
            if ( !PrimaryGroupKeyWordsManager.addRecord(kw) )
            {
                output += "Error! 新增主分类（id为" + primaryGroupId + "）关键词\"" + keys[i] + "\"失败！返回false。\n";
                errorCount++;
            }
            else
                output += "Ok! 新增主分类（id为" + primaryGroupId + "）关键词\"" + keys[i] + "\"成功！返回true。\n";
        }
        return output;
    }

    private static string getKeyWordsOfCertainPrimaryGroup()
    {
        string output = "getKeyWordsOfCertainPrimaryGroup\n";
        PrimaryGroups pg = new PrimaryGroups(primaryGroupId, "");
        List<string> list = PrimaryGroupKeyWordsManager.getKeyWordsOfCertainPrimaryGroup(pg);
        if (list == null)
        {
            output += "Error! getKeyWordsOfCertainPrimaryGroup失败，返回null。\n";
            errorCount++;
            return output;
        }
        for (int i = 0; i < count; i++)
        {
            if ( !list.Contains(keys[i]))
                output += "Error! 未拿到keyWord为" + keys[i] + "的测试记录！\n";
            else
                output += "Ok! 成功拿到keyWord为" + keys[i] + "的测试记录！\n";
        }

        return output;
    }

    private static string deleteRecord()
    {
        string output = "deleteRecord\n";

        for (int i = 0; i < count; i++)
        {
            PrimaryGroupKeyWords kw = new PrimaryGroupKeyWords(primaryGroupId, keys[i]);
            if (!PrimaryGroupKeyWordsManager.deleteRecord(kw))
            {
                output += "Error! 删除测试关键词（keyWord为\"" + keys[i] + "\"）失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 删除测试关键词（keyWord为\"" + keys[i] + "\"）成功！\n";
        }

        return output;
    }
}