using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float stayTime; //머무를 시간
    [SerializeField] bool isMoving;
    [SerializeField] Vector3 dest;
    // Start is called before the first frame update
    void Start()
    {
        dest = SetMoveDestination();
    }

    // Update is called once per frame
    void Update()
    {
        //n초마다 랜덤 목적지 정하고 움직임
        //목적지에 도달하면 n초 대기 후 다음 목적지 정하기
        Move();

        if(!isMoving)
        {
            timer += Time.deltaTime;
            if (timer >= stayTime)
            {
                dest = SetMoveDestination();
                timer = 0;
            }
        }

        
    }

    void Move()
    {
        if(isMoving)
        {
            //transform.LookAt(dest);
            transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime * speed);

            if (transform.position == dest)
            {
                isMoving = false;
            }
        }

    }

    Vector3 SetMoveDestination()
    {
        isMoving = true;
        float x = Random.Range(-13f, 13f);
        float z = Random.Range(-7f, 7f);
        return new Vector3(x, 0, z);
    }
}
