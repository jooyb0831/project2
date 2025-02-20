using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    private int damage = 5;
    private float speed = 5;
    private float power = 10;
    private Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }
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
