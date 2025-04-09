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

    void Start()
    {
        coll = GetComponent<BoxCollider>();
        rigid = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Invoke(nameof(DestroyFloor), destroyTime);
        }
    }

    void DestroyFloor()
    {
        rigid.isKinematic = false;
        rigid.useGravity = true;
    }
}
