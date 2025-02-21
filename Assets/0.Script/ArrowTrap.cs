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


    private Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    /// <summary>
    /// 화살 발사
    /// </summary>
    void Fire()
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
        Player player = other.GetComponent<Player>();

        if (player)
        {
            Debug.Log("ss");
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.GetComponent<WallTrap>())
        {
            Destroy(gameObject, 1.5f);
        }
    }
}
