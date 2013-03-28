using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Manager页面可能用到的后台函数
/// </summary>
public class ManagerAspxAssist
{
	public ManagerAspxAssist()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /*
     * 输入：新文章的标题和内容
     * 输出：成功返回得到的articleId，失败返回-1
     * 功能：本函数需要依次实现以下功能：
     *       1、新建一个article的bean实例，将title填进去
     *       2、截取content的前100~150字作为abstract，填进article实例中去
     *       3、将article实例的time字段设为当前时间，wordCount字段设为-1，heat字段设为0
     *       4、调用dal层函数在数据库Article表中新增一条记录，得到返回的articleId
     *       5、以articleId为文件名、title + content为文件内容将文章以txt格式存储于本地文件系统中（放到Articles目录下面）
     *       6、将文章存储的路径填写到数据库article表的fileURL字段中
     *       7、调用parseArticle函数，将该文章进行解析和自动分类
     *       8、返回得到的articleId。
     * 用途说明：管理员使用该函数向数据库中新增文章
     */
    public static int addArticleWrapper( string title, string content )
    {
        return -1;
    }

    /*
     * 输入：一个Article的bean实例以及其content字符串
     * 输出：执行结果标示符（暂定如下：0表示成功；-1~-5表示不同类型的失败）
     * 功能：
     *      1、对传入参数标示的文章内容进行分词、词频统计、TF·IDF计算等
     *      2、将文章总词数（article表的wordCount字段）更新到Article表中
     *      3、将分词结果更新到GlobalParse数据表中，注意参数中的articleNumber字段值要设置好：如果GlobalParse表中尚无该词记录，则
     *         articleNumber设为1；若GlobalParse表中已有该词记录，则读取其articleNumber并加一再存进去
     *      4、将分词结果更新到LocalParse数据表中，注意参数中的count字段值的设置要设置好：如果LocalParse表中尚无该词记录，则
     *         count设为1；若LocalParse表中已有该词记录，则读取其count并加一再存进去
     *      5、对文章进行分类，并将分类结果更新到Article2Group数据表中。若没有任何类别适合该文章，则默认应该将其分到groupId为0的类别中，该类别即“分组失败”类别或“其他”类别（视用户需求而定）
     *      6、返回执行结果。请实现者自己定义一下不同的返回值标示何种类型的失败
     *
     * 用途说明：该函数为私有函数，只被addArticleWrapper函数在将新文章添加到数据库后自动调用，用于解析新文章。
     * 
     */
    private static void parseArticle(Article a , string content )
    {

    }

    /*
     * 输入：一个Article的bean实例
     * 输出：该文章所属的所有类别的groupId的列表
     * 功能：查询指定文章所属的所有的组别的ID
     * 用途说明：管理员调用addArticleWrapper函数将一篇新文章插入数据库后，得到返回的articleId，
     *           便可用本函数查询该文章的自动分类结果（以组别ID的列表为形式）
     */
    public static List<int> getGroupIdsByArticleWrapper(Article a)
    {
        return null;
    }

    /*
     * 输入：组别ID的列表
     * 输出：对应的组别名称的列表
     * 功能：查询数据库，将传入的组别ID翻译成组别名称，并返回
     * 用途说明：管理员通过getGroupIdsByArticleWrapper函数得到了一系列组别的ID，再通过本函数将之翻译成为组别的名称。
     * 注：之所以要将getGroupIdsByArticleWrapper和getGroupNamesByGroupIds两个函数的功能分开，主要是考虑到降低代码耦合性，
     *     同时，即便将两个函数的功能合并到一起，也不会降低数据库的读操作开销。
     */
    public static List<String> getGroupNamesByGroupIds(List<int> groupIds)
    {
        return null;
    }

    /*
     * 输入：一个文章的Article实例，以及一个文章所属的groupId的列表
     * 输出：成功返回true，失败返回false
     * 功能：对指定文章进行分类，将其分到groupIds所包含的所有组别中去（即一篇文章可以被分到多个类别中）。
     * 用途说明：管理员新增一篇文章到数据库中去以后，系统会自动将文章分类，并将结果显示在页面上，管理员若对自动
     *           分类的结果不满意，可以手动调整，调整完毕后调用本函数修改文章的分类结果
     */
    public static bool changeGroupRelation(Article a, List<int> groupIds)
    {
        return false;
    }

    /*
     * 输入：待分词的文本字符串
     * 输出：以List<string[]>为格式的分词结果。其中List的每一项（即每一个string[]）是一个词，string[]的第一项是词的内容，第二项是标识该词词性
     *       的英文代码。关于词性的标注，网上和代码里都没有找到翻译表，目前我仅测试出其中几种较为常用的词性：（1）名词：n（2）动词：v（3）形容词：a
     * 功能：分词。
     * 
     * 用途说明：用法举例：若要解析“这是一张非常漂亮的桌子”，则可以使用如下代码段：
     * 
     *       List<string[]> output = stringParse("这是一张非常漂亮的桌子");
     *       for (int i = 0; i < output.Count; i++)
     *       {
     *          Response.Write(output.ElementAt(i)[0] + "&emsp;" + output.ElementAt(i)[1] + "<br />");
     *       }       
     *       
     * 分词技术说明：该分词解决方案使用的是“SharpICTCLAS分词系统 1.0”的研究成果和代码，其字典库为在“Group/App_Code/Data”目录下，
     *     其核心驱动在“Group/Bin”目录下，其调用入口在“Group/App_Code/parse”目录下。
     */
    private static List<string[]> stringParse(string content)
    {
        return Program.stringParse(content);
    }
}