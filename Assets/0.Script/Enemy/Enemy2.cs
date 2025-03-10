using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy2 : Enemy
{
    [SerializeField] NavMeshAgent agent;

    private Vector3 targetPos;
    float dist;

     void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SetData(2);
        agent.speed = data.Speed;
        enemyUI.SetUI(data.EnemyName, data.MAXHP, this);
    }


    protected override void EnemyMove()
    {
        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);

        dist = Vector3.Distance(p.transform.position, transform.position);

        if (dist < 6 && dist > 1.5f)
        {
            state = State.Walk;
            animator.SetTrigger("Walk");
            //transform.position = Vector3.MoveTowards(transform.position, p.transform.position, Time.deltaTime* data.Speed);
            agent.SetDestination(p.transform.position);

        }

        else if (dist <= 1.5f)
        {
            agent.SetDestination(transform.position);
            Attack();
        }

        else
        {
            state = State.Idle;
            animator.SetTrigger("Idle");
            agent.SetDestination(transform.position);

        }
        base.EnemyMove();
    }

    protected override void TakeItem()
    {
        base.TakeItem();
    }

    [SerializeField] float atkCoolTime;
    [SerializeField] float atkTimer;
    [SerializeField] GameObject slimeBall;
    void Attack()
    {
        atkTimer += Time.deltaTime;
        if (atkTimer >= atkCoolTime)
        {
            atkTimer = 0;
            state = State.Attack;
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.SetTrigger("Idle");
        }

    }
}
