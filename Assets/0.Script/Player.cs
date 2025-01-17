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
        Mine,
        Gather,
        Dead
    }

    public enum WeaponEquipState
    {
        None,
        Arrow,
        Sword
    }

    public enum ToolEquipState
    {
        None,
        PickAxe,
        Axe

    }

    private Rigidbody rigid;
    private Animator animator;
    private PlayerData pd;
    private Inventory inven;
    public State state = State.Idle;
    public WeaponEquipState weaponEquipState = WeaponEquipState.None;
    public ToolEquipState toolEquipState = ToolEquipState.None;

    private float speed;
    [SerializeField] Transform foot;


    public Transform swordPos;
    public Transform weapon1Rest;
    public Transform backWeaponRest;

    public Transform toolPos;
    // Start is called before the first frame update
    void Start()
    {

        pd = GameManager.Instance.PlayerData;
        inven = GameManager.Instance.Inven;
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


        if (Input.GetKeyDown(KeyCode.E))
        {
            state = State.Gather;
            animator.SetTrigger("Gather");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentTool == null)
            {
                return;
            }
            int toolST = currentTool.GetComponent<Tool>().data.useST;
            if (pd.ST < toolST)
            {
                Debug.Log("스태미너가 부족합니다.");
                return;
            }
            pd.ST -= toolST;
            state = State.Mine;
            animator.SetTrigger("Mine");
        }


        if (state == State.Run)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                pd.SP -= pd.minSP;
                timer = 1;
            }
        }
    }

    public Weapon curWeapon = null;
    public Weapon equipedWeapon = null;
    public GameObject currentTool = null;
    /// <summary>
    /// 공격시 무기 손에 장착하는 함수
    /// </summary>
    public void Weapon()
    {
        if (curWeapon == null)
        {
            return;
        }
        else
        {
            if (weaponEquipState.Equals(WeaponEquipState.Sword))
            {
                if (equipedWeapon != null)
                {
                    return;
                }
                equipedWeapon = Instantiate(curWeapon, swordPos);
                equipedWeapon.transform.localPosition = Vector3.zero;
                curWeapon.gameObject.SetActive(false);
            }
        }
    }

    [SerializeField] Transform bowAttackPos;
    public Weapon curBow = null;
    public Weapon equipedBow = null;
    public void BowEquip()
    {
        //활 자체를 활 슬롯에 장착하지 않았을 경우 리턴
        if(curBow == null)
        {
            return;
        }
        //이미 손에 활이 있는 경우 리턴
        if(equipedBow !=null)
        {
            return;
        }

        //위치에 활 생성
        equipedBow = Instantiate(curBow, bowAttackPos);
        equipedBow.transform.localRotation = Quaternion.identity;
        equipedBow.transform.localPosition = Vector3.zero;
    }
    

    void Move()
    {
        if (state == State.Gather || state == State.Dead || state == State.Bow 
            || state == State.Attack || state == State.Hit)
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

        if (state == State.AttackIdle)
        {
            if (x != 0 || z != 0)
            {
                state = State.Walk;
            }
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
                return;
            }

        }

        if (x != 0 || z != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (pd.SP <= 0)
                {
                    state = State.Walk;
                    MoveAnimatorSet(new Vector3(x, 0, z), State.Walk);
                    return;
                }
                state = State.Run;
                MoveAnimatorSet(new Vector3(x, 0, z), State.Run);
                GameUI.Instance.spUI.SetActive(true);

            }

            else
            {
                state = State.Walk;
                MoveAnimatorSet(new Vector3(x, 0, z), State.Walk);
                //Walk();
            }
        }

        else
        {
            if (state == State.Hit || state == State.AttackIdle || state == State.Attack)
            {
                return;
            }
            animator.SetTrigger("Idle");
            state = State.Idle;
        }

    }

    void Walk()
    {
        state = State.Walk;
        speed = pd.Speed;

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetTrigger("WalkForwardL");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animator.SetTrigger("WalkForwardR");
            }
            else
            {
                animator.SetTrigger("WalkForward");
            }
        }

        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetTrigger("WalkBackwardR");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animator.SetTrigger("WalkBackwardL");
            }
            else
            {
                animator.SetTrigger("WalkBackward");
            }
        }

        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetTrigger("WalkForwardL");
        }

        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetTrigger("WalkForwardR");
        }
    }

    void Run()
    {
        speed = pd.RunSpeed;
        state = State.Run;

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetTrigger("RunForwardL");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animator.SetTrigger("RunForwardR");
            }
            else
            {
                animator.SetTrigger("RunForward");
            }
        }

        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetTrigger("RunBackwardL");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                animator.SetTrigger("RunBackwardR");
            }
            else
            {
                animator.SetTrigger("RunBackward");
            }
        }

        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetTrigger("RunForwardL");
        }

        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetTrigger("RunForwardR");
        }
    }

    void MoveAnimatorSet(Vector3 dir, State state)
    {
        animator.SetTrigger("Walk");
        switch (state)
        {
            case State.Walk:
                {
                    
                    speed = pd.Speed;
                    animator.SetFloat("floatX", dir.x * 0.5f);
                    animator.SetFloat("floatZ", dir.z * 0.5f);
                    break;
                }
            case State.Run:
                {
                    
                    speed = pd.RunSpeed;
                    animator.SetFloat("floatX", dir.x);
                    animator.SetFloat("floatZ", dir.z);
                    break;
                }
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
            if (state == State.Bow)
            {
                return;
            }
            if (state != State.Attack)
            {
                state = State.Attack;
                animator.SetBool("Attacking", true);
                if (weaponEquipState.Equals(WeaponEquipState.Sword))
                {
                    Weapon();
                    animator.SetTrigger("Sword");
                }

                if (weaponEquipState.Equals(WeaponEquipState.None))
                {
                    animator.SetTrigger("Punch");
                }
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
                isAttacking = false;
            }
        }
        else
        {
            atkIdleTimer = 0;
        }


        if (state == State.Idle || state == State.Walk || state == State.Run)
        {
            if (equipedWeapon != null)
            {
                Destroy(equipedWeapon.gameObject);
                curWeapon.gameObject.SetActive(true);
            }

            if(equipedBow != null)
            {
                Destroy(equipedBow.gameObject);
                curBow.gameObject.SetActive(true);
            }
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
            if(!pd.bowEquiped)
            {
                Debug.Log("장착된 활이 없습니다");
                return;
            }
            BowEquip();
            state = State.Bow;
            if (Input.GetMouseButtonDown(0))
            {
                if(inven.FindItem(4).data.count==0)
                {
                    Debug.Log("화살이 부족합니다.");
                    return;
                }
                GameUI.Instance.arrowUI.SetActive(true);
                isCharging = true;
                animator.SetTrigger("Bow");

                //화살 생성
                ar = Pooling.Instance.GetPool(DicKey.arrow, Camera.main.transform).GetComponent<Arrow>();
            }

            if (Input.GetMouseButton(0))
            {
                if(inven.FindItem(4).data.count==0)
                {
                    Debug.Log("화살이 부족합니다.");
                    return;
                }
                //화살 차징
                ar.ArrowCharge();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if(inven.FindItem(4).data.count==0)
                {
                    Debug.Log("화살이 부족합니다.");
                    return;
                }
                //화살 발사
                ar.gameObject.layer = default;
                isCharging = false;
                animator.speed = 1;
                ar.Fire();
                ar.transform.SetParent(arrows);
                ar = null;

                //인벤토리에서 화살 사용 처리
                inven.UseItem(inven.FindItem(4));
                GameUI.Instance.arrowUI.SetActive(false);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (isCharging)
            {
                isCharging = false;
            }
            if (animator.speed != 1)
            {
                animator.speed = 1;
            }

            //animator.SetTrigger("Idle");
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
            //콤보 불가능
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
        animator.ResetTrigger("Walk");
        state = State.AttackIdle;
        //comboIndex = 0;
    }

    public void AttackEnd()
    {
        Debug.Log("end");
        isAttacking = false;
        animator.SetBool("Attacking", false);
        animator.ResetTrigger("Walk");
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
        if(state.Equals(State.Jump))
        {
            return;
        }
        rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        if (state.Equals(State.Run))
        {
            animator.SetTrigger("JumpRun");
        }
        else
        {
            animator.SetTrigger("Jump");
        }
        state = State.Jump;

    }

    [SerializeField] bool spRecoverStarted = false;
    [SerializeField] float spRecoverTimer = 1;
    [SerializeField] float spRecoverDelay = 2;
    [SerializeField] float plusTimer = 1;
    void RecoverSP()
    {
        if (state != State.Run)
        {
            if (pd.SP >= pd.MAXSP)
            {
                GameUI.Instance.spUI.SetActive(false);
                spRecoverStarted = false;
                return;
            }
            if (!spRecoverStarted)
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
                if (plusTimer <= 0)
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
        if (pd.HP <= 0)
        {
            Dead();
            return;
        }
        state = State.Hit;
        animator.SetTrigger("Hit");
    }

    void Dead()
    {
        if (pd.HP < 0)
        {
            pd.HP = 0;
        }
        state = State.Dead;
        animator.SetTrigger("Dead");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            state = State.Idle;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            if (state == State.Hit)
            {
                return;
            }
            int dmg = other.gameObject.GetComponent<EnemyWeapon>().enemy.data.AtkPower;
            TakeDamage(dmg);
        }
    }
}
