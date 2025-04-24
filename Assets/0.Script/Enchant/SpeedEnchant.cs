using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEnchant : EnchantSample
{
    void Start()
    {
        Init();
        SetData();
    }

    public override void Init()
    {
        base.Init();
    }

    public void SetData()
    {
        Init();
        needGold = enchantSystem.data.SPDdata.Gold;
        needLevel = enchantSystem.data.SPDdata.NeedLv;
        needItemCnt = enchantSystem.data.SPDdata.ItemCnt;
        curItemCnt = inven.FindItemCnt(enchantSystem.data.SPDdata.ItemIdx);

        curStatTxt.text = $"{pd.Speed}";
        upStatTxt.text = $"{enchantSystem.data.SPDdata.NextStat}";
        needLvTxt.text = $"{pd.Level} / {enchantSystem.data.SPDdata.NeedLv}";
        goldTxt.text = $"{pd.Gold} / {enchantSystem.data.SPDdata.Gold}";
        int itemCnt = inven.FindItemCnt(enchantSystem.data.SPDdata.ItemIdx);
        itemTxt.text = $"{itemCnt} / {enchantSystem.data.SPDdata.ItemCnt}";
    }

    public void OnClicked()
    {
        if (pd.Gold >= needGold && pd.Level >= needLevel && curItemCnt >= needItemCnt)
        {
            pd.Speed = enchantSystem.data.SPDdata.NextStat;
            UseResource();
            enchantSystem.SpeedEnchant();
            SetData();
        }
        else
        {
            if (pd.Gold < needGold)
            {
                gameUI.DisplayInfo(2);
            }

            else if (pd.Level < needLevel)
            {
                gameUI.DisplayInfo(10);
            }

            else if (curItemCnt < needItemCnt)
            {
                gameUI.DisplayInfo(3);
            }
        }
    }
}
