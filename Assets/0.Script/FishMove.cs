using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float stayTime; //머무를 시간

    //X와 Z좌표의 제한값
    private const float X_POS = 13f;
    private const float Z_POS = 7f;

    //Y좌표 고정값
    private const float Y_POS = -0.4f;

    //물고기가 움직이는 중인지 체크하는 Bool
    public bool isMoving;

    //목적지 좌표를 담을 변수 
    private Vector3 dest;

    void Start()
    {
        dest = SetMoveDestination();
    }

    void Update()
    {
        Move();
    }

    /// <summary>
    /// 물고기의 움직임
    /// </summary>
    void Move()
    {
        //움직이는 중이라면
        if(isMoving)
        {   
            //목적지를 쳐다보게 함
            transform.LookAt(dest);

            //목적지로 이동
            transform.position = Vector3.MoveTowards(transform.position, dest, 
                                                    Time.deltaTime * speed);

            //목적지에 도달하였으면 움직임 종료
            if (transform.position == dest)
            {
                isMoving = false;
            }
        }

        //움직이지 않으면(정지상태)
        else if (!isMoving)
        {
            //머무르는 시간 카운트 시작
            timer += Time.deltaTime;

            //머무르는 시간 경과시
            if (timer >= stayTime)
            {
                //다음 목적지 정함
                dest = SetMoveDestination();
                timer = 0;
            }
        }

    }

    /// <summary>
    /// 랜덤한 목적지 좌표를 반환하는 함수
    /// </summary>
    /// <returns></returns>
    Vector3 SetMoveDestination()
    {
        //움직이는 상태로 변경
        isMoving = true;
        
        //랜덤한 float 받아서 좌표 설정
        float x = Random.Range(-X_POS, X_POS);
        float z = Random.Range(-Z_POS, Z_POS);

        return new Vector3(x, Y_POS, z);
    }
}
