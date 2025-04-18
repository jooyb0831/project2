using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantData
{
    public class SpeedEnchantData
    {
        public int EnchantLV {get;set;} = 1;
        public int NextStat {get;set;} = 5;
        public int NeedLv {get;set;} = 4;
        public int Gold {get;set;} = 15;
        public int ItemIdx {get;set;} = 12;
        public int ItemCnt {get;set;} = 2;
    }

    public class AttackEnchantData
    {
        public int EnchantLV {get;set;} = 1;
        public int NextStat {get;set;} = 3;
        public int NeedLv {get;set;} = 4;
        public int Gold {get;set;} = 15;
        public int ItemIdx { get; set; } = 10;
        public int ItemCnt { get; set; } = 5;
    }

    public class InvenEnchantData
    {
        public int EnchantLV { get; set; } = 1;
        public int NextStat { get; set; }
        public int NeedLv {get;set;} = 5;
        public int Gold { get; set; } = 15;
        public int ItemIdx { get; set; } = 9;
        public int ItemCnt { get; set; } = 3;
    }

    public SpeedEnchantData SPDdata = new SpeedEnchantData();
    public AttackEnchantData ATKdata = new AttackEnchantData();
    public InvenEnchantData InvenENdata = new InvenEnchantData();
}
public class EnchantSystem : Singleton<EnchantSystem>
{
    private PlayerData pd;
    private InventoryData inventoryData;
    public EnchantData data = new EnchantData();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;
        inventoryData = GameManager.Instance.Inven.inventoryData;
        Init();
    }

    void Init()
    {
        data.InvenENdata.NextStat = inventoryData.curInvenSlots + 1;
    }

    public void SpeedEnchant()
    {
        data.SPDdata.EnchantLV ++;
        data.SPDdata.NextStat ++;
        data.SPDdata.NeedLv ++;
        data.SPDdata.Gold += 10;

        if(data.SPDdata.EnchantLV > 3)
        {
            data.SPDdata.ItemCnt = 5;
        }
    }

    public void ATKEnchant()
    {
        data.ATKdata.EnchantLV ++;
        data.ATKdata.NextStat ++;
        data.ATKdata.NeedLv ++;
        data.ATKdata.Gold += 15;

        if(data.ATKdata.EnchantLV>5)
        {
            data.ATKdata.ItemCnt = 8;
        }
    }

    public void InvenEnchant()
    {
        data.InvenENdata.NextStat ++;
        data.InvenENdata.NeedLv +=2;
        data.InvenENdata.Gold += 20;
        data.InvenENdata.ItemCnt +=3;
    }
}
