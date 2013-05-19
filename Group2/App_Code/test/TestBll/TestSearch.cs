using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestSearch 的摘要说明
/// </summary>
public class TestSearch
{
	public TestSearch()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static string[] titles;    // 测试文章的title列表
    private static int[] ids;   // 测试文章的id列表
    private static int errorCount = 0;  //全局错误数
    private static string output = "";  //全局输出
    private static int primaryGroupId = 1;  //默认关联分类
    private static int OtherGroupId = 0;  //默认的“其他”分类
    private static string[] key;    //搜索关键词组

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试Search过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes)
    {
        count = testTimes;
        errorCount = 0;
        ids = new int[count];
        titles = new string[count];
        key = new string[] { "测试" , "样例" };

        output = "";
        output += "开始测试Search（测试样本数为" + count + "）\n\n";

        // Prepare
        if (!prepare())
            return output;

        // Test

        output += ">> Case 1 :\n";
        searchAll();
        output += "\n";
        output += ">> Case 2 :\n";
        searchInPrimaryGroup();
        output += "\n";
        output += ">> Case 3 :\n";
        searchInOther();
        output += "\n";

        // Clean
        clean();

        return output;
    }

    private static void searchAll()
    {
        output += "searchAll\n";
        List<Article> list = Search.searchAll(key);
        if (list == null)
        {
            output += "Error! 通过searchAll搜索失败！返回值为null。\n";
            errorCount++;
            return ;
        }

        if (list.Count < count)
        {
            output += "Error! 通过searchAll搜索结果列表长度（=" + list.Count + "）小于测试用例数量！\n";
            errorCount++;            
        }
        else
            output += "Ok! 通过searchAll搜索结果列表长度（=" + list.Count + "）不少于测试用例数量！\n";
        
    }

    private static void searchInPrimaryGroup()
    {
        output += "searchInPrimaryGroup\n";
        List<Article> list = Search.searchInPrimaryGroup(key,primaryGroupId);
        if (list == null)
        {
            output += "Error! 通过searchInPrimaryGroup在id为" + primaryGroupId + "的主分类中搜索失败！返回值为null。\n";
            errorCount++;
            return;
        }

        if (list.Count < count/2 - 1 )
        {
            output += "Error! 通过searchAll搜索结果列表长度（=" + list.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 通过searchAll搜索结果列表长度（=" + list.Count + "）不少于测试用例数量！\n";

    }

    private static void searchInOther()
    {
        output += "searchInOther\n";
        List<Article> list = Search.searchInOther(key);
        if (list == null)
        {
            output += "Error! 通过searchInOther搜索失败！返回值为null。\n";
            errorCount++;
            return;
        }

        if (list.Count < count / 2 - 1 )
        {
            output += "Error! 通过searchInOther搜索结果列表长度（=" + list.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 通过searchInOther搜索结果列表长度（=" + list.Count + "）不少于测试用例数量！\n";

    }

    private static bool prepare()
    { 
        Random r = new Random();
        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);
            titles[i] = "测试样例" + no;
            Article a = new Article(no, titles[i], titles[i], "", DateTime.Now, 0, 0);
            ids[i] = ArticleManager.addRecord(a);
            if (ids[i] < 0)
            {
                output += "Error! 新增测试文章\"" + titles[i] + "\"失败！返回值为" + ids[i] + "。测试无法继续进行。请先解决ArticleManager中的错误。\n";
                errorCount++;
                return false;
            }
            if (i < count/2)
            {
                Article2Group a2g = new Article2Group(ids[i], primaryGroupId);
                if (!Article2GroupManager.addRecord(a2g))
                {
                    output += "Error! 将id为" + ids[i] + "的文章关联到id为" + primaryGroupId + "的主分类时失败！测试无法继续进行。请先解决Article2GroupManager中的错误。\n";
                    errorCount++;
                    return false;
                }
            }
            else
            {
                Article2Group a2g = new Article2Group(ids[i], OtherGroupId);
                if (!Article2GroupManager.addRecord(a2g))
                {
                    output += "Error! 将id为" + ids[i] + "的文章关联到id为" + OtherGroupId + "的主分类时失败！测试无法继续进行。请先解决Article2GroupManager中的错误。\n";
                    errorCount++;
                    return false;
                }
            }
        }
        return true;
    }

    private static bool clean()
    {
        for (int i = 0; i < count; i++)
        {
            if (i < count / 2)
            {
                Article2Group a2g = new Article2Group(ids[i], primaryGroupId);
                if (!Article2GroupManager.deleteRecord(a2g))
                {
                    output += "Error! 将id为" + ids[i] + "的文章取消与id为" + primaryGroupId + "的主分类的关联时失败！测试无法继续进行。请先解决Article2GroupManager中的错误。\n";
                    errorCount++;
                    return false;
                }
            }
            else
            {
                Article2Group a2g = new Article2Group(ids[i], OtherGroupId);
                if (!Article2GroupManager.deleteRecord(a2g))
                {
                    output += "Error! 将id为" + ids[i] + "的文章取消与id为" + OtherGroupId + "的主分类的关联时失败！测试无法继续进行。请先解决Article2GroupManager中的错误。\n";
                    errorCount++;
                    return false;
                }
            }
            Article a = new Article(ids[i], titles[i], titles[i], "", DateTime.Now, 0, 0);
            if (!ArticleManager.deleteRecord(a))
            {
                output += "Error! 删除测试文章\"" + titles[i] + "\"失败！测试无法继续进行。请先解决ArticleManager中的错误。\n";
                errorCount++;
                return false;
            }
        }

        return true;
    }
}