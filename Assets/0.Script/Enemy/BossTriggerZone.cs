using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerZone : MonoBehaviour
{
    [SerializeField] BossEnemy boss;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            boss.TriggerBoss();
            GetComponent<BoxCollider>().enabled = false;
        }   
    }
}
