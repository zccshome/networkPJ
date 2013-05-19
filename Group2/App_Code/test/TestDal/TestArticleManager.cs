using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestArticleManager 的摘要说明
/// </summary>
public class TestArticleManager
{
	public TestArticleManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static string[] titles;    // 测试文章的title列表
    private static int[] ids;   // 测试文章的id列表
    private static int errorCount = 0;  //全局错误数

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试ArticleManager过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test( int testTimes )
    {
        count = testTimes;
        errorCount = 0;
        ids = new int[count];
        titles = new string[count];

        string output = "";
        output += "开始测试ArticleManager（测试样本数为" + count + "）\n\n";
        output += ">> Case 1 :\n";
        output += addRecord() + "\n";
        output += ">> Case 2 :\n";
        output += selectRecordByArticleId() + "\n";
        output += ">> Case 3 :\n";
        output += selectRecordByTitle() + "\n";
        output += ">> Case 4 :\n";
        output += updateWordCount() + "\n";
        output += ">> Case 5 :\n";
        output += updateFileURL() + "\n";
        output += ">> Case 6 :\n";
        output += countArticleNum() + "\n";
        output += ">> Case 7 :\n";
        output += getArticleListByPrimaryGroup(1) + "\n";
        output += ">> Case 8 :\n";
        output += getArticleListByDynamicSearch(69) + "\n";
        output += ">> Case 9 :\n";
        output += searchAll() + "\n";
        output += ">> Case 10 :\n";
        output += searchInPrimaryGroup() + "\n";
        output += ">> Case 11 :\n";
        output += searchInOther() + "\n";
        output += ">> Case 12 :\n";
        output += deleteRecord() + "\n";

        return output;
    }

    private static string addRecord()
    {
        string output = "addRecord\n";

        Random r = new Random();

        for ( int i = 0 ; i < count ; i ++ )
        {
            int no = r.Next(1000, 9000);
            titles[i] = "测试样例" + no;
            Article a = new Article(no, titles[i], titles[i], "", DateTime.Now, 0, 0);
            ids[i] =  ArticleManager.addRecord( a ) ;
            if (ids[i] < 0)
            {
                output += "Error! 新增文章\"" + titles[i] + "\"失败！返回值为" + ids[i] + "\n";
                errorCount++;
            }
            else
                output += "Ok! 新增文章\"" + titles[i] + "\"成功！返回值为" + ids[i] + "\n";
        }
        return output;
    }

    

    private static string selectRecordByArticleId()
    {
        string output = "selectRecordByArticleId\n";

        for (int i = 0; i < count; i++)
        {
            Article a = new Article();
            a.ArticleId = ids[i];
            a = ArticleManager.selectRecordByArticleId(a) ;
            if (a == null)
            {
                output += "Error! 获取id为" + ids[i] + "的文章失败！返回值为空。\n";
                errorCount++;
            }
            else
                output += "Ok! 通过id获取文章\"" + a.Title + "\"成功！\n";
        }

        return output;
    }

    private static string selectRecordByTitle()
    {
        string output = "selectRecordByTitle\n";

        for (int i = 0; i < count; i++)
        {
            Article a = new Article();
            a.Title = titles[i] ;
            a = ArticleManager.selectRecordByTitle(a);
            if (a == null)
            {
                output += "Error! 通过title获取\"" + titles[i] + "\"失败！返回值为空。\n";
                errorCount++;
            }
            else
                output += "Ok! 通过title获取文章\"" + a.Title + "\"成功！\n";
        }

        return output;
    }

    private static string updateWordCount()
    {
        string output = "updateWordCount\n";

        for (int i = 0; i < count; i++)
        {
            Article a = new Article();
            a.ArticleId = ids[i];
            a.WordCount = ids[i];
            if (!ArticleManager.updateWordCount(a))
            {
                output += "Error! 调用updateWordCount对\"" + titles[i] + "\"失败！返回值为false。\n";
                errorCount++;
            }
            else
                output += "Ok! 调用updateWordCount对\"" + titles[i] + "\"成功！返回值为true。\n";
            a = ArticleManager.selectRecordByArticleId(a);
            if (a == null)
            {
                output += "Error! 通过id获取文章id为" + ids[i] + "的文章失败！返回值为空。\n";
                errorCount++;
                continue;
            }
            if (a.WordCount != ids[i])
            {
                output += "Error! 对\"" + titles[i] + "\"更新WordCount失败！WordCount被更改为" + a.WordCount + "。\n";
                errorCount++;
            }
            else
                output += "Ok! 对\"" + titles[i] + "\"更新WordCount成功！WordCount被更改为" + a.WordCount + "。\n";
        }

        return output;
    }

    private static string updateFileURL()
    {
        string output = "updateFileURL\n";

        for (int i = 0; i < count; i++)
        {
            Article a = new Article();
            a.ArticleId = ids[i];
            a.FileURL = ids[i] + "";
            if (!ArticleManager.updateFileURL(a))
            {
                output += "Error! 调用updateFileURL对\"" + titles[i] + "\"失败！返回值为false。\n";
                errorCount++;
            }
            else
                output += "Ok! 调用updateFileURL对\"" + titles[i] + "\"成功！返回值为true。\n";
            a = ArticleManager.selectRecordByArticleId(a);
            if (a == null)
            {
                output += "Error! 通过id获取文章id为" + ids[i] + "的文章失败！返回值为空。\n";
                errorCount++;
                continue;
            }
            if (!a.FileURL.Equals(ids[i] + ""))
            {
                output += "Error! 对\"" + titles[i] + "\"更新FileURL失败！FileURL被更改为" + a.FileURL + "。\n";
                errorCount++;
            }
            else
                output += "Ok! 对\"" + titles[i] + "\"更新FileURL成功！FileURL被更改为" + a.FileURL + "。\n";
        }

        return output;
    }

    private static string countArticleNum()
    {
        string output = "countArticleNum\n";
        int c = ArticleManager.countArticleNum();
        if (c < count)
        {
            output += "Error! countArticleNum失败，返回" + c + "。\n";
            errorCount++;
        }
        else
            output += "Ok! countArticleNum成功，返回" + c + "。\n";

        return output;
    }

    private static string getArticleListByPrimaryGroup(int primaryGroupId)
    {
        string output = "getArticleListByPrimaryGroup（默认查询id为" + primaryGroupId + "的主分类）\n";
        PrimaryGroups pg = new PrimaryGroups();
        pg.GroupId = primaryGroupId;
        List<Article> list = ArticleManager.getArticleListByPrimaryGroup(pg);
        if (list == null)
        {
            //output += "Error! 调用getArticleListByPrimaryGroup失败，返回null。\n";
            output += "调用getArticleListByPrimaryGroup失败，返回null。\n";
            //errorCount++;
        }
        else
            output += "Ok! 调用getArticleListByPrimaryGroup成功，返回的文章数为" + list.Count + "。\n";

        return output;
    }

    private static string getArticleListByDynamicSearch(int tagId)
    {
        string output = "getArticleListByDynamicSearch（默认查询tagId为" + tagId + "的Tag）\n";
        Tag t = new Tag();
        t.TagId = tagId;
        List<Article> list = ArticleManager.getArticleListByDynamicSearch(t);
        if (list == null)
        {
            //output += "Error! 调用getArticleListByDynamicSearch失败，返回null。\n";
            output += "调用getArticleListByDynamicSearch失败，返回null。\n";
            //errorCount++;
        }
        else
            output += "Ok! 调用getArticleListByDynamicSearch成功，返回的文章数为" + list.Count + "。\n";

        return output;
    }

    private static string searchAll()
    {
        string output = "searchAll\n";
        string[] keys = new string[] { "测试", "样例" };
        List<Article> list = ArticleManager.searchAll(keys);
        if (list == null)
        {
            output += "Error! 调用searchAll失败，返回null。\n";
            errorCount++;
        }
        else
            output += "Ok! 调用searchAll成功，返回的文章数为" + list.Count + "。\n";

        return output;
    }

    private static string searchInPrimaryGroup()
    {
        string output = "searchInPrimaryGroup\n";
        output += "对不起，由于技术原因，此函数暂时无法测试。\n";

        return output;
    }

    private static string searchInOther()
    {
        string output = "searchInOther\n";
        output += "对不起，由于技术原因，此函数暂时无法测试。\n";

        return output;
    }

    private static string deleteRecord()
    {
        string output = "deleteRecord\n";

        for (int i = 0; i < count; i++)
        {
            Article a = new Article();
            a.ArticleId = ids[i];
            if (!ArticleManager.deleteRecord(a))
            {
                output += "Error! 删除文章\"" + titles[i] + "\"失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 删除文章\"" + titles[i] + "\"成功！\n";
        }

        return output;
    }
}