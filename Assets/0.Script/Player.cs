using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    #region State Enums
    /// <summary>
    /// 플레이어의 상태
    /// </summary>
    public enum State
    {
        Idle,
        Walk,
        Run,
        Jump,
        Roll,
        AttackIdle,
        Attack,
        Skill,
        Hit,
        Bow,
        Mine,
        Gather,
        Dead
    }

    /// <summary>
    /// 무기 장착 상태
    /// </summary>
    public enum WeaponEquipState
    {
        None,
        Arrow,
        Sword
    }

    /// <summary>
    /// 도구 장착 상태
    /// </summary>
    public enum ToolEquipState
    {
        None,
        PickAxe,
        Axe
    }

    /// <summary>
    /// 스킬 장착 상태
    /// </summary>
    public enum SkillState
    {
        None,
        Qskill,
        Rskill

    }
    #endregion

    #region 컴포넌트 변수
    private Rigidbody rigid;
    [HideInInspector] public Animator animator;
    private PlayerData pd;
    private SkillSystem skSystem;
    private Inventory inven;
    private GameUI gameUI;
    private Pooling pooling;
    #endregion

    #region State변수
    public State state = State.Idle; //플레이어의 상태
    [HideInInspector] public WeaponEquipState weaponEquipState = WeaponEquipState.None; //플레이어 무기 상태
    [HideInInspector] public ToolEquipState toolEquipState = ToolEquipState.None; //플레이어 도구 상태
    [HideInInspector] public SkillState skillState = SkillState.None; //플레이어 스킬 상태
    #endregion

    #region Transforms
    [SerializeField] Transform foot; //플레이어의 FootTransform
    public Transform swordPos; //칼 장착 Transform
    public Transform weapon1Rest; //무기 대기 Transform
    public Transform backWeaponRest; //무기 등에 맸을 때 Transform
    [SerializeField] Transform bowAttackPos; //활 장착 Transform
    public Transform toolPos; //도구 장착 Transform

    public Transform sheildPos; //쉴드 스킬 Transform
    #endregion

    #region Weapons
    [HideInInspector] public Weapon curWeapon = null; //현재 장착하고 있는 무기
    [HideInInspector] public Weapon equipedWeapon = null; //손에 들고 있는 무기
    [HideInInspector] public GameObject currentTool = null; //손에 들고 있는 도구
    [HideInInspector] public Weapon curBow = null; //현재 장착중인 활
    [HideInInspector] public Weapon equipedBow = null; //손에 있는 활
    #endregion

    #region 공격관련
    bool isComboExist; //콤보가 존재하는지
    bool isComboEnable; //콤보가 가능한지
    int comboIndex;
    bool isAttacking; //공격중인지
    float atkIdleTimer; //공격 대기 시간 체크 타이머
    [SerializeField] const float ATK_IDLE_TIME = 1.5f; // 공격 대기 시간(1.5초)
    #endregion

    #region 활 관련 변수
    bool isCharging = false; //화살 차징 중인지 여부
    Arrow ar;
    [SerializeField] Transform arrows; //화살 오브젝트 모이는 Transform
    #endregion

    #region SP회복 관련 변수
    bool spRecoverStarted = false;
    float spRecoverDelay = 2; //SP회복을 시작할 때 까지의 딜레이
    float plusTimer = 1; //sp증가 타이머(1초)
    #endregion


    private float speed; //플레이어 스피드
    private float nomalSpeed; //원래 스피드 저장용 변수
    private float runSpeed; //원래의 달리기 스피드 저장용 변수
    private float sprdTimer = 1f; //스태미너 감소 시간(1초)
    const float JUMP_POWER = 5f; //점프 파워

    public Camera charUICam; //Inventory창에 띄울 캐릭터 카메라

    [SerializeField] CapsuleCollider playerColl; //플레이어의 콜리더
    private const float COLL_ORIGIN_RAD = 0.25f;
    private const float COLL_ORIGIN_HEIGHT = 1.8f;


    void Start()
    {
        Init();
    }

    void Init()
    {
        pd = GameManager.Instance.PlayerData;
        inven = GameManager.Instance.Inven;
        pooling = GameManager.Instance.Pooling;
        skSystem = GameManager.Instance.SkillSystem;
        gameUI = GameManager.Instance.GameUI;
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("Idle");
        nomalSpeed = pd.Speed;
        runSpeed = pd.RunSpeed;
    }


    void Update()
    {
        if (state.Equals(State.Dead))
        {
            return;
        }

        // 메뉴 탭이 열렸을 때는 모든 키 입력 안받게 만들기
        if (GameManager.Instance.isPaused)
        {
            animator.SetBool("Attacking", false);
            animator.SetTrigger("Idle");
            state = State.Idle;
            return;
        }

        Debug.DrawRay(foot.position, Vector3.down * 0.1f, Color.red);

        //움직임 받기
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        RecoverSP();
        Attack();
        Move(x, z);
        Bow();

        #region 키 이벤트
        //Q스킬 사용
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //현재 장착한 qSkill이 null일 경우 리턴
            if (skSystem.qSkill == null) 
            {
                return;
            }

            Skill qSkill = skSystem.qSkill.GetComponent<Skill>();
            //스킬 쿨타임이 안 됐을 경우(작동중일 경우)
            if (qSkill.isWorking)
            {
                gameUI.DisplayInfo(4); //UI에 쿨타임이 안됐음을 표시
                return;
            }
            //MP가 부족할 경우
            if (pd.CURMP < qSkill.data.skillData.mp)
            {
                gameUI.DisplayInfo(1); //UI에 MP가 부족함을 표시
                return;
            }
            //스킬 사용 State로 변경
            skillState = SkillState.Qskill;
            qSkill.SkillAct(); //스킬 작동
        }

        //R스킬 사용
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (skSystem.rSkill == null)
            {
                return;
            }

            Skill rSkill = skSystem.rSkill.GetComponent<Skill>();
            if (rSkill.isWorking)
            {
                gameUI.DisplayInfo(4); //UI에 쿨타임이 안됐음을 표시
                return;
            }

            if (pd.CURMP < rSkill.data.skillData.mp)
            {
                gameUI.DisplayInfo(1); //UI에 MP가 부족함을 표시
                return;
            }
            skillState = SkillState.Rskill;
            rSkill.SkillAct();
        }

        //도구 및 아이템 사용(상호작용)
        if (Input.GetKeyDown(KeyCode.F))
        {
            //현재 장착한 도구가 없다면 리턴
            if (currentTool == null)
            {
                return;
            }
            // 현재 장착한 Tool의 데이터 중 ST 차감 받아오기
            int toolST = currentTool.GetComponent<Tool>().data.useST;
            //ST가 부족하다면
            if (pd.ST < toolST)
            {
                gameUI.DisplayInfo(0); //스태미너가 부족함을 UI에 표시
                return;
            }
            pd.ST -= toolST;
            state = State.Mine;
            animator.SetTrigger("Mine");
        }

        //구르기
        if (Input.GetKeyDown(KeyCode.C))
        {
            //SP가 5 미만일 경우 구르지 못함
            if (pd.SP < 5) return;
            gameUI.spUI.SetActive(true);
            pd.SP -= 5;
            RollAnimatorSet(new Vector3(x, 0, z));

        }
        #endregion

        //달리기 시 SP 감소 처리
        if (state == State.Run)
        {
            sprdTimer -= Time.deltaTime;
            if (sprdTimer <= 0)
            {
                pd.SP -= pd.minSP; //초당 pd.minSP만큼 감소하게끔.
                sprdTimer = 1;
            }
        }

        StateCheck();
    }

    /// <summary>
    /// 아이템 수집하는 애니메이션 호출
    /// </summary>
    public void GatherAnim(bool isST, int ST = 0)
    {
        if (state.Equals(State.Walk) || state.Equals(State.Run))
        {
            animator.ResetTrigger("Walk");
        }
        state = State.Gather;
        animator.SetTrigger("Gather");

        if (isST)
        {
            pd.ST -= ST;
        }
    }

    /// <summary>
    /// 공격시 무기 손에 장착하는 함수
    /// </summary>
    public void Weapon()
    {
        //장착한 무기가 없으면 리턴
        if (curWeapon == null)
        {
            Debug.Log("무기");
            return;
        }
        else
        {
            //장착한 무기가 칼일 경우
            if (weaponEquipState.Equals(WeaponEquipState.Sword))
            {
                //무기를 손에 들고 있으면 리턴
                if (equipedWeapon != null)
                {
                    return;
                }

                //무기를 손에 들고 있지 않으면 손에 무기 생성
                equipedWeapon = Instantiate(curWeapon, swordPos);
                equipedWeapon.transform.localPosition = Vector3.zero;

                //플레이어에 부착되어있는 무기 비활성화
                curWeapon.gameObject.SetActive(false);
            }
        }
    }


    /// <summary>
    /// 활 장착하는 코드
    /// </summary>
    public void BowEquip()
    {
        //활 자체를 활 슬롯에 장착하지 않았을 경우 리턴
        if (curBow == null)
        {
            return;
        }

        //이미 손에 활이 있는 경우 리턴
        if (equipedBow != null)
        {
            return;
        }

        //위치에 활 오브젝트 생성
        equipedBow = Instantiate(curBow, bowAttackPos);
        equipedBow.transform.localRotation = Quaternion.identity;
        equipedBow.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 플레이어 움직임
    /// </summary>
    void Move(float x, float z)
    {
        //특정 State의 경우에는 움직이지 못하도록 Return
        switch (state)
        {
            case State.Gather:
            case State.Dead:
            case State.Bow:
            case State.Attack:
            case State.Hit:
                return;
            default:
                break;
        }

        //기본 움직임
        transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * speed);

        switch (state)
        {
            case State.Jump:
                return;
            case State.Skill:
                {
                    if (skSystem.qSkill != null)
                    {
                        if (skSystem.qSkill?.GetComponent<Skill>().data.skillData.index == 0)
                        {
                            return;
                        }
                        break; ;
                    }
                    else if (skSystem.rSkill != null)
                    {
                        if (skSystem.rSkill.GetComponent<Skill>().data.skillData.index == 0)
                        {
                            return;
                        }
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

            case State.AttackIdle: //공격 대기 상태일때
                if (x != 0 || z != 0) //이동값이 있다면
                {
                    state = State.Walk; //움직이는 상태로 변경
                }
                break;

            default:
                break;
        }

        #region 점프
        RaycastHit hit;
        //foot.position에서 Vector3.down으로 Ray를 0.05만큼 쏘았을 때
        if (Physics.Raycast(foot.position, Vector3.down * 0.05f, out hit))
        {
            //Ground가 아니라면
            if (!hit.collider.CompareTag("Ground")) 
            {
                 //점프중인 상태
                state = State.Jump;
            }
        }

        //Jump키를 눌렀을 때
        if (Input.GetButtonDown("Jump"))
        {
            //이미 점프중인 상태라면 리턴
            if (state == State.Jump)
            {
                return;
            }
            else
            {
                //점프 실행
                Jump();
                return;
            }

        }
        #endregion

        //플레이어의 이동값이 있을 때 = 움직이는 중
        if (x != 0 || z != 0)
        {
            //Shift키를 누르면 달리기
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //SP가 0이면 달리지 못하게 함
                if (pd.SP <= 0)
                {
                    state = State.Walk;
                }
                else
                {
                    state = State.Run;
                    gameUI.spUI.SetActive(true); //SP관련 UI Active
                }
            }

            else
            {
                state = State.Walk;
            }

            MoveAnimatorSet(new Vector3(x, 0, z), state);
        }

        // 이동 중이 아닐 때(정지상태일 때)
        else
        {
            switch (state)
            {
                case State.Hit:
                case State.AttackIdle:
                case State.Attack:
                case State.Skill:
                case State.Mine:
                    return;

                default:
                    break;
            }
            animator.SetTrigger("Idle"); //Idle 애니메이션 Trigger
            state = State.Idle;
        }

    }


    /// <summary>
    /// MoveAnimation 세팅 함수
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="state"></param>
    void MoveAnimatorSet(Vector3 dir, State state)
    {
        animator.SetTrigger("Walk");
        switch (state)
        {
            case State.Walk:
                {
                    speed = nomalSpeed;
                    animator.SetFloat("floatX", dir.x * 0.5f);
                    animator.SetFloat("floatZ", dir.z * 0.5f);
                    break;
                }

            case State.Run:
                {
                    speed = runSpeed;
                    animator.SetFloat("floatX", dir.x);
                    animator.SetFloat("floatZ", dir.z);
                    break;
                }
        }
    }

    /// <summary>
    /// 구르기 애니메이션 세팅 함수
    /// </summary>
    /// <param name="dir"></param>
    void RollAnimatorSet(Vector3 dir)
    {
        animator.SetTrigger("Roll");
        animator.SetFloat("floatX", dir.x);
        animator.SetFloat("floatZ", dir.z);
    }

    public void ColliderSmall()
    {
        playerColl.radius = 0.1f;
        playerColl.height = 1f;
    }

    public void ColliderOrigin()
    {
        playerColl.radius = COLL_ORIGIN_RAD;
        playerColl.height = COLL_ORIGIN_HEIGHT;
    }


    /// <summary>
    /// 플레이어 공격
    /// </summary>
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (state == State.Bow) //활 사용중이면 리턴
            {
                return;
            }

            if (state != State.Attack) //공격State가 아닐 경우
            {
                state = State.Attack; //공격 State로 변경
                animator.SetBool("Attacking", true); //애니메이션 Set

                //무기 상태에 따라 구분
                switch (weaponEquipState)
                {
                    case WeaponEquipState.None:
                        {
                            animator.SetTrigger("Punch");
                            break;
                        }

                    case WeaponEquipState.Sword:
                        {
                            Weapon();
                            animator.SetTrigger("Sword");
                            break;
                        }
                }
            }

            else //공격State일 경우 AttackCombo 실행
            {
                AttackCombo();
            }

        }
    }


    /// <summary>
    /// State에 따라 실행되는 함수
    /// </summary>
    void StateCheck()
    {
        //스킬상태 None으로 변경
        if (state == State.AttackIdle)
        {
            //공격 대기 시간 체크
            atkIdleTimer += Time.deltaTime;
            if (atkIdleTimer >= ATK_IDLE_TIME)
            {
                //공격 대기 상태에서 Idle상태로 변경하기
                atkIdleTimer = 0;
                animator.SetTrigger("Idle");
                state = State.Idle;
                isAttacking = false;
            }
        }

        else //공격 대기 상태가 아닐 경우
        {
            atkIdleTimer = 0;
        }

        //스킬 실행상태가 아니면
        if (state != State.Skill)
        {
            //스킬상태 None으로 변경
            skillState = SkillState.None;
        }

        //Idle, walk, run 상태일 때
        if (state == State.Idle || state == State.Walk || state == State.Run)
        {
            animator.SetBool("Attacking", false); //애니메이션 세팅

            //손에 장착한 무기가 있다면
            if (equipedWeapon != null)
            {
                Destroy(equipedWeapon.gameObject); //손에 있는 무기 비활성화
                curWeapon.gameObject.SetActive(true); //장착하는 무기 활성화
            }

            //손에 장착중인 활이 있다면
            if (equipedBow != null)
            {
                Destroy(equipedBow.gameObject); //손에 있는 활 비활성화
                curBow.gameObject.SetActive(true); //장착한 활 활성화
            }
        }

    }


    /// <summary>
    /// 활 작동 함수
    /// </summary>
    void Bow()
    {
        if (Input.GetMouseButton(1)) //오른쪽 마우스 클릭중
        {
            if (!pd.bowEquiped) // 장착된 활이 없으면 리턴
            {
                return;
            }

            BowEquip(); //화살 장착
            state = State.Bow; //상태 변경

            //마우스 좌클릭 시 화살 생성
            if (Input.GetMouseButtonDown(0))
            {
                //인벤토리에 화살이 부족할 경우 리턴
                if (inven.FindItem(4).data.count == 0)
                {
                    gameUI.DisplayInfo(5); //UI에 화살이 부족함을 표시하는 코드
                    return;
                }

                gameUI.arrowUI.SetActive(true); //활 UI 활성화
                isCharging = true; //차징 상태
                animator.SetTrigger("Bow"); //애니메이션 세팅

                //화살 생성
                ar =pooling.GetPool(DicKey.arrow, Camera.main.transform).GetComponent<Arrow>();
            }

            //좌클릭을 꾹 누르고 있으면 화살 차징
            if (Input.GetMouseButton(0))
            {
                // 인벤토리에 화살이 없거나 부족할 경우 리턴
                if (inven.FindItem(4).data.count == 0 || inven.FindItem(4) == null)
                {
                    gameUI.DisplayInfo(5); //UI에 화살이 부족함을 표시하는 코드
                    return;
                }

                //화살 차징
                ar.ArrowCharge();
            }

            //좌클릭을 떼면 화살 발사
            if (Input.GetMouseButtonUp(0))
            {
                //인벤에 화살 없으면 리턴
                if (inven.FindItem(4).data.count == 0)
                {
                    gameUI.DisplayInfo(5); //UI에 화살이 부족함을 표시하는 코드
                    return;
                }

                //화살 발사
                ar.gameObject.layer = default; //레이어 변경(카메라에 보이게끔)
                isCharging = false; //차징 아닌 상태
                animator.speed = 1; //애니메이션 재생
                ar.Fire(); //화살 발사
                ar.transform.SetParent(arrows); //부모 오브젝트 변경
                ar = null; //화살 비우기

                //인벤토리에서 화살 사용 처리
                inven.UseItem(inven.FindItem(4));

                //활 UI 비활성화(끄기)
                gameUI.arrowUI.SetActive(false);
            }
        }

        //우클릭 해제(줌이 끝났을 경우)
        if (Input.GetMouseButtonUp(1))
        {
            //차징중이었다면 차징 아니게 변경
            if (isCharging)
            {
                isCharging = false;
            }

            //애니메이션의 속도 1로 변경
            if (animator.speed != 1)
            {
                animator.speed = 1;
            }

            //화살이 생성되었다면 화살 삭제
            if (ar != null)
            {
                pooling.SetPool(DicKey.arrow, ar.gameObject);
                ar = null;
            }
            state = State.Idle;
        }
    }

    /// <summary>
    /// 활 차징 중 애니메이션 일시중지(Anim)
    /// </summary>
    public void BowAnimationStop()
    {
        if (isCharging)
        {
            animator.speed = 0;
        }

    }

    /// <summary>
    /// 공격 콤보 함수
    /// </summary>
    void AttackCombo()
    {
        // 콤보가 가능한 상태에서 공격키를 눌렀을 때
        if (isComboEnable)
        {
            //콤보 불가능으로 바꾸고
            isComboEnable = false;
            //콤보가 있다는 것으로 인식
            isComboExist = true;
            return;
        }

        // 공격중이었다면 리턴
        if (isAttacking) return;

        //공격중으로 변경
        isAttacking = true;
    }

    /// <summary>
    /// 콤보여부 판단 함수
    /// </summary>
    public void Combo_Exist()
    {
        //콤보가 없으면
        if (!isComboExist)
        {
            //종료
            AttackEnd();
        }

        //콤보가 있다면
        else
        {
            //콤보 트리거 발동
            animator.SetTrigger("Combo");
            isAttacking = false;
            //콤보 없는 것으로 바꾸기
            isComboExist = false;
        }
    }

    /// <summary>
    /// 콤보 가능한 상태 함수(Animation에서 호출)
    /// </summary>
    public void Combo_Enable()
    {
        isComboEnable = true;
    }

    /// <summary>
    /// 콤보 불가능한 상태 함수(Animation에서 호출)
    /// </summary>
    public void Combo_Disable()
    {
        isComboEnable = false;
    }

    /// <summary>
    /// 공격종료 함수(Animation호출)
    /// </summary>
    public void AttackEnd()
    {
        isAttacking = false;
        animator.SetBool("Attacking", false);
        animator.ResetTrigger("Walk");
        state = State.AttackIdle;
        comboIndex = 0;
    }

    /// <summary>
    /// IdleState로 변경하는 함수(Animation호출)
    /// </summary>
    public void ToIdleState()
    {
        if (state == State.Dead || state == State.Attack)
        {
            return;
        }
        animator.SetTrigger("Idle");
        state = State.Idle;

    }

    /// <summary>
    /// 점프구현 함수
    /// </summary>
    void Jump()
    {
        //이미 점프 중인 상태일 경우 리턴
        if (state.Equals(State.Jump))
        {
            return;
        }

        //SP가 3보다 작으면 점프X
        if (pd.SP < 3)
        {
            return;
        }

        //점프 물리
        rigid.AddForce(Vector3.up * JUMP_POWER, ForceMode.Impulse);
        pd.SP -= 3; // SP 감소
        gameUI.spUI.SetActive(true); //GameUI에서 SP UI 표시
        state = State.Jump;
        //달리기 중에 점프했을 경우 JumpRun애니메이션 트리거
        if (state.Equals(State.Run))
        {
            animator.SetTrigger("JumpRun");
        }
        else
        {
            animator.SetTrigger("Jump");
        }

    }

    /// <summary>
    /// SP회복함수
    /// </summary>
    void RecoverSP()
    {
        //달리는 상태가 아닐 때
        if (state != State.Run)
        {
            //SP가 다 충전되었다면 리턴
            if (pd.SP >= pd.MAXSP)
            {
                gameUI.spUI.SetActive(false);
                spRecoverStarted = false;
                return;
            }

            //SP회복중이 아닐 경우
            if (!spRecoverStarted)
            {
                //SP회복까지의 대기시간 체크
                spRecoverDelay -= Time.deltaTime;

                if (spRecoverDelay <= 0)
                {
                    spRecoverDelay = pd.delaySP; //대기시간 초기화
                    spRecoverStarted = true; //회복상태로 변경
                }
            }

            //SP회복중일경우
            else
            {
                //1초당 pd.plusSP만큼씩 회복
                plusTimer -= Time.deltaTime;
                if (plusTimer <= 0)
                {
                    plusTimer = 1;
                    pd.SP += pd.plusSP;
                }
            }
        }
    }


    /// <summary>
    /// 플레이어 피격 함수
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
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

    /// <summary>
    /// 플레이어 사망 함수
    /// </summary>
    void Dead()
    {
        if (pd.HP < 0)
        {
            pd.HP = 0;
        }
        state = State.Dead;
        animator.SetTrigger("Dead");
        gameUI.GameOver();
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

        //적 무기와 접촉했을 경우
        if (other.CompareTag("EnemyWeapon"))
        {
            if (other.GetComponent<ArrowTrap>())
            {
                return;
            }

            //해당 Enemy를 받아옴.
            Enemy enemy = other.GetComponent<EnemyWeapon>().enemy;
            if (state == State.Hit)
            {
                return;
            }

            //적이 공격 상태일 때만 데미지 받게끔
            if (enemy.state.Equals(Enemy.State.Attack))
            {
                //Enemy의 공격력을 받아옴
                int dmg = enemy.data.AtkPower;
                TakeDamage(dmg); //피격 처리
            }

        }

        //DeadZone에 접촉했을 경우 바로 사망처리
        if (other.CompareTag("DeadZone"))
        {
            Dead();
        }
    }


    /// <summary>
    /// 플레이어의 속도 변화 처리 함수
    /// </summary>
    /// <param name="isInstant">일시성여부</param>
    public void ChangeSpeed(bool isInstant = false)
    {
        //기존의 Speed값의 반으로
        nomalSpeed = pd.Speed / 2;
        runSpeed = pd.RunSpeed / 2;

        //일시적일 경우
        if (isInstant)
        {
            Invoke(nameof(ResetSpeed), 2f);
        }
    }


    /// <summary>
    /// 플레이어의 스피드 원래대로 돌리기
    /// </summary>
    public void ResetSpeed()
    {
        nomalSpeed = pd.Speed;
        runSpeed = pd.RunSpeed; ;
    }

}