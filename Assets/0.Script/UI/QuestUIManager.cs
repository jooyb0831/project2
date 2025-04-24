using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    private QuestManager qm;
    // Start is called before the first frame update
    void Start()
    {
        qm = GameManager.Instance.QuestManager;
        SetQuestUI();
    }

    void SetQuestUI()
    {
        if (qm.onGoingQuestLists.Count != 0)
        {
            qm.questUILists.Clear();
            qm.questMenuUILists.Clear();
            for (int i = 0; i < qm.onGoingQuestLists.Count; i++)
            {
                qm.onGoingQuestLists[i].QuestAdd();
            }
        }
        else
        {
            return;
        }
    }
}
