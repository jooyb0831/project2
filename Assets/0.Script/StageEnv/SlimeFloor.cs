using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlimeFloor : MonoBehaviour
{

    void OnTriggerEnter(Collider collision)
    {
        //플레이어와 접촉했을 경우
        if(collision.CompareTag("Player"))
        {
            Player player = collision.transform.parent.parent.GetComponent<Player>();
            player.ChangeSpeed(); //속도 변하게
        }
    }

    void OnTriggerExit(Collider collision)
    {   
        //플레이어가 벗어났을 경우
        if(collision.CompareTag("Player"))
        {
            Player player = collision.transform.parent.parent.GetComponent<Player>();
            player.ResetSpeed(); //속도 원래대로
        }
    }
}
