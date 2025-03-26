using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Player p;
    private SceneChanger sc;
    private Vector3 camPos;
    private Vector3 targetPos;

    private float dist;

    [SerializeField] GameObject uiObj;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        sc = GameManager.Instance.SceneChanger;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, p.transform.position);

        if(dist < 2.5f)
        {
            uiObj.SetActive(true);
            camPos = Camera.main.transform.position;
            targetPos = new Vector3(camPos.x, transform.position.y, camPos.z);
            uiObj.transform.LookAt(targetPos);

            if(Input.GetKeyDown(KeyCode.E))
            {
                sc.GoLobby();
            }
        }
        else
        {
            uiObj.SetActive(false);
        }
        
    }

}
