using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    //데미지
    private int damage = 5;

    //속도
    private float speed = 5;

    //파워
    private float power = 10;

    public bool isOneShot;

    private Rigidbody rigid;
    private Pooling pooling;
    // Start is called before the first frame update
    void Start()
    {
        pooling = GameManager.Instance.Pooling;
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOneShot)
        {
            Fire();
        }

    }

    /// <summary>
    /// 화살의 초기화
    /// </summary>
    public void Initialize()
    {
        rigid.velocity = Vector3.zero;
        rigid.useGravity = false;
        transform.position = Vector3.zero;
    }

    /// <summary>
    /// 화살 발사
    /// </summary>
    public void Fire()
    {
        //rigid가 없다면 받아올것
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }

        //발사되면서 중력 작용 받게끔
        rigid.useGravity = true;
        Vector3 dir = transform.up * speed * power;
        rigid.velocity = dir;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            //플레이어 받아오기
            Player p = other.transform.parent.parent.GetComponent<Player>();
            p.TakeDamage(damage); //피격처리

            //한 번만 발사되는 경우
            if(isOneShot)
            {
                Destroy(gameObject); //삭제
            }
            else
            {
                ReturnObj(); //풀로 리턴
            }
        }

        if (other.GetComponent<WallTrap2>())
        {
            if (isOneShot)
            {
                Destroy(gameObject);
            }
            else
            {
                Invoke(nameof(ReturnObj), 1.5f);
            }
        }

        if(other.CompareTag("MagicShield"))
        {
            ReturnObj();
        }
        
    }


    /// <summary>
    /// 화살을 풀로 돌려놓기
    /// </summary>
    void ReturnObj()
    {
        pooling.SetPool(DicKey.arrowTrap, gameObject);
    }
}
