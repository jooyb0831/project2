using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stage1Mgr : MonoBehaviour
{
    [SerializeField] Transform stage1Enemies;
    public bool isStage1Cleared;
    [SerializeField] GameObject portal;
    [SerializeField] GameObject box;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(stage1Enemies.childCount==0)
        {
            isStage1Cleared = true;
        }

        if (isStage1Cleared)
        {
            if (portal.activeSelf)
            {
                return;
            }
            Reward();
        }
    }


    public void Reward()
    {
        portal.SetActive(true);
    }
}
