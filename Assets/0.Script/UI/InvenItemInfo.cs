using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InvenItemInfo : MonoBehaviour
{
    [SerializeField] TMP_Text itemTitleTxt;
    [SerializeField] TMP_Text itemPriceTxt;
    [SerializeField] TMP_Text itemInfoTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(InvenItem item)
    {
        itemTitleTxt.text = $"<{item.data.itemTitle}>";
        itemPriceTxt.text = $"가격 : {item.data.price} 골드";
        itemInfoTxt.text = item.data.itemExplain;
    }
}
