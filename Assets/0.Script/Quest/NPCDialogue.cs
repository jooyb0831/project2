using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    private QuestManager qmgr;
    private SceneChanger sc;
    public TMP_Text stringArea;//다이얼로그 표시하는 TMP
    int idx = 0; //텍스트 넘어가는 번호 체크용 변수
    public List<string> basicDialogue; //기본 다이얼로그 담는 리스트
    public List<string> yesDialogue; //'Yes'를 선택했을 때의 다이얼로그 리스트
    public List<string> noDialogue; //'NO'를 선택했을 때의 다이얼로그 리스트
    [SerializeField] List<string> currentDialogue; // 현재의 다이얼로그
    [SerializeField] GameObject answerWindow; //대답창
    [SerializeField] GameObject nextBtn; //버튼
    [SerializeField] bool isBasicDialogue = true; //기본 다이얼로그인지에 대한 체크 여부

    void Start()
    {
        qmgr = GameManager.Instance.QuestManager;
        sc = GameManager.Instance.SceneChanger;
    }
    public Quest curQuest = null;

    /// <summary>
    /// UI 다이얼로그 세팅
    /// </summary>
    /// <param name="dialogue"></param>
    public void SetCurDialogue(List<string> dialogue)
    {
        currentDialogue.Clear();
        for (int i = 0; i < dialogue.Count; i++)
        {
            currentDialogue.Add(dialogue[i]);
        }
        stringArea.text = currentDialogue[0];
    }

    /// <summary>
    /// Next버튼
    /// </summary>
    public void OnClickNextBtn()
    {
        idx++;

        if (idx < currentDialogue.Count)
        {
            stringArea.text = currentDialogue[idx];
        }

        else if (idx >= currentDialogue.Count)
        {
            //퀘스트 조건 달성 시
            if (curQuest.data.qState.Equals(QuestState.Done))
            {
                curQuest.QuestReward();
                curQuest.QuesUIRemove();
                gameObject.SetActive(false);
                curQuest = null;
                NPCUI.Instance.QuestReset();
                idx = 0;
                ExitScene();
                return;
            }

            //퀘스트 수락 중 조건 미달성 시
            else if (curQuest.data.qState.Equals(QuestState.Start))
            {
                ExitScene();
            }

            //모든 퀘스트 종료했을 경우
            else if (NPCUI.Instance.allDone)
            {
                ExitScene();
            }

            //퀘스트 미수락 상태
            else if (curQuest.data.qState.Equals(QuestState.None))
            {
                //기본 다이얼로그였다면
                if (isBasicDialogue)
                {
                    //수락 창 활성화
                    answerWindow.SetActive(true);

                    //버튼 사라짐
                    nextBtn.gameObject.SetActive(false);
                }

                //기본 다이얼로그가 아닌 상태 : 대화 종료
                else
                {
                    isBasicDialogue = true;
                    ExitScene();
                }
            }
        }
    }

    void ExitScene()
    {
        if (sc == null)
        {
            sc = GameManager.Instance.SceneChanger;
        }
        Cursor.visible = false;
        sc.GoNPC(false);
    }

    /// <summary>
    /// 다이얼로그 재생
    /// </summary>
    /// <param name="strs"></param>
    void DialoguePlayer(List<string> strs)
    {
        idx++;
        if (idx < strs.Count)
        {
            stringArea.text = strs[idx];
        }
        else if (idx == strs.Count)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Yes버튼
    /// </summary>
    public void OnYesClicked()
    {
        SetCurDialogue(yesDialogue);
        isBasicDialogue = false;
        idx = 0;
        NPCUI.Instance.onGoingQuest = curQuest;
        qmgr.AddQuest(curQuest);
        answerWindow.SetActive(false);
        nextBtn.SetActive(true);
    }

    /// <summary>
    /// No버튼
    /// </summary>
    public void OnNoClicked()
    {
        SetCurDialogue(noDialogue);
        isBasicDialogue = false;
        idx = 0;
        answerWindow.SetActive(false);
        nextBtn.SetActive(true);
    }

}
