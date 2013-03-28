using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///操作 LocalParseManager 的底层函数类
/// </summary>
public class LocalParseManager
{
	public LocalParseManager()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    //根据传入参数增加一条新的记录
    //注：传入参数中的count字段值需要上层设置好，本函数只管存储
    //成功返回true，失败返回false
    public static bool addRecord(LocalParse lp)
    {
        return false;
    }

    //根据传入参数的articleId和wordContent查询一条localParse的记录（仅读取传入参数中的articleId和wordContent字段）
    //返回一个LocalParse的bean
    public static LocalParse selectRecord(LocalParse lp)
    {
        return null;
    }

    //通过传入参数的articleId和wordContent字段找到相应记录，并将其count字段更新为传入参数中的相应值（仅读取传入参数中除type之外的字段）
    //成功返回true，失败返回false
    public static bool updateRecord(LocalParse lp)
    {
        return false;
    }
}