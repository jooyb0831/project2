using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class HPPotion : FieldItem
{   
    public int Recover {get;set;} = 10;
    private PlayerData pd;

    [SerializeField] GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        pd = GameManager.Instance.PlayerData;
    }

    
    public override bool ItemUseCheck()
    {
        if(pd == null)
        {
            pd=GameManager.Instance.PlayerData;
        }
        if(pd.HP==pd.MAXHP)
        {
            return false;;
        }
        else
        {
            return true;
        }

    }

    public override void UseItem()
    {
        if(p == null)
        {
            p =GameManager.Instance.Player;
        }
        
        Instantiate(effect, p.transform);

        if(pd.MAXHP-pd.HP>=Recover)
        {
            pd.HP+=Recover;
        }
        else
        {
            pd.HP = pd.MAXHP;
        }
        base.UseItem();
    }
}
