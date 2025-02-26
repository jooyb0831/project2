using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMenuUIPreset : Singleton<QuestMenuUIPreset>
{
    public Quest quest;
    public int questIdx;
    bool isAdd = false;
    public string questTitle;
    public int index;
    public int maxCnt;
    private int crCnt;
    public int exp;
    public int gold;
    public int objIndex;
    public string questExplain;
    public string questReward;

    [SerializeField] TMP_Text questTitleTxt;
    [SerializeField] TMP_Text questExplainTxt;
    [SerializeField] TMP_Text questRewardTxt;

    [SerializeField] Button btn;

    public int curCnt
    {
        get { return crCnt; }
        set
        {
            crCnt = value;
            questTitleTxt.text = $"{questTitle} ({curCnt}/{maxCnt})";
        }
    }
    public void SetData(QuestData data)
    {

        questTitle = data.QuestTitle;
        questIdx = data.QuestNumber;
        curCnt = data.CURCNT;
        maxCnt = data.MAXCNT;
        exp = data.EXP;
        gold = data.Gold;
        objIndex = data.ObjIndex;
        questTitleTxt.text = $"{questTitle} ({curCnt}/{maxCnt})";
        questExplain = data.QuestExplain;
        questReward = data.QuestRewardTxt;
        questExplainTxt.text = questExplain;
        questRewardTxt.text = questReward;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (quest != null && !isAdd)
        {
            //GameManager.Instance.questManager.qMUIList.Add(this);
            isAdd = true;
        }
    }
}
