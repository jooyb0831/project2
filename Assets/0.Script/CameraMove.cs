using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Player p;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        CameraView();
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

        Vector3 vec = p.transform.position;
        vec.y = p.transform.position.y+1.7f;
        transform.position = vec;

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

}
