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
        Attack,
        Hit,
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
        UpdateAttacking();
        /*
        Move();
        Attack();
        */
    }

    void Move()
    {
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

        if(Input.GetMouseButtonDown(0))
        {
            state = State.Attack;
            isAtkStart = true;
            clickCnt++;
            animator.SetTrigger("Punch");
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
            animator.SetTrigger("Idle");
            state = State.Idle;
        }
        
    }

    [SerializeField] bool bComboExist;
    [SerializeField] bool bComboEnable;
    [SerializeField] int comboIndex;
    [SerializeField] bool bAttacking;
    

    void UpdateAttacking()
    {
        if(!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

        // 콤보가 가능한 상태에서 E키를 눌렀을 때
        if(bComboEnable)
        {
            // 콤보 불가능으로 만들고
            bComboEnable = false;

            //콤보가 있다는 것으로 인식
            bComboExist = true;

            return;
        }

        if(bAttacking)
        {
            return;
        }

        bAttacking = true;
        animator.SetBool("Attacking", bAttacking);
    }

    public void Combo_Enable()
    {
        bComboEnable = true;
    }

    public void Combo_Disable()
    {
        bComboEnable = false;
    }

    public void Combo_Exist()
    {
        //콤보가 없으면
        if (!bComboExist)
        {
            //종료
            EndAttack();
            return;
        }
        
        //콤보가 있다면
        comboIndex++;
        //콤보 트리거 발동
        animator.SetTrigger("Combo");
        //콤보 없는 것으로 바꾸기
        bComboExist = false;
    }

    public void EndAttack()
    {
        bAttacking = false;
        animator.SetBool("Attacking", bAttacking);
        comboIndex = 0;
    }



    void Jump()
    {
        rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        
    }

    [SerializeField] float timer;
    [SerializeField] int clickCnt;
    bool isAtkStart = false;
    void Attack()
    {
        if(isAtkStart)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 2f)
        {
            timer = 0;
            clickCnt = 0;
            isAtkStart = false;
        }

        /*
        while (timer>0 && timer<=2f)
        {
            if(Input.GetMouseButtonDown(0))
            {
                clickCnt++;
            }
            else
            {
                return;
            }
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            state = State.Idle;
        }
    }
}
