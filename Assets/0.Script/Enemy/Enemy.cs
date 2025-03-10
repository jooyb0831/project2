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
                if (enemyUI != null)
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

    [SerializeField] public EnemyUI enemyUI; //UI
    [SerializeField] protected GameObject item; //드랍 아이템

    #region 컴포넌트 변수 
    protected Animator animator;
    protected Player p;
    protected PlayerData pd;
    protected SkillSystem skSystem;

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
        animator = GetComponent<Animator>();
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
        data.enemyUI = enemyUI;
      
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
        //플레이어 펀치에 맞았을 경우
        if (other.CompareTag("Punch") && p.state.Equals(Player.State.Attack))
        {
            TakeDamage(pd.BasicAtk);
        }

        //플레이어 무기에 맞았을 경우
        if (other.GetComponent<Weapon>())
        {
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
        enemyUI.DeadUI();
    }

    /// <summary>
    /// 아이템 드랍 함수_수정필요
    /// </summary>
    protected virtual void TakeItem()
    {
        float dist = Vector3.Distance(p.transform.position, transform.position);

        if (dist < 2.5f)
        {
            //아이템 수집하기
            if (Input.GetKeyDown(KeyCode.E))
            {
                ItemType type = item.GetComponent<FieldItem>().itemData.type;
                GameObject obj = null;
                switch(type)
                {
                    case ItemType.Ore:
                    {
                        obj = Pooling.Instance.GetPool(DicKey.stone, transform);
                        break;
                    }
                    case ItemType.Wood:
                    {
                        obj = Pooling.Instance.GetPool(DicKey.wood, transform);
                        break;
                    }
                    default :
                        obj = Instantiate(item, transform);
                        break;
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
