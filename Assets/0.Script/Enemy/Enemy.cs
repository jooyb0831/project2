using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Enemy의 Data
    /// </summary>
    public class Data
    {
        public EnemyUI enemyUI;
        public BossEnemyUI bossUI;
        public int MAXHP { get; set; }

        private int hp;
        public int CURHP
        {
            get { return hp; }
            set
            {
                hp = value;
                if (enemyUI != null)
                {
                    enemyUI.HP = hp;
                }
                if (bossUI != null)
                {
                    bossUI.HP = hp;
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

    public EnemyUI enemyUI; //UI
    public BossEnemyUI bossUI;
    [SerializeField] protected FieldItem item; //드랍 아이템

    #region 컴포넌트 변수 
    protected Animator animator;
    protected Player p;
    protected PlayerData pd;
    protected SkillSystem skSystem;
    protected Pooling pooling;
    protected NavMeshAgent agent;
    protected Rigidbody rigid;

    protected JsonData jd;
    #endregion
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
        jd = GameManager.Instance.JsonData;
        pooling = GameManager.Instance.Pooling;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        state = State.Idle;
    }

    /// <summary>
    /// Json(EnemyData)의 데이터를 오브젝트에 세팅
    /// </summary>
    /// <param name="idx"></param>
    public virtual void SetData(int idx)
    {
        EnemyData enemyData = jd.enemyData.eData[idx];
        data.MAXHP = enemyData.maxHP;
        data.CURHP = data.MAXHP;
        data.EnemyName = enemyData.enemyName;
        data.Index = enemyData.index;
        data.Speed = enemyData.speed;
        data.AtkPower = enemyData.atkPower;
        data.EXP = enemyData.exp;
        if(enemyUI!=null)
        {
            data.enemyUI = enemyUI;
        }
        if(bossUI!=null)
        {
            data.bossUI = bossUI;
        }

    }

    void Update()
    {
        //게임이 일시 중지 상태이면 애니메이션 일시 정지하게끔
        if (GameManager.Instance.isPaused)
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
            if(agent!=null)
            {
                agent.SetDestination(transform.position);
            }

            if(GetComponent<CapsuleCollider>())
            {
                GetComponent<CapsuleCollider>().isTrigger = true;
            }
           
            return;
        }
        EnemyMove();
    }

    /// <summary>
    /// 적 이동 함수
    /// </summary>
    protected virtual void EnemyMove()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        //플레이어 펀치에 맞았을 경우
        if (other.CompareTag("Punch") && p.state.Equals(Player.State.Attack))
        {
            if(state.Equals(State.Hit)) return;
            TakeDamage(pd.BasicAtk);
        }

        //플레이어 무기에 맞았을 경우
        if (other.GetComponent<Weapon>())
        {
            if(state.Equals(State.Hit)) return;

            if (p.state.Equals(Player.State.Attack))
            {
                TakeDamage(other.GetComponent<Weapon>().weaponData.atkDmg);
            }

            else if (p.state.Equals(Player.State.Skill))
            {
                if (p.skillState.Equals(Player.SkillState.Qskill))
                {
                    TakeDamage(skSystem.qSkill.GetComponent<Skill>().data.Damage);
                }

            }
        }

        //플레이어 화살에 맞았을 경우
        Arrow arrow = other.GetComponent<Arrow>();
        if (arrow)
        {
            if(state.Equals(State.Hit)) return;
            pooling.SetPool(DicKey.arrow, arrow.gameObject);
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
        if(bossUI!=null)
        {
            bossUI.HPBarCheck();
        }
        if (data.CURHP <= 0)
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
        pd.EXP += data.EXP;
        Debug.Log(pd.EXP);
        state = State.Dead;
        animator.SetTrigger("Fall");

        if(enemyUI!=null)
        {
            enemyUI.DeadUI();
        }
        if(!rigid.useGravity)
        {
            rigid.useGravity = true;
        }

    }

    /// <summary>
    /// 아이템 드랍 
    /// </summary>
    protected virtual void TakeItem()
    {
        float dist = Vector3.Distance(p.transform.position, transform.position);

        if (dist < 2.5f)
        {
            //드랍 아이템 활성화하기
            if (Input.GetKeyDown(KeyCode.E))
            {
                p.GatherAnim(false);
                item.transform.SetParent(null);
                item.gameObject.SetActive(true);
                DestroyEnemy();
            }
        }
    }

    /// <summary>
    /// 적 제거하기
    /// </summary>
    protected virtual void DestroyEnemy()
    {
        Destroy(gameObject, 0.5f);
    }

    /// <summary>
    /// Idle 상태로 돌아가기
    /// </summary>
    public virtual void ToIdleState()
    {
        state = State.Idle;
    }
}
