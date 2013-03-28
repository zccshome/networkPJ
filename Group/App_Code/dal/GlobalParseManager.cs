using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///操作 GlobalParseManager 的底层函数类
/// </summary>
public class GlobalParseManager
{
	public GlobalParseManager()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    //根据传入的参数在数据库中增加一条记录
    //注：传入参数中的articleNumber字段值需要上层设置好，本函数只管存储
    //成功返回true，失败返回false
    public static bool addRecord(GlobalParse gp)
    {
        return false;
    }

    //根据传入参数的wordContent获取一条GlobalParse的记录（仅读取传入参数中的wordContent字段）
    //返回一个GlobalParse的bean
    public static GlobalParse selectRecordByWordContent(GlobalParse gp)
    {
        return null;
    }

    //用传入参数更新数据库中wordContent为传入参数中wordContent的那一条记录
    //成功返回true，失败返回false
    public static bool updateRecord(GlobalParse gp)
    {
        return false;
    }
}