using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// GroupQuery 的摘要说明
/// </summary>
public class GroupQuery
{
	public GroupQuery()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /************************************************************************************************************************************************************
     * 
     * 【关于本类用法的重要说明】
     * 系统中需要用到分类列表的地方及函数调用建议：
     * 1、管理员手动调整分类时，需要用到所有主分类的列表。建议调用getAllPrimaryGroups()；
     * 2、管理员新增分类时，需要用到所有公共分类（主分类+子分类）的列表。建议调用getAllPrimaryGroups()和getAllSecondaryGroups()两个方法，
     *    并调用sortGroupNodeList()将结果进行整理；
     * 3、公共新闻页面（未注册用户可见）上，需要用到所有公共分类的列表来生成导航树。函数调用建议同第二点。
     * 4、用户新闻页面（注册用户私有的）上，需要用到用户关注了的公共分类列表和用户自己的兴趣标签列表来生成导航树。建议调用getFocusedPublicGroupsByUserId()和
     *    getAllInterestLabelsByUserId(int userId)这两个方法，并调用sortGroupNodeList()将结果进行整理；
     * 5、关注选择页面（FocusSelect.aspx）上，需要用到所有公共分类的列表和用户自己的兴趣标签列表来供用户重新选择关注对象。建议调用getAllPrimaryGroups()、
     *    getAllSecondaryGroups()和getAllInterestLabelsByUserId(int userId)这三个方法，并调用sortGroupNodeList()将结果进行整理；
     *    
     ************************************************************************************************************************************************************/


    /**
     * 输入：空
     * 输出：所有主分类的列表
     * 功能：获取所有主分类的列表，例如“体育”、“经济”等。
     * 注意：返回的结果中不包括“其他”这个主分类。
     */
    public static List<GroupNode> getAllPrimaryGroups()
    {
        return null;
    }

    /**
     * 输入：空
     * 输出：所有子分类的列表
     * 功能：获取所有主分类的所有子分类的列表
     * 注意：返回的结果中包括主分类下的各个“其他”子分类。
     */
    public static List<GroupNode> getAllSecondaryGroups()
    {
        return null;
    }

    /**
     * 输入：指定用户的userId
     * 输出：该用户关注的所有公共分类（主分类+子分类）的列表
     * 功能：获取所有主分类的所有子分类的列表
     * 注意：返回的结果中包括主分类下的各个“其他”子分类。
     */
    public static List<GroupNode> getFocusedPublicGroupsByUserId(int userId)
    {
        return null;
    }

    /**
     * 输入：指定用户的userId
     * 输出：指定用户的所有兴趣标签的列表Tag列表
     * 功能：获取指定用户的所有兴趣标签的列表
     */
    public static List<GroupNode> getAllInterestLabelsByUserId(int userId)
    {
        return null;
    }

    /**
     * 输入：多个待合并和整理的List<GroupNode>
     * 输出：一个整理好的List<GroupNode>
     * 功能：将多个List<GroupNode>合并到一个List<GroupNode>中去，并将其中的所有GroupNode元素进行排序，排序后的结果应该如下：
     *       返回值List中第一个GroupNode存储第一个主分类的信息，接下来存储第一个主分类的第一个子分类的信息，以此类推，直到第一个主分类的所有子分类和兴趣标签都存储完毕，
     *       开始存储第二个主分类。
     */
    public static List<GroupNode> sortGroupNodeList(List<GroupNode>[] gnl)
    {
        return null;
    }
}