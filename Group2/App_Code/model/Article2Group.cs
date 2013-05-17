using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Article2Group 的 model
/// </summary>
public class Article2Group
{
    public Article2Group() { }
    public Article2Group(int articleId, int groupId)
    {
        ArticleId = articleId;
        GroupId = groupId;
    }

    private int articleId; //文章ID
    private int groupId;   //文章被分到的类

    public int ArticleId
    {
        get { return articleId; }
        set { articleId = value; }
    }

    public int GroupId
    {
        get { return groupId; }
        set { groupId = value; }
    }
}