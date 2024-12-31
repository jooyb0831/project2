using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy0 : Enemy
{
    [SerializeField] NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        
        data.MAXHP = JsonData.Instance.enemyData.eData[0].maxHP;
        data.CURHP = data.MAXHP;
        data.EnemyName = JsonData.Instance.enemyData.eData[0].enemyName;
        data.Index = JsonData.Instance.enemyData.eData[0].index;
        data.Speed = JsonData.Instance.enemyData.eData[0].speed;
        data.AtkPower = JsonData.Instance.enemyData.eData[0].atkPower;
        data.EXP = JsonData.Instance.enemyData.eData[0].exp;
        agent.speed = data.Speed;
        base.Init();
    }
    // Update is called once per frame
    void Update()
    {
        if(state == State.Dead)
        {
            TakeItem();
            return;
        }
        Move();
    }

    void Move()
    {

        float dist = Vector3.Distance(p.transform.position, transform.position);

        if (dist < 10 && dist >3)
        {
            state = State.Walk;
            animator.SetTrigger("Walk");
            agent.SetDestination(p.transform.position);
            
        }

        else if (dist<=3)
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
        if(atkTimer>=atkCoolTime)
        {
            atkTimer = 0;
            state = State.Attack;
            animator.SetTrigger("Attack");
        }
        else
        {
            state = State.Idle;
            animator.SetTrigger("Idle");
        }
       
    }    

}
