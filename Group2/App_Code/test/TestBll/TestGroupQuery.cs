using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TestGroupQuery 的摘要说明
/// </summary>
public class TestGroupQuery
{
	public TestGroupQuery()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private static int count = 10;  // 样本数
    private static int errorCount = 0;  //全局错误数
    private static PrimaryGroups[] pgs ;    //全局测试主分类
    private static Tag[] tags;    //全局测试Tag
    private static Tag[] interLabels;    //全局测试兴趣标签 
    private static User sampleUser;     //全局测试用户
    private static string output;   //全局结果输出

    // 返回简略测试结果报告
    public static string generalTest(int testTimes)
    {
        test(testTimes);
        string output = "测试GroupQuery过程中出现" + errorCount + "次错误（测试样本数为" + count + "）；\n";
        return output;
    }

    // 返回详细测试结果报告
    public static string test(int testTimes)
    {
        count = testTimes;
        errorCount = 0;
        pgs = new PrimaryGroups[count] ;
        tags = new Tag[count];
        interLabels = new Tag[count];

        output = "";
        output += "开始测试GroupQuery（测试样本数为" + count + "）\n\n";
        

        // Prepare

        if (!prepare())
            return output;

        // Test

        output += ">> Case 1 :\n";
        getAllPrimaryGroups() ;
        output += "\n";
        output += ">> Case 2 :\n";
        getAllSecondaryGroups() ;
        output += "\n";
        output += ">> Case 3 :\n";
        getFocusedPublicGroupsByUserId() ;
        output += "\n";
        output += ">> Case 4 :\n";
        getAllInterestLabelsByUserId() ;
        output += "\n";
        output += ">> Case 5 :\n";
        sortGroupNodeList() ;
        output += "\n";


        // Clean

        clean();

        return output;
    }

