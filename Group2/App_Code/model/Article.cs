using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Article 的 model
/// </summary>
public class Article
{
    public Article() { }

    public Article(string title, string articleAbstract, string fileURL,
        DateTime time, int wordCount, int heat)
    {
        Title = title;
        Abstrct = articleAbstract;
        FileURL = fileURL;
        Time = time;
        WordCount = wordCount;
        Heat = heat;
    }
    public Article(int articleId, string title, string articleAbstract, string fileURL,
        DateTime time, int wordCount, int heat)
    {
        ArticleId = articleId;
        new Article(articleId, title, articleAbstract, fileURL, time, wordCount, heat);

    }

    private int articleId;  //文章的自增ID
    private string title;   //文章标题
    private string abstrct; //文章摘要（300字符长度）
    private string fileURL;  //文章在文件系统中的地址
    private DateTime time;  //文章被收录进数据库的时间
    private int wordCount;  //文章被parse以后的字数
    private int heat;       //文章推荐热度

    public int ArticleId
    {
        get { return articleId; }
        set { articleId = value; }
    }

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string Abstrct
    {
        get { return abstrct; }
        set { abstrct = value; }
    }

    public string FileURL
    {
        get { return fileURL; }
        set { fileURL = value; }
    }

    public DateTime Time
    {
        get { return time; }
        set { time = value; }
    }

    public int WordCount
    {
        get { return wordCount; }
        set { wordCount = value; }
    }

    public int Heat
    {
        get { return heat; }
        set { heat = value; }
    }

}