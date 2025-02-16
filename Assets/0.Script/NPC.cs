using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Player p;
    [SerializeField] GameObject textObj;
    float dist;
    Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, p.transform.position);
        
        targetPos = new Vector3(p.transform.position.x, transform.position.y, p.transform.position.z);
        transform.LookAt(targetPos);

        if (dist<2f)
        {
            textObj.SetActive(true);
        }
        else
        {
            textObj.SetActive(false);
        }

    }
}
