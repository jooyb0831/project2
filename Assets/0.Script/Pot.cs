using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Player player;
    private PlayerData pd;
    private Inventory inven;

    //POT의 안내 UI를 담을 GameObject
    [SerializeField] GameObject UI;

    //거리를 체크할 함수
    private float dist;

    void Start()
    {
        player = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        inven = GameManager.Instance.Inven;
    }


    void Update()
    {   
        //거리 측정
        dist = Vector3.Distance(transform.position, player.transform.position);

        //거리가 2미만일 경우
        if (dist < 2f)
        {   
            //안내 UI 활성화 및 방향 조정
            UI.SetActive(true);
            Vector3 pos = new Vector3(player.transform.position.x, UI.transform.position.y, player.transform.position.z);
            UI.transform.LookAt(pos);

            //F키를 누르면
            if(Input.GetKeyDown(KeyCode.F))
            {
                //CookingUI창 활성화
                CookingUI.Instance.EnableWindow();
            }
        }

        //거리가 2이상이면 UI비활성화
        else
        {
            UI.SetActive(false);
        }
    }
}
