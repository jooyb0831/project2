using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest0 : Quest
{

    public override void Init()
    {
        base.Init();
        data.qType = QuestType.Stage;
        data.QuestTitle = "스테이지1 클리어";
        data.QuestNumber = 0;
        data.MAXCNT = 0;
        data.CURCNT = 0;
        data.EXP = 10;
        data.Gold = 15;
        data.QuestExplain = "스테이지1을 클리어하세요.";
        data.QuestRewardTxt = $"보상 : 골드 {data.Gold}, EXP {data.EXP}";

        data.basicDialogue = jsonData.questDialogueData.questDialogueData[0].questBasic;
        data.yesDialogue = jsonData.questDialogueData.questDialogueData[0].questY;
        data.noDialogue = jsonData.questDialogueData.questDialogueData[0].questN;

        questUI.quest = this;
        qMenuUI.quest = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void QuestAdd()
    {
        base.QuestAdd();
        QuestUI qUI = Instantiate(questUI, gi.questArea);
        QuestMenuUIPreset qMUI = Instantiate(qMenuUI, menuUI.questArea);

        qUI.SetData(this.data);
        qMUI.SetData(this.data);

        qMgr.questUILists.Add(qUI);
        qMgr.questMenuUILists.Add(qMUI);

        thisQuestUI = qUI;
        thisQuestMenuUI = qMUI;
    }

    public override void QuestReward()
    {
        base.QuestReward();
        pData.EXP += data.EXP;
        pData.Gold += data.Gold;
        data.qState = QuestState.End;
    }

    public override void QuesUIRemove()
    {
        base.QuesUIRemove();
        thisQuestUI.gameObject.SetActive(false);
        thisQuestMenuUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(pData.StageCleared[0])
        {
            data.qState = QuestState.Done;
        }
    }
}
