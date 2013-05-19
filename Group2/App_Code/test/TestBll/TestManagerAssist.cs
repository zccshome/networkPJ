using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestManagerAssist 的摘要说明
/// </summary>
public class TestManagerAssist
{
	public TestManagerAssist()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static int errorCount = 0;  //全局错误数
    private static int[] ids;   //全局测试文章id列表
    private static string[] titles;    // 测试文章的title列表
    private static PrimaryGroups[] primaryGroup ;    //全局测试主分类
    private static string output;   //全局结果输出

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试ManagerAssist过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes)
    {
        count = testTimes;
        errorCount = 0;
        primaryGroup = new PrimaryGroups[2];
        ids = new int[count];
        titles = new string[count];

        output = "";
        output += "开始测试ManagerAssist（测试样本数为" + count + "）\n\n";


        // Prepare

        if (!prepare())
            return output;

        // Test

        output += ">> Case 1 :\n";
        addArticle();
        output += "\n";
        output += ">> Case 2 :\n";
        getGroupIdsByArticleWrapper();
        output += "\n";
        output += ">> Case 3 :\n";
        getPrimaryGroupNamesByPrimaryGroupIds();
        output += "\n";
        output += ">> Case 4 :\n";
        changeGroupRelation();
        output += "\n";
        //output += ">> Case 5 :\n";
        //addPrimaryGroup();
        output += "\n";
        output += ">> Case 5 :\n";
        addSecondaryGroup();
        output += "\n";


        // Clean

        clean();

        return output;
    }

    private static void addArticle()
    {
        output += "addArticle\n";
        output += "对不起，由于技术原因，此函数暂时无法测试。\n";
    }

    private static void getGroupIdsByArticleWrapper()
    {
        output += "getGroupIdsByArticleWrapper\n";
        output += "由于此函数仅仅是一个包装函数，具体内容已在Article2GroupManager.getGroupIdsByArticle中进行了测试，故此处不再测试。\n";
    }

    private static void getPrimaryGroupNamesByPrimaryGroupIds()
    {
        output += "getPrimaryGroupNamesByPrimaryGroupIds\n";

        List<string[]> list = PrimaryGroupMananger.getAllGroups();
        List<int> idList = new List<int>();
        foreach (string[] item in list)
            idList.Add( int.Parse(item[0] ));
        List<string> nameList = ManagerAssist.getPrimaryGroupNamesByPrimaryGroupIds(idList);
        if (nameList == null)
        {
            output += "Error! getPrimaryGroupNamesByPrimaryGroupIds失败，返回null。\n";
            errorCount++;
            return;
        }
        if (nameList.Count < 3)
        {
            output += "Error! 返回列表长度（=" + nameList.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 返回列表长度（=" + nameList.Count + "）不少于测试用例数量！\n";
    }

    private static void changeGroupRelation()
    {
        output += "changeGroupRelation\n";

        List<int> list0_pre = Article2GroupManager.getArticleIdsByGroup(primaryGroup[0]);
        List<int> list1_pre = Article2GroupManager.getArticleIdsByGroup(primaryGroup[1]);

        int list1_pre_len = 0 ;
        if (list1_pre !=null)
            list1_pre_len = list1_pre.Count ;

        output += "调用changeGroupRelation前，primaryGroup[0]下有" + list0_pre.Count + "篇文章，primaryGroup[1]下有" + list1_pre_len + "篇文章。\n";
        List<int> groupIds = new List<int>();
        groupIds.Add(primaryGroup[0].GroupId);
        groupIds.Add(primaryGroup[1].GroupId);
        for (int i = 0; i < count; i++)
        {
            Article a = new Article();
            a.ArticleId = ids[i];
            ManagerAssist.changeGroupRelation(a, groupIds);
        }
        List<int> list0_post = Article2GroupManager.getArticleIdsByGroup(primaryGroup[0]);
        List<int> list1_post = Article2GroupManager.getArticleIdsByGroup(primaryGroup[1]);
        output += "调用changeGroupRelation前，primaryGroup[0]下有" + list0_post.Count + "篇文章，primaryGroup[1]下有" + list1_post.Count + "篇文章。\n";

        if (list0_pre.Count - list1_pre_len > list0_post.Count - list1_post.Count)
            output += "Ok! changeGroupRelation执行成功！\n";
        else
        {
            output += "Ok! changeGroupRelation执行失败！\n";
            errorCount++;
        }
    }

    private static void addPrimaryGroup()
    {
        output += "addPrimaryGroup\n";

        // add
        string name = "sample123";
        string keywords = "qawsed rftgyh";
        ManagerAssist.addPrimaryGroup(name, keywords);

        // check
        List<string[]> list = PrimaryGroupMananger.getAllGroups();
        bool found = false;
        int groupId = -1 ;
        foreach( string[] item in list )
            if (item[1].Equals("sample123"))
            {
                found = true;
                groupId = int.Parse(item[0]);
                break;
            }
        if (!found)
        {
            output += "Error! addPrimaryGroup执行失败！主分类未加入数据库中。\n";
            errorCount++;
        }
        else
            output += "Ok! addPrimaryGroup执行成功！主分类已加入数据库中。\n";

        PrimaryGroups pg = new PrimaryGroups( groupId , name ) ;
        List<string> list2 = PrimaryGroupKeyWordsManager.getKeyWordsOfCertainPrimaryGroup(pg);
        if ( list2.Count != 2 )
            output += "Error! PrimaryGroupKeyWords数目不对！应为2\n";
        else
            output += "Ok! PrimaryGroupKeyWords数目正确！应为2\n";

        if (list2.First().Equals("qawsed") || list2.First().Equals("rftgyh"))
            output += "Ok! PrimaryGroupKeyWords中对应的第一个关键词通过验证！\n";
        else
        {
            output += "Error! PrimaryGroupKeyWords中对应的第一个关键词未通过验证！\n";
            errorCount++;
        }

        List<Tag> tagL = TagManager.getAllTagsByCertainGroupId( groupId );
        if ( tagL == null || tagL.Count != 1 )
        {
            output += "Error! addPrimaryGroup调用后没有将“其他”子分类插入tag表！\n";
            errorCount++;
        }
        else
            output += "Ok! addPrimaryGroup调用后成功将“其他”子分类插入tag表！\n";

        // delete
        foreach (string item in list2)
            if (!PrimaryGroupKeyWordsManager.deleteRecord(new PrimaryGroupKeyWords(groupId, item)))
            {
                output += "Error! 删除测试关键词失败！\n";
                errorCount++;
            }
        Tag t = new Tag();
        t.TagId = tagL.First().TagId ;
        if( !TagManager.deleteTag(t) )
        {
            output += "Error! 删除测试主分类下“其他”子分类失败！\n";
            errorCount++;
        }
        PrimaryGroups temp = new PrimaryGroups() ;
        temp.GroupId = groupId;
        if ( !PrimaryGroupMananger.deleteRecord( temp ) )
        {
            output += "Error! 删除测试主分类失败！\n";
            errorCount++;
        }
        
    }

    private static void addSecondaryGroup()
    {
        output += "addSecondaryGroup\n";
        output += "由于此函数仅仅是一个包装函数，具体内容已在TagManager.addTag中进行了测试，故此处不再测试。\n";
    }

    private static bool prepare()
    {
        for (int i = 0; i < 2; i++)
        {
            primaryGroup[i] = new PrimaryGroups(0, "test1");
            primaryGroup[i].GroupId = PrimaryGroupMananger.addRecord(primaryGroup[i]);
            if (primaryGroup[i].GroupId < 0)
            {
                output += "Error! 新增测试辅助主分类\"test\"失败！返回值为" + primaryGroup[i].GroupId + "。测试无法继续进行。请先解决PrimaryGroupsManager中的错误。\n";
                errorCount++;
                return false;
            }
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
            Article2Group a2g = new Article2Group(ids[i], primaryGroup[0].GroupId);
            if (!Article2GroupManager.addRecord(a2g))
            {
                output += "Error! 将id为" + ids[i] + "的文章关联到id为" + primaryGroup[0].GroupId + "的主分类时失败！测试无法继续进行。请先解决Article2GroupManager中的错误。\n";
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
            Article2Group a2g = new Article2Group(ids[i], primaryGroup[1].GroupId);
            if (!Article2GroupManager.deleteRecord(a2g))
            {
                output += "Error! 删除关联（文章id为" + ids[i] + "，主分类id为" + primaryGroup[1].GroupId + "）失败！\n";
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

        if (!PrimaryGroupMananger.deleteRecord(primaryGroup[0]))
        {
            output += "Error! 删除测试辅助主分类失败！\n";
            errorCount++;
            return false;
        }
        if (!PrimaryGroupMananger.deleteRecord(primaryGroup[1]))
        {
            output += "Error! 删除测试辅助主分类失败！\n";
            errorCount++;
            return false;
        }

        return true;
    }
}