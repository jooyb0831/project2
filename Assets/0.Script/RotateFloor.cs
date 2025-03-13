using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFloor : MonoBehaviour
{
    [SerializeField] float rotInterval;
    private float timer;
    private float speed = 80f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}
