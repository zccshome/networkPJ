using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// TagManager 的摘要说明
/// </summary>
public class TagManager
{
	public TagManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //根据传入参数增加一条新的记录
    //成功返回新增记录的id，失败返回-1
    public static int addTag( Tag t )
    {
        string sqlStr = "INSERT INTO tag(tagName, tagKeys, groupId, tagTime, isPrivate) values(@tName, @tKeys, @gId, @tTime, @iPrivate) SELECT Scope_Identity();";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@tName", t.TagName);
        cmd.Parameters.AddWithValue("@tKeys", t.TagKeys);
        cmd.Parameters.AddWithValue("@gId", t.GroupId);
        cmd.Parameters.AddWithValue("@tTime", t.TagTime);
        cmd.Parameters.AddWithValue("@iPrivate", t.IsPrivate);
        Object result = DBHelper.GetOneResult(cmd);
        if (result != null)
        {
            return Convert.ToInt32(result);
        }
        return -1;
    }

    //根据传入参数删除对应的记录（仅读取传入参数中的tagId字段）
    //成功返回true，失败返回false
    public static bool deleteTag(Tag t)
    {
        string sqlStr = "DELETE FROM tag WHERE tagId=@tId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@tId", t.TagId);
        return DBHelper.ExecSQL(cmd);

    }

    //根据传入参数的tagId查询一条Tag的记录（仅读取传入参数中的tagId字段）
    //返回一个Tag的model
    public static Tag getTag(Tag t)
    {
        return getTagById(t.TagId);
    }

    //将传入的int数组作为tagId，返回相应的tag的列表
    //返回tag的数组
    public static List<Tag> getTagsByIdList( List<int> tagIds )
    {
        if (tagIds == null)
            return null;
        List<Tag> result = new List<Tag>();
        tagIds.ForEach(delegate(int tagId)
        {
            result.Add(getTagById(tagId));
        });
        return result;
         

    }

    /*
      * 输入：无
      * 输出：所有Tag的列表。格式为Tag
      * 功能：返回所有Tag的列表
      * 说明：从功能上讲，其实仅返回tagName的List也是可以的，之所以还要返回tagId，是因为tagId是主键，而tagName理论上
      *       是可以重复的。
      */
    public static List<Tag> getAllTagsByCertainGroupId(int groupId)
    {
        
        List<Tag> result = new List<Tag>();
        string sqlStr = "SELECT * FROM tag WHERE groupId=@gId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@gId", groupId);
        DataSet searchedSet = DBHelper.GetDataSet(cmd);
        if (searchedSet.Tables[0].Rows.Count != 0)
        {
            for (int i = 0; i < searchedSet.Tables[0].Rows.Count; i++)
            {
                Tag t = new Tag();
                t.TagId = Convert.ToInt32(searchedSet.Tables[0].Rows[i]["tagId"]);
                t.TagName = searchedSet.Tables[0].Rows[i]["tagName"].ToString();
                t.TagKeys = searchedSet.Tables[0].Rows[i]["tagKeys"].ToString();
                t.GroupId = Convert.ToInt32(searchedSet.Tables[0].Rows[i]["groupId"]);
                t.TagTime = Convert.ToDateTime(searchedSet.Tables[0].Rows[i]["tagTime"]);
                t.IsPrivate = Convert.ToInt32(searchedSet.Tables[0].Rows[i]["isPrivate"]);
                result.Add(t);
            }
            return result;
        }
        return null;
    }


    ////////////////////////////////////私有方法
    private static Tag getTagById(int tagId)
    {
        string sqlStr = "SELECT * FROM tag WHERE tagId=@tId";
        SqlCommand cmd = new SqlCommand(sqlStr);
        cmd.Parameters.AddWithValue("@tId", tagId);
        DataSet result = DBHelper.GetDataSet(cmd);
        if (result.Tables[0].Rows.Count != 0)
        {
            Tag t = new Tag();
            t.TagId = tagId;
            t.TagName = result.Tables[0].Rows[0]["tagName"].ToString();
            t.TagKeys = result.Tables[0].Rows[0]["tagKeys"].ToString();
            t.GroupId = Convert.ToInt32(result.Tables[0].Rows[0]["groupId"]);
            t.TagTime = Convert.ToDateTime(result.Tables[0].Rows[0]["tagTime"]);
            t.IsPrivate = Convert.ToInt32(result.Tables[0].Rows[0]["isPrivate"]);
            return t;
        }
        return null;
    }


}