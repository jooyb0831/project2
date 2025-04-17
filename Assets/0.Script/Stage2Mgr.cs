using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Mgr : MonoBehaviour
{
    private PlayerData pd;
    [SerializeField] Enemy bossEnemy;
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
    }


    void Update()
    {
        if(bossEnemy == null)
        {
            pd.StageCleared[1] = true;
        }
    }
}
