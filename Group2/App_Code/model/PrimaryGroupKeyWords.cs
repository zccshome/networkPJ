using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// PrimaryGroupKeyWords 的摘要说明
/// </summary>
public class PrimaryGroupKeyWords
{
	public PrimaryGroupKeyWords()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public PrimaryGroupKeyWords(int primaryGroupId , string keyWord)
    {
        PrimaryGroupId = primaryGroupId;
        KeyWord = keyWord;
    }

    private int primaryGroupId; //主分类id
    private string keyWord;     //某个关键词（同一个主分类可以有多个关键词）

    public int PrimaryGroupId
    {
        get { return primaryGroupId; }
        set { primaryGroupId = value; }
    }
    public string KeyWord
    {
        get { return keyWord; }
        set { keyWord = value; }
    }

}