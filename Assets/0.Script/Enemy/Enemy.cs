using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        animator = GetComponent<Animator>();
        state = State.Idle;
        enemyUI.SetUI(data.EnemyName, data.MAXHP, this);

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

        Arrow arrow = other.GetComponent<Arrow>();
        if(arrow)
        {
            Pooling.Instance.SetPool(DicKey.arrow, arrow.gameObject);
            TakeDamage(arrow.Damage);
        }
    }
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
    
    
    void Dead()
    {
        state = State.Dead;
        animator.SetTrigger("Fall");
        //경험치 추가

        //아이템 드롭.
        enemyUI.DeadUI();
    }

    protected virtual void TakeItem()
    {
        float dist = Vector3.Distance(p.transform.position, transform.position);

        if(dist<2.5f)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(item, transform);
            }
        }
    }
}
