using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvenItem : MonoBehaviour
{
    [SerializeField] Image itemBG;
    [SerializeField] Image itemIcon;
    [SerializeField] GameObject cntBG;
    [SerializeField] TMP_Text cntTxt;
    private Inventory inventory;

    public InvenData data;

    public void SetData(InvenData data)
    {
        this.data = data;
        itemIcon.sprite = data.iconSprite;
        itemBG.sprite = data.bgSprite;
        cntTxt.text = $"{data.count}";
        cntBG.SetActive(data.count <= 1 ? false : true);
    }

    public void SetInventory(Inventory inven)
    {
        inventory = inven;
    }

    public void ItemCntChange(InvenData data)
    {

        cntTxt.text = $"{data.count}";
        cntBG.SetActive(data.count <= 1 ? false : true);
        /*
        if (data.inQuickSlot)
        {
            data.qItem.ItemCntChange(this);
        }
        */
    }
}
