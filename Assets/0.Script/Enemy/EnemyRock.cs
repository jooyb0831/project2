using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyRock : MonoBehaviour
{
    //데미지
    public int damage = 15;
    
    //발사 Power
    private float power = 500f;

    private Rigidbody rigid;
    private Pooling pooling;
    private CapsuleCollider coll;
    // Start is called before the first frame update
    void Start()
    {
        pooling = GameManager.Instance.Pooling;
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
    }

    public void Initialize()
    {
        if(pooling == null)
        {
            pooling = GameManager.Instance.Pooling;
        }
        //coll.isTrigger = true;
        rigid.velocity = Vector3.zero;
        //transform.localScale = Vector3.one;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void ThrowRock(Vector3 dir)
    {
        if(coll == null)
        {
            coll = GetComponent<CapsuleCollider>();
        }

        if(rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }
        rigid.AddForce(dir * power, ForceMode.Impulse);
        
    }

    public void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.CompareTag("Player"))
        {
            Player player = coll.transform.parent.parent.GetComponent<Player>();
            player.TakeDamage(damage);
            Invoke(nameof(ReturnItem), 1f);
        }
    }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("DeadZone"))
        {
            Invoke(nameof(ReturnItem), 1f);
        }
    }


    void ReturnItem()
    {
        pooling.SetPool(DicKey.enemyRock, gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
