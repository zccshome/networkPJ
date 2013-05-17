using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// FocusAndCreateTag 的摘要说明
/// </summary>
public class FocusAndCreateTag
{
	public FocusAndCreateTag()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /**
     * 输入：用户关注的所有tag（这里只有子分类，不会出现兴趣标签）的tagId列表
     * 输出：成功返回true，失败返回false
     * 功能：记录用户关注的子分类信息到数据库
     * 注意：进行数据库操作时，要先删除该用户的所有非兴趣标签类型的关注，再重新添加用户关注的子分类。
     *       对兴趣标签的增删都有专门的方法进行操作，这不是本方法的涉足范围，本方法不可以增删用户自己的兴趣标签，
     *       只能对用户关于公共分类（主分类和子分类）的关注链接进行操作！！！
     *       
     * 还有任何不理解的地方请果断问清楚。
     */
    public static bool saveFocus(List<int> tagIds)
    {
        return false ;
    }

    /**
     * 输入：新建兴趣标签所属的主分类，兴趣标签的名称，关键词列表（多个关键词之间用空格分隔），用户的userId
     * 输出：成功返回true，失败返回false
     * 功能：在指定的主分类下新建用户私有的兴趣标签
     * 注意：要对primaryGroupId的取值范围做检查
     */
    public static bool addTag(int primaryGroupId, string tagName, string tagKeys, int userId)
    {
        return false;
    }

    /**
     * 输入：一个User2Tag的model
     * 输出：成功返回true，失败返回false
     * 功能：删除指定的User2Tag记录
     */
    public static bool deleteTag( User2Tag u2t )
    {
        return false;
    }
}