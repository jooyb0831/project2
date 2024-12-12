using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
            /*
            if (StatUI.Instance != null)
            {
                StatUI.Instance.Speed = (int)speed;
            }
            */
        }
    }

    public float RunSpeed { get; set; } = 6f;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Speed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
