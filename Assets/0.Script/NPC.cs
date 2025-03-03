using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
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
                npcCam.gameObject.SetActive(true);
                sc.GoNPC(true);
                //gameUI.UISwitch(true);
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                sc.GoNPC(false);
            }
        }
        else
        {
            textObj.SetActive(false);
        }

        if(!sc.sceneType.Equals(SceneType.NPC))
        {
            npcCam.gameObject.SetActive(false);
        }

    }

    
}
