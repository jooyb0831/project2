using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class Enemy0 : Enemy
{
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
        SetData(0);
        agent.speed = data.Speed;
        enemyUI.SetUI(data.EnemyName, data.MAXHP, this);
    }


    protected override void EnemyMove()
    {
        //공격중이거나 피격 상태이면 움직이지 못하게 리턴
        if(state.Equals(State.Attack) || state.Equals(State.Hit)) return;

        //이동할 포지션 설정 및 해당 위치로 바라보게 설정
        targetPos = new Vector3(p.transform.position.x, 
                                transform.position.y,
                                p.transform.position.z);
        transform.LookAt(targetPos);

        dist = Vector3.Distance(p.transform.position, transform.position);

        //거리가 10과 1.5사이일 경우 
        if (dist < 10 && dist > 1.5f)
        {
            //플레이어쪽으로 다가오게 설정
            state = State.Walk;
            animator.SetTrigger("Walk");
            agent.SetDestination(p.transform.position);

        }

        //거리가 1.5이하
        else if (dist <= 1.5f)
        {   
            //움직임 중지하고 공격
            agent.SetDestination(transform.position);
            Attack();
        }

        //거리가 멀 경우 대기 상태
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
        //공격 시간 계산산
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

    public override void ToIdleState()
    {
        base.ToIdleState();
    }

}
