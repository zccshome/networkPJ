using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// DBHelper 的摘要说明
/// </summary>
public class DBHelper
{
	public DBHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 提供到数据库的连接，设定用户名密码；
    /// 连接成功返回SqlConnection对象
    /// </summary>
    /// <returns></returns>
    public static SqlConnection GetConnection()
    {
        string sqlStr = "Server='10.131.228.247';Uid=sa;Pwd=root;DataBase=group";
        SqlConnection con = new SqlConnection(sqlStr);
        return con;
    }

    public static bool ExecSQL(SqlCommand myCmd)
    {
        
        SqlConnection myConn = GetConnection();
        myCmd.Connection = myConn;
        myConn.Open();
        try
        {
            myCmd.ExecuteNonQuery();
            myConn.Close();
        }
        catch
        {
            myConn.Close();
            return false;
        }
        return true;

    }

    public static DataSet GetDataSet(SqlCommand cmd)
    {
        SqlConnection myConn = GetConnection();
        cmd.Connection = myConn;
        myConn.Open();
        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
        DataSet dataset = new DataSet();
        adapt.Fill(dataset);
        myConn.Close();
        return dataset;
    }

    public static object GetOneResult(SqlCommand cmd)
    {
        SqlConnection myConn = GetConnection();
        cmd.Connection = myConn;
        myConn.Open();
        object result = null;
        try
        {
            result = cmd.ExecuteScalar();
            myConn.Close();
        }
        catch
        {
            myConn.Close();

        }
        return result;
    }

    public static SqlCommand cmdProducer(string sqlStr)
    {
        SqlCommand cmd = new SqlCommand(sqlStr);
        Dictionary<string, object> dict = new Dictionary<string, object>();
        
        return cmd;
    }
}