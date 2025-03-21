using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy5 : Enemy
{//rue
    [SerializeField] NavMeshAgent agent;

    
    private Vector3 targetPos;
    private float dist;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SetData(5);
        agent.speed = data.Speed;
        enemyUI.SetUI(data.EnemyName, data.MAXHP, this);
    }

    protected override void EnemyMove()
    {
        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);
        agent.SetDestination(p.transform.position);
        base.EnemyMove();
    }

    protected override void TakeItem()
    {
        base.TakeItem();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
