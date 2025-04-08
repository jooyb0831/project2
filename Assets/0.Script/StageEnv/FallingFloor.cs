using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FallingFloor : MonoBehaviour
{
    private Collider coll;
    private Rigidbody rigid;
    [SerializeField] float timer;
    [SerializeField] float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player)
        {
            //player.transform.SetParent(transform);
            Invoke("DestroyFloor", destroyTime);
        }
    }

    void DestroyFloor()
    {
        rigid.isKinematic = false;
        rigid.useGravity = true;
    }
}
