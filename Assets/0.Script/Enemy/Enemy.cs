using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Enemy의 Data
    /// </summary>
    public class Data
    {
        public EnemyUI enemyUI;
        public int MAXHP { get; set; }

        private int hp;
        public int CURHP
        {
            get { return hp; }
            set
            {
                hp = value;
                if(enemyUI!=null)
                {
                    enemyUI.HP = hp;
                }
                
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

    [SerializeField] public EnemyUI enemyUI;
    [SerializeField] protected GameObject item;

    protected Animator animator;
    protected Player p;
    protected PlayerData pd;
    protected SkillSystem skSystem;

    public Data data = new Data();

    public State state = State.Idle;

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
    }

    void Update()
    {
        //게임이 일시 중지 상태이면 애니메이션 일시 정지하게끔
        if(GameManager.Instance.isPaused)
        {
            animator.speed = 0;
            return;
        }
        else
        {
            animator.speed = 1;
        }

        //Dead상태일 경우 처리하는 코드
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
    /// 적의 피격 함수
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
    /// 사망 처리 함수
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
    /// 아이템 드랍 함수_수정필요
    /// </summary>
    protected virtual void TakeItem()
    {
        float dist = Vector3.Distance(p.transform.position, transform.position);

        if(dist<2.5f)
        {
            //아이템 수집하기
            if(Input.GetKeyDown(KeyCode.E))
            {
                GameObject obj = null;
                if(item.GetComponent<FieldItem>().itemData.type.Equals(ItemType.Ore))
                {
                    obj = Pooling.Instance.GetPool(DicKey.stone, transform);
                }
                else
                {
                    obj = Instantiate(item, transform);
                }
                obj.transform.SetParent(null);
                DestroyEnemy();
            }
        }
    }

    /// <summary>
    /// 제거하기
    /// </summary>
    void DestroyEnemy()
    {
        Destroy(gameObject, 0.5f);
    }
}
