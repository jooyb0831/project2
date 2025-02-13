using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Enemy�� Data
    /// </summary>
    public class Data
    {
        public int MAXHP { get; set; }

        private int hp;
        public int CURHP
        {
            get { return hp; }
            set
            {
                hp = value;
                EnemyUI.Instance.HP = hp;
            }
        }
        public string EnemyName { get; set; }
        public int Index { get; set; }
        public float Speed { get; set; }
        public int AtkPower { get; set; }
        public int EXP { get; set; }
    }

    /// <summary>
    /// Enemy State
    /// </summary>
    public enum State
    {
        Idle,
        Walk,
        Hit,
        Attack,
        Dead
    }

    [SerializeField] protected EnemyUI enemyUI;
    [SerializeField] protected GameObject item;

    protected Animator animator;
    protected Player p;
    protected PlayerData pd;
    protected SkillSystem skSystem;

    public Data data = new Data();

    [HideInInspector] public State state = State.Idle;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        skSystem = GameManager.Instance.SkillSystem;
        animator = GetComponent<Animator>();
        state = State.Idle;
        enemyUI.SetUI(data.EnemyName, data.MAXHP, this);

    }

    void Update()
    {
        //������ �Ͻ� ���� �����̸� �ִϸ��̼� �Ͻ� �����ϰԲ�
        if(GameManager.Instance.isPaused)
        {
            animator.speed = 0;
            return;
        }
        else
        {
            animator.speed = 1;
        }

        //Dead������ ��� ó���ϴ� �ڵ�
        if (state == State.Dead)
        {
            TakeItem();
            return;
        }
        EnemyMove();
    }

    protected virtual void EnemyMove()
    {

    }

    void Hit()
    {

    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p)
        {
            data.CURHP -= pd.BasicAtk;
            if (data.CURHP <= 0)
            {
                state = State.Dead;
                animator.SetTrigger("Fall");
            }
            else
            {
                state = State.Hit;
                animator.SetTrigger("Hit");
            }

            
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Punch") && p.state.Equals(Player.State.Attack))
        {
            TakeDamage(pd.BasicAtk);
        }

        if(other.GetComponent<Weapon>())
        {
            if(p.state.Equals(Player.State.Attack))
            {
                TakeDamage(other.GetComponent<Weapon>().weaponData.atkDmg);
            }

            else if(p.state.Equals(Player.State.Skill))
            {
                if(p.skillState.Equals(Player.SkillState.Qskill))
                {
                    TakeDamage(skSystem.qSkill.GetComponent<Skill>().data.Damage);
                }
                
            }
        }

        Arrow arrow = other.GetComponent<Arrow>();
        if(arrow)
        {
            Pooling.Instance.SetPool(DicKey.arrow, arrow.gameObject);
            TakeDamage(arrow.Damage);
        }
    }


    /// <summary>
    /// ���� �ǰ� �Լ�
    /// </summary>
    /// <param name="damage"></param>
    void TakeDamage(int damage)
    {
        data.CURHP -= damage;
        if(data.CURHP<=0)
        {
            Dead();
        }
        else
        {
            state = State.Hit;
            animator.SetTrigger("Hit");
        }
    }
    
    /// <summary>
    /// ��� ó�� �Լ�
    /// </summary>
    void Dead()
    {
        pd.EXP+=data.EXP;
        Debug.Log(pd.EXP);
        state = State.Dead;
        animator.SetTrigger("Fall");
        enemyUI.DeadUI();
    }

    /// <summary>
    /// ������ ��� �Լ�_�����ʿ�
    /// </summary>
    protected virtual void TakeItem()
    {
        float dist = Vector3.Distance(p.transform.position, transform.position);

        if(dist<2.5f)
        {
            //������ �����ϱ�
            if(Input.GetKeyDown(KeyCode.E))
            {
                GameObject obj = Pooling.Instance.GetPool(DicKey.stone, transform);
                obj.transform.SetParent(null);
                DestroyEnemy();
            }
        }
    }

    /// <summary>
    /// �����ϱ�
    /// </summary>
    void DestroyEnemy()
    {
        Destroy(gameObject, 0.5f);
    }
}
