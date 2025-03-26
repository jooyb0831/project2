using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy6 : Enemy
{
    private Vector3 targetPos;
    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Init()
    {
        base.Init();
        SetData(6);
        enemyUI.SetUI(data.EnemyName, data.MAXHP, this);
    }

    protected override void EnemyMove()
    {
        //Move Position
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
