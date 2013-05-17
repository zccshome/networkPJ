using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// GroupNode 的摘要说明
/// </summary>
public class GroupNode
{
	public GroupNode()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public GroupNode(string nodeName, int primaryGroupId, bool isPrimaryGroup, int id)
    {
        // 待实现！！！
    }

    private string nodeName;        //主/子分类名称
    private int primaryGroupId;     //隶属于的主分类的groupId。若本身就是主分类，则此处也填写自己的groupId
    private bool isPrimaryGroup;    //是否是主分类（1 - 是，0 - 不是）
    private int id;                //groupId（主分类）/tagId（子分类）
    private bool isInterestLabel;   //是否是兴趣标签（1 - 是，0 - 不是）

}