using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlimeFloor : MonoBehaviour
{

    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision.transform.parent.parent.GetComponent<Player>();
            player.ChangeSpeed();
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision.transform.parent.parent.GetComponent<Player>();
            player.ResetSpeed();
        }
    }
}
