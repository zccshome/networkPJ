using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// User2TagManager 的摘要说明
/// </summary>
public class User2TagManager
{
	public User2TagManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //根据传入参数增加一条新的记录
    //成功返回true，失败返回false
    public static bool addRecord(User2Tag u2t )
    {
        string sqlStr = "INSERT INTO user2tag(userId, tagId) VALUES(@uId, @tId)";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@uId", u2t.UserId);
        cmd.Parameters.AddWithValue("@tId", u2t.TagId);
        return DBHelper.ExecSQL(cmd);
 
    }

    //根据传入参数删除对应的记录
    //成功返回true，失败返回false
    public static bool deleteRecord(User2Tag u2t)
    {
        string sqlStr = "DELETE FROM user2tag WHERE userId=@uId AND tagId=@tId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@uId", u2t.UserId);
        cmd.Parameters.AddWithValue("@tId", u2t.TagId);
        return DBHelper.ExecSQL(cmd);

    }

    //根据传入参数的userId查询该用户关注的所有tag记录（仅读取传入参数中的userId字段）
    //返回一个tagId的list
    public static List<int> getTagListByUserId(User u)
    {
        string sqlStr = "SELECT * FROM user2tag WHERE userId=@uId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@uId", u.UserId);
        List<int> result = new List<int>();
        DataSet searchedSet = DBHelper.GetDataSet(cmd);
        if (searchedSet.Tables[0].Rows.Count != 0)
        {
            for (int i = 0; i < searchedSet.Tables[0].Rows.Count ; i++)
            {
                result.Add(Convert.ToInt32(searchedSet.Tables[0].Rows[i]["tagId"]));
            }
            return result;
        }
        return null;
    }
}