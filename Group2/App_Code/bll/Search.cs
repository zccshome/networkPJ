using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Search 的摘要说明
/// </summary>
public class Search
{
	public Search()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /*
     * 输入：搜索关键词列表
     * 输出：Article的列表
     * 功能：在数据库所有文章中搜索标题和摘要中包含全部指定关键词的文章并返回
     */
    public static List<Article> searchAll( string[] key )
    {
        return ArticleManager.searchAll( key );
    }

    /*
     * 输入：搜索关键词列表，限定为搜索范围的主分类的id
     * 输出：Article的列表
     * 功能：在数据库所有属于指定主分类的文章中搜索标题和摘要中包含全部指定关键词的文章并返回
     */
    public static List<Article> searchInPrimaryGroup( string[] key , int primaryGroupId )
    {
        return ArticleManager.searchInPrimaryGroup( key , primaryGroupId ) ;
    }

    /*
     * 输入：搜索关键词列表
     * 输出：Article的列表
     * 功能：在数据库所有属于“其他”主分类的文章中搜索标题和摘要中包含全部指定关键词的文章并返回
     */
    public static List<Article> searchInOther( string[] key )
    {
        return ArticleManager.searchInOther( key );
    }
}