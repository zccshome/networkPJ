using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestPrimaryGroupManager 的摘要说明
/// </summary>
public class TestPrimaryGroupManager
{
	public TestPrimaryGroupManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static int[] ids;   // 测试样例的groupId列表
    private static int errorCount = 0;  //全局错误数

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试PrimaryGroupManager过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes)
    {
        count = testTimes;
        errorCount = 0;
        ids = new int[count];

        string output = "";
        output += "开始测试PrimaryGroupManager（测试样本数为" + count + "）\n\n";
        output += ">> Case 1 :\n";
        output += addRecord() + "\n";
        output += ">> Case 2 :\n";
        output += selectRecord() + "\n";
        output += ">> Case 3 :\n";
        output += updateRecord() + "\n";
        output += ">> Case 4 :\n";
        output += getAllGroups() + "\n";
        output += ">> Case 5 :\n";
        output += deleteRecord() + "\n";
        //*/
        return output;
    }

    private static string addRecord()
    {
        string output = "addRecord\n";

        Random r = new Random();

        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);
            string name = "测试样例" + no;
            PrimaryGroups pg = new PrimaryGroups(no , name);
            int id = PrimaryGroupMananger.addRecord(pg) ;
            ids[i] = id;
            if ( id < 0 )
            {
                output += "Error! 新增主分类\"" + name + "\"失败！返回值为" + id + "\n";
                errorCount++;
            }
            else
                output += "Ok! 新增主分类\"" + name + "\"成功！返回值为" + id + "\n";
        }
        return output;
    }

    private static string selectRecord()
    {
        string output = "selectRecord\n";

        for (int i = 0; i < count; i++)
        {
            PrimaryGroups pg = new PrimaryGroups( ids[i] , "" );
            pg = PrimaryGroupMananger.selectRecord(pg);
            if (pg == null)
            {
                output += "Error! 获取id为" + ids[i] + "的主分类记录失败！返回值为null。\n";
                errorCount++;
            }
            else
                output += "Ok! 获取id为" + ids[i] + "的主分类记录成功！\n";
        }

        return output;
    }

    private static string updateRecord()
    {
        string output = "updateRecord\n";

        for (int i = 0; i < count; i++)
        {
            PrimaryGroups pg = new PrimaryGroups(ids[i], "test");
            //output +=  pg.GroupId + "\t" + pg.GroupName + "\n";
            if (!PrimaryGroupMananger.updateRecord(pg))
            {
                output += "Error! 调用updateRecord对id为\"" + ids[i] + "\"的测试主分类记录失败！返回值为false。\n";
                errorCount++;
            }
            else
                output += "Ok! 调用updateRecord对id为\"" + ids[i] + "\"的测试主分类记录成功！返回值为true。\n";
            pg = PrimaryGroupMananger.selectRecord(pg);
            if (pg == null)
            {
                output += "Error! 获取id为" + ids[i] + "的测试主分类失败！返回值为空。\n";
                errorCount++;
                continue;
            }
            if ( !pg.GroupName.Equals("test"))
            {
                output += "Error! 对id为" + ids[i] + "的测试主分类更新GroupName失败！GroupName被更改为" + pg.GroupName + "。\n";
                errorCount++;
            }
            else
                output += "Ok! 对id为" + ids[i] + "的测试主分类更新GroupName成功！GroupName被更改为" + pg.GroupName + "。\n";
        }

        return output;
    }

    private static string getAllGroups()
    {
        string output = "getAllGroups\n";
        List<string[]> list = PrimaryGroupMananger.getAllGroups();
        if (list == null)
        {
            output += "Error! getAllGroups失败，返回null。\n";
            errorCount++;
            return output;
        }
        if ( list.Count < count )
            output += "Error! 返回的主分类记录条数少于测试样本数！\n";
        else
            output += "Ok! 返回的主分类记录条数多于测试样本数！\n";
        
        /*
        for (int i = 0; i < count; i++)
        {
            if ( !list.Contains(new string[] { ids[i] + "" , "test" }))
                //output += "Error! 未拿到id为" + ids[i] + "的测试主分类记录！\n";
                output += list.ElementAt(i)[0] + "\t" + list.ElementAt(i)[1] + "\n";
            else
                output += "Ok! 成功拿到id为" + ids[i] + "的测试主分类记录！\n";
        }
        */

        return output;
    }

    private static string deleteRecord()
    {
        string output = "deleteRecord\n";

        for (int i = 0; i < count; i++)
        {
            PrimaryGroups pg = new PrimaryGroups(ids[i], "");
            if (!PrimaryGroupMananger.deleteRecord(pg))
            {
                output += "Error! 删除测试主分类（id为\"" + ids[i] + "\"）失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 删除测试主分类（id为\"" + ids[i] + "\"）成功！\n";
        }

        return output;
    }
}