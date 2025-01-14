using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isFilled;
    public bool isLocked;
    public int indexNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocked)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