    private static string getAllPrimaryGroups()
    {
        output += "getAllPrimaryGroups\n";

        List<GroupNode> list = GroupQuery.getAllPrimaryGroups();

        if (list == null)
        {
            output += "Error! 通过getAllPrimaryGroups获取List<GroupNode>记录失败！返回值为null。\n";
            errorCount++;
            return output;
        }

        if (list.Count < count )
        {
            output += "Error! 通过getAllPrimaryGroups获取的GroupNode列表长度（=" + list.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 通过getAllPrimaryGroups获取的GroupNode列表长度（=" + list.Count + "）大于测试用例数量！\n";

        /*for (int i = 0; i < list.Count; i++)
        {
            output += list.ElementAt(i).Id + "\t" + list.ElementAt(i).NodeName + "\n";
        }*/

            return output;
    }

    private static string getAllSecondaryGroups()
    {
        output += "getAllSecondaryGroups\n";

        List<GroupNode> list = GroupQuery.getAllSecondaryGroups();

        if (list == null)
        {
            output += "Error! 通过getAllSecondaryGroups获取List<GroupNode>记录失败！返回值为null。\n";
            errorCount++;
            return output;
        }

        if (list.Count < count )
        {
            output += "Error! 通过getAllSecondaryGroups获取的GroupNode列表长度（=" + list.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 通过getAllSecondaryGroups获取的GroupNode列表长度（=" + list.Count + "）大于测试用例数量！\n";
        
        return output;
    }

    private static string getFocusedPublicGroupsByUserId()
    {
        output += "getFocusedPublicGroupsByUserId\n";

        List<GroupNode> list = GroupQuery.getFocusedPublicGroupsByUserId(sampleUser.UserId);

        if (list == null)
        {
            output += "Error! 通过getFocusedPublicGroupsByUserId获取List<GroupNode>记录失败！返回值为null。\n";
            errorCount++;
            return output;
        }

        if (list.Count < count )
        {
            output += "Error! 通过getFocusedPublicGroupsByUserId获取的GroupNode列表长度（=" + list.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 通过getFocusedPublicGroupsByUserId获取的GroupNode列表长度（=" + list.Count + "）大于测试用例数量！\n";
        
        return output;
    }

    private static string getAllInterestLabelsByUserId()
    {
        output += "getAllInterestLabelsByUserId\n";

        List<GroupNode> list = GroupQuery.getAllInterestLabelsByUserId(sampleUser.UserId);

        if (list == null)
        {
            output += "Error! 通过getAllInterestLabelsByUserId获取List<GroupNode>记录失败！返回值为null。\n";
            errorCount++;
            return output;
        }

        if (list.Count < count )
        {
            output += "Error! 通过getAllInterestLabelsByUserId获取的GroupNode列表长度（=" + list.Count + "）小于测试用例数量！\n";
            errorCount++;
        }
        else
            output += "Ok! 通过getAllInterestLabelsByUserId获取的GroupNode列表长度（=" + list.Count + "）大于测试用例数量！\n";
        
        return output;        
    }

    private static string sortGroupNodeList()
    {
        output += "sortGroupNodeList\n";

        List<GroupNode> list1 = GroupQuery.getAllPrimaryGroups();
        List<GroupNode> list2 = GroupQuery.getAllSecondaryGroups();
        List<GroupNode> list3 = GroupQuery.getFocusedPublicGroupsByUserId(sampleUser.UserId);
        List<GroupNode> list4 = GroupQuery.getAllInterestLabelsByUserId(sampleUser.UserId);

        /*
        output += "> 1 \n";
        foreach (GroupNode node in list1)
        {
            output += "\t" + node.NodeName + "\t" + node.PrimaryGroupId + "\t" + node.IsPrimaryGroup + "\t" + node.Id + "\t" + node.IsInterestLabel + "\n";
        }
        output += "> 2 \n";
        foreach (GroupNode node in list2)
        {
            output += "\t" + node.NodeName + "\t" + node.PrimaryGroupId + "\t" + node.IsPrimaryGroup + "\t" + node.Id + "\t" + node.IsInterestLabel + "\n";
        }
        output += "> 3 \n";
        foreach (GroupNode node in list3)
        {
            output += "\t" + node.NodeName + "\t" + node.PrimaryGroupId + "\t" + node.IsPrimaryGroup + "\t" + node.Id + "\t" + node.IsInterestLabel + "\n";
        }
        output += "> 4 \n";
        foreach (GroupNode node in list4)
        {
            output += "\t" + node.NodeName + "\t" + node.PrimaryGroupId + "\t" + node.IsPrimaryGroup + "\t" + node.Id + "\t" + node.IsInterestLabel + "\n";
        }
         */

        if (list1 == null || list2 == null || list3 == null || list4 == null)
        {
            output += "Error! 获取List<GroupNode>记录失败！返回值为null。\n";
            errorCount++;
            return output;
        }

        List<GroupNode> list = GroupQuery.sortGroupNodeList(new List<GroupNode>[] { list1, list2, list3, list4 });

        if (list == null)
        {
            output += "Error! 通过sortGroupNodeList整理List<GroupNode>[]失败！返回值为null。\n";
            errorCount++;
            return output;
        }

        output += "\nThe Result:\n";
        output += "\t名称\t隶属于\t是否为主\tid\t是否私有\n";
        foreach (GroupNode node in list)
        {
            output += "\t" + node.NodeName + "\t" + node.PrimaryGroupId + "\t" + node.IsPrimaryGroup + "\t" + node.Id + "\t" + node.IsInterestLabel + "\n";
        }

        return output;
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

            // add interest labels
            interLabels[i] = new Tag("intre" + no, "intre" + no, pgs[i].GroupId, DateTime.Now, 1);
            interLabels[i].TagId = TagManager.addTag(interLabels[i]);
            if (interLabels[i].TagId < 0)
            {
                output += "Error! 新增兴趣标签失败！返回值为" + interLabels[i].TagId + "。测试无法继续进行。请先解决TagManager中的错误。\n";
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

            // link user to interest labels
            User2Tag u2t2 = new User2Tag(sampleUser.UserId, interLabels[i].TagId);
            if (!User2TagManager.addRecord(u2t2))
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

            // unlink user to interest labels
            User2Tag u2t2 = new User2Tag(sampleUser.UserId, interLabels[i].TagId);
            if (!User2TagManager.deleteRecord(u2t2))
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

            // delete interest labels
            if (!TagManager.deleteTag(interLabels[i]))
            {
                output += "Error! 删除测试兴趣标签失败！测试无法继续进行。请先解决TagManager中的错误。\n";
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