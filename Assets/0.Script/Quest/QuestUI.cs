using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : Singleton<QuestUI>
{
    public Quest quest;
    bool isAdd = false;
    public TMP_Text questTitleTxt;
    public TMP_Text questExplainTxt;
    public string questTitle;
    public int questIdx;
    public int maxCnt;
    public int objIndex;
    public string questExplain;
    public string questReward;

    [SerializeField] int crCnt;
    public int curCnt
    {
        get { return crCnt; }
        set
        {
            crCnt = value;
            if (quest.data.qType != QuestType.Stage) //퀘스트 타입이 스테이지가 아닐때만
            {
                questTitleTxt.text = $"{questTitle}({crCnt}/{maxCnt})";
            }

        }
    }

    public void SetData(QuestData data)
    {
        questTitle = data.QuestTitle;
        questIdx = data.QuestNumber;
        curCnt = data.CURCNT;
        maxCnt = data.MAXCNT;
        objIndex = data.ObjIndex;
        QuestTxtShow(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (quest != null && isAdd)
        {
            isAdd = true;
        }
    }

    void QuestTxtShow(bool isDone)
    {
        if(!isDone)
        {
            questTitleTxt.text = quest.data.QuestTitle;
        }
        else
        {
            questTitleTxt.text = $"{quest.data.QuestTitle}(완료)";
        }
        
        questExplainTxt.text = quest.data.QuestExplain;
    }
}
