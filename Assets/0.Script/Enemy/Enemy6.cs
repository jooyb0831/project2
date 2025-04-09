using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy6 : Enemy
{
    private Vector3 targetPos;
    private float dist;

    [SerializeField] bool moveDirX;
    [SerializeField] float minPos;
    [SerializeField] float maxPos;

    [SerializeField] bool isTriggerd;
    private bool isBack;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SetData(6);
        enemyUI.SetUI(data.EnemyName, data.MAXHP, this);
    }

    protected override void EnemyMove()
    {
        if (state.Equals(State.Dead))
        {
            return;
        }
        dist = Vector3.Distance(p.transform.position, transform.position);
        if (!isTriggerd)
        {
            IdleMove();
        }

        //플레이어와의 거리가 7미만일 경우부터 접근
        if (dist < 7 && dist > 1.3f)
        {   
            transform.localScale = Vector3.one;
            isTriggerd = true;
            //플레이어쪽으로 이동
            state = State.Walk;
            targetPos = new Vector3(p.transform.position.x, p.transform.position.y + 1f, p.transform.position.z);
            transform.LookAt(targetPos);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, data.Speed * Time.deltaTime);
        }
        //거리가 5 미만일 경우 공격
        else if (dist <= 1.3f)
        {
            Attack();
        }
    }

    void IdleMove()
    {
        state = State.Idle;
        if (isBack)
        {
            transform.Translate(data.Speed * Time.deltaTime * Vector3.back);
            transform.localScale = new Vector3 (1,1,-1);
        }
        else
        {
            transform.Translate(data.Speed * Time.deltaTime * Vector3.forward);
            transform.localScale = Vector3.one;
        }
        DirectionCheck();
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

    void DirectionCheck()
    {
        float pos = moveDirX ? transform.position.x : transform.position.z;

        if (pos >= maxPos)
        {
            isBack = true;
        }
        else if (pos <= minPos)
        {
            isBack = false;
        }
    }
}
