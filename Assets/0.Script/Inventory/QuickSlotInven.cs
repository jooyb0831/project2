using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotInven : MonoBehaviour
{
    public bool isInven;
    public int quickSlotIdx;
    public bool isFilled;
    public bool isShopSlot;
    public QuickSlot quickSlot;
    [SerializeField] QuickInven quickItem;
    [SerializeField] QuickInven quickInvenSample;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(InvenItem item)
    {
        item.data.inQuickSlot = true;
        quickItem = Instantiate(quickInvenSample, quickSlot.transform);
        quickItem.SetData(item);
        quickItem.SetInvenItem(item);
        quickSlot.isFilled = true;
        quickItem.transform.SetAsFirstSibling();
        item.data.qItem = quickItem;
    }

    public void RemoveItem(InvenItem item)
    {
        isFilled = false;
        item.data.inQuickSlot = false;
        item.data.qItem = null;
        if(quickSlot!=null)
        {
            quickSlot.isFilled = false;
        }
        if(quickItem!=null)
        {
            Destroy(quickItem.gameObject);
        }

    }

    public void TransferItem(InvenItem item)
    {
        isFilled = false;
    }
}
