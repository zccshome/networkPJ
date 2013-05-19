using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// User 的摘要说明
/// </summary>
public class User
{
	public User()
	{
	}


    public User(string userName, string userPass, string userMail)
    {
        UserName = userName;
        UserPass = userPass;
        UserMail = userMail;
    }

    public User(int userId, string userName, string userPass, string userMail)
    {
        UserId = userId;
        UserName = userName;
        UserPass = userPass;
        UserMail = userMail;
    }


    private int userId;         //用户的id
    private string userName;    //用户名称
    private string userPass;    //用户密码
    private string userMail;    //用户邮箱

    public int UserId
    {
        get { return userId; }
        set { userId = value; }
    }

    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }

    public string UserPass
    {
        get { return userPass; }
        set { userPass = value; }
    }

    public string UserMail
    {
        get { return userMail; }
        set { userMail = value; }
    }



}