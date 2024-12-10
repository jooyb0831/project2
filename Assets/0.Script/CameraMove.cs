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
        Vector3 vec = p.transform.position;
        vec.y = 1.8f;

        transform.position = vec;
    }
}
