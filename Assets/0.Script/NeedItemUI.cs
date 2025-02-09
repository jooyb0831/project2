using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NeedItemUI : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemTitle;
    [SerializeField] TMP_Text itemCntTxt;
    int needCnt;
    int curCnt;

    private ItemData data;
    private Inventory inven;
    // Start is called before the first frame update
    void Start()
    {
        inven = GameManager.Instance.Inven;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int FindInvenCnt(int itemCodeIdx)
    {
        return inven.FindItem(itemCodeIdx).data.count;
    }

    public void SetData(ItemData data, int cnt)
    {
        this.data = data;
        //int curCnt = FindInvenCnt(data.itemIdx);
        itemIcon.sprite = data.invenIcon;
        itemTitle.text = data.itemTitle;
        itemCntTxt.text = $"{curCnt} / {cnt}";
    }
}
