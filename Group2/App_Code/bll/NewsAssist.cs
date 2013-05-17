using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

/// <summary>
/// News页面可能用到后台函数
/// </summary>
public class NewsAssist
{
	public NewsAssist()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /************************************************************************************************************************************************************
     * 
     * 【关于本类用法的重要说明】
     * 当前台用户点击导航树上的某一个节点时，需要调用本类中的getArticleList方法族获取文章列表。这个类的难点就在于理解获取文章列表的三个方法的差异。
     * 获取文章列表有三个不同方法，分别对应以下三种情况：
     * 1、用户点击了非“其他”的主分类之后，需要显示指定主分类下所有文章的列表，此时调用getArticleListOfCertainPrimaryGroup(GroupNode gn)；
     * 2、用户点击了非“其他”的主分类下的非“其他”的子分类或兴趣标签，需要通过动态搜索显示指定子分类或兴趣标签下所有文章的列表，此时调用getArticleListByDynamicSearch(GroupNode gn)；
     * 3、用户点击了“其他”主分类，需要显示所有没有被分到任何主分类的文章列表，此时调用getArticleListOfOthers( int userId , GroupNode gn )，并将第二个参数gn置为null；
     * 4、用户点击了非“其他”的主分类下的“其他”子分类，需要显示该主分类下所有未在任何一个该用户关注了的子分类和兴趣标签下的文章的列表，
     *    此时调用getArticleListOfOthers( int userId , GroupNode gn )，并将第二个参数设置为被点击的“其他”子分类对应的GroupNode实例。
     *    
     * 注：关于三个函数的具体功能请参看相应函数前的注释
     * 
     ************************************************************************************************************************************************************/



    /*
     * 输入：一个GroupNode的model实例（仅使用其中的id字段，将这个字段作为groupId解析）
     * 输出：Article的列表
     * 功能：读取传入参数中的id，获取数据库中该主分类的全部文章的Article的model对象列表并返回
     * 用途说明：当用户点击某个主分类查看该分类下所有文章时，此函数返回该主分类所有文章的列表
     */
    public static List<Article> getArticleListOfCertainPrimaryGroup(GroupNode gn)
    {
        return null;
    }

    /*
     * 输入：一个GroupNode的model实例（仅使用其中的id字段，将这个字段作为tagId解析）
     * 输出：Article的列表
     * 功能：读取传入参数中的id，获取数据库中该子分类或兴趣标签所在的主分类下的全部文章中包含了该子分类或兴趣标签中全部关键词
     *       的Article的model对象列表并返回。主要运用动态搜索（即时搜索）的思路来实现。
     * 用途说明：当用户点击某个子分类或兴趣标签查看该分类下所有文章时，此函数返回该分类所有文章的列表
     */
    public static List<Article> getArticleListByDynamicSearch(GroupNode gn)
    {
        return null;
    }

    /*
     * 输入：第一个参数用户的userId，第二个参数有两种可能：
     *       1、若第二个参数不为null，则表示一个普通主分类下的“其他”子分类的GroupNode实例（仅使用其中的primaryGroupId字段）；
     *       2、若第二个参数为null，则表示“其他”主分类。
     * 输出：Article的列表
     * 功能：这个方法的功能比较难理解。以下是大致实现思路：
     *       1、如果第二个参数不为null：
     *          （1）根据参数gn的primaryGroupId字段确定主分类，记为M；
     *          （2）拿到M下的所有文章列表，记为Q；
     *          （3）根据userId查找该用户关注了M下的哪些子分类和兴趣标签；
     *          （4）利用getArticleListByDynamicSearch函数拿到在M下该用户关注的所有子分类和兴趣标签对应的文章的并集，记为R；
     *          （5）返回值为 Q - R
     *       2、如果第二个参数为null，比较简单，只需返回所有文章中没有被分到任何主分类的文章的列表即可
     * 用途说明：当用户点击某个主分类下“其他”子分类或“其他”主分类查看该分类下所有文章时，此函数返回该分类所有文章的列表
     */
    public static List<Article> getArticleListOfOthers( int userId , GroupNode gn )
    {
        return null;
    }

    /**
     * 输入：int值，某篇文章的articleId
     * 输出：成功返回articleId对应的article的model，失败返回null
     * 功能：根据文章的articleId得到对应的article的model
     * 用途说明：文章显示页面需要这个函数来获取要显示的文章信息
     */
    public static Article getArticleByIdWrapper(int articleId)
    {
        return null;
    }

    /*
     * 输入：一个Article的model实例（仅使用其中的fileURL字段）
     * 输出：该文章的content的字符串（不含标题）
     * 功能：根据传入参数的fileURL字段，从文件系统中读取相应的文本文件内容，将content以字符串形式返回
     * 用途说明：当用户对文章列表中的某篇文章感兴趣时，点击文章列表，调用此函数，返回文章全文内容用于web显示
     */
    public static string getArticleContentByArticleModel(Article a)
    {
        return null;
    }

}