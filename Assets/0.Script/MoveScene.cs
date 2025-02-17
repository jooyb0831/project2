using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    private Player p;
    private SceneChanger sc;

    [SerializeField] GameObject txtObj;

    private float dist;
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

        if(dist<5f)
        {
            txtObj.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                sc.GoStage1();
            }
        }
        else
        {
            txtObj.SetActive(false);
        }
    }
}
