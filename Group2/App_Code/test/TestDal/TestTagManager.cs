using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestTagManager 的摘要说明
/// </summary>
public class TestTagManager
{
	public TestTagManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static int[] ids;   // 测试用例的tagId列表
    private static int errorCount = 0;  //全局错误数
    private static int primaryGroupId = 1;  //测试用例关联的主分类

    // 返回简略测试结果报告
    public static string generalTest(int testTimes , int groupId)
    {
        test(testTimes , groupId);
        string output = "测试TagManager过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes, int groupId)
    {
        count = testTimes;
        errorCount = 0;
        ids = new int[count];
        primaryGroupId = groupId;

        string output = "";
        output += "开始测试TagManager（测试样本数为" + count + "）\n\n";
        output += ">> Case 1 :\n";
        output += addTag() + "\n";
        output += ">> Case 2 :\n";
        output += getTag() + "\n";
        output += ">> Case 3 :\n";
        output += getTagsByIdList() + "\n";
        output += ">> Case 4 :\n";
        output += getAllTagsByCertainGroupId() + "\n";
        output += ">> Case 5 :\n";
        output += deleteTag() + "\n";

        return output;
    }

    private static string addTag()
    {
        string output = "addTag\n";

        Random r = new Random();

        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);
            string name = "测试样例" + no;
            Tag t = new Tag( name , "" , primaryGroupId , DateTime.Now , no/2 );
            ids[i] = TagManager.addTag(t);
            if (ids[i] < 0)
            {
                output += "Error! 新增Tag\"" + name + "\"失败！返回值为" + ids[i] + "\n";
                errorCount++;
            }
            else
                output += "Ok! 新增Tag\"" + name + "\"成功！返回值为" + ids[i] + "\n";
        }
        return output;
    }

    private static string getTag()
    {
        string output = "getTag\n";

        for (int i = 0; i < count; i++)
        {
            Tag t = new Tag(ids[i], "", "", primaryGroupId, DateTime.Now, 0);
            t = TagManager.getTag(t);
            if (t == null)
            {
                output += "Error! 获取id为" + ids[i] + "的Tag失败！返回值为空。\n";
                errorCount++;
            }
            else
                output += "Ok! 获取id为" + ids[i] + "的Tag成功！\n";
        }

        return output;
    }

    private static string getTagsByIdList()
    {
        string output = "getTagsByIdList\n";
        List<int> idList = new List<int>();
        for (int i = 0; i < count; i++)
            idList.Add(ids[i]);
        List<Tag> list = TagManager.getTagsByIdList(idList);
        if (list == null)
        {
            output += "Error! 调用getTagsByIdList失败，返回null。\n";
            errorCount++;
        }
        else
            output += "Ok! 调用getTagsByIdList成功，返回的Tag数为" + list.Count + "。\n";

        return output;
    }

    private static string getAllTagsByCertainGroupId()
    {
        string output = "getAllTagsByCertainGroupId\n";
        List<Tag> list = TagManager.getAllTagsByCertainGroupId(primaryGroupId);
        if (list == null)
        {
            output += "Error! 调用getAllTagsByCertainGroupId失败，返回null。\n";
            errorCount++;
        }
        else
            output += "Ok! 调用getAllTagsByCertainGroupId成功，返回的文章数为" + list.Count + "。\n";

        return output;
    }


    private static string deleteTag()
    {
        string output = "deleteTag\n";

        for (int i = 0; i < count; i++)
        {
            Tag t = new Tag( ids[i] , "" , "", primaryGroupId, DateTime.Now, 0);
            if (!TagManager.deleteTag(t))
            {
                output += "Error! 删除Tag（id为\"" + ids[i] + "\"）失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 删除Tag（id为\"" + ids[i] + "\"）成功！\n";
        }

        return output;
    }
}