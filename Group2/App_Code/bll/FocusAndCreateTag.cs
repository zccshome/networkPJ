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
     * 输入：用户关注的所有tag（这里只有子分类，不会出现兴趣标签）的tagId列表（包括所有的“其他”子分类），用户的id
     * 输出：成功返回true，失败返回false
     * 功能：记录用户关注的子分类信息到数据库
     * 注意：进行数据库操作时，要先删除该用户的所有非兴趣标签类型的关注，再重新添加用户关注的子分类。
     *       对兴趣标签的增删都有专门的方法进行操作，这不是本方法的涉足范围，本方法不可以增删用户自己的兴趣标签，
     *       只能对用户关于公共分类（主分类和子分类）的关注链接进行操作！！！
     *       
     * 还有任何不理解的地方请果断问清楚。
     */
    public static bool saveFocus(List<int> tagIds , int userId )
    {
        //拿到指定用户旧的关注列表（包括子分类和兴趣标签）
        User u = new User();
        u.UserId = userId;
        User u2 = UserManager.getUserById(u);
        List<int> oldTagList = User2TagManager.getTagListByUserId(u2);
        
        //根据列表信息，删除用户对子分类的全部关注，只留下对兴趣标签的关注
        if ( oldTagList != null )
            foreach (int tempTagId in oldTagList)
            {
                Tag t = new Tag();
                t.TagId = tempTagId;
                Tag t2 = TagManager.getTag(t);
                if ( t2.IsPrivate == 0 )
                {
                    // 删除用户关注
                    User2Tag u2t = new User2Tag(userId ,tempTagId );
                    User2TagManager.deleteRecord(u2t);
                }
            }
        
        //将用户的新的对子分类的关注列表存入User2Tag表中
        foreach (int tempNewTagId in tagIds)
        {
            User2Tag u2t = new User2Tag(userId,tempNewTagId);
            if (!User2TagManager.addRecord(u2t))
                continue;
        }
        return true ;
    }

    /**
     * 输入：新建兴趣标签所属的主分类，兴趣标签的名称，关键词列表（多个关键词之间用空格分隔），用户的userId
     * 输出：成功返回true，失败返回false
     * 功能：在指定的主分类下新建用户私有的兴趣标签
     * 注意：要对primaryGroupId的取值范围做检查
     */
    public static bool addTag(int primaryGroupId, string tagName, string tagKeys, int userId)
    {
        // 建立Tag
        Tag t = new Tag( -1 , tagName , tagKeys , primaryGroupId , DateTime.Now , 1 );
        int tagId = TagManager.addTag(t);
        if (tagId < 0)
            return false;
        // 建立用户到tag的链接
        User2Tag u2t = new User2Tag( userId , tagId );
        return User2TagManager.addRecord(u2t);
    }

    /**
     * 输入：一个User2Tag的model
     * 输出：成功返回true，失败返回false
     * 功能：删除指定的User2Tag记录
     */
    public static bool deleteTag( User2Tag u2t )
    {
        if (!User2TagManager.deleteRecord(u2t))
            return false;
        // 如果是兴趣标签，则除了删除User2Tag表中记录，还要删除Tag表中这个兴趣标签本身
        Tag t = new Tag();
        t.TagId = u2t.TagId;
        Tag t2 = TagManager.getTag(t);
        if ( t2.IsPrivate == 1 )
            return TagManager.deleteTag(t2);
        else
            return true;
    }
}