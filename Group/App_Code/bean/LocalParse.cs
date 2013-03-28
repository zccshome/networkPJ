using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///LocalParse 的 bean
/// </summary>
public class LocalParse
{
    public LocalParse() { }

    public LocalParse( int articleId , string wordContent , string type , int count )
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    private int articleId;  //文章ID
    private string wordContent; //词语内容
    private string type;        //词性。以英文字符（串）表示      
    private int count;      //该词语在该文章中出现的次数

    public int ArticleId
    {
        get { return articleId; }
        set { articleId = value; }
    }

    public string WordContent
    {
        get { return wordContent; }
        set { wordContent = value; }
    }

    public string Type
    {
        get { return type; }
        set { type = value; }
    }

    public int Count
    {
        get { return count; }
        set { count = value; }
    }
}