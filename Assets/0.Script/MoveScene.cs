using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    enum GoType
    {
        Mine,
        Stage,
        Forest,
        Lobby

    }
    private Player p;
    private SceneChanger sc;
    [SerializeField] GoType goType;

    [SerializeField] GameObject txtObj;

    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        sc = GameManager.Instance.SceneChanger;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, p.transform.position);

        if(dist<5f)
        {
            txtObj.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                GoScene(goType);
            }
        }
        else
        {
            txtObj.SetActive(false);
        }
    }

    void GoScene(GoType type)
    {
        switch(type)
        {
            case GoType.Mine:
                sc.GoMine();
                break;
            
            case GoType.Stage:
                sc.GoStage1();
                break;
        }
    }
}
