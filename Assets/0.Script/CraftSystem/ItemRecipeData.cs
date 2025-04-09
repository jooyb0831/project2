using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRecipeData : MonoBehaviour
{
    public string ItemTitle { get; set; }
    public string ItemExplain { get; set; }
    public int NeedLv { get; set; }

    public Sprite ItemIcon;

    public FieldItem[] Resources;

    public int[] RcCnts;

    public GameObject Item;


    public void SetData()
    {
        ItemTitle = Item.GetComponent<FieldItem>().itemData.itemTitle;
        ItemExplain = Item.GetComponent<FieldItem>().itemData.itemExplain;
    }
}
