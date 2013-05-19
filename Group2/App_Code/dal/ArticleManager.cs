using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

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

        string title = a.Title;
        string abstrct = a.Abstrct;
        string fileURL = a.FileURL;
        DateTime time = a.Time;
        int wordCount = a.WordCount;
        int heat = a.Heat;
        string sqlStr = "INSERT INTO article (title, abstract, fileURL, time, wordCount, heat) VALUES ('" + title + "','" + abstrct + "','" + fileURL + "','" + time + "'," + wordCount + "," + heat + ") SELECT Scope_Identity()";
        object result = SqlManager.InsertAndGetId(sqlStr);
        if (result != null)
        {
            return Convert.ToInt32(result);
        }
        return -1;


    }

    // 删除传入参数中id所示的记录，不对除Article表之外的任何表进行操作。仅读取传入参数中的ArticleId字段
    // 仅供测试函数调用
    public static bool deleteRecord( Article a )
    {
        string sqlStr = "DELETE FROM article WHERE articleId=@aId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@aId",a.ArticleId);
        return DBHelper.ExecSQL(cmd);
        
    }

    //返回数据库中相应的article记录（仅使用传入参数中的articleId字段）
    //返回一个Article的bean
    public static Article selectRecordByArticleId(Article a)
    {
        int articleId = a.ArticleId;
        //****************************调用这个函数的上层注意检查是否为null，如果为null说明没有查到
        return getArticleById(articleId);

    }

    //返回数据库中相应的article记录（仅使用传入参数中的title字段）
    //返回一个Article的bean
    public static Article selectRecordByTitle(Article a)
    {
        string title = a.Title;
        string sqlStr = "SELECT * FROM article WHERE title='" + title + "'";
        DataSet dataset = SqlManager.GetDataSet(sqlStr, "article"); //执行查询操作
        //****************************调用这个函数的上层注意检查是否为null，如果为null说明没有查到
        if (dataset.Tables[0].Rows.Count != 0)
        {
            a.Title = dataset.Tables[0].Rows[0]["title"].ToString();
            a.Abstrct = dataset.Tables[0].Rows[0]["abstract"].ToString();
            a.FileURL = dataset.Tables[0].Rows[0]["fileURL"].ToString();
            a.Time = (DateTime)dataset.Tables[0].Rows[0]["time"];
            a.WordCount = Convert.ToInt32(dataset.Tables[0].Rows[0]["wordCount"]);
            a.Heat = Convert.ToInt32(dataset.Tables[0].Rows[0]["heat"]);
            return a;
        }
        return null;
    }

    //将传入参数中的articleId对应的数据库中的文章的wordCount字段值设为传入参数中的wordCount值（仅读取传入参数中的articleId和wordCount字段）
    //成功返回true，失败返回false
    public static bool updateWordCount(Article a)
    {
        int articleId = a.ArticleId;
        int wordCount = a.WordCount;
        //update 表名 set admin='名称' where id=1
        string sqlStr = "UPDATE article SET wordCount=" + wordCount + " WHERE articleId=" + articleId;
        return SqlManager.ExecSQL(sqlStr);
    }

    //将传入参数中的articleId对应的数据库中的文章的fileURL字段值设为传入参数中的fileURL值（仅读取传入参数中的articleId和fileURL字段）
    //成功返回true，失败返回false
    public static bool updateFileURL(Article a)
    {
        int articleId = a.ArticleId;
        string fileURL = a.FileURL;
        string sqlStr = "UPDATE article SET fileURL='" + fileURL + "' WHERE articleId=" + articleId;
        return SqlManager.ExecSQL(sqlStr);
    }

    //返回数据库中的文章总篇数。建议通过查询article表的条目数来确定。
    public static int countArticleNum()
    {
        string sqlStr = "SELECT count(articleId) AS totalNum FROM article";
        DataSet dataset = SqlManager.GetDataSet(sqlStr, "article");
        return Convert.ToInt32(dataset.Tables[0].Rows[0]["totalNum"]);
    }

    //获取该分类下的所有文章的article的列表（仅读取传入参数中的GroupId字段）
    //返回article的列表List<Article>
    public static List<Article> getArticleListByPrimaryGroup(PrimaryGroups g)
    {

        List<int> articlesByGroupId = Article2GroupManager.getArticleIdsByGroup(g);
        return getArticleListByArticleIdList(articlesByGroupId);

    }

    //根据传入的articleId数组，获取相应文章的article对象的列表
    //返回article的列表List<Article>
    public static List<Article> getArticleListByArticleIdList(List<int> articleIdList)
    {
        if (articleIdList == null || articleIdList.Count < 1)
            return null;
        List<Article> resultList = new List<Article>();
        articleIdList.ForEach(delegate(int articleId)
        {
            resultList.Add(getArticleById(articleId));
        });
        return resultList;
    }

    //******************************************** SEARCH  ****************************************//
    /*
     * 输入：搜索关键词列表
     * 输出：Article的列表
     * 功能：在数据库所有文章中搜索标题和摘要中包含全部指定关键词的文章并返回
     */
    public static List<Article> searchAll(string[] key)
    {
        string oriStr = "SELECT * FROM article WHERE ";
        string sqlStr = sqlStringProducer(oriStr, key);
        SqlCommand cmd = new SqlCommand(sqlStr);
        DataSet resultSet = DBHelper.GetDataSet(cmd);
        return getAllArticleFromDataSet(resultSet);
    }

    /*
     * 输入：搜索关键词列表，限定为搜索范围的主分类的id
     * 输出：Article的列表
     * 功能：在数据库所有属于指定主分类的文章中搜索标题和摘要中包含全部指定关键词的文章并返回
     */
    public static List<Article> searchInPrimaryGroup(string[] key, int primaryGroupId)
    {
        string oriStr = "SELECT * FROM v_ArticleInnerGroup WHERE groupId=" + primaryGroupId + " AND ";
        string sqlStr = sqlStringProducer(oriStr, key);
        SqlCommand cmd = new SqlCommand(sqlStr);
        DataSet resultSet = DBHelper.GetDataSet(cmd);
        return getAllArticleFromDataSet(resultSet);
    }

    /*
     * 输入：搜索关键词列表
     * 输出：Article的列表
     * 功能：在数据库所有属于“其他”主分类的文章中搜索标题和摘要中包含全部指定关键词的文章并返回
     */
    public static List<Article> searchInOther(string[] key)
    {
        string oriStr = "SELECT * FROM v_ArticleInnerGroup WHERE groupId=0"  + " AND ";
        string sqlStr = sqlStringProducer(oriStr, key);
        SqlCommand cmd = new SqlCommand(sqlStr);
        DataSet resultSet = DBHelper.GetDataSet(cmd);
        return getAllArticleFromDataSet(resultSet);
    }



    //获取指定Tag下的所有文章的article的列表（仅读取传入参数中的TagId字段）
    //返回article的列表List<Article>
    public static List<Article> getArticleListByDynamicSearch(Tag t)
    {
        if (t == null)
            return null;
        t = TagManager.getTag(t);
        if (t == null)
            return null;
        string [] keys = t.TagKeys.Trim().Split(' ');
        return searchInPrimaryGroup(keys, t.GroupId);
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
    /*
    public static string[] getKeysArray(string keys)
    {
        
        string[] tempArr = keys.Trim().Split(' ');
        int length = tempArr.Length;
        string[] resultArr = new string[length];
        for (int i = 0; i < length; i++)
        {
            resultArr[i] = '%' + tempArr[i] + '%';
        }
        return resultArr;
    }

    public static string sqlStringProducer(string keys)
    {
        string[] keysArray = getKeysArray(keys);
        string sqlStr = "SELECT * FROM article WHERE ";
        for (int i = 0; i < keysArray.Length; i++)
        {
            if (i == keysArray.Length - 1)
                sqlStr += "abstract LIKE " + keysArray[i];
            else
                sqlStr += "abstract LIKE " + keysArray[i] + " AND ";
        }
        return sqlStr;
    }
    */

    public static string sqlStringProducer(string oriStr, string[] keysArray)
    {
        string sqlStr = oriStr;
        for (int i = 0; i < keysArray.Length; i++)
        {
            if (i == keysArray.Length - 1)
                sqlStr += "abstract LIKE '%" + keysArray[i] + "%'";
            else
                sqlStr += "abstract LIKE '%" + keysArray[i] + "%' AND ";
        }
        return sqlStr;
       
    }

    private static List<Article> getAllArticleFromDataSet(DataSet resultSet)
    {
        List<Article> resultList = new List<Article>();
        if (resultSet.Tables[0].Rows.Count != 0)
        {
            for (int i = 0; i < resultSet.Tables[0].Rows.Count; i++)
            {
                Article art = new Article();
                art.ArticleId = Convert.ToInt32(resultSet.Tables[0].Rows[i]["articleId"]);
                art.Title = Convert.ToString(resultSet.Tables[0].Rows[i]["title"]);
                art.Abstrct = Convert.ToString(resultSet.Tables[0].Rows[i]["abstract"]);
                art.FileURL = Convert.ToString(resultSet.Tables[0].Rows[i]["fileURL"]);
                art.Time = Convert.ToDateTime(resultSet.Tables[0].Rows[i]["time"]);
                art.WordCount = Convert.ToInt32(resultSet.Tables[0].Rows[i]["wordCount"]);
                art.Heat = Convert.ToInt32(resultSet.Tables[0].Rows[i]["heat"]);
                resultList.Add(art);
            }
            return resultList;
        }
        return null;
    }

}