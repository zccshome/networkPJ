using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestFocusAndCreateTag 的摘要说明
/// </summary>
public class TestFocusAndCreateTag
{
	public TestFocusAndCreateTag()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static int errorCount = 0;  //全局错误数
    private static PrimaryGroups[] pgs;    //全局测试主分类
    private static Tag[] tags;    //全局测试Tag
    private static Tag[] interLabels;    //全局测试兴趣标签 
    private static User sampleUser;     //全局测试用户
    private static string output;   //全局结果输出

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试FocusAndCreateTag过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes)
    {
        count = testTimes;
        errorCount = 0;
        pgs = new PrimaryGroups[count];
        tags = new Tag[count];
        interLabels = new Tag[count];

        output = "";
        output += "开始测试FocusAndCreateTag（测试样本数为" + count + "）\n\n";


        // Prepare

        if (!prepare())
            return output;

        // Test

        output += ">> Case 1 :\n";
        saveFocus(); 
        output += "\n";
        output += ">> Case 2 :\n";
        addTag();
        output += "\n";
        output += ">> Case 3 :\n";
        deleteTag();
        output += "\n";


        // Clean

        clean();

        return output;
    }

    private static void saveFocus()
    {

        output += "saveFocus\n";

        List<int> list1 = new List<int>() ;
        List<int> list2 = new List<int>();
        for (int i = 0; i < count / 2; i++)
            list1.Add(tags[i].TagId);
        for (int i = count/2; i < count; i++)
            list2.Add(tags[i].TagId);

        // 第一次测试
        output += "第一次测试：\n";
        if ( !FocusAndCreateTag.saveFocus(list1, sampleUser.UserId) )
        {
            output += "Error! saveFocus调用失败！返回false\n";
            errorCount++;
            return ;
        }
        else
            output += "Ok! saveFocus调用成功！返回true\n";

        // 第一次测试成果检查
        List<int> result1 = User2TagManager.getTagListByUserId(sampleUser);
        if (result1 == null)
        {
            output += "Error! saveFocus执行失败！getTagListByUserId返回值为null。\n";
            errorCount++;
            return;
        }
        if (result1.Count < count/2 - 1 )
        {
            output += "Error! 通过getTagListByUserId返回列表长度（=" + result1.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 通过getTagListByUserId返回列表长度（=" + result1.Count + "）不少于测试用例数量！\n";

        // 第二次测试
        output += "第二次测试：\n";
        if (!FocusAndCreateTag.saveFocus(list2, sampleUser.UserId))
        {
            output += "Error! saveFocus调用失败！返回false\n";
            errorCount++;
            return;
        }
        else
            output += "Ok! saveFocus调用成功！返回true\n";

        // 第二次测试成果检查
        List<int> result2 = User2TagManager.getTagListByUserId(sampleUser);
        if (result2 == null)
        {
            output += "Error! saveFocus执行失败！getTagListByUserId返回值为null。\n";
            errorCount++;
            return;
        }
        if (result2.Count < count / 2 - 1)
        {
            output += "Error! 通过getTagListByUserId返回列表长度（=" + result2.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 通过getTagListByUserId返回列表长度（=" + result2.Count + "）不少于测试用例数量！\n";
    }

    private static void addTag()
    {
        output += "addTag\n";

        Random r = new Random();

        // insert
        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);

            if (!FocusAndCreateTag.addTag(pgs[i].GroupId, "intre" + no, "intre" + no, sampleUser.UserId))
            {
                output += "Error! 新增兴趣标签失败！返回false。\n";
                errorCount++;
                continue;
            }   
        }

        // check
        List<int> list = User2TagManager.getTagListByUserId(sampleUser);
        if (list.Count < count)
        {
            output += "Error! 通过getTagListByUserId返回列表长度（=" + list.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 通过getTagListByUserId返回列表长度（=" + list.Count + "）不少于测试用例数量！\n";
    }

    private static void deleteTag()
    {
        output += "deleteTag\n";

        List<int> list = User2TagManager.getTagListByUserId(sampleUser);
        foreach (int id in list)
        {
            User2Tag u2t2 = new User2Tag(sampleUser.UserId, id);
            if (!FocusAndCreateTag.deleteTag(u2t2))
            {
                output += "Error! 删除User2Tag记录失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 删除User2Tag记录成功！\n";
        }
        List<int> list2 = User2TagManager.getTagListByUserId(sampleUser);

        int list2_len = 0;
        if (list2 != null)
            list2_len = list2.Count;

        if (list2_len == list.Count)
            output += "Error! 调用deleteTag前后getTagListByUserId返回的列表长度相同！未能真正删除User2Tag记录！\n";
    }

    private static bool prepare()
    {
        Random r = new Random();

        // add global user
        sampleUser = new User("test", "test", "test");
        sampleUser.UserId = UserManager.addUser(sampleUser);
        if (sampleUser.UserId < 0)
        {
            output += "Error! 新增用户\"test\"失败！返回值为" + sampleUser.UserId + "。测试无法继续进行。请先解决UserManager中的错误。\n";
            errorCount++;
            return false;
        }

        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);

            // add primary groups
            pgs[i] = new PrimaryGroups(100, no + "");
            pgs[i].GroupId = PrimaryGroupMananger.addRecord(pgs[i]);
            if (pgs[i].GroupId < 0)
            {
                output += "Error! 新增主分类失败！返回值为" + pgs[i].GroupId + "。测试无法继续进行。请先解决PrimaryGroupManager中的错误。\n";
                errorCount++;
                return false;
            }

            // add public sub groups
            tags[i] = new Tag("sub" + no, "sub" + no, pgs[i].GroupId, DateTime.Now, 0);
            tags[i].TagId = TagManager.addTag(tags[i]);
            if (tags[i].TagId < 0)
            {
                output += "Error! 新增公共子分类失败！返回值为" + tags[i].TagId + "。测试无法继续进行。请先解决TagManager中的错误。\n";
                errorCount++;
                return false;
            }

            // link user to sub groups
            User2Tag u2t = new User2Tag(sampleUser.UserId, tags[i].TagId);
            if (!User2TagManager.addRecord(u2t))
            {
                output += "Error! 新增User2Tag记录失败！测试无法继续进行。请先解决User2TagManager中的错误。\n";
                errorCount++;
                return false;
            }
        }

        return true;
    }

    private static bool clean()
    {
        

        for (int i = 0; i < count; i++)
        {
            // unlink user to sub groups
            User2Tag u2t = new User2Tag(sampleUser.UserId, tags[i].TagId);
            if (!User2TagManager.deleteRecord(u2t))
            {
                output += "Error! 删除User2Tag记录失败！测试无法继续进行。请先解决User2TagManager中的错误。\n";
                errorCount++;
                return false;
            }

            // delete public sub groups
            if (!TagManager.deleteTag(tags[i]))
            {
                output += "Error! 删除测试公共子分类失败！测试无法继续进行。请先解决TagManager中的错误。\n";
                errorCount++;
                return false;
            }

            // delete primary groups
            if (!PrimaryGroupMananger.deleteRecord(pgs[i]))
            {
                output += "Error! 删除测试主分类失败！测试无法继续进行。请先解决PrimaryGroupManager中的错误。\n";
                errorCount++;
                return false;
            }
        }

        if (!UserManager.deleteRecord(sampleUser))
        {
            output += "Error! 删除用户\"test\"失败！测试无法继续进行。请先解决UserManager中的错误。\n";
            errorCount++;
            return false;
        }

        return true;
    }
}