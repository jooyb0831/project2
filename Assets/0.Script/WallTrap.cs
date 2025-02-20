using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : MonoBehaviour
{
    [SerializeField] ArrowTrap[] arrowTraps;
    bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if(player)
        {
            if(!isTriggered)
            {
                for(int i = 0; i<arrowTraps.Length; i++)
                {
                    arrowTraps[i].gameObject.SetActive(true);
                };
                isTriggered = true;
            }
            
        }
    }
}
