using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnchantSample : MonoBehaviour
{
    protected PlayerData pd;
    protected GameUI gameUI;
    protected Inventory inven;
    protected EnchantSystem enchantSystem;

    protected int needGold;
    protected int needLevel;
    protected int needItemCnt;
    protected int curItemCnt;


    [SerializeField] protected TMP_Text curStatTxt;
    [SerializeField] protected TMP_Text upStatTxt;
    [SerializeField] protected TMP_Text needLvTxt;
    [SerializeField] protected TMP_Text goldTxt;
    [SerializeField] protected TMP_Text itemTxt; 

    void Start()
    {
        Init();
    }

    void Update()
    {
        needLvTxt.color = (pd.Level < needLevel) ? Color.red : Color.white;
        goldTxt.color = (pd.Gold < needGold) ? Color.red : Color.white;
        itemTxt.color = (curItemCnt < needItemCnt) ? Color.red : Color.white;
    }

    public virtual void Init()
    {
        pd = GameManager.Instance.PlayerData;
        gameUI = GameManager.Instance.GameUI;
        inven = GameManager.Instance.Inven;
        enchantSystem = GameManager.Instance.EnchantSystem;
    }

    protected void UseResource()
    {
        pd.Gold -= needGold;
        InvenItem item = inven.FindItem(enchantSystem.data.SPDdata.ItemIdx);
        inven.InvenItemCntChange(item, -needItemCnt);
    }
    
}
