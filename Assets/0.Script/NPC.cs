using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    private Player p;
    private GameUI gameUI;
    private SceneChanger sc;

    //대화 안내말을 담을 게임오브젝트
    [SerializeField] GameObject textObj;

    //npc를 보여줄 카메라
    public Camera npcCam;

    //NPC의 인덱스 번호
    [SerializeField] int npcIdx;

    //거리를 체크할 변수 
    private float dist;

    //타겟 위치를 받을 변수
    private Vector3 targetPos;

    void Start()
    {
        p = GameManager.Instance.Player;
        gameUI = GameManager.Instance.GameUI;
        sc = GameManager.Instance.SceneChanger;
    }

    void Update()
    {
        //거리 입력받기
        dist = Vector3.Distance(transform.position, p.transform.position);

        //타겟 위치 지정 및 보는 방향 설정
        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);

        //거리가 2보다 가까우면
        if (dist < 2f)
        {
            //대화 안내창 Active
            textObj.SetActive(true);
            //E키를 누르면
            if (Input.GetKeyDown(KeyCode.E))
            {
                //NPC의 Index를 체크
                CheckNPC(npcIdx);
            }
        }
        //거리가 2보다 멀면
        else
        {
            //대화 안내창 비활성화
            textObj.SetActive(false);
        }

        //현재 씬이 NPC 씬이 아닐 경우
        if (!sc.sceneType.Equals(SceneType.NPC))
        {
            //NPC의 Index가 1이면
            if (npcIdx == 1)
            {
                //npc 카메라 비활성화
                npcCam.gameObject.SetActive(false);
            }
        }

    }

    /// <summary>
    /// NPC의 인덱스에 따라 씬을 호출하는 함수
    /// </summary>
    /// <param name="num">npc인덱스</param>
    void CheckNPC(int num)
    {
        //NPC Index가 1번일 경우 = 퀘스트 NPC
        if (num == 1)
        {
            //카메라 전환
            npcCam.gameObject.SetActive(true);
            Cursor.visible = true;
            
            //NPC 대화창 호출
            sc.GoNPC(true);
        }
        //NPC Index가 2번일 경우 = 상인 NPC
        else if (num == 2)
        {
            //기타 환경 움직임과 카메라 움직임 일시정지
            GameManager.Instance.PauseScene(true);
            
            //상점 인벤토리 세팅 및 상점 창 호출
            ShopUI.Instance.SetShopInven();
            ShopUI.Instance.window.SetActive(true);
        }

        //NPC Index가 3일 경우 = 강화 NPC
        else if (num == 3)
        {
            //기타 환경 움직임과 카메라 움직임 일시정지
            GameManager.Instance.PauseScene(true);
            EnchantUI.Instance.EnableWindow();
        }
    }
}
