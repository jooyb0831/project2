using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerData
{
    public bool isQuest1Started = false;
}


public class QuestManager : MonoBehaviour
{
    
    public List<Quest> questLists;
    public List<Quest> onGoingQuestLists;
    public List<QuestUI> questUILists;
    public List<QuestMenuUIPreset> questMenuUILists;

    private PlayerData pd;
    public QuestManagerData data = new QuestManagerData();

    public List<int> enemyKillCnt;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;
    }

    QuestUI FindQUI(int idx)
    {
        QuestUI qUI = null;
        for(int i = 0; i<questUILists.Count; i++)
        {
            if(questUILists[i].questIdx == idx)
            {
                qUI = questUILists[i];
                break;
            }
        }
        return qUI;
    }

    /// <summary>
    /// Quest의 QuickUI찾아서 리턴하는 함수
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    QuestMenuUIPreset FindQMUI(int idx)
    {
        QuestMenuUIPreset qMUI = null;
        for(int i = 0; i<questMenuUILists.Count; i++)
        {
            if(questMenuUILists[i].questIdx == idx)
            {
                qMUI = questMenuUILists[i];
                break;
            }
        }
        return qMUI;
    }
    
    /// <summary>
    /// 퀘스트 추가(시작)
    /// </summary>
    /// <param name="quest"></param>
    public void AddQuest(Quest quest)
    {
        onGoingQuestLists.Add(quest);
        quest.data.qState = QuestState.Start;
        quest.QuestAdd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
