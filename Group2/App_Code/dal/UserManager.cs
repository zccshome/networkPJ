using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// UserManager 的摘要说明
/// </summary>
public class UserManager
{
	public UserManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //根据传入参数增加一条新的记录
    //成功返回新用户的userId，已有重复账号返回-1，数据库操作失败返回-2

    //什么叫做已有重复账号？用户名相同？还是注册邮箱相同？
    public static int addUser(User u)
    {
        if (userMailRegistered(u.UserMail))
            return -1;
        string sqlStr = "INSERT INTO users (userName, userPass, userMail) VALUES(@uName, @uPass, @uMail) SELECT Scope_Identity();";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@uName", u.UserName);
        cmd.Parameters.AddWithValue("@uPass", u.UserPass);
        cmd.Parameters.AddWithValue("@uMail", u.UserMail);
        Object result = DBHelper.GetOneResult(cmd);
        if (result != null)
        {
            return Convert.ToInt32(result);
        }
        return -2;
    }

    //根据传入参数的userId查询一条User的记录（仅读取传入参数中的userId字段）
    //返回一个User的model
    public static User getUserById(User u)
    {
        string sqlStr = "SELECT * FROM users WHERE userId=@uId";
        SqlCommand cmd= new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@uId", u.UserId);
        DataSet searchedSet = DBHelper.GetDataSet(cmd);
        if (searchedSet.Tables[0].Rows.Count != 0)
        {
            u.UserName = Convert.ToString(searchedSet.Tables[0].Rows[0]["userName"]);
            u.UserPass = Convert.ToString(searchedSet.Tables[0].Rows[0]["userPass"]);
            u.UserMail = Convert.ToString(searchedSet.Tables[0].Rows[0]["userMail"]);
            return u;
        }
        return null;
    }

    //根据传入参数的userName和userPass查询一条User的记录（仅读取传入参数中的userName和userPass字段）
    //返回一个User的model（其中的userPass字段为空）
    //注意：返回值中的userPass字段必须设置为空
    public static User getUserByNameAndPass(User u)
    {
        string sqlStr = "SELECT * FROM users WHERE userName=@uName AND userPass=@uPass";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@uName", u.UserName);
        cmd.Parameters.AddWithValue("@uPass", u.UserPass);
        DataSet searchedSet = DBHelper.GetDataSet(cmd);
        if (searchedSet.Tables[0].Rows.Count != 0)
        {
            u.UserId = Convert.ToInt32(searchedSet.Tables[0].Rows[0]["userId"]);
            u.UserName = Convert.ToString(searchedSet.Tables[0].Rows[0]["userName"]);
            u.UserPass = null;
            u.UserMail = Convert.ToString(searchedSet.Tables[0].Rows[0]["userMail"]);
            return u;
        }
        return null;
    }

    //检查传入的userMail是否已经被user表中某个user注册过了，若是则返回true，否则返回false
    //传入的邮箱地址已经被注册过返回true，否则返回false
    public static bool userMailRegistered (string userMail)
    {
        string sqlStr = "SELECT * FROM users WHERE userMail=@uMail";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@uMail", userMail);
        DataSet searchedSet = DBHelper.GetDataSet(cmd);
        if (searchedSet.Tables[0].Rows.Count != 0)
            return true;
        return false;
        
    }

    // 删除记录，不对除User表之外的任何表进行操作。仅读取传入参数中的UserId字段
    // 仅供测试函数调用
    public static bool deleteRecord(User u)
    {
        string sqlStr = "DELETE FROM users WHERE userId=@uId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@uId", u.UserId);
        return DBHelper.ExecSQL(cmd);
  
    }
}