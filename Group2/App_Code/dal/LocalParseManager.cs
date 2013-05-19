using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///操作 LocalParseManager 的底层函数类
/// </summary>
public class LocalParseManager
{
    public LocalParseManager()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }


    //根据传入参数增加一条新的记录
    //注：传入参数中的count字段值需要上层设置好，本函数只管存储
    //成功返回true，失败返回false
    public static bool addRecord(LocalParse lp)
    {
        /*
        int articleId = lp.ArticleId;
        string wordContent = lp.WordContent;
        string type = lp.Type;
        int count = lp.Count;
        string sqlStr = "INSERT INTO localParse (articleId, wordContent, type, counts) VALUES (" + articleId + ",'" +
           wordContent + "','" + type + "'," + count + ")";
        //string sqlStr = "INSERT INTO localParse (articleId, wordContent, type, counts) VALUES (" + articleId + ",俄罗斯,n," + count + ")";
        return SqlManager.ExecSQL(sqlStr);
        */
         
       
        string sqlStr = "INSERT INTO localParse (articleId, wordContent, type, counts) VALUES (@aId, @wContent, @type, @counts)";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@aId", lp.ArticleId);
        cmd.Parameters.AddWithValue("@wContent", lp.WordContent);
        cmd.Parameters.AddWithValue("@type", lp.Type);
        cmd.Parameters.AddWithValue("@counts", lp.Count);
        return DBHelper.ExecSQL(cmd);

    }

    //根据传入参数的articleId和wordContent查询一条localParse的记录（仅读取传入参数中的articleId和wordContent字段）
    //返回一个LocalParse的bean
    public static LocalParse selectRecord(LocalParse lp)
    {
        int articleId = lp.ArticleId;
        string wordContent = lp.WordContent;
        string sqlStr = "SELECT * FROM localParse WHERE articleId=" + articleId + " AND wordContent='" + wordContent + "'";
        DataSet dataset = SqlManager.GetDataSet(sqlStr, "localParse");
        if (dataset.Tables[0].Rows.Count == 0)
            return null;
        lp.Type = dataset.Tables[0].Rows[0]["type"].ToString();
        lp.Count = Convert.ToInt32(dataset.Tables[0].Rows[0]["counts"]);
        return lp;
    }

    //通过传入参数的articleId和wordContent字段找到相应记录，并将其count字段更新为传入参数中的相应值（仅读取传入参数中除type之外的字段）
    //成功返回true，失败返回false
    public static bool updateRecord(LocalParse lp)
    {
        int articleId = lp.ArticleId;
        string wordContent = lp.WordContent;
        string type = lp.Type;
        int count = lp.Count;
        string sqlStr = "UPDATE localParse SET type='" + type + "', counts=" + count + " WHERE articleId=" + articleId + " AND wordContent='" + wordContent + "'";
        return SqlManager.ExecSQL(sqlStr);
    }

    // 删除传入参数中id所示的记录，不对除LocalParse表之外的任何表进行操作。仅读取传入参数中的ArticleId字段
    // 仅供测试函数调用
    public static bool deleteRecord(LocalParse lp)
    {
        string sqlStr = "DELETE FROM localParse WHERE articleId=@aId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@aId", lp.ArticleId);
        return DBHelper.ExecSQL(cmd);

    }
}