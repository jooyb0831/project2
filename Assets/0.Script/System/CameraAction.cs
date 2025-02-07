using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAction : MonoBehaviour
{
    public GameObject target;

    public float offsetX = 0;
    public float offsetY = 10f;
    public float offsetZ = -10f;

    public float angleX = 0;
    public float angleY = 0;
    public float angleZ = 0;
    public float camSpeed = 10f;
    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = new Vector3(target.transform.position.x + offsetX, 
            target.transform.position.y + offsetY, 
            target.transform.position.z + offsetX);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime*camSpeed);
    }
}
