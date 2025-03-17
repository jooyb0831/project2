using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlimeBall : MonoBehaviour
{
    //데미지
    public int damage = 10;
    //발사 Power
    private float power = 5f;

    private Rigidbody rigid;

    private SphereCollider coll;

    private Pooling pooling;
    



    // Start is called before the first frame update
    void Start()
    {
        pooling = GameManager.Instance.Pooling;
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<SphereCollider>();
    }

    public void Initialize()
    {
        if(pooling == null)
        {
            pooling = GameManager.Instance.Pooling;
        }
        coll.isTrigger = true;
        rigid.velocity = Vector3.zero;
        transform.localScale = Vector3.one * 0.5f;
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
            coll.isTrigger = false;
            transform.DOScale(Vector3.one * 0.1f, 1.0f)
            .OnComplete(() => BallRetrun());
        }

        Player p = other.GetComponent<Player>();
        if(p)
        {
            p.TakeDamage(damage);
            p.ChangeSpeed(true);
            pooling.SetPool(DicKey.slimeBall, gameObject);
        }
    }

    void BallRetrun()
    {
        pooling.SetPool(DicKey.slimeBall, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
