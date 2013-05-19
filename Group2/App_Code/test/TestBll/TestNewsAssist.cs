using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestNewsAssist 的摘要说明
/// </summary>
public class TestNewsAssist
{
	public TestNewsAssist()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static string[] titles;    // 测试文章的title列表
    private static int[] ids;   // 测试文章的id列表
    private static int errorCount = 0;  //全局错误数
    private static PrimaryGroups primaryGroup; // 默认将文章关联到的测试主分类
    private static string output;   // 全局输出

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试NewsAssist过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes)
    {
        count = testTimes;
        errorCount = 0;
        ids = new int[count];
        titles = new string[count];
        primaryGroup = new PrimaryGroups();

        output = "";
        output += "开始测试NewsAssist（测试样本数为" + count + "）\n\n";

        // Prepare
        if (!prepare())
            return output;

        // Test
        output += ">> Case 1 :\n";
        getArticleListOfCertainPrimaryGroup();
        output += "\n";
        output += ">> Case 2 :\n";
        getArticleListByDynamicSearch();
        output += "\n";
        output += ">> Case 3 :\n";
        getArticleListOfOthers();
        output += "\n";
        output += ">> Case 4 :\n";
        getArticleByIdWrapper();
        output += "\n";
        output += ">> Case 5 :\n";
        getArticleContentByArticleModel();
        output += "\n";

        // Clean
        clean();//*/

        return output;
    }

    private static void getArticleListOfCertainPrimaryGroup()
    {
        output += "getArticleListOfCertainPrimaryGroup\n";

        GroupNode gn = new GroupNode();
        gn.Id = primaryGroup.GroupId ;
        List<Article> list = NewsAssist.getArticleListOfCertainPrimaryGroup(gn);
        if (list == null)
        {
            output += "Error! getArticleListOfCertainPrimaryGroup失败，返回null。\n";
            errorCount++;
            return;
        }

        if (list.Count < count)
        {
            output += "Error! 返回列表长度（=" + list.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 返回列表长度（=" + list.Count + "）不少于测试用例数量！\n";
    }

    private static void getArticleListByDynamicSearch()
    {
        output += "getArticleListByDynamicSearch\n";

        // add tags
        string name = "测试 样例";
        Tag t = new Tag(name, name, primaryGroup.GroupId, DateTime.Now, 0);
        t.TagId = TagManager.addTag(t);
        if (t.TagId < 0)
        {
            output += "Error! 新增Tag\"" + name + "\"失败！返回值为" + t.TagId + "\n";
            errorCount++;
            return;
        }

        // Test
        GroupNode gn = new GroupNode();
        gn.Id = t.TagId;
        List<Article> list = NewsAssist.getArticleListByDynamicSearch(gn);
        if (list == null)
        {
            output += "Error! getArticleListByDynamicSearch失败，返回null。\n";
            errorCount++;
            return;
        }
        if (list.Count < count)
        {
            output += "Error! 返回列表长度（=" + list.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 返回列表长度（=" + list.Count + "）不少于测试用例数量！\n";
    
        // delete tag
        if (!TagManager.deleteTag(t))
        {
            output += "Error! 删除Tag（id为\"" + t.TagId + "\"）失败！\n";
            errorCount++;
            return;
        }
    
    }

    private static void getArticleListOfOthers()
    {
        output += "getArticleListOfOthers\n";
        output += "对不起，由于技术原因，此函数暂时无法测试。\n";
    }

    private static void getArticleByIdWrapper()
    {
        output += "getArticleByIdWrapper\n";

        for (int i = 0; i < count; i++)
        {
            Article a = new Article();
            a.ArticleId = ids[i];
            a = ArticleManager.selectRecordByArticleId(a);
            if (a == null)
            {
                output += "Error! 获取id为" + ids[i] + "的文章失败！返回值为空。\n";
                errorCount++;
            }
            else
                output += "Ok! 通过id获取文章\"" + a.Title + "\"成功！\n";
        }
    }

    private static void getArticleContentByArticleModel()
    {
        output += "getArticleContentByArticleModel\n";
        output += "对不起，由于技术原因，此函数暂时无法测试。\n";
    }

    private static bool prepare()
    {
        primaryGroup = new PrimaryGroups(0, "test");
        primaryGroup.GroupId = PrimaryGroupMananger.addRecord(primaryGroup);
        if (primaryGroup.GroupId < 0)
        {
            output += "Error! 新增测试辅助主分类\"test\"失败！返回值为" + primaryGroup.GroupId + "。测试无法继续进行。请先解决PrimaryGroupsManager中的错误。\n";
            errorCount++;
            return false;
        }

        Random r = new Random();

        for (int i = 0; i < count; i++)
        {
            int no = r.Next(1000, 9000);
            titles[i] = "测试样例" + no;
            Article a = new Article(no, titles[i], titles[i], "", DateTime.Now, 0, 0);
            ids[i] = ArticleManager.addRecord(a);
            if (ids[i] < 0)
            {
                output += "Error! 新增文章\"" + titles[i] + "\"失败！返回值为" + ids[i] + "。测试无法继续进行。请先解决Article2GroupManager中的错误。\n";
                errorCount++;
                return false;
            }

            output += "Ok! 新增文章\"" + titles[i] + "\"成功！返回值为" + ids[i] + "\n";
            Article2Group a2g = new Article2Group(ids[i], primaryGroup.GroupId);
            if (!Article2GroupManager.addRecord(a2g))
            {
                output += "Error! 将id为" + ids[i] + "的文章关联到id为" + primaryGroup.GroupId + "的主分类时失败！测试无法继续进行。请先解决Article2GroupManager中的错误。\n";
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
            Article2Group a2g = new Article2Group(ids[i], primaryGroup.GroupId);
            if (!Article2GroupManager.deleteRecord(a2g))
            {
                output += "Error! 删除关联（文章id为" + ids[i] + "，主分类id为" + primaryGroup.GroupId + "）失败！\n";
                errorCount++;
            }

            Article a = new Article();
            a.ArticleId = ids[i];
            if (!ArticleManager.deleteRecord(a))
            {
                output += "Error! 删除文章\"" + titles[i] + "\"失败！\n";
                errorCount++;
            }
        }

        if (!PrimaryGroupMananger.deleteRecord(primaryGroup))
        {
            output += "Error! 删除测试辅助主分类失败！\n";
            errorCount++;
            return false;
        }

        return true;
    }
}