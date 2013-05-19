using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///操作 GlobalParseManager 的底层函数类
/// </summary>
public class GlobalParseManager
{
    public GlobalParseManager()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    //根据传入的参数在数据库中增加一条记录
    //注：传入参数中的articleNumber字段值需要上层设置好，本函数只管存储
    //成功返回true，失败返回false
    public static bool addRecord(GlobalParse gp)
    {
        string wordContent = gp.WordContent;
        string type = gp.Type;
        int articleNumber = gp.ArticleNumber;
        string sqlStr = "INSERT INTO globalParse (wordContent, type, articleNumber) VALUES('" + wordContent + "','" + type + "'," + articleNumber + ")";
        return SqlManager.ExecSQL(sqlStr);

    }

    //根据传入参数的wordContent获取一条GlobalParse的记录（仅读取传入参数中的wordContent字段）
    //返回一个GlobalParse的bean
    public static GlobalParse selectRecordByWordContent(GlobalParse gp)
    {
        string wordContent = gp.WordContent;
        string sqlStr = "SELECT * FROM globalParse WHERE wordContent='" + wordContent + "'";
        DataSet dataset = SqlManager.GetDataSet(sqlStr, "globalParse");
        if (dataset.Tables[0].Rows.Count == 0)
            return null;
        gp.Type = dataset.Tables[0].Rows[0]["type"].ToString();
        gp.ArticleNumber = Convert.ToInt32(dataset.Tables[0].Rows[0]["articleNumber"]);
        return gp;
    }

    //用传入参数更新数据库中wordContent为传入参数中wordContent的那一条记录
    //成功返回true，失败返回false
    public static bool updateRecord(GlobalParse gp)
    {
        string wordContent = gp.WordContent;
        string type = gp.Type;
        int articleNumber = gp.ArticleNumber;
        //update 表名 set admin='名称' where id=1
        string sqlStr = "UPDATE globalParse SET type='" + type + "', articleNumber=" + articleNumber + " WHERE wordContent='" + wordContent + "'";
        return SqlManager.ExecSQL(sqlStr);
    }

    // 删除记录，不对除GlobalParse表之外的任何表进行操作。仅读取传入参数中的WordContent字段
    // 仅供测试函数调用
    public static bool deleteRecord(GlobalParse gp)
    {
        string sqlStr = "DELETE FROM globalParse WHERE wordContent=@wContent";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@wContent", gp.WordContent);
        return DBHelper.ExecSQL(cmd);
    }
}