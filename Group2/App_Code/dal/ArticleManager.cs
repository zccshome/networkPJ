using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
///操作 ArticleManager 的底层函数类
/// </summary>
public class ArticleManager
{
    public ArticleManager()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    //根据传入参数的内容增加一条记录（仅使用传入参数中除articleId之外的其他字段）
    //注意：传入参数中的time、heat和wordCount字段均需要上层书写，本函数只管存储
    //成功返回新增记录的articleId，失败返回-1
    public static int addRecord(Article a)
    {
        return -1;
    }

    //返回数据库中相应的article记录（仅使用传入参数中的articleId字段）
    //返回一个Article的model
    public static Article selectRecordByArticleId(Article a)
    {
        return null;

    }

    //返回数据库中相应的article记录（仅使用传入参数中的title字段）
    //返回一个Article的model
    public static Article selectRecordByTitle(Article a)
    {
        return null;
    }

    //将传入参数中的articleId对应的数据库中的文章的wordCount字段值设为传入参数中的wordCount值（仅读取传入参数中的articleId和wordCount字段）
    //成功返回true，失败返回false
    public static bool updateWordCount(Article a)
    {
        return false;
    }

    //将传入参数中的articleId对应的数据库中的文章的fileURL字段值设为传入参数中的fileURL值（仅读取传入参数中的articleId和fileURL字段）
    //成功返回true，失败返回false
    public static bool updateFileURL(Article a)
    {
        return false;
    }

    //返回数据库中的文章总篇数。建议通过查询article表的条目数来确定。
    public static int countArticleNum()
    {
        return -1;
    }

    //获取指定主分类下的所有文章的article的列表（仅读取传入参数中的GroupId字段）
    //返回article的列表List<Article>
    public static List<Article> getArticleListByPrimaryGroup(PrimaryGroups g)
    {
        return null;
    }

    //获取指定Tag下的所有文章的article的列表（仅读取传入参数中的TagId字段）
    //返回article的列表List<Article>
    public static List<Article> getArticleListByDynamicSearch(Tag t)
    {
        return null;
    }

    //根据传入的articleId数组，获取相应文章的article对象的列表
    //返回article的列表List<Article>
    private static List<Article> getArticleListByArticleIdList(List<int> articleIdList)
    {
        return null;
    }

    /*
     * 输入：搜索关键词列表
     * 输出：Article的列表
     * 功能：在数据库所有文章中搜索标题包含全部指定关键词的文章并返回
     */
    public static List<Article> searchAll(string[] key)
    {
        return null;
    }

    /*
     * 输入：搜索关键词列表，限定为搜索范围的主分类的id
     * 输出：Article的列表
     * 功能：在数据库所有属于指定主分类的文章中搜索标题包含全部指定关键词的文章并返回
     */
    public static List<Article> searchInPrimaryGroup(string[] key, int primaryGroupId)
    {
        return null;
    }

    /*
     * 输入：搜索关键词列表
     * 输出：Article的列表
     * 功能：在数据库所有属于“其他”主分类的文章中搜索标题包含全部指定关键词的文章并返回
     */
    public static List<Article> searchInOther(string[] key)
    {
        return null;
    }

    //***************************************   *************************
    //***********这是我的私有方法****************************
    private static Article getArticleById(int articleId)
    {
        string sqlStr = "SELECT * FROM article WHERE articleId=" + articleId;
        Article tempArticle = null;
        DataSet dataset = SqlManager.GetDataSet(sqlStr, "article"); //执行查询操作
        //****************************调用这个函数的上层注意检查是否为null，如果为null说明没有查到
        if (dataset.Tables[0].Rows.Count != 0 )
        {
            tempArticle = new Article();
            tempArticle.ArticleId = articleId;
            tempArticle.Title = dataset.Tables[0].Rows[0]["title"].ToString();
            tempArticle.Abstrct = dataset.Tables[0].Rows[0]["abstract"].ToString();
            tempArticle.FileURL = dataset.Tables[0].Rows[0]["fileURL"].ToString();
            tempArticle.Time = (DateTime)dataset.Tables[0].Rows[0]["time"];
            tempArticle.WordCount = Convert.ToInt32(dataset.Tables[0].Rows[0]["wordCount"]);
            tempArticle.Heat = Convert.ToInt32(dataset.Tables[0].Rows[0]["heat"]);
            return tempArticle;
        }
        return tempArticle;

    }
}