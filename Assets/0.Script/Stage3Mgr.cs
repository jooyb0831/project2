using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Mgr : MonoBehaviour
{
    private PlayerData pd;
    [SerializeField] Enemy bossEnemy;
    [SerializeField] GameObject[] deadZone;
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
    }


    void Update()
    {
        if(bossEnemy == null)
        {
            pd.StageCleared[2] = true;
            foreach(var item in deadZone)
            {
                item.SetActive(false);
            }
        }
    }
}
