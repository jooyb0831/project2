using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Enemy의 Data 클래스
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

        if (enemyUI != null)
        {
            data.enemyUI = enemyUI;
        }
        if (bossUI != null)
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
            TakeItem(); //아이템 수집

            //NavMeshAgent를 사용하여 agent가 null이 아닐 경우
            if (agent != null)
            {
                //움직임 중지
                agent.SetDestination(transform.position);
            }

            //CapsuleCollider가 있을 경우
            if (GetComponent<CapsuleCollider>())
            {
                //트리거 처리하여 물리적 충돌 안 받게끔.
                GetComponent<CapsuleCollider>().isTrigger = true;
            }

            return; // 리턴함으로써 움직이지 못함
        }

        EnemyMove(); //적 움직임
    }

    /// <summary>
    /// 적 이동 함수
    /// </summary>
    protected virtual void EnemyMove()
    {
        //내부 구현은 상속받는 각각의 Enemy 클래스에서 구현
    }

    /// <summary>
    /// 적의 피격 함수
    /// </summary>
    /// <param name="damage"></param>
    void TakeDamage(int damage)
    {
        data.CURHP -= damage; //체력 감소

        //bossUI가 있다면
        if (bossUI != null)
        {
            bossUI.HPBarCheck();
        }

        if (data.CURHP <= 0)
        {
            Dead();
            return;
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
        if(state == State.Attack)
        {
            animator.ResetTrigger("Attack");
        }
        
        //죽음 처리
        state = State.Dead;
        animator.SetTrigger("Fall");

        //enemyUI가 있다면
        if (enemyUI != null)
        {
            enemyUI.DeadUI(); //죽은 후 표기될 UI로 변경
        }

        //중력 작용 안 받았을 경우
        if (!rigid.useGravity)
        {
            //중력 영향 받게
            rigid.useGravity = true;
        }
        pd.EXP += data.EXP; //경험치 증가
    }


    private void OnTriggerEnter(Collider other)
    {
        //플레이어 펀치에 맞았을 경우 + 플레이어가 공격 상태일 경우
        if (other.CompareTag("Punch") && p.state.Equals(Player.State.Attack))
        {
            if (state.Equals(State.Hit)) return; //이미 피격 상태라면 리턴
            TakeDamage(pd.BasicAtk); //피격처리
        }

        //플레이어 무기에 맞았을 경우
        if (other.GetComponent<Weapon>())
        {
            if (state.Equals(State.Hit)) return; //이미 피격 상태라면 리턴

            //플레이어가 일반 공격 상태일 때
            if (p.state.Equals(Player.State.Attack)) 
            {
                //피격처리
                TakeDamage(other.GetComponent<Weapon>().weaponData.atkDmg);
            }

            //플레이어가 스킬 공격 상태일 때
            else if (p.state.Equals(Player.State.Skill))
            {   
                //Q스킬일 경우
                if (p.skillState.Equals(Player.SkillState.Qskill))
                {   
                    //피격 처리
                    TakeDamage(skSystem.qSkill.GetComponent<Skill>().data.skillData.damage);
                }
                //R스킬일 경우
                else if (p.skillState.Equals(Player.SkillState.Rskill))
                {   
                    //피격처리
                    TakeDamage(skSystem.rSkill.GetComponent<Skill>().data.skillData.damage);
                }
            }
        }

        //플레이어 화살에 맞았을 경우
        Arrow arrow = other.GetComponent<Arrow>();
        if (arrow)
        {
            if (state.Equals(State.Hit)) return; //이미 피격 상태라면 리턴
            pooling.SetPool(DicKey.arrow, arrow.gameObject); //화살을 Pool로 돌려주기
            TakeDamage(arrow.Damage); //피격처리
        }
    }




    /// <summary>
    /// 아이템 드랍 
    /// </summary>
    protected virtual void TakeItem()
    {
        float dist = Vector3.Distance(p.transform.position, transform.position);

        //특정 거리 미만이 되면
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

    public virtual void DeadState()
    {
        if(state != State.Dead)
        {
            state = State.Dead;
        }
    }
}
