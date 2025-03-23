using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTriggerZone : MonoBehaviour
{
    [SerializeField] Enemy5 enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            enemy.isAwaken = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
