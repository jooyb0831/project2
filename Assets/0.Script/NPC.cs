using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] int npcIdx;
    private Player p;
    private GameUI gameUI;
    private SceneChanger sc;
    [SerializeField] GameObject textObj;
    public Camera npcCam;
    float dist;
    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        gameUI = GameManager.Instance.GameUI;
        sc = GameManager.Instance.SceneChanger;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, p.transform.position);
        
        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);

        if (dist < 2f)
        {
            textObj.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                CheckNPC(npcIdx);
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                //sc.GoNPC(false);
            }
        }
        else
        {
            textObj.SetActive(false);
        }

        if(!sc.sceneType.Equals(SceneType.NPC))
        {
            if(npcIdx==1)
            {
                npcCam.gameObject.SetActive(false);
            }

        }

    }

    void CheckNPC(int num)
    {
        if(num == 1)
        {
            npcCam.gameObject.SetActive(true);
            sc.GoNPC(true);
        }
        else if (num == 2)
        {
            //카메라 움직임, 캐릭터 움직임 정지
            GameManager.Instance.isPaused = true;
            Camera.main.GetComponent<CameraMove>().enabled = false;
            ShopUI.Instance.SetShopInven();
            ShopUI.Instance.window.SetActive(true);
        }
    }

    
}
