using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : MonoBehaviour
{
    //발사되는 화살 트랩을 담을 배열.
    [SerializeField] ArrowTrap[] arrowTraps;
    
    //발사되었는지 여부 체크
    bool isTriggered = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///
    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if(player)
        {
            //트리거존을 지나면 화살 발사
            //이미 트리거 된 상태라면 더이상 발사 안 되게
            if(!isTriggered)
            {
                for(int i = 0; i<arrowTraps.Length; i++)
                {
                    arrowTraps[i].gameObject.SetActive(true);
                }
                isTriggered = true;
            }
            
        }
    }
}
