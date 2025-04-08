using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolData
{
    public string toolNmae{get;set;}
    public int useST{get;set;}
    public int lv{get;set;}
}

public class Tool : MonoBehaviour
{
    protected Player p;
    protected PlayerData pd;
    [SerializeField] protected GameObject tool;
    public GameObject obj;
    public bool isEquiped;
    public ToolData data = new ToolData();


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;

        isEquiped = false;
    }

    public virtual void SetTool()
    {
        if (p == null)
        {
            p = GameManager.Instance.Player;
        }
        isEquiped = true;
        obj = this.gameObject;
        if(!obj.activeSelf)
        {
            obj.SetActive(true);
        }
    }

    private void Update()
    {
        
    }



}
