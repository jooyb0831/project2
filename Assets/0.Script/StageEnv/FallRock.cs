using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
    private Pooling pooling;

    void Start()
    {
        pooling = GameManager.Instance.Pooling;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DeadZone"))
        {
            Invoke(nameof(ReturnObj), 1.0f);
        }
    }

    void ReturnObj()
    {
        if (pooling == null)
        {
            pooling = GameManager.Instance.Pooling;
        }
        pooling.SetPool(DicKey.fallRock, gameObject);
    }
}
