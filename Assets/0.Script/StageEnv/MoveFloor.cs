using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField] bool moveDirX;
    [SerializeField] bool isBack;
    [SerializeField] float zPos1;
    [SerializeField] float zPos2;
    private float speed = 7f;

    void Update()
    {
        if(isBack)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        MoveCheck();

    }

    void MoveCheck()
    {
        float pos = 0f;
        if (moveDirX)
        {
            pos = transform.position.x;
        }
        else
        {
            pos = transform.position.z;
        }
        if (pos >= zPos1)
        {
            isBack = true;
        }
        else if (pos <= zPos2)
        {
            isBack = false;
        }

   

    }

    public void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player)
        {
            player.transform.SetParent(transform);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player)
        {
            player.transform.SetParent(null);
        }
    }

}
