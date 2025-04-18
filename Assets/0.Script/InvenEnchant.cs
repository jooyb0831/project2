using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenEnchant : EnchantSample
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
        needGold = enchantSystem.data.InvenENdata.Gold;
        needLevel = enchantSystem.data.InvenENdata.NeedLv;
        needItemCnt = enchantSystem.data.InvenENdata.ItemCnt;
        curItemCnt = inven.FindItemCnt(enchantSystem.data.InvenENdata.ItemIdx);

        curStatTxt.text = $"{inven.inventoryData.curInvenSlots}";
        upStatTxt.text = $"{enchantSystem.data.InvenENdata.NextStat}";
        needLvTxt.text = $"{pd.Level} / {enchantSystem.data.InvenENdata.NeedLv}";
        goldTxt.text = $"{pd.Gold} / {enchantSystem.data.InvenENdata.Gold}";
        int itemCnt = inven.FindItemCnt(enchantSystem.data.InvenENdata.ItemIdx);
        itemTxt.text = $"{itemCnt} / {enchantSystem.data.InvenENdata.ItemCnt}";
    }

    public void OnClicked()
    {
        if (pd.Gold >= needGold && pd.Level >= needLevel && curItemCnt >= needItemCnt)
        {
            inven.inventoryData.curInvenSlots = enchantSystem.data.InvenENdata.NextStat;
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
