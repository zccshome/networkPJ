using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Class1 的摘要说明
/// </summary>
public class GroupNodeComparer : IEqualityComparer<GroupNode>
{
    public static bool equals(GroupNode miaowuNode, GroupNode wuwuNode)
    {
        if (wuwuNode.PrimaryGroupId == miaowuNode.PrimaryGroupId &&
            //wuwuNode.NodeName.Equals(miaowuNode.NodeName) &&
            wuwuNode.Id == miaowuNode.Id &&
            //wuwuNode.IsInterestLabel == miaowuNode.isInterestLabel &&
            wuwuNode.IsPrimaryGroup == miaowuNode.IsPrimaryGroup
            )
        {
            return true;
        }
        return false;
    }
    public bool Equals(GroupNode miaowuNode, GroupNode wuwuNode)
    {
        if (wuwuNode.PrimaryGroupId == miaowuNode.PrimaryGroupId &&
            //wuwuNode.NodeName.Equals(miaowuNode.NodeName) &&
            wuwuNode.Id == miaowuNode.Id &&
            //wuwuNode.IsInterestLabel == miaowuNode.isInterestLabel &&
            wuwuNode.IsPrimaryGroup == miaowuNode.IsPrimaryGroup
            )
        {
            return true;
        }
        return false;
    }
    public int GetHashCode(GroupNode p)
    {
        return p.GetHashCode();
    }
}