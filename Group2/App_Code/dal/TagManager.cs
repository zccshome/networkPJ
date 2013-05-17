using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    //成功返回true，失败返回false
    public static bool addTag( Tag t )
    {
        return false;
    }

    //根据传入参数删除对应的记录（仅读取传入参数中的tagId字段）
    //成功返回true，失败返回false
    public static bool deleteTag(Tag t)
    {
        return false;
    }

    //根据传入参数的tagId查询一条Tag的记录（仅读取传入参数中的tagId字段）
    //返回一个Tag的model
    public static Tag getTag(Tag t)
    {
        return null;
    }

    //将传入的int数组作为tagId，返回相应的tag的列表
    //返回tag的数组
    public static List<Tag> getTagsByIdList( List<int> tagIds )
    {
        return null;
    }
}