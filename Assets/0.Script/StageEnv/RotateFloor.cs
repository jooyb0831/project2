using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFloor : MonoBehaviour
{
    [SerializeField] float rotInterval;
    [SerializeField] bool isMoving = false;
    [SerializeField] bool isUp = true;
    [SerializeField] float timer;
    private float speed = 80f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        }

        else if(!isMoving)
        {
            timer += Time.deltaTime;
            if (timer >= rotInterval)
            {
                timer = 0;
                isMoving = true;
            }
        }

        float y = transform.rotation.eulerAngles.y;
        if(isUp)
        {
            if (y >= 180f)
            {
                isMoving = false;
                isUp = false;
                //speed = 0;
            }
        }
        else if (!isUp)
        {
            if(y >=270f)
            {
                transform.rotation = Quaternion.Euler(new Vector3 (0, 90, 0));
                isMoving = false;
                isUp = true;
            }
        }
    }
}
