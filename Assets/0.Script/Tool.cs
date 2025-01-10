using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    protected Player p;
    [SerializeField] protected GameObject tool;
    public GameObject obj;
    public bool isEquiped;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        p = GameManager.Instance.Player;

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
