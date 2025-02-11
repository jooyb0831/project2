using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkTable : MonoBehaviour
{
    private Player p;
    private SceneChanger sc;
    [SerializeField] GameObject txtObj;
    Vector3 targetPos;
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
        targetPos = new Vector3(txtObj.transform.position.x, txtObj.transform.position.y, p.transform.position.z);
        txtObj.transform.LookAt(targetPos);

        if(dist<2f)
        {
            txtObj.SetActive(true);

            if(Input.GetKeyDown(KeyCode.F))
            {
                //sc.GoCraftUI();
                GameManager.Instance.isPaused = true;
                Camera.main.GetComponent<CameraMove>().enabled = false;
                CraftUI.Instance.EnableWindow();
                
            }
        }
        else
        {
            txtObj.SetActive(false);
        }
    }
}
