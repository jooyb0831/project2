using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : Enemy
{
    [SerializeField] NavMeshAgent agent;
    private Vector3 targetPos;
    float dist;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SetData(3);
        agent.speed = data.Speed;
        enemyUI.SetUI(data.EnemyName, data.MAXHP, this);
    }

    protected override void EnemyMove()
    {
        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);

        dist = Vector3.Distance(p.transform.position, transform.position);

        if (dist < 10f && dist > 3f)
        {
            state = State.Walk;
            animator.SetTrigger("Walk");
            agent.SetDestination(p.transform.position);
        }

        else if (dist <= 3.0f)
        {
            agent.SetDestination(transform.position);
            Attack();
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
