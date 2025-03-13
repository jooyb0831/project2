using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider collision)
    {
        Player player = collision.GetComponent<Player>();

        if(player)
        {
            player.ChangeSpeed();
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Player player = collision.GetComponent<Player>();

        if(player)
        {
            player.ResetSpeed();
        }
    }
}
