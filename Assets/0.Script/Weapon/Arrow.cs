using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    /// <summary>
    /// 화살의 데미지
    /// </summary>
    public int Damage { get; set; } = 5;

    //화살 속도
    private float speed = 0.5f;

    //화살 차징의 최대 파워
    private const int MAX_POWER = 15;

    //화살의 발사 Power
    private float power;
    public float Power
    {
        get { return power; }
        set
        {
            power = value;
            ArrowUI.Instance.Power = power;
        }
    }

    //파워 차징 기준 타이머(속도) 0.1초 기준
    [SerializeField] float chargeTimer = 0.1f;

    private Rigidbody rigid;
    private Player p;
    private Pooling pooling;
    bool isEnd = false;


    void Start()
    {
        Init();
    }

    void Init()
    {
        rigid = GetComponent<Rigidbody>();
        p = GameManager.Instance.Player;
        pooling = GameManager.Instance.Pooling;

        //화살 UI에 화살 세팅
        ArrowUI.Instance.arrow = this;
        Power = 0;
    }

    /// <summary>
    /// 화살 차징
    /// </summary>
    public void ArrowCharge()
    {
        // 화살의 현재 차징 파워가 MAX_POWER보다 크면 차징 중지(Return)
        if (Power >= MAX_POWER) return;

        //화살 차징 타이머 계산
        chargeTimer -= Time.deltaTime;

        if (chargeTimer <= 0)
        {
            Power += 1;
            chargeTimer = 0.1f;
        }
    }

    /// <summary>
    /// 화살 발사
    /// </summary>
    public void Fire()
    {
        //rigid가 없으면 rigid 받기
        if (rigid == null) rigid = GetComponent<Rigidbody>();
        //중력 활성화
        rigid.useGravity = true;
        //방향 설정
        Vector3 dir = transform.up * speed * Power * 10;
        rigid.velocity = dir;

    }


    public void End(Transform trans)
    {
        isEnd = true;
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        transform.SetParent(trans);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            pooling.SetPool(DicKey.arrow, gameObject);
        }
    }



    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Initialize()
    {
        isEnd = false;
        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
        Power = 0;
        transform.position = Vector3.zero;
        gameObject.layer = 6;
        chargeTimer = 0.5f;
        transform.localRotation = Quaternion.identity;
    }
}
