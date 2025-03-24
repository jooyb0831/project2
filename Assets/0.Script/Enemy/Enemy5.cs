using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy5 : Enemy
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform tirggerZone;

    [SerializeField] CapsuleCollider coll;

    public bool isAwaken = false;

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
        coll = GetComponentInChildren<CapsuleCollider>();
        coll.enabled = false;
    }

    protected override void EnemyMove()
    {
        if (!isAwaken)
        {
            return;
        }

        dist = Vector3.Distance(p.transform.position, transform.position);
        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);

        if (dist > 15)
        {
            state = State.Idle;
            animator.SetTrigger("Idle");
            agent.SetDestination(transform.position);
        }
        else if (dist <= 0.8f)
        {
            state = State.Attack;
            Attack();
            animator.SetTrigger("Idle");
            agent.SetDestination(transform.position);
        }
        else
        {
            state = State.Walk;
            agent.SetDestination(p.transform.position);
            animator.SetTrigger("Walk");
        }

        base.EnemyMove();
    }

    [SerializeField] float atkCoolTime;
    [SerializeField] float timer;
    void Attack()
    {
        timer += Time.deltaTime;
        if (timer >= atkCoolTime)
        {
            coll.enabled = true;
            Invoke(nameof(CollOff), 1f);
        }
    }

    void CollOff()
    {
        coll.enabled = false;
        timer = 0;
    }

    protected override void TakeItem()
    {
        base.TakeItem();
    }
}
