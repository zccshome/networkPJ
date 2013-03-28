using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Groups 的 bean
/// </summary>
public class Groups
{
    public Groups() { }

    public Groups( int groupId , string groupName )
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    private int groupId;    //组类别的自增ID
    private string groupName;   //组名（eg.体育）

    public int GroupId
    {
        get { return groupId; }
        set { groupId = value; }
    }

    public string GroupName
    {
        get { return groupName; }
        set { groupName = value; }
    }
}