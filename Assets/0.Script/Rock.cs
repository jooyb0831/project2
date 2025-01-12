using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] int hitCnt = 12;
    [SerializeField] int curHit = 0;
    [SerializeField] GameObject stone;
    [SerializeField] Transform area;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(curHit>=hitCnt)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PickAxe>())
        {
            curHit += 1;
            if (curHit > 0)
            {
                if (curHit % 3 == 0)
                {
                    //Pooling으로 처리하기
                    GameObject obj = Instantiate(stone, area);
                    obj.transform.SetParent(null);
                }
            }
        }
    }
}
