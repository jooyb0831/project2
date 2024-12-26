using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 5;
    float speed = 0.5f;
    float deg;
    public float power;
    Rigidbody rigid;
    private Player p;

    bool isEnd = false;
    
    [SerializeField] float chargeTimer = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        p = GameManager.Instance.Player;
        power = 70;
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

    // È­»ì Â÷Â¡
    public void ArrowCharge()
    {
        if (power >= 130)
        {
            return;
        }
        chargeTimer -= Time.deltaTime;
        if(chargeTimer<=0)
        {
            power += 10;
            chargeTimer = 0.5f;
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
        Vector3 dir = transform.up * speed * power;
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
            //End(other.transform);
        }
    }

    public void Initialize()
    {
        isEnd = false;
        rigid.useGravity = false;
        power = 70;
        transform.position = Vector3.zero;
        chargeTimer = 0;
        transform.localRotation = Quaternion.Euler(90, 0, 0);
    }
}
