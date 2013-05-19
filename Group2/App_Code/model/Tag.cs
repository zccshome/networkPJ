using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Tag 的摘要说明
/// Tag是指标签，包括兴趣标签和小分类，是指在我们的程序中需要动态去查找的分类
/// 
/// </summary>
public class Tag
{
    public Tag() { }
    public Tag(string tagName, string tagKeys, int groupId, DateTime tagTime, int isPrivate)
    {
        TagName = tagName;
        TagKeys = tagKeys;
        GroupId = groupId;
        TagTime = tagTime;
        IsPrivate = isPrivate;
    }
    public Tag(int tagId, string tagName, string tagKeys, int groupId, DateTime tagTime, int isPrivate)
    {
        TagId = tagId;
        TagName = tagName;
        TagKeys = tagKeys;
        GroupId = groupId;
        TagTime = tagTime;
        IsPrivate = isPrivate;
    }

    private int tagId;      //tag的id
    private string tagName; //tag的名字
    private string tagKeys; //tag的关键字，使用空格分开，总长度不超过33个汉字
    private int groupId;    //tag所属的大分类
    private DateTime tagTime;   //tag加上的时间
    private int isPrivate;  //tag是否是私有的，即是否是由某个用户指定的，0 表示 公共子分类， 1 表示 私有兴趣标签

    public int TagId
    {
        get { return tagId; }
        set { tagId = value; }
    }

    public string TagName
    {
        get { return tagName; }
        set { tagName = value; }
    }

    public string TagKeys
    {
        get { return tagKeys; }
        set { tagKeys = value; }
    }


    public int GroupId
    {
        get { return groupId; }
        set { groupId = value; }
    }


    public DateTime TagTime
    {
        get { return tagTime; }
        set { tagTime = value; }
    }


    public int IsPrivate
    {
        get { return isPrivate; }
        set { isPrivate = value; }
    
    }

    
}