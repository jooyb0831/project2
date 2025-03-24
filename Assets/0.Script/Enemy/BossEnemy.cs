using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : Enemy
{
    [SerializeField] Portal portal;
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
        if (state.Equals(State.Dead))
        {
            GetComponent<CapsuleCollider>().enabled = false;
            agent.SetDestination(transform.position);
            return;
        }

        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);

        dist = Vector3.Distance(p.transform.position, transform.position);


        //추적
        if (dist < 15f && dist > 10f)
        {
            state = State.Walk;
            animator.SetTrigger("Walk");
            agent.SetDestination(p.transform.position);
        }
        else if (dist <= 10f && dist > 7f)
        {

            agent.SetDestination(transform.position);
            Attack(1);

        }
        
        else if (dist <= 5)
        {
            agent.SetDestination(transform.position);
            Attack(2);
        }
        
        base.EnemyMove();
    }

    [SerializeField] Transform stoneArea;
    [SerializeField] float atkCoolTime;
    [SerializeField] float atkTimer;

    [SerializeField] float atk2CoolTime;
    [SerializeField] float atk2Timer;
    void Attack(int num)
    {
        if (num == 1)
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
                state = State.Idle;
                animator.SetTrigger("Idle");
            }
        }

        else if (num == 2)
        {
            atk2Timer += Time.deltaTime;
            if (atk2Timer >= atk2CoolTime)
            {
                atk2Timer = 0;
                state = State.Attack;
                animator.SetTrigger("Attack2");
            }
            else
            {
                animator.SetTrigger("Idle");
            }
        }
    }

    protected override void DestroyEnemy()
    {
        GameObject obj = Instantiate(portal, transform).gameObject;
        obj.transform.SetParent(null);
        base.DestroyEnemy();
    }


    public void ThrowRock()
    {
        EnemyRock rock = pooling.GetPool(DicKey.enemyRock, stoneArea).GetComponent<EnemyRock>();
        rock.ThrowRock(transform.forward);
        rock.transform.localScale = Vector3.one * 5f;
    }


}
