using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///操作 GroupMananger 的底层函数类
/// </summary>
public class PrimaryGroupMananger
{
    public PrimaryGroupMananger()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
        //System.Diagnostics.Debug.Write("create a group!");
    }

    //根据传入参数增加一条记录
    //成功返回新增记录的groupId，失败返回-1
    //groupId非自增，由程序判断，选择空的
    public static int addRecord(PrimaryGroups g)
    {
        string sqlStr = "INSERT INTO primaryGroups (groupName) VALUES (@pName) SELECT Scope_Identity()";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@pName", g.GroupName);
        Object result = DBHelper.GetOneResult(cmd);
        if (result != null)
        {
            g.GroupId = Convert.ToInt32(result);
            return g.GroupId;
        }
        return -1;

    }

    //根据传入参数中的groupId查询一条相应记录（仅读取传入参数中的groupId字段）
    //返回一个Group的model
    //失败则返回null
    public static PrimaryGroups selectRecord(PrimaryGroups g)
    {
        string sqlStr = "SELECT * FROM primaryGroups WHERE groupId=@gId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@gId", g.GroupId);
        DataSet resultSet = DBHelper.GetDataSet(cmd);
        if (resultSet.Tables[0].Rows.Count != 0)
        {
            g.GroupName = resultSet.Tables[0].Rows[0]["groupName"].ToString();
            return g;
        }
        return null;
    }

    //根据传入参数更新数据库中groupId为传入参数的groupId的相应记录
    //成功返回true，失败返回false
    public static bool updateRecord(PrimaryGroups g)
    {
        string sqlStr = "UPDATE primaryGroups SET groupName=@gName WHERE groupId=@gId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@gName", g.GroupName);
        cmd.Parameters.AddWithValue("@gId", g.GroupId);
        return DBHelper.ExecSQL(cmd);

    }

     /*
      * 输入：无
      * 输出：所有组别的列表。格式为List<string[]>，每一个元素的string[]有两项，第一项是groupId，第二项是groupName
      * 功能：返回所有组别的列表，供管理员在不满意文章自动分类结果的情况下依据该列表进行手动分类
      * 说明：从功能上讲，其实仅返回groupName的List也是可以的，之所以还要返回groupId，是因为groupId是主键，而groupName理论上
      *       是可以重复的。
      */
    public static List<string[]> getAllGroups()
    {
        string sqlStr = "SELECT * FROM primaryGroups";
        SqlCommand cmd = new SqlCommand(sqlStr);
        DataSet searchedSet = DBHelper.GetDataSet(cmd);
        List<string[]> result = new List<string[]>();
        if (searchedSet.Tables[0].Rows.Count != 0)
        {
            for(int i =0;i<searchedSet.Tables[0].Rows.Count;i++)
            {
                string[] tempArr = new string[2];
                tempArr[0] = searchedSet.Tables[0].Rows[i]["groupId"].ToString();
                tempArr[1] = searchedSet.Tables[0].Rows[i]["groupName"].ToString();
                result.Add(tempArr);
            }
            return result;
        }
        return null;
    }

    // 删除传入参数中id所示的记录，不对除PrimaryGroups表之外的任何表进行操作。仅读取传入参数中的PrimaryGroupsId字段
    // 仅供测试函数调用
    public static bool deleteRecord(PrimaryGroups pg)
    {
        string sqlStr = "DELETE FROM primaryGroups WHERE groupId=@gId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@gId", pg.GroupId);
        return DBHelper.ExecSQL(cmd);
    }
}