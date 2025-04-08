using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
    private Pooling pooling;
    // Start is called before the first frame update
    void Start()
    {
        pooling = GameManager.Instance.Pooling;
    }

    // Update is called once per frame
    void Update()
    {
        
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
