using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : MonoBehaviour
{
    //데미지
    public int damage = 10;
    //발사 Power
    private float power = 5f;

    private Rigidbody rigid;

    private Pooling pooling;



    // Start is called before the first frame update
    void Start()
    {
        pooling = GameManager.Instance.Pooling;
        rigid = GetComponent<Rigidbody>();
    }

    public void Initialize()
    {
        if(pooling == null)
        {
            pooling = GameManager.Instance.Pooling;
        }
        rigid.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void Fire(Vector3 dir)
    {
        if(rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }

        Vector3 force = transform.forward * power * Time.deltaTime;
        rigid.AddForce(dir * power, ForceMode.Impulse);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            pooling.SetPool(DicKey.slimeBall, gameObject);
        }

        Player p = other.GetComponent<Player>();
        if(p)
        {
            p.TakeDamage(damage);
            p.ChangeSpeed(true);
            pooling.SetPool(DicKey.slimeBall, gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
