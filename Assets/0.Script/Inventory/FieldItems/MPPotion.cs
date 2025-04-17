using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPPotion : FieldItem
{
public int Recover { get; set; } = 10;

    [SerializeField] GameObject effect;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
    }

    public override bool ItemUseCheck()
    {
        if(pd == null)
        {
            pd = GameManager.Instance.PlayerData;
        }
        
        return pd.CURMP != pd.MAXMP;
    }

    public override void UseItem()
    {
        if (p == null)
        {
            p = GameManager.Instance.Player;
        }

        Instantiate(effect, p.transform);

        if (pd.MAXMP - pd.CURMP >= Recover)
        {
            pd.CURMP += Recover;
        }
        else
        {
            pd.CURMP = pd.MAXMP;
        }
        base.UseItem();
    }
}
