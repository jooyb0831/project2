using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk,
        Run,
        Jump,
        AttackIdle,
        Attack,
        Hit,
        Bow,
        Gather,
        Dead
    }

    private Rigidbody rigid;
    private Animator animator;
    private PlayerData pd;
    public State state = State.Idle;

    private float speed;
    [SerializeField] Transform foot;
    // Start is called before the first frame update
    void Start()
    {

        pd = GameManager.Instance.PlayerData;
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("Idle");
    }

    [SerializeField] float timer = 1f;
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(foot.position, Vector3.down * 0.1f, Color.red);
        RecoverSP();
        Attack();
        StateCheck();
        Move();
        Bow();


        if(Input.GetKeyDown(KeyCode.E))
        {
            state = State.Gather;
            animator.SetTrigger("Gather");
        }

        
        if(state == State.Run)
        {
            timer -= Time.deltaTime;
            if(timer<=0)
            {
                pd.SP -= pd.minSP;
                timer = 1;
            }
        }



        
    }

    void Move()
    {
        if(state == State.Gather || state == State.Dead)
        {
            return;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * speed);

        if (state == State.Jump)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(foot.position, Vector3.down * 0.1f, out hit))
        {
            if (!hit.collider.CompareTag("Ground"))
            {
                state = State.Jump;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (state == State.Jump)
            {
                return;
            }
            else
            {
                Jump();
                state = State.Jump;
                return;
            }
                
        }

        if (x != 0 || z != 0)
        {

            if(Input.GetKey(KeyCode.LeftShift))
            {
               if(pd.SP<=0)
                {
                    speed = pd.Speed;
                    state = State.Walk;

                    if(x<0)
                    {
                        animator.SetTrigger("WalkBackward");
                    }
                    else
                    {
                        animator.SetTrigger("WalkForward");
                    }
                    
                    return;
                }
                GameUI.Instance.spUI.SetActive(true);
                speed = pd.RunSpeed;
                state = State.Run;
                animator.SetTrigger("RunForward");
            }
            else
            {
                if(x<0)
                {
                    animator.SetTrigger("WalkBackward");
                }
                else
                {
                    animator.SetTrigger("WalkForward");
                }
                
                state = State.Walk;
                speed = pd.Speed;
            }
        }
        else
        {
            if(state == State.Hit || state == State.AttackIdle || state == State.Attack)
            {
                return;
            }
            animator.SetTrigger("Idle");
            state = State.Idle;
        }
        
    }

    [SerializeField] bool isComboExist;
    [SerializeField] bool isComboEnable;
    [SerializeField] int comboIndex;
    [SerializeField] bool isAttacking;
    

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(state == State.Bow)
            {
                return;
            }
            if (state != State.Attack)
            {
                state = State.Attack;
                animator.SetBool("Attacking", true);
            }
            else
            {
                AttackCombo();
            }
            
        }
    }

    [SerializeField] float atkIdleTimer;
    void StateCheck()
    {
        if (state == State.AttackIdle)
        {
            atkIdleTimer += Time.deltaTime;

            if (atkIdleTimer >= 2f)
            {
                atkIdleTimer = 0;
                animator.SetTrigger("Idle");
                state = State.Idle;
            }
        }
        else
        {
            atkIdleTimer = 0;
        }

    }

    [SerializeField] Transform pos;
    [SerializeField] bool isCharging = false;
    [SerializeField] Arrow ar;
    [SerializeField] Transform arrows;
    void Bow()
    {
        
        if (Input.GetMouseButton(1))
        {
            isCharging = true;
            animator.SetTrigger("Bow");
            state = State.Bow;

            if(Input.GetMouseButtonDown(0))
            {
                //화살 생성
                ar = Pooling.Instance.GetPool(DicKey.arrow, Camera.main.transform).GetComponent<Arrow>();
            }

            if (Input.GetMouseButton(0))
            {
                //화살 차징
                ar.ArrowCharge();
            }

            if (Input.GetMouseButtonUp(0))
            {
                //화살 발사
                ar.gameObject.layer = default;
                isCharging = false;
                animator.speed = 1;
                ar.Fire();
                ar.transform.SetParent(arrows);
                ar = null;
            }
        }
        if(Input.GetMouseButtonUp(1))
        {
            if(isCharging)
            {
                isCharging = false;
            }
            if(animator.speed!=1)
            {
                animator.speed = 1;
            }

            animator.SetTrigger("Idle");
            state = State.Idle;
        }
    }

    
    public void BowAnimationStop()
    {
        if (isCharging)
        {
            animator.speed = 0;
        }

    }

    void AttackCombo()
    {

        // 콤보가 가능한 상태에서 E키를 눌렀을 때
        if (isComboEnable)
        {
            // 콤보 불가능으로 만들고
            isComboEnable = false;

            //콤보가 있다는 것으로 인식
            isComboExist = true;

            return;
        }

        if (isAttacking)
        {
            return;
        }

        isAttacking = true;
    }

    public void Combo_Enable()
    {
        isComboEnable = true;
    }

    public void Combo_Disable()
    {
        isComboEnable = false;
    }

    public void Combo_Exist()
    {
        //콤보가 없으면
        if (!isComboExist)
        {
            //종료
            EndAttack();
        }
        else
        {
            //콤보가 있다면
            comboIndex++;
            //콤보 트리거 발동
            animator.SetTrigger("Combo");
            isAttacking = false;
            //콤보 없는 것으로 바꾸기
            isComboExist = false;
        }


    }

    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("Attacking", false);
        state = State.AttackIdle;
        //comboIndex = 0;
    }

    public void AttackEnd()
    {
        Debug.Log("end");
        isAttacking = false;
        animator.SetBool("Attacking", false);
        state = State.AttackIdle;
    }


    public void ToIdleState()
    {
        if (state == State.Dead)
        {
            return;
        }
        Debug.Log("act");
        state = State.Idle;
    }

    void Jump()
    {
        rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        
    }

    [SerializeField] bool spRecoverStarted = false;
    [SerializeField] float spRecoverTimer = 1;
    [SerializeField] float spRecoverDelay = 2;
    [SerializeField] float plusTimer =1;
    void RecoverSP()
    {
        if (state != State.Run)
        {
            if(pd.SP>=pd.MAXSP)
            {
                GameUI.Instance.spUI.SetActive(false);
                spRecoverStarted = false;
                return;
            }
            if(!spRecoverStarted)
            {
                spRecoverDelay -= Time.deltaTime;
                if (spRecoverDelay <= 0)
                {
                    spRecoverDelay = pd.delaySP;
                    spRecoverStarted = true;
                }
            }
            else
            {
                plusTimer -= Time.deltaTime;
                if(plusTimer <= 0)
                {
                    plusTimer = 1;
                    pd.SP += pd.plusSP;
                }
            }
        }
    }


    void TakeDamage(int damage)
    {
        pd.HP -= damage;
        if(pd.HP<=0)
        {
            Dead();
            return;
        }
        state = State.Hit;
        animator.SetTrigger("Hit");
    }

    void Dead()
    {
        state = State.Dead;
        animator.SetTrigger("Dead");
    }




    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            state = State.Idle;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyWeapon"))
        {
            int dmg = other.gameObject.GetComponent<EnemyWeapon>().enemy.data.AtkPower;
            TakeDamage(dmg);
        }
    }
}
