using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public QuestType qType;
    public QuestState qState = QuestState.None;
    public string QuestTitle;
    public int QuestNumber; //퀘스트 넘버
    public int MAXCNT;
    public int CURCNT;
    public int EXP; //퀘스트 보상 경험치
    public int Gold; //퀘스트 보상 골드
    public GameObject rewardItem = null; //퀘스트 보상 아이템
    public int ObjIndex; //퀘스트 대상 오브젝트의 고유코드번호(수집, 몬스터)
    public string QuestExplain;
    public string QuestRewardTxt;

    //퀘스트 대사 다이얼로그 모음
    public string[] basicDialogue;
    public string[] yesDialogue;
    public string[] noDialogue;
}

/// <summary>
/// 퀘스트의 진행상황
/// </summary>
public enum QuestState
{
    None, //시작하지 않음
    Start, //시작함(퀘스트 수주)
    Done, //완료함(조건충족)
    End //종료됨(보상수령->완전종료)
}
public enum QuestType
{
    Kill,
    Stage,
    Gather
}

public class Quest : MonoBehaviour
{
    public QuestData data = new QuestData();
    public QuestUI questUI;
    public QuestMenuUIPreset qMenuUI;
    public QuestUI thisQuestUI;
    public QuestMenuUIPreset thisQuestMenuUI;

    protected GameUI gi;
    protected MenuUI menuUI;
    protected PlayerData pData;
    protected QuestManager qMgr;
    protected JsonData jsonData;


    public string qTitle;
    public int curCnt;
    public int maxCnt;

    public virtual void Init()
    {
        if(pData == null)
        {
            pData = GameManager.Instance.PlayerData;
        }
        if (gi == null)
        {
            gi = GameManager.Instance.GameUI;
        }
        if (menuUI == null)
        {
            menuUI = GameManager.Instance.MenuUI;
        }
        if(jsonData == null)
        {
            jsonData = GameManager.Instance.JsonData;
        }
        if(qMgr==null)
        {
            qMgr = GameManager.Instance.QuestManager;
        }

        data.CURCNT = 0;
    }

    /// <summary>
    /// QuestAdd
    /// </summary>
    public virtual void QuestAdd()
    {
        if (gi == null)
        {
            gi = GameManager.Instance.GameUI;
        }
        if (menuUI == null)
        {
            menuUI = GameManager.Instance.MenuUI;
        }
    }

    /// <summary>
    /// Quest RewardSet
    /// </summary>
    public virtual void QuestReward()
    {
        if (pData == null)
        {
            pData = GameManager.Instance.PlayerData;
        }
    }

    /// <summary>
    /// QuestÁ¦°Å
    /// </summary>
    public virtual void QuesUIRemove()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
