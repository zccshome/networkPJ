using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// User2Tag 的摘要说明
/// </summary>
public class User2Tag
{
	public User2Tag()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public User2Tag(int userId, int tagId)
    {
        UserId = userId;
        TagId = tagId;
    }

    private int userId;     // 用户id
    private int tagId;      // 该用户关注的Tag的id

    public int TagId 
    {
        get{return tagId; }
        set{tagId = value;} 
    }
    public int UserId {
        get { return userId; }
        set { userId = value; } 
    }
}