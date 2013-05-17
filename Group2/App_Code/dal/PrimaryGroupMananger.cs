using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
///操作 GroupMananger 的底层函数类
/// </summary>
public class PrimaryGroupMananger
{
    public PrimaryGroupMananger()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
        //System.Diagnostics.Debug.Write("create a group!");
    }

    //根据传入参数增加一条记录
    //成功返回新增记录的groupId，失败返回-1
    public static int addRecord(PrimaryGroups g)
    {
        return -1;
    }

    //根据传入参数中的groupId查询一条相应记录（仅读取传入参数中的groupId字段）
    //返回一个Group的model
    public static PrimaryGroups selectRecord(PrimaryGroups g)
    {
        return null;
    }

    //根据传入参数更新数据库中groupId为传入参数的groupId的相应记录
    //成功返回true，失败返回false
    public static bool updateRecord(PrimaryGroups g)
    {
        return false;
    }

     /*
      * 输入：无
      * 输出：所有组别的列表。格式为List<string[]>，每一个元素的string[]有两项，第一项是groupId，第二项是groupName
      * 功能：返回所有组别的列表，供管理员在不满意文章自动分类结果的情况下依据该列表进行手动分类
      * 说明：从功能上讲，其实仅返回groupName的List也是可以的，之所以还要返回groupId，是因为groupId是主键，而groupName理论上
      *       是可以重复的。
      */
    public static List<string[]> getAllGroups()
    {
        return null;
    }
}