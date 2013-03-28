using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Global_Parse 的 bean 
/// </summary>
public class GlobalParse
{
    public GlobalParse() { }

    public GlobalParse( string wordContent , string type , int articleNumber )
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    private string wordContent;  //词语内容
    private string type;        //词性。以英文字符（串）表示
    private int articleNumber; //该词语出现在数据库多少篇不同的文章中

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

    public int ArticleNumber
    {
        get { return articleNumber; }
        set { articleNumber = value; }
    }
}