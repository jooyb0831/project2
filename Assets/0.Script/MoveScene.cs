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
        Lobby,
        Fishing

    }
    private Player p;
    private PlayerData pd;
    private SceneChanger sc;
    private GameUI gameUI;
    [SerializeField] GoType goType;

    [SerializeField] GameObject txtObj;

    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        sc = GameManager.Instance.SceneChanger;
        gameUI = GameManager.Instance.GameUI;
    }

    void Update()
    {
        dist = Vector3.Distance(transform.position, p.transform.position);

        if(dist < 3f)
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
                StageSelect.Instance.TurnOnMap();
                break;

            case GoType.Lobby:
                sc.GoLobby();
                break;

            case GoType.Forest:
                sc.GoForest();
                break;

            case GoType.Fishing:
                {
                    if (pd.ST < 5)
                    {
                        gameUI.DisplayInfo(0);
                        return;
                    }
                    pd.ST -= 5;
                    Cursor.visible = true;
                    sc.GoFishing();
                }
                break;

        }
    }
}
