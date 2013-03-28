using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///操作 ArticleManager 的底层函数类
/// </summary>
public class ArticleManager
{
	public ArticleManager()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    //根据传入参数的内容增加一条记录（仅使用传入参数中除articleId之外的其他字段）
    //注意：传入参数中的time、heat和wordCount字段均需要上层书写，本函数只管存储
    //成功返回新增记录的articleId，失败返回-1
    public static int addRecord(Article a)
    {
        return -1;
    }

    //返回数据库中相应的article记录（仅使用传入参数中的articleId字段）
    //返回一个Article的bean
    public static Article selectRecordByArticleId(Article a)
    {
        return null;
    }

    //返回数据库中相应的article记录（仅使用传入参数中的title字段）
    //返回一个Article的bean
    public static Article selectRecordByTitle(Article a)
    {
        return null;
    }

    //将传入参数中的articleId对应的数据库中的文章的wordCount字段值设为传入参数中的wordCount值（仅读取传入参数中的articleId和wordCount字段）
    //成功返回true，失败返回false
    public static bool updateWordCount(Article a)
    {
        return false;
    }

    //将传入参数中的articleId对应的数据库中的文章的fileURL字段值设为传入参数中的fileURL值（仅读取传入参数中的articleId和fileURL字段）
    //成功返回true，失败返回false
    public static bool updateFileURL(Article a)
    {
        return false;
    }

    //返回数据库中的文章总篇数。建议通过查询article表的条目数来确定。
    public static int countArticleNum()
    {
        return 0;
    }

    //获取该分类下的所有文章的article的列表（仅读取传入参数中的GroupId字段）
    //返回article的列表List<Article>
    public static List<Article> getArticleListByGroup(Groups g)
    {
        return null;
    }

    //根据传入的articleId数组，获取相应文章的article对象的列表
    //返回article的列表List<Article>
    public static List<Article> getArticleListByArticleIdList(List<int> articleIdList )
    {
        return null;
    }
}