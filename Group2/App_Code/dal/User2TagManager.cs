using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        return false;
    }

    //根据传入参数删除对应的记录
    //成功返回true，失败返回false
    public static bool deleteRecord(User2Tag u2t)
    {
        return false;
    }

    //根据传入参数的userId查询该用户关注的所有tag记录（仅读取传入参数中的userId字段）
    //返回一个tagId的list
    public static List<int> getTagListByUserId(User u)
    {
        return null;
    }
}