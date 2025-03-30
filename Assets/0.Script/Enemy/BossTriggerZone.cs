using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerZone : MonoBehaviour
{
    [SerializeField] Enemy boss;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            if(boss.gameObject.GetComponent<BossEnemy>())
            {
                boss.gameObject.GetComponent<BossEnemy>().TriggerBoss();
            }
            else if (boss.gameObject.GetComponent<Enemy8>())
            {
                boss.gameObject.GetComponent<Enemy8>().TriggerBoss();
            }
            
            GetComponent<BoxCollider>().enabled = false;
        }   
    }
}
