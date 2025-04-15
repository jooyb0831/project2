using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrilledFish : FieldItem
{
    public int Recover {get;set;} = 10;

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
        if (pd == null)
        {
            pd = GameManager.Instance.PlayerData;
        }

        if (pd.ST == pd.MAXST)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override void UseItem()
    {
        if (p == null)
        {
            p = GameManager.Instance.Player;
        }

        if (pd.MAXST - pd.ST >= Recover)
        {
            pd.ST += Recover;
        }
        else
        {
            pd.ST = pd.MAXST;
        }
        base.UseItem();
    }
}
