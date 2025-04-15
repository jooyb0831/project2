using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float stayTime; //머무를 시간

    private const float X_POS = 13f;
    private const float Z_POS = 7f;
    public bool isMoving;
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
            transform.LookAt(dest);
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
        float x = Random.Range(-X_POS, X_POS);
        float z = Random.Range(-Z_POS, Z_POS);
        return new Vector3(x, -0.4f, z);
    }
}
