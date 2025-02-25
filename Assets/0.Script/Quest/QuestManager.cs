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

    private PlayerData pd;
    public QuestManagerData data = new QuestManagerData();

    public List<int> enemyKillCnt;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
