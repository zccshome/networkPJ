using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///操作 GroupMananger 的底层函数类
/// </summary>
public class GroupMananger
{
	public GroupMananger()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    //根据传入参数增加一条记录
    //成功返回新增记录的groupId，失败返回-1
    public static int addRecord(Groups g)
    {
        return -1;
    }

    //根据传入参数中的groupId查询一条相应记录（仅读取传入参数中的groupId字段）
    //返回一个Group的bean
    public static Groups selectRecord(Groups g)
    {
        return null;
    }

    //根据传入参数更新数据库中groupId为传入参数的groupId的相应记录
    //成功返回true，失败返回false
    public static bool updateRecord(Groups g)
    {
        return false;
    }
}