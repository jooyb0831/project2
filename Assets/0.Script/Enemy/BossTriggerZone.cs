using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerZone : MonoBehaviour
{
    [SerializeField] Enemy boss;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(boss.gameObject.GetComponent<BossEnemy>())
            {
                boss.gameObject.GetComponent<BossEnemy>().TriggerBoss();
            }
            else if (boss.gameObject.GetComponent<Enemy8>())
            {
                boss.gameObject.GetComponent<Enemy8>().TriggerBoss();
            }
            else if (boss.gameObject.GetComponent<Enemy1>())
            {
                boss.gameObject.GetComponent<Enemy1>().isTriggered = true;
            }
            
            GetComponent<BoxCollider>().enabled = false;
        }   
    }
}
