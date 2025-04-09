using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : Enemy
{
    public bool isTriggered;
    private Vector3 targetPos;
    private float dist;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SetData(1);
        agent.speed = data.Speed;
        enemyUI.SetUI(data.EnemyName, data.MAXHP, this);
    }
    

    protected override void EnemyMove()
    {
        //트리거되지 않으면 움직이지 않음
        if(!isTriggered) return;

        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);

        dist = Vector3.Distance(p.transform.position, transform.position);

        if (dist < 10 && dist > 1.5f)
        {
            state = State.Walk;
            animator.SetTrigger("Walk");
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
