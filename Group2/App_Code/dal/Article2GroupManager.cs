using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
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
        //从传入类中取得参数

        /*
        int aritcleId = a2g.ArticleId;
        int groupId = a2g.GroupId;
        string sqlStr = "INSERT INTO article2group (articleId, groupId) VALUES (" + aritcleId + "," + groupId + ")";
        return SqlManager.ExecSQL(sqlStr);
         * */

        string sqlStr = "INSERT INTO article2group (articleId, groupId) VALUES (@aId, @gId)";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@aId",a2g.ArticleId);
        cmd.Parameters.AddWithValue("@gId", a2g.GroupId);
        return DBHelper.ExecSQL(cmd);
    }

    //删除Article2Group表中对应于传入参数的那一条记录
    //成功返回true，失败返回false
    public static bool deleteRecord(Article2Group a2g)
    {
        /*
        int aritcleId = a2g.ArticleId;
        int groupId = a2g.GroupId;
        string sqlStr = "DELETE FROM article2group WHERE articleId=" + aritcleId + " AND groupId=" + groupId + ";";
        return SqlManager.ExecSQL(sqlStr);
         * */
        string sqlStr = "DELETE FROM article2group WHERE articleId=@aId AND groupId=@gId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@aId", a2g.ArticleId);
        cmd.Parameters.AddWithValue("@gId", a2g.GroupId);
        return DBHelper.ExecSQL(cmd);

    }

    //仅读取传入参数中的ArticleId字段，获取该文章所属的全部Group的groupId的列表
    //返回groupId的列表List<int>
    public static List<int> getGroupIdsByArticle(Article a)
    {
        int articleId = a.ArticleId;
        string sqlStr = "SELECT groupId FROM article2group WHERE articleId=" + articleId;
        DataSet dataset = SqlManager.GetDataSet(sqlStr, "article2group"); //执行查询操作
        if (dataset.Tables[0].Rows.Count == 0)
            return null;
        List<int> resultList = new List<int>();
        for (int i = 0; i <= dataset.Tables[0].Rows.Count - 1; i++)
        {
            //将取得的dataset中所有的groupId强制转化为int类型并加入到resultList中
            resultList.Add(Convert.ToInt32(dataset.Tables[0].Rows[i]["groupId"]));

        }
        return resultList;
    }

    //仅读取传入参数中的GroupId，获取该分类下的所有文章的articleId的列表
    //返回articleId的列表List<int>
    public static List<int> getArticleIdsByGroup(PrimaryGroups g)
    {
        int groupId = g.GroupId;
        string sqlStr = "SELECT articleId FROM article2group WHERE groupId=" + groupId;
        DataSet dataset = SqlManager.GetDataSet(sqlStr, "article2group"); //执行查询操作
        if (dataset.Tables[0].Rows.Count == 0)
            return null;
        List<int> resultList = new List<int>();
        for (int i = 0; i <= dataset.Tables[0].Rows.Count - 1; i++)
        {
            //将取得的dataset中所有的articleId强制转化为int类型并加入到resultList中
            resultList.Add(Convert.ToInt32(dataset.Tables[0].Rows[i]["articleId"]));

        }
        return resultList;
    }


}