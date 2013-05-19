using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestArticle2GroupManager 的摘要说明
/// </summary>
public class TestArticle2GroupManager
{
	public TestArticle2GroupManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static string[] titles;    // 测试文章的title列表
    private static int[] ids;   // 测试文章的id列表
    private static int errorCount = 0;  //全局错误数
    private static int primaryGroupId = 0; // 默认将文章关联到的主分类id

    // 返回简略测试结果报告
    public static string generalTest(int testTimes , int GroupId )
    {
        test(testTimes , GroupId );
        string output = "测试Article2GroupManager过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes, int GroupId)
    {
        count = testTimes;
        errorCount = 0;
        ids = new int[count];
        titles = new string[count];
        primaryGroupId = GroupId;

        string output = "";
        output += "开始测试Article2GroupManager（测试样本默认关联id为" + primaryGroupId + "的主分类）（测试样本数为" + count + "）\n\n";
        
        output += ">> Case 1 :\n";
        output += addRecord() + "\n";
        output += ">> Case 2 :\n";
        output += getGroupIdsByArticle() + "\n";
        output += ">> Case 3 :\n";
        output += getArticleIdsByGroup() + "\n";        
        output += ">> Case 4 :\n";
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
            titles[i] = "测试样例" + no;
            Article a = new Article(no, titles[i], titles[i], "", DateTime.Now, 0, 0);
            ids[i] = ArticleManager.addRecord(a);
            if (ids[i] < 0)
            {
                output += "Error! 新增文章\"" + titles[i] + "\"失败！返回值为" + ids[i] + "\n";
                errorCount++;
                continue;
            }
            
            output += "Ok! 新增文章\"" + titles[i] + "\"成功！返回值为" + ids[i] + "\n";
            Article2Group a2g = new Article2Group( ids[i] , primaryGroupId );
            if (!Article2GroupManager.addRecord(a2g))
            {
                output += "Error! 将id为" + ids[i] + "的文章关联到id为" + primaryGroupId + "的主分类时失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 将id为" + ids[i] + "的文章关联到id为" + primaryGroupId + "的主分类时成功！\n";
        }
        return output;
    }

    private static string getGroupIdsByArticle()
    {
        string output = "getGroupIdsByArticle\n";

        for (int i = 0; i < count; i++)
        {
            Article a = new Article();
            a.ArticleId = ids[i];
            List<int> list = Article2GroupManager.getGroupIdsByArticle(a) ;
            if( list == null )
            {
                output += "Error! 调用getGroupIdsByArticle失败！返回值为null。\n";
                errorCount++;
                continue ;
            }
            if ( !list.Contains(primaryGroupId) )
            {
                output += "Error! 未找到关联（文章id为" + ids[i] + "，主分类id为" + primaryGroupId + "）！\n";
                errorCount++;
            }
            else
                output += "Ok! 找到关联（文章id为" + ids[i] + "，主分类id为" + primaryGroupId + "）！\n";
        }

        return output;
    }

    private static string getArticleIdsByGroup()
    {
        string output = "getArticleIdsByGroup\n";
        PrimaryGroups pg = new PrimaryGroups();
        pg.GroupId = primaryGroupId;
        List<int> list = Article2GroupManager.getArticleIdsByGroup(pg);
        if (list == null)
        {
            output += "Error! 调用getArticleIdsByGroup失败，返回null。\n";
            errorCount++;
            return output ;
        }
        for (int i = 0; i < count; i++)
        {
            if ( !list.Contains(ids[i]) )
            {
                output += "Error! id为" + primaryGroupId + "的主分类中未找到id为" + ids[i] + "的文章！\n";
                errorCount++;
            }
            else
                output += "Ok! id为" + primaryGroupId + "的主分类中找到了id为" + ids[i] + "的文章！\n";
        }
        return output;
    }

    private static string deleteRecord()
    {
        string output = "deleteRecord\n";

        for (int i = 0; i < count; i++)
        {
            Article2Group a2g = new Article2Group(ids[i], primaryGroupId);
            if (!Article2GroupManager.deleteRecord(a2g))
            {
                output += "Error! 删除关联（文章id为" + ids[i] + "，主分类id为" + primaryGroupId + "）失败！\n";
                errorCount++;
            }
            else
                output += "Ok! 删除关联（文章id为" + ids[i] + "，主分类id为" + primaryGroupId + "）成功！\n";

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