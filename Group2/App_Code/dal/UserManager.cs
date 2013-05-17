using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// UserManager 的摘要说明
/// </summary>
public class UserManager
{
	public UserManager()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //根据传入参数增加一条新的记录
    //成功返回1，已有重复账号返回0，数据库操作失败返回-1
    public static int addUser(User u)
    {
        return -1;
    }

    //根据传入参数的userId查询一条User的记录（仅读取传入参数中的userId字段）
    //返回一个User的model
    public static User getUserById(User u)
    {
        return null;
    }

    //根据传入参数的userName和userPass查询一条User的记录（仅读取传入参数中的userName和userPass字段）
    //返回一个User的model（其中的userPass字段为空）
    //注意：返回值中的userPass字段必须设置为空
    public static User getUserByNameAndPass(User u)
    {
        return null;
    }

    //检查传入的userMail是否已经被user表中某个user注册过了，若是则返回true，否则返回false
    //传入的邮箱地址已经被注册过返回true，否则返回false
    public static bool userMailRegistered (string userMail)
    {
        return false;
    }
}