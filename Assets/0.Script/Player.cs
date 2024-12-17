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

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(foot.position, Vector3.down * 0.1f, Color.red);
        Attack();
        StateCheck();
        Move();

        if(Input.GetKeyDown(KeyCode.E))
        {
            state = State.Gather;
            animator.SetTrigger("Gather");
        }
    }

    void Move()
    {
        if(state == State.Gather)
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
                speed = pd.RunSpeed;
                state = State.Run;
                animator.SetTrigger("RunForward");
            }
            else
            {
                animator.SetTrigger("WalkForward");
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

    void Jump()
    {
        rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        
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
            state = State.Hit;
            animator.SetTrigger("Hit");
        }
    }
}
