using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int Damage { get; set; } = 5;
    float speed = 0.5f;
    float deg;

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
    Rigidbody rigid;
    private Player p;

    bool isEnd = false;
    
    [SerializeField] float chargeTimer = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        p = GameManager.Instance.Player;
        ArrowUI.Instance.arrow = this;
        Power = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        /*
        if(isEnd)
        {
            float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            return;
        }
        */
        
    }

    // 화살 차징
    public void ArrowCharge()
    {
        if (Power >= 15)
        {
            return;
        }
        chargeTimer -= Time.deltaTime;
        if(chargeTimer<=0)
        {
            Power += 1;
            chargeTimer = 0.1f;
        }

        Debug.Log(power);
    }

    public void Fire()
    {
        if(rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }
        rigid.useGravity = true;
        Vector3 dir = transform.up * speed * Power*10;
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
        if(other.CompareTag("Ground"))
        {
            Pooling.Instance.SetPool(DicKey.arrow, gameObject);
        }
    }

    


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
