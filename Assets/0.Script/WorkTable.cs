using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkTable : MonoBehaviour
{
    private Player p;
    private SceneChanger sc;
    [SerializeField] GameObject txtObj;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        sc = GameManager.Instance.SceneChanger;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, p.transform.position);

        if(dist<2f)
        {
            txtObj.SetActive(true);

            if(Input.GetKeyDown(KeyCode.F))
            {
                sc.GoCraftUI();
            }
        }
        else
        {
            txtObj.SetActive(false);
        }
    }
}
