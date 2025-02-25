using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : Singleton<QuestUI>
{
    public Quest quest;
    bool isAdd = false;
    public TMP_Text questTxt;
    public string questTitle;
    public int index;
    public int maxCnt;
    public int objIndex;
    public string questExplainTxt;
    public string questRewardTxt;

    [SerializeField] int crCnt;
    public int curCnt
    {
        get { return crCnt; }
        set
        {
            crCnt = value;
            questTxt.text = $"{questTitle} ({crCnt}/{maxCnt})";
        }
    }

    public void SetData (QuestData data)
    {
        questTitle = data.QuestTitle;
        index = data.QuestNumber;
        curCnt = data.curCount;
        maxCnt = data.maxCount;
        objIndex = data.objIndex;
        questTxt.text = $"{questTitle} ({curCnt}/{maxCnt})";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(quest!=null && isAdd)
        {
            isAdd = true;
        }
    }
}
