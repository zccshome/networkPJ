using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

/// <summary>
/// Manager页面可能用到的后台函数
/// </summary>
public class ManagerAssist
{
    public static List<String> stop_list = new List<String>();

	public ManagerAssist()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /*
     * 输入：新文章的标题和内容
     * 输出：成功返回得到的articleId，失败返回-1
     * 功能：本函数需要依次实现以下功能：
     *       1、新建一个article的model实例，将title填进去
     *       2、截取content的前100~150字作为abstract，填进article实例中去
     *       3、将article实例的time字段设为当前时间，wordCount字段设为-1，heat字段设为0
     *       4、调用dal层函数在数据库Article表中新增一条记录，得到返回的articleId
     *       5、以articleId为文件名、title + content为文件内容将文章以txt格式存储于本地文件系统中（放到App_Data\Articles目录下面）
     *       6、将文章存储的路径填写到数据库article表的fileURL字段中
     *       7、调用parseArticle函数，将该文章进行解析和自动分类
     *       8、返回得到的articleId。
     * 用途说明：管理员使用该函数向数据库中新增文章
     */
    public static int addArticle( string title, string content )
    {
        Article a = new Article();
        if (title.Contains("\""))
            title = title.Replace("\"", "\\\"");
        if (title.Contains("'"))
            title = title.Replace("'", "\\'");
        a.Title = title;
        if (content.Length > 300)
            a.Abstrct = content.Substring(4, 300);
        else
            a.Abstrct = content.Substring(4, content.Length-4);
        a.FileURL = ""; // 这里 FileURF 先随便设一个，下面如果插入成功的话，再重新update
        a.Time = DateTime.Now;
        a.WordCount = 0;
        a.Heat = 0;

        int aid = ArticleManager.addRecord(a);
        if (aid > -1)
        {
            string dirPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "App_Data\\Articles";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath); // 如果文件夹 Article不存在则新建
            }
            a.ArticleId = aid;
            a.FileURL = "\\" + aid + ".txt";
            ArticleManager.updateFileURL(a);    // 重新设置 FileURL
            parseArticle(a, content);   // 这里面要从新设置一下 wordCount
            ArticleManager.updateWordCount(a);

            FileStream fs = new FileStream(dirPath + a.FileURL, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(content);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        return aid;
    }

    /*
     * 输入：一个Article的model实例以及其content字符串
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
        // 在这里先行数据库中读出 各个 primary group 的 关键词列表
        List<List<string>> allGroupKeywordList = new List<List<string>>();
        List<string[]> allGroup = PrimaryGroupMananger.getAllGroups();
        foreach (string[] ag in allGroup)
        {
            PrimaryGroups g = new PrimaryGroups();
            g.GroupId = Convert.ToInt32(ag[0]);
            g.GroupName = ag[1];
            allGroupKeywordList.Add(PrimaryGroupKeyWordsManager.getKeyWordsOfCertainPrimaryGroup(g));
        }


        List<String[]> parseList = stringParse(content);
        a.WordCount = parseList.Count;
        Dictionary<String, Int32> dic = new Dictionary<String, Int32>();
        int wordMount = 0;
        foreach (String[] tempString in parseList)
        {
            if (stop_list.Contains(tempString[0]))
                continue;
            if (!dic.ContainsKey(tempString[0]))
            {
                dic.Add(tempString[0], 1);
                wordMount++;
            }
            else
            {
                int tempInt = dic[tempString[0]];
                dic[tempString[0]] = tempInt + 1;
            }
        }
        int articleID = a.ArticleId;
        Dictionary<String, Double> tf_idf = new Dictionary<String, Double>();
        foreach (KeyValuePair<String, Int32> keyPair in dic)
        {
            GlobalParse tempGP = new GlobalParse();
            tempGP.ArticleNumber = 1; tempGP.WordContent = keyPair.Key; tempGP.Type = "q";
            if (GlobalParseManager.addRecord(tempGP) == false)
            {
                int num = GlobalParseManager.selectRecordByWordContent(tempGP).ArticleNumber + 1;
                tempGP.ArticleNumber = num;
                GlobalParseManager.updateRecord(tempGP);
            }
            tempGP = GlobalParseManager.selectRecordByWordContent(tempGP);
            LocalParse tempLP = new LocalParse();
            tempLP.ArticleId = articleID; tempLP.WordContent = tempGP.WordContent; tempLP.Count = keyPair.Value;
            tempLP.Type = "q";
            LocalParseManager.addRecord(tempLP);

            // Counting tf_idf
            int tf_fenzi = tempLP.Count; int tf_fenmu = wordMount;
            int total_document_number = ArticleManager.countArticleNum();
            int document_number_with_word = tempGP.ArticleNumber;
            double tf_idf_value = ((double)tf_fenzi) / tf_fenmu;// *Math.Log((total_document_number / document_number_with_word), Math.E);
            tf_idf.Add(keyPair.Key, tf_idf_value);
        }
        //tf_idf.OrderByDescending(s => s.Value);
        double[] answer = new double[allGroup.Count];
        for (int i = 0; i < answer.Length; i++)
            answer[i] = 0;
        //Console.Write(tf_idf.ElementAt(0).Key);
        //Console.WriteLine(other.Count);

        for (int i = 0; i < answer.Length; i++)
        {
            List<string> list = allGroupKeywordList[i];
            if (list == null)
                continue;
            foreach (String tempS in list)
            {
                if (tf_idf.ContainsKey(tempS))
                    answer[i] += tf_idf[tempS];
            }
        }
        Article2Group a2g = new Article2Group();
        a2g.ArticleId = a.ArticleId;
        a2g.GroupId = max(answer);
        Article2GroupManager.addRecord(a2g);
    }

    static int max(double[] a)
    {
        int index = 0, length = a.Length;
        double max = a[0];
        for (int i = 1; i < length; i++) 
            if (a[i] > max) { 
                max = a[i]; 
                index = i; 
            }
        return index;
    }

    /*
     * 输入：一个Article的model实例
     * 输出：该文章所属的所有类别的groupId的列表
     * 功能：查询指定文章所属的所有的组别的ID
     * 用途说明：管理员调用addArticleWrapper函数将一篇新文章插入数据库后，得到返回的articleId，
     *           便可用本函数查询该文章的自动分类结果（以组别ID的列表为形式）
     */
    public static List<int> getGroupIdsByArticleWrapper(Article a)
    {
        return Article2GroupManager.getGroupIdsByArticle(a);
    }

    /*
     * 输入：组别ID的列表
     * 输出：对应的组别名称的列表
     * 功能：查询数据库，将传入的组别ID翻译成组别名称，并返回
     * 用途说明：管理员通过getGroupIdsByArticleWrapper函数得到了一系列组别的ID，再通过本函数将之翻译成为组别的名称。
     * 注：之所以要将getGroupIdsByArticleWrapper和getGroupNamesByGroupIds两个函数的功能分开，主要是考虑到降低代码耦合性，
     *     同时，即便将两个函数的功能合并到一起，也不会降低数据库的读操作开销。
     */
    public static List<String> getPrimaryGroupNamesByPrimaryGroupIds(List<int> groupIds)
    {
        List<string> result = new List<string>();
        Console.Write(groupIds[0]);
        foreach (int id in groupIds)
        {
            PrimaryGroups g = new PrimaryGroups();
            g.GroupId = id;
            PrimaryGroups sg = PrimaryGroupMananger.selectRecord(g);
            if (sg != null)
                result.Add(g.GroupName);
        }

        return result;
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
        bool result = true;

        // 先删除旧的 groupIds
        List<int> old_gids = Article2GroupManager.getGroupIdsByArticle(a);  // 这里我认为这个传入的 Article 的 id 是已经赋值了的
        foreach (int old_gid in old_gids)
        {
            Article2GroupManager.deleteRecord(new Article2Group(a.ArticleId, old_gid));
        }
        // 再插入新的 groupIds
        foreach (int gid in groupIds)
        {
            if (Article2GroupManager.addRecord(new Article2Group(a.ArticleId, gid)) == false)
                result = false; // 只要有一条记录插入不成功，则返回 false
        }

        return result;
    }

    /**
     * 输入：新主分类的名称，以及相应的关键词列表。各个关键词之间用空格分隔。
     * 输出：返回所有未被分到任何主分类的文章列表
     * 功能：1、新增一个主分类
     *       2、在该主分类下增加一个“其他”子分类
     *       2、调用NewsAssist.cs中getArticleListOfOthers( int userId , GroupNode gn )方法，通过将第二个参数置为null，拿到所有未被分到任何主分类的文章列表；
     *       3、返回这些文章的列表，让管理员查看一下其中有没有可以直接被分到新的主分类去的文章。
     */
    public static List<Article> addPrimaryGroup( string name , string keywordsList )
    {
        // 功能：1、新增一个主分类
        PrimaryGroups pg = new PrimaryGroups();
        pg.GroupName = name;
        int gid = PrimaryGroupMananger.addRecord(pg);
        PrimaryGroupKeyWords pgk = new PrimaryGroupKeyWords();
        pgk.PrimaryGroupId = gid;
        pgk.KeyWord = keywordsList;
        bool addPGKsuccess = PrimaryGroupKeyWordsManager.addRecord(pgk);
        if (addPGKsuccess)
        {
            addSecondaryGroup(gid, "其他", "");
        }

        // 功能：2、调用Articlemanager.cs 中的方法,返回所有未分类的文章
        PrimaryGroups p = new PrimaryGroups();
        p.GroupId = 0;
        return ArticleManager.getArticleListByPrimaryGroup(p);
    }

    /**
     * 输入：新子分类隶属于的主分类id，新子分类的名称，相应的关键词列表
     * 输出：成功返回true，失败返回false
     * 功能：管理员新增一个子分类（tag 的 isPrivate 设为 0）
     * 注意： tag 的 tagTime 由本函数设置
     */
    public static bool addSecondaryGroup( int primaryGroupId , string name, string keywords )
    {
        Tag t = new Tag();
        t.TagName = name;
        t.TagKeys = keywords;
        t.GroupId = primaryGroupId;
        t.TagTime = DateTime.Now;
        t.IsPrivate = 0;
        int tid = TagManager.addTag(t);
        if (tid > 0)
            return true;
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
     * 分词技术说明：该分词解决方案使用的是“SharpICTCLAS分词系统 1.0”的研究成果和代码，其字典库为在“Group/App_Data/Data”目录下，
     *     其核心驱动在“Group/Bin”目录下，其调用入口在“Group/App_Code/parse”目录下。
     */
    private static List<string[]> stringParse(string content)
    {
        // 因为调用的分词的方法中，传入string 不能带有 \r\n之类的，因此需要先替换
        content = content.Replace("\r", "");
        content = content.Replace("\n", "");
        content = content.Replace("\t", "");

        return Program.stringParse(content);
    }
}