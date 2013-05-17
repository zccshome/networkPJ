using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        return false;
    }

    //根据传入参数删除对应的记录
    //成功返回true，失败返回false
    public static bool deleteRecord(PrimaryGroupKeyWords p)
    {
        return false;
    }

    //返回指定主分类的所有关键词的列表
    public static List<string> getKeyWordsOfCertainPrimaryGroup(PrimaryGroups g)
    {
        return null;
    }
}