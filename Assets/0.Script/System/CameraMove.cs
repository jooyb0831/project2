using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Player p;
    [SerializeField] float rotSpeed;
    [SerializeField] float camSpeed;
    [SerializeField] float maxDist;
    float camDist;
    Vector3 pos;
    Vector3 rot;


    //https://rito15.github.io/posts/unity-fps-tps-character/
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        camDist = maxDist;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraView();

        if(Input.GetMouseButton(1))
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }


    void CameraView()
    {

        float mX = Input.GetAxis("Mouse X");
        float mY = Input.GetAxis("Mouse Y");

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

    float NormalizeAngle(float angle)
    {
        while (angle > 360)
            angle -= 360;
        while (angle < 0)
            angle += 360;

        return angle;
    }

    //https://hustlehustle.tistory.com/22
    void Obstacle()
    {
        RaycastHit hit;
        Vector3 dir = transform.position - p.transform.position;
        Debug.DrawRay(p.transform.position, dir.normalized * dir.magnitude, Color.red);
        if(Physics.Raycast(p.transform.position, dir.normalized, out hit, dir.magnitude))
        {
            Debug.Log(hit.transform.name);
        }
    }


    public float zoomLevel = 20f;
    private bool isZoomed = false;

    void ZoomIn()
    {
        Camera.main.fieldOfView = zoomLevel;
        isZoomed = true;
        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("Player"));
    }

    void ZoomOut()
    {
        Camera.main.fieldOfView = 60f;
        isZoomed = false;
        Camera.main.cullingMask = -1;
    }

}
