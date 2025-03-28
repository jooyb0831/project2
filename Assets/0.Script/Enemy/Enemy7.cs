using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7 : Enemy
{
    [SerializeField] SlimeBall slimeBall;
    [SerializeField] Transform firePos;

    private Vector3 targetPos;
    private float dist;


    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SetData(7);
        enemyUI.SetUI(data.EnemyName, data.MAXHP, this);
    }

    protected override void EnemyMove()
    {
        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);
        dist = Vector3.Distance(p.transform.position, transform.position);

        //거리가 10미만이 되면 공격
        if (dist < 10)
        {
            Attack();
        }
        else
        {
            state = State.Idle;
            animator.SetTrigger("Idle");
        }
        base.EnemyMove();
    }


    [SerializeField] float atkCoolTime;
    [SerializeField] float atkTimer;
    void Attack()
    {
        atkTimer += Time.deltaTime;
        if (atkTimer >= atkCoolTime)
        {
            SlimeBall ball = pooling.GetPool(DicKey.slimeBall, firePos).GetComponent<SlimeBall>();
            ball.Fire(transform.forward);
            ball.transform.SetParent(null);
            state = State.Attack;
            animator.SetTrigger("Attack");
            atkTimer = 0;
        }
        else
        {
            state = State.Idle;
            animator.SetTrigger("Idle");
        }

    }
}
