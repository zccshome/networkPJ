using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///操作 Article2GroupManager 的底层函数类
/// </summary>
public class Article2GroupManager
{
    public Article2GroupManager()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    // 将传入参数作为一条记录插入数据库
    // 成功返回true，失败返回false
    public static bool addRecord(Article2Group a2g)
    {
        return false;
    }

    //删除Article2Group表中对应于传入参数的那一条记录
    //成功返回true，失败返回false
    public static bool deleteRecord(Article2Group a2g)
    {
        return false;
    }

    //仅读取传入参数中的ArticleId字段，获取该文章所属的全部Group的groupId的列表
    //返回groupId的列表List<int>
    public static List<int> getGroupIdsByArticle(Article a)
    {
        return null;
    }

    //仅读取传入参数中的GroupId，获取该分类下的所有文章的articleId的列表
    //返回articleId的列表List<int>
    public static List<int> getArticleIdsBtGroup(Groups g)
    {
        return null;
    }


}