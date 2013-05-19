using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestUser2TagManager 的摘要说明
/// </summary>
public class TestUser2TagManager
{
	public TestUser2TagManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static int[] ids;   // 测试Tag的tagId列表
    private static User sampleUser;  // 测试用例关联到的测试用户
    private static int errorCount = 0;  //全局错误数

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试User2TagManager过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes)
    {
        count = testTimes;
        errorCount = 0;
        ids = new int[count];

        string output = "";
        output += "开始测试User2TagManager（测试样本数为" + count + "）\n\n";

        sampleUser = new User( 0 , "", "", "" );
        int aId = UserManager.addUser(sampleUser);
        if (aId < 0)
        {
            output += "Error! 增加测试样例用户失败！测试无法继续进行。请先解决UserManager中的错误。\n";
            errorCount++;
            return output;
        }
        else
            sampleUser.UserId = aId;

        output += ">> Case 1 :\n";
        output += addRecord() + "\n";
        output += ">> Case 2 :\n";
        output += getTagListByUserId() + "\n";
        output += ">> Case 3 :\n";
        output += deleteRecord() + "\n";

        if (!UserManager.deleteRecord(sampleUser))
        {
            output += "Error! 全部测试完成后自动删除测试样例用户失败！请先解决UserManager中的错误。\n";
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
            Tag t = new Tag( "" , "" , 1 , DateTime.Now , 1 );
            ids[i] = TagManager.addTag(t);
            if (ids[i] < 0)
            {
                output += "Error! 新增测试Tag失败！返回值为" + ids[i] + "\n";
                errorCount++;
                continue;
            }

            output += "Ok! 新增测试Tag成功！返回值为" + ids[i] + "\n";
            User2Tag u2t = new User2Tag(sampleUser.UserId, ids[i]);
            if (!User2TagManager.addRecord(u2t))
            {
                output += "Error! 将id为" + ids[i] + "的Tag关联到id为" + sampleUser.UserId + "的用户时失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 将id为" + ids[i] + "的Tag关联到id为" + sampleUser.UserId + "的用户时成功！\n";
        }
        return output;
    }

    private static string getTagListByUserId()
    {
        string output = "getTagListByUserId\n";
        List<int> list = User2TagManager.getTagListByUserId(sampleUser);
        if (list == null)
        {
            output += "Error! 调用getTagListByUserId失败，返回null。\n";
            errorCount++;
        }
        else
            output += "Ok! 调用getTagListByUserId成功，返回的tag数为" + list.Count + "。\n";

        return output;
    }

    private static string deleteRecord()
    {
        string output = "deleteRecord\n";

        for (int i = 0; i < count; i++)
        {
            User2Tag u2t = new User2Tag(sampleUser.UserId, ids[i]);
            if (!User2TagManager.deleteRecord(u2t))
            {
                output += "Error! 删除关联（TagId为" + ids[i] + "，用户id为" + sampleUser.UserId + "）失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 删除关联（TagId为" + ids[i] + "，用户id为" + sampleUser.UserId + "）成功！\n";
            Tag t = new Tag( ids[i] , "" , "" , 1 , DateTime.Now , 1 );
            if (!TagManager.deleteTag(t))
            {
                output += "Error! 删除测试Tag失败（TagId为" + ids[i] + "）！\n";
                errorCount++;
            }
            else
                output += "Ok! 删除测试Tag成功（TagId为" + ids[i] + "）！\n";
        }       

        return output;
    }

}