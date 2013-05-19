using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// GroupNode 的摘要说明
/// </summary>
public class GroupNode:System.IComparable<GroupNode>
{
	public GroupNode()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public GroupNode(string nodeName, int primaryGroupId, bool isPrimaryGroup, int id)
    {
        NodeName = nodeName;
        PrimaryGroupId = primaryGroupId;
        IsPrimaryGroup = isPrimaryGroup;
        Id = id;
    }
    public GroupNode(string nodeName, int primaryGroupId, bool isPrimaryGroup, int id, bool isInterestLabel)
    {
        IsInterestLabel = isInterestLabel;
        NodeName = nodeName;
        PrimaryGroupId = primaryGroupId;
        IsPrimaryGroup = isPrimaryGroup;
        Id = id;
        //new GroupNode(nodeName, primaryGroupId, isPrimaryGroup, id);
    }


    private string nodeName;        //主/子分类名称
    private int primaryGroupId;     //隶属于的主分类的groupId。若本身就是主分类，则此处也填写自己的groupId
    private bool isPrimaryGroup;    //是否是主分类（1 - 是，0 - 不是）
    private int id;                //groupId（主分类）/tagId（子分类）
    private bool isInterestLabel;   //是否是兴趣标签（1 - 是，0 - 不是）


    public string NodeName 
    {
        get { return nodeName; }
        set { nodeName = value; }
    }
    public int PrimaryGroupId
    {
        get { return primaryGroupId; }
        set { primaryGroupId = value; }
    }
    public bool IsPrimaryGroup
    {
        get { return isPrimaryGroup; }
        set { isPrimaryGroup = value; }
    }
    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public bool IsInterestLabel
    {
        get { return isInterestLabel; }
        set { isInterestLabel = value; }
    }
    public int CompareTo(GroupNode miaowuNode)
    {
        if (miaowuNode.IsPrimaryGroup == true && miaowuNode.NodeName.Equals("其他"))
            return -1;
        else if (this.IsPrimaryGroup == true && this.NodeName.Equals("其他"))
            return 1;
        else if(miaowuNode.Id == this.Id && miaowuNode.IsPrimaryGroup == this.IsPrimaryGroup && miaowuNode.PrimaryGroupId == this.PrimaryGroupId)
            return 0;
        else if (this.PrimaryGroupId < miaowuNode.PrimaryGroupId)
            return -1;
        else if (this.PrimaryGroupId > miaowuNode.PrimaryGroupId)
            return 1;
        else if (this.PrimaryGroupId == miaowuNode.PrimaryGroupId && this.isPrimaryGroup == true)
            return -1;
        else if (this.PrimaryGroupId == miaowuNode.PrimaryGroupId && miaowuNode.isPrimaryGroup == true)
            return 1;
        else if (this.PrimaryGroupId == miaowuNode.PrimaryGroupId && this.isPrimaryGroup == false && this.NodeName.Equals("其他"))
            return 1;
        else if (this.PrimaryGroupId == miaowuNode.PrimaryGroupId && miaowuNode.isPrimaryGroup == false && miaowuNode.NodeName.Equals("其他"))
            return -1;
        else if (this.PrimaryGroupId == miaowuNode.PrimaryGroupId && this.Id < miaowuNode.id)
            return -1;
        else if (this.PrimaryGroupId == miaowuNode.PrimaryGroupId && this.Id > miaowuNode.id)
            return 1;
        return 0;
    }
}