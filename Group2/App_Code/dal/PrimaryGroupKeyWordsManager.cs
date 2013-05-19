using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// PrimaryGroupKeyWordsManager 的摘要说明
/// </summary>
public class PrimaryGroupKeyWordsManager
{
    public PrimaryGroupKeyWordsManager()
    {
    //
    // TODO: 在此处添加构造函数逻辑	
    //

    }

    //根据传入参数增加一条新的记录
    //成功返回true，失败返回false
    public static bool addRecord( PrimaryGroupKeyWords p )
    {
        string sqlStr = "INSERT INTO primaryGroupKeyWords (primaryGroupId, keyWord) VALUES (@pgId, @kWord)";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@pgId", p.PrimaryGroupId);
        cmd.Parameters.AddWithValue("@kWord", p.KeyWord);
        return DBHelper.ExecSQL(cmd);

    }

    //根据传入参数删除对应的记录
    //成功返回true，失败返回false
    public static bool deleteRecord(PrimaryGroupKeyWords p)
    {
        string sqlStr = "DELETE FROM primaryGroupKeyWords WHERE primaryGroupId=@pgId AND keyWord=@kWords";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@pgId", p.PrimaryGroupId);
        cmd.Parameters.AddWithValue("@kWords", p.KeyWord);
        return DBHelper.ExecSQL(cmd);

    }

    //返回指定主分类的所有关键词的列表
    public static List<string> getKeyWordsOfCertainPrimaryGroup(PrimaryGroups g)
    {
        List<string> result = new List<string>();
        string sqlStr = "SELECT * FROM primaryGroupKeyWords WHERE primaryGroupId=@pgId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@pgId", g.GroupId);
        DataSet resultSet = DBHelper.GetDataSet(cmd);
        if (resultSet.Tables[0].Rows.Count != 0)
        {
            for (int i = 0; i < resultSet.Tables[0].Rows.Count ; i++) 
            {
                string tempStr = resultSet.Tables[0].Rows[i]["keyWord"].ToString();
                result.Add(tempStr);
            }
            return result;
        }
        return null;
    }
}