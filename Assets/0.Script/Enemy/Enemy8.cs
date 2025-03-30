using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8 : Enemy
{
    [SerializeField] Portal portal;
    [SerializeField] GameObject deadUI;
    [SerializeField] CapsuleCollider fireColl;
    [SerializeField] bool isFireAtkReady;

    private bool isTriggered;
    private Vector3 targetPos;
    private float dist;

    private SceneChanger sceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SetData(8);
        //agent.speed = data.Speed;
        //coll = GetComponentInChildren<CapsuleCollider>();
        //coll.enabled = false;
    }

    public void TriggerBoss()
    {
        isTriggered = true;
        BossEnemyUI.Instance.SetUI(this);
    }

    protected override void EnemyMove()
    {

        if (!isTriggered)
        {
            return;
        }

        if (state.Equals(State.Dead))
        {
            if (!deadUI.activeSelf)
            {
                deadUI.SetActive(true);
            }
            bossUI.TurnOffUI();
            return;
        }

        if (!isFireAtkReady)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkCoolTime)
            {
                atkTimer = 0;
                isFireAtkReady = true;
            }
        }

        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);


        dist = Vector3.Distance(p.transform.position, transform.position);

        if (dist < 15f && dist > 5f)
        {
            state = State.Walk;
            transform.position = Vector3.MoveTowards(transform.position, p.transform.position, Time.deltaTime * data.Speed);
            animator.SetTrigger("Walk");
        }

        else if (dist <= 5f)
        {
            if (isFireAtkReady)
            {

                Attack(1);
            }
            else
            {
                if(dist<=3f)
                {
                    Attack(2);
                }
                else if(dist>3f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, p.transform.position, Time.deltaTime * data.Speed);
                }
            }
        }
        base.EnemyMove();
    }

    [SerializeField] Transform firePos;
    [SerializeField] float atkCoolTime;
    [SerializeField] float atkTimer;
    [SerializeField] float atk2CoolTime;
    [SerializeField] float atk2Timer;
    void Attack(int atkNum)
    {
        if(atkNum == 1)
        {

            animator.SetTrigger("Attack");
        }

        else if(atkNum == 2)
        {
            atk2Timer += Time.deltaTime;
            if (atk2Timer >= atk2CoolTime)
            {
                atk2Timer = 0;
                state = State.Attack;
                animator.SetTrigger("Attack2");
            }
        }
    }

    [SerializeField] ParticleSystem fire;
    public void Fire()
    {
        fire.Play();
        fireColl.enabled = true;
    }

    public void FireEnd()
    {
        fireColl.enabled = false;
        isFireAtkReady = false;
    }

    protected override void DestroyEnemy()
    {
        GameObject obj = Instantiate(portal, transform).gameObject;
        obj.transform.SetParent(null);
        base.DestroyEnemy();
    }
}
