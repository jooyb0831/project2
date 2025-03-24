using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;


public class Enemy2 : Enemy
{
    [SerializeField] SlimeBall slimeBall;
    [SerializeField] Transform firePos;

    private SphereCollider coll;
    private Vector3 targetPos;
    float dist;

     void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        coll = GetComponent<SphereCollider>();
        SetData(2);
        agent.speed = data.Speed;
        enemyUI.SetUI(data.EnemyName, data.MAXHP, this);
    }


    protected override void EnemyMove()
    {
        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);

        dist = Vector3.Distance(p.transform.position, transform.position);

        if (dist < 15 && dist > 5f)
        {
            state = State.Walk;
            animator.SetTrigger("Walk");
            //transform.position = Vector3.MoveTowards(transform.position, p.transform.position, Time.deltaTime* data.Speed);
            agent.SetDestination(p.transform.position);

        }

        else if (dist <= 5f && dist > 3f)
        {
            agent.SetDestination(transform.position);
            Attack(1);
        }

        else if (dist<=3f)
        {
            agent.SetDestination(transform.position);
            Attack(2);
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
    
    bool isFire = false;
    void Attack(int number)
    {
        if (number == 1)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkCoolTime)
            {
                atkTimer = 0;
                state = State.Attack;
                SlimeBall ball = pooling.GetPool(DicKey.slimeBall, firePos).GetComponent<SlimeBall>();
                ball.Fire(transform.forward);
                ball.transform.SetParent(null);
                animator.SetTrigger("Attack");
            }
            else
            {
                animator.SetTrigger("Idle");
            }
        }
        else if (number == 2)
        {
            state = State.Attack;
            animator.SetTrigger("Attack2");
        }

    }


}
