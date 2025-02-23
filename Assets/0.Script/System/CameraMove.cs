using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Player p;

    //회전속도
    [SerializeField] float rotSpeed;
    //이동속도
    [SerializeField] float camSpeed;

    //최대거리
    [SerializeField] float maxDist;

    //장애물 있을 때 거리
    [SerializeField] float obstacleDist;

#region 코드 내 활용 변수
    //카메라의 거리
    private float camDist;

    //포지션과 회전
    private Vector3 pos;
    private Vector3 rot;
#endregion


    //https://rito15.github.io/posts/unity-fps-tps-character/
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        camDist = maxDist;
    }

    void Update()
    {
        //장애물에 부딪혔을 경우 처리되는 코드
        Obstacle();
        pos = p.transform.position - transform.forward * camDist;
        rot = new Vector3(this.transform.rotation.eulerAngles.x, p.transform.rotation.eulerAngles.y, 0);
        pos.y += 1f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraView();
        
        //오른쪽 마우스 누르고 있으면 줌인
        if(Input.GetMouseButton(1))
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    /// <summary>
    /// 카메라 작동 매커니즘
    /// </summary>
    void CameraView()
    {
        //마우스 인풋값을 float으로 받음
        float mX = Input.GetAxis("Mouse X");
        float mY = Input.GetAxis("Mouse Y");

        //회전
        transform.Rotate(Vector3.up, mX * 5f, Space.World);
        transform.Rotate(Vector3.left, mY * 5f, Space.Self);

        Vector3 curRotation = transform.eulerAngles;
        curRotation.x = ClampAngle(curRotation.x, -25f, 25f);
        curRotation.z = 0;
        transform.localRotation = Quaternion.Euler(curRotation);

        p.transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0);
        pos = p.transform.position - transform.forward * camDist;
        rot = new Vector3(this.transform.rotation.eulerAngles.x, p.transform.rotation.eulerAngles.y, 0);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(rot), rotSpeed * Time.deltaTime);
        pos.y += 1.8f;
        transform.position = Vector3.Lerp(transform.position, pos, camSpeed * Time.deltaTime);

        /*
        Vector3 vec = p.transform.position;
        vec.z = p.transform.position.z - 0.5f;
        vec.y = p.transform.position.y + 1.8f;
        transform.position = vec;
        */
    }


    /// <summary>
    /// 회전각 제한
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    float ClampAngle(float angle, float min, float max)
    {
        angle = NormalizeAngle(angle);
        if (angle > 180) angle -= 360;
        else if (angle < -180) angle += 360;

        min = NormalizeAngle(min);
        if (min > 180) min -= 360;
        else if (min < -180) min += 360;

        max = NormalizeAngle(max);
        if (max > 180) max -= 360;
        else if (max < -180) max += 360;

        return Mathf.Clamp(angle, min, max);
    }
    
    /// <summary>
    /// 각도 변경
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    float NormalizeAngle(float angle)
    {
        while (angle > 360)
            angle -= 360;
        while (angle < 0)
            angle += 360;

        return angle;
    }

    //https://hustlehustle.tistory.com/22
    /// <summary>
    /// 카메라가 장애물(벽)에 부딪혔을 경우
    /// </summary>
    void Obstacle()
    {
        RaycastHit hit;
        Vector3 dir = transform.position - p.transform.position;
        Debug.DrawRay(p.transform.position, dir.normalized * dir.magnitude, Color.red);
        if(Physics.Raycast(p.transform.position, dir.normalized, out hit, dir.magnitude, LayerMask.GetMask("Wall")))
        {
            //Debug.Log(hit.transform.name);
            Vector3 dist = hit.point - p.transform.position;
            camDist = dist.magnitude * obstacleDist;
        }
        else
        {
            camDist = Mathf.Clamp(camDist + camSpeed * Time.deltaTime , 1f, maxDist);
        }
    }


    public float zoomLevel = 20f;
    private bool isZoomed = false;

    /// <summary>
    /// 줌인
    /// </summary>
    void ZoomIn()
    {
        Camera.main.fieldOfView = zoomLevel;
        isZoomed = true;

        //줌 인하면 플레이어 안 보이게
        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("Player"));
    }

    /// <summary>
    /// 줌아웃
    /// </summary>
    void ZoomOut()
    {
        Camera.main.fieldOfView = 60f;
        isZoomed = false;
        Camera.main.cullingMask = -1;
    }

}
