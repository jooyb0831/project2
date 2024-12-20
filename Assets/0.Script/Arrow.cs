using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    float speed = 0.5f;
    float deg;
    public float power;
    Rigidbody rigid;
    private Player p;

    [SerializeField] float chargeTimer = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        p = GameManager.Instance.Player;
        power = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // È­»ì Â÷Â¡
    public void ArrowCharge()
    {
        if (power >= 100)
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
        Vector3 dir =  Vector3.forward * speed * power;
        rigid.velocity = dir;
    }


}
