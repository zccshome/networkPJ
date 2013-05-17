using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// SqlManager 的摘要说明
/// </summary>
public class SqlManager
{
	public SqlManager()
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

    /// <summary>
    /// 用来执行对数据库中的数据【添加、修改、删除】等功能。成功返回true，否则返回false；
    /// </summary>
    /// <param name="sqlStr"></param>
    /// <returns></returns>
    public static bool ExecSQL(string sqlStr)
    {
        //System.Diagnostics.Debug.Write(sqlStr);      ///////******************************888
        SqlConnection myConn = GetConnection();
        myConn.Open();
        SqlCommand myCmd = new SqlCommand(sqlStr, myConn);
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
    /// <summary>
    /// 用于执行SQL语句并返回数据集，
    /// 主要对数据库中的数据进行 【查询】
    /// 参数为要执行的sql语句和表名
    /// ？？？不知道为什么要有表名，还没仔细看呢
    /// </summary>
    /// <param name="sqlStr"></param>
    /// <param name="Tablename"></param>
    /// <returns></returns>
    public static System.Data.DataSet GetDataSet(string sqlStr, string Tablename)
    {
        SqlConnection myConn = GetConnection();
        myConn.Open();
        SqlDataAdapter adapt = new SqlDataAdapter(sqlStr, myConn);
        DataSet dataset = new DataSet();
        adapt.Fill(dataset, Tablename);
        myConn.Close();
        return dataset;
    }

    public static object InsertAndGetId(string sqlStr)
    {
        SqlConnection myConn = GetConnection();
        myConn.Open();
        SqlCommand myCmd = new SqlCommand(sqlStr, myConn);
        object result = null;
        try
        {
            result = myCmd.ExecuteScalar();
            myConn.Close();
        }
        catch
        {
            myConn.Close();

        }
        return result;
    }

}