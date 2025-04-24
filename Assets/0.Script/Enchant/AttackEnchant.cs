using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnchant : EnchantSample
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
        needGold = enchantSystem.data.ATKdata.Gold;
        needLevel = enchantSystem.data.ATKdata.NeedLv;
        needItemCnt = enchantSystem.data.ATKdata.ItemCnt;
        curItemCnt = inven.FindItemCnt(enchantSystem.data.ATKdata.ItemIdx);

        curStatTxt.text = $"{pd.BasicAtk}";
        upStatTxt.text = $"{enchantSystem.data.ATKdata.NextStat}";
        needLvTxt.text = $"{pd.Level} / {enchantSystem.data.ATKdata.NeedLv}";
        goldTxt.text = $"{pd.Gold} / {enchantSystem.data.ATKdata.Gold}";
        int itemCnt = inven.FindItemCnt(enchantSystem.data.ATKdata.ItemIdx);
        itemTxt.text = $"{itemCnt} / {enchantSystem.data.ATKdata.ItemCnt}";
    }

    public void OnClicked()
    {
        if (pd.Gold >= needGold && pd.Level >= needLevel && curItemCnt >= needItemCnt)
        {
            pd.BasicAtk = enchantSystem.data.ATKdata.NextStat;
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
