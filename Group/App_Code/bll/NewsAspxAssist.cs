using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// News页面可能用到后台函数
/// </summary>
public class NewsAspxAssist
{
	public NewsAspxAssist()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /*
     * 输入：一个Groups的bean实例（仅使用其中的groupId字段）
     * 输出：Article的列表
     * 功能：读取传入参数中的groupId，获取数据库中该分类的全部文章的Article的bean对象列表并返回
     * 用途说明：当用户点击某个分类查看该分类下所有文章时，此函数返回所有文章的列表
     */
    public static List<Article> getArticleListWrapper(Groups g)
    {
        return null;
    }

    /*
     * 输入：一个Article的bean实例（仅使用其中的fileURL字段）
     * 输出：该文章的content的字符串（不含标题）
     * 功能：根据传入参数的fileURL字段，从文件系统中读取相应的文本文件内容，将content以字符串形式返回
     * 用途说明：当用户对文章列表中的某篇文章感兴趣时，点击文章列表，调用此函数，返回文章全文内容用于web显示
     */
    public static string getArticleContentByArticleBean(Article a)
    {
        return null;
    }
}