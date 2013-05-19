using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AccountAssist 的摘要说明
/// </summary>
public class AccountAssist
{
	public AccountAssist()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /*
     * 输入：一个User的model，除了userId之外所有字段都已填好，并已经经过合法性检查
     * 输出：成功返回userId，已有重复账号返回-1，数据库操作失败返回-2，邮箱重复返回-3
     * 功能：本函数需要依次实现以下功能：
     *       1、调用mailReplicated函数检查是否已经存在重复账号
     *       2、如无重复则将账户信息存入数据库
     *       3、根据数据库返回的信息判断是否成功，并返回相应信息
     * 用途说明：新用户注册
     */
    public static int register( User u )
    {
        String userMail = u.UserMail;
        if (mailReplicated(userMail))
            return -3;
        int newUserID = UserManager.addUser(u);
        if (newUserID > 0)
            u.UserId = newUserID;
        return newUserID;
    }

    /*
     * 输入：一个User的model，其中的userName和userPass字段都已填好，并已经经过合法性检查
     * 输出：成功返回该用户的User的model（其中的userPass字段为空），失败返回null
     * 功能：检查账户是否存在，存在返回User的model（其中的userPass字段为空），失败返回null
     * 注意：若登陆成功要在session中写入userId。
     * 用途说明：用户登录
     */
    public static User login( User u )
    {
        User uid = UserManager.getUserByNameAndPass(u);
        if (uid == null)
            return null;
        uid.UserPass = "";
        //Session["userId"] = uid.UserId;
        return uid;
    }

    /**
     * 输入：邮箱地址的字符串
     * 输出：若该邮箱已经被注册过，则返回true，否则返回false
     * 功能：检查邮箱是否已经被其他用户注册过
     * 建议：调用UserManager的userMailRegistered函数
     */
    private static bool mailReplicated( string userMail )
    {
        return UserManager.userMailRegistered(userMail) ;
    }
}