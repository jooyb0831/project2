using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NPCUI : Singleton<NPCUI>
{
    public NPCDialogue window;
    public List<string> basicDialogue;
    public List<string> yesDialogue;
    public List<string> noDialogue;
    public List<string> questGoingDialogue;
    public List<string> questEndDialogue;
    public List<string> noQuestDialogue;
    public Quest curQuest = null;
    public Quest onGoingQuest = null;
    private QuestManager qm;
    public bool allDone = false;

    void Start()
    {
        qm = GameManager.Instance.QuestManager;
        SetQuest();
        Init();
    }
    void Init()
    {
        questGoingDialogue.Add("우선 일을 다 마치고 와.");
        questEndDialogue.Add("고맙다. 여기 약속한 보상이야.");
        noQuestDialogue.Add("아직은 부탁할 일이 없어.");

        if (qm.onGoingQuestLists.Count != 0 && !allDone)
        {
            onGoingQuest = qm.onGoingQuestLists[0];
        }
        

        if (curQuest == null)
        {
            allDone = true;
        }

        SetString();
    }

    public void SetQuest()
    {
        for (int i = 0; i < qm.questLists.Count; i++)
        {
            if (qm.questLists[i].data.qState != QuestState.End)
            {
                curQuest = qm.questLists[i];
                break;
            }
        }
        window.curQuest = curQuest;
       
    }

    public void SetString()
    {
        if (allDone)
        {
            window.SetCurDialogue(noQuestDialogue);
            return;
        }

        if (onGoingQuest == null && !allDone)
        {
            for (int i = 0; i < curQuest.data.basicDialogue.Length; i++)
            {
                basicDialogue.Add(curQuest.data.basicDialogue[i]);
                window.basicDialogue.Add(curQuest.data.basicDialogue[i]);
            }
            for (int i = 0; i < curQuest.data.yesDialogue.Length; i++)
            {
                yesDialogue.Add(curQuest.data.yesDialogue[i]);
                window.yesDialogue.Add(curQuest.data.yesDialogue[i]);
            }
            for (int i = 0; i < curQuest.data.noDialogue.Length; i++)
            {
                noDialogue.Add(curQuest.data.noDialogue[i]);
                window.noDialogue.Add(curQuest.data.noDialogue[i]);
            }
            window.SetCurDialogue(basicDialogue);
        }

        else if (onGoingQuest != null)
        {
            if (onGoingQuest.data.qState.Equals(QuestState.Done))
            {
                window.SetCurDialogue(questEndDialogue);
            }
            if(onGoingQuest.data.qState.Equals(QuestState.Start))
            {
                window.SetCurDialogue(questGoingDialogue);
            }
        }
    }

    public void QuestReset()
    {
        qm.onGoingQuestLists.Clear();
        onGoingQuest = null;
        SetQuest();
    }
}
