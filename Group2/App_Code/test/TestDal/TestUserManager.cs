using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestUserManager 的摘要说明
/// </summary>
public class TestUserManager
{
	public TestUserManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static int[] ids;   // 测试用户的id列表
    private static string[] names; //测试用户的name列表（测试用户的name、pass和邮箱地址相同）
    private static int errorCount = 0;  //全局错误数

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试UserManager过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes)
    {
        count = testTimes;
        errorCount = 0;
        ids = new int[count];
        names = new string[count];

        string output = "";
        output += "开始测试UserManager（测试样本数为" + count + "）\n\n";
        output += ">> Case 1 :\n";
        output += addUser() + "\n";
        output += ">> Case 2 :\n";
        output += getUserById() + "\n";
        output += ">> Case 3 :\n";
        output += getUserByNameAndPass() + "\n";
        output += ">> Case 4 :\n";
        output += userMailRegistered() + "\n";
        output += ">> Case 12 :\n";
        output += deleteRecord() + "\n";

        return output;
    }

    private static string addUser()
    {
        string output = "addUser\n";

        Random r = new Random();

        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);
            names[i] = "测试样例" + no;
            User u = new User(names[i], names[i], names[i]);
            ids[i] = UserManager.addUser(u);
            if (ids[i] < 0)
            {
                output += "Error! 新增用户\"" + names[i] + "\"失败！返回值为" + ids[i] + "\n";
                errorCount++;
            }
            else
                output += "Ok! 新增用户\"" + names[i] + "\"成功！返回值为" + ids[i] + "\n";
        }
        return output;
    }

    private static string getUserById()
    {
        string output = "getUserById\n";

        for (int i = 0; i < count; i++)
        {
            User u = new User( ids[i] , "" , "" , "" );
            u = UserManager.getUserById(u);
            if (u == null)
            {
                output += "Error! 获取id为" + ids[i] + "的用户失败！返回值为空。\n";
                errorCount++;
            }
            else
                output += "Ok! 获取用户\"" + names[i] + "\"成功！\n";
        }

        return output;
    }

    private static string getUserByNameAndPass()
    {
        string output = "getUserByNameAndPass\n";

        for (int i = 0; i < count; i++)
        {
            User u = new User(names[i], names[i], names[i]);
            u = UserManager.getUserByNameAndPass(u);
            if (u == null)
            {
                output += "Error! 获取用户名和密码为" + names[i] + "的用户失败！返回值为空。\n";
                errorCount++;
            }
            else
                output += "Ok! 获取用户名和密码为" + names[i] + "\"的用户成功！\n";
        }

        return output;
    }

    private static string userMailRegistered()
    {
        string output = "userMailRegistered\n";

        for (int i = 0; i < count; i++)
        {
            if (!UserManager.userMailRegistered(names[i]))
            {
                output += "Error! 验证邮箱已被注册，失败！" + names[i] + "的邮箱已被注册但未被检测到重复！\n";
                errorCount++;
            }
            else
                output += "Ok! 验证邮箱已被注册，成功！" + names[i] + "的邮箱已被注册且被检测到重复！\n";
        }
        for (int i = 0; i < count; i++)
        {
            Random r = new Random();
            string mail = r.Next(1000, 9000) + "" ;
            if (UserManager.userMailRegistered(mail))
            {
                output += "Error! 验证邮箱未被注册，失败！" + mail + "的邮箱未被注册但被检测到重复！\n";
                errorCount++;
            }
            else
                output += "Ok! 验证邮箱未被注册，成功！" + mail + "的邮箱未被注册且未被检测到重复！\n";
        }

        return output;
    }

    private static string deleteRecord()
    {
        string output = "deleteRecord\n";

        for (int i = 0; i < count; i++)
        {
            User u = new User(ids[i],names[i], names[i], names[i]);
            if (!UserManager.deleteRecord(u))
            {
                output += "Error! 删除测试用户\"" + names[i] + "\"失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 删除测试用户\"" + names[i] + "\"成功！\n";
        }

        return output;
    }
}