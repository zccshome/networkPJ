using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestAccountAssist 的摘要说明
/// </summary>
public class TestAccountAssist
{
	public TestAccountAssist()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static string[] names;    //测试用户的name列表（测试用户的name、pass和邮箱地址相同）
    private static int[] ids;       //测试用户的id列表
    private static int errorCount = 0;  //全局错误数

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试AccountAssist过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes)
    {
        count = testTimes;
        errorCount = 0;
        names = new string[count];
        ids = new int[count];

        string output = "";
        output += "开始测试AccountAssist（测试样本数为" + count + "）\n\n";
        output += ">> Case 1 :\n";
        output += registerOk() + "\n";
        output += ">> Case 2 :\n";
        output += registerError() + "\n";
        output += ">> Case 3 :\n";
        output += loginOk() + "\n";
        output += ">> Case 4 :\n";
        output += loginError() + "\n";
        //*/

        clean();

        return output;
    }

    private static string registerOk()
    {
        string output = "registerOk\n";

        Random r = new Random();

        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);
            names[i] = "测试样例" + no;
            User u = new User(names[i], names[i], names[i]);
            ids[i] = AccountAssist.register(u) ;
            if ( ids[i] < 0 )
            {
                output += "Error! 新用户\"" + names[i] + "\"注册失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 新用户\"" + names[i] + "\"注册成功！\n";
        }
        return output;
    }

    private static string registerError()
    {
        string output = "registerError\n";

        for (int i = 0; i < count; i++)
        {
            User u = new User(names[i], names[i], names[i]);
            if (AccountAssist.register(u) >0 )
            {
                output += "Error! 重复用户\"" + names[i] + "\"注册居然成功？！\n";
                errorCount++;
            }
            else
                output += "Ok! 重复用户\"" + names[i] + "\"注册失败！\n";
        }
        return output;
    }

    private static string loginOk()
    {
        string output = "loginOk\n";

        for (int i = 0; i < count; i++)
        {
            User u = new User(names[i], names[i], names[i]);
            if (AccountAssist.login(u)==null)
            {
                output += "Error! 已注册用户\"" + names[i] + "\"登陆失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 已注册用户\"" + names[i] + "\"登陆成功！\n";
        }
        return output;
    }

    private static string loginError()
    {
        string output = "loginError\n";

        Random r = new Random();

        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);
            string temp = "样例" + no;
            User u = new User(temp, temp, temp);
            if (AccountAssist.login(u)!=null)
            {
                output += "Error! 未注册用户\"" + temp + "\"登陆居然成功？！\n";
                errorCount++;
            }
            else
                output += "Ok! 未注册用户\"" + temp + "\"登陆失败！\n";
        }
        return output;
    }

    private static string clean()
    {
        string output = "";

        for (int i = 0; i < count; i++)
        {
            User u = new User(ids[i], names[i], names[i], names[i]);
            if (!UserManager.deleteRecord(u))
            {
                output += "Error! 删除测试用户\"" + names[i] + "\"失败！测试无法继续进行。请先解决UserManager中的错误。\n";
                errorCount++;
            }
        }
        return output;
    }
}