using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InvenData
{
    public Sprite iconSprite;
    public Sprite bgSprite;
    public ItemType type;
    public int count;
    public int itemIdx;
    public string itemTitle;
    public int price;
    public int slotIdx;
    public bool inQuickSlot = false;
    public int quickSlotIdx;
    public QuickInven qItem;
    public FieldItem fieldItem = null;
}
public enum ItemType
{
    Ore,
    Wood,
    Tool
}

public class InventoryData
{
    public int curInvenSlots = 10;
    public List<InvenItem> items = new();
    public bool invenFull = false;
}


public class Inventory : Singleton<Inventory>
{
    private PlayerData pd;
    private Player p;
    [SerializeField] InvenItem invenItem;
    public Transform[] invenSlots;
    public Transform[] quickSlots;

    public InventoryData inventoryData = new();
    public List<InvenItem> invenItems = new();
    private List<int> itemIdxList = new();
    public List<InvenData> invenDatas = new();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;
    }


    /// <summary>
    /// �κ��丮�� ������ �߰��ϴ� �Լ�(ItemData �μ�)
    /// </summary>
    /// <param name="itemData"></param>
    public void GetItem(ItemData itemData)
    {
        if (itemIdxList.Contains(itemData.itemIdx))
        {
            ItemCheck(itemData);
            Destroy(itemData.obj);
            return;
        }

        bool isFull = EmptySlotCheck();
        if (isFull)
        {
            itemData.obj.GetComponent<FieldItem>().InvenFull(isFull);
            return;
        }

        itemData.obj.GetComponent<FieldItem>().InvenFull(isFull);
        itemIdxList.Add(itemData.itemIdx);
        int index = SlotCheck();
        InvenItem item = Instantiate(invenItem, invenSlots[index]);
        invenSlots[index].GetComponent<Slot>().isFilled = true;

        InvenData data = new InvenData
        {
            itemTitle = itemData.itemTitle,
            iconSprite = itemData.invenIcon,
            bgSprite = itemData.bgSprite,
            type = itemData.type,
            count = itemData.count,
            price = itemData.price,
            //usage = itemData.usage,
            itemIdx = itemData.itemIdx,
            fieldItem = itemData.obj.GetComponent<FieldItem>(),
            slotIdx = index
        };
        item.SetData(data);
        item.SetInventory(this);
        invenItems.Add(item);
        invenDatas.Add(item.data);
        inventoryData.items.Add(item);
        Destroy(itemData.obj);
    }

    /// <summary>
    /// �κ��丮�� ������ �߰�(InvenItem �μ�)
    /// </summary>
    /// <param name="invenItem"></param>
    public void GetItem(InvenItem invenItem)
    {
        if (itemIdxList.Contains(invenItem.data.itemIdx))
        {
            ItemCheck(invenItem);
            return;
        }

        itemIdxList.Add(invenItem.data.itemIdx);
        int index = SlotCheck();
        InvenItem item = Instantiate(invenItem, invenSlots[index]);
        invenSlots[index].GetComponent<Slot>().isFilled = true;
        item.data.slotIdx = index;
        item.SetInventory(this);
        invenItems.Add(item);
        invenDatas.Add(item.data);
        inventoryData.items.Add(item);
    }

    /// <summary>
    /// ���� �ε���
    /// </summary>
    /// <returns></returns>
    int SlotCheck()
    {
        int number = -1;
        for (int i = 0; i < invenSlots.Length; i++)
        {
            if (!invenSlots[i].GetComponent<Slot>().isFilled
                && !invenSlots[i].GetComponent<Slot>().isLocked)
            {
                number = i;
                break;
            }
        }
        return number;
    }

    /// <summary>
    /// �� ���� üũ
    /// </summary>
    /// <returns></returns>
    public bool EmptySlotCheck()
    {
        bool isFull = false;
        for (int i = 0; i < invenSlots.Length; i++)
        {
            if (invenSlots[i] == null)
            {
                isFull = true;
                break;
            }
            if (!invenSlots[i].GetComponent<Slot>().isFilled)
            {
                isFull = false;
                break;
            }
            else
            {
                isFull = true;
            }

        }
        return isFull;
    }

    /// <summary>
    /// �ߺ������� üũ(ItemData)
    /// </summary>
    /// <param name="itemData"></param>
    void ItemCheck(ItemData itemData)
    {
        InvenItem invenItem = null;
        for (int i = 0; i < invenItems.Count; i++)
        {
            if (invenItems[i].data.itemIdx == itemData.itemIdx)
            {
                invenItem = invenItems[i];
                break;
            }
        }
        invenItem.data.count += itemData.count;
        invenItem.GetComponent<InvenItem>().ItemCntChange(invenItem.data);
        
        if (invenItem.data.inQuickSlot)
        {
            invenItem.data.qItem.ItemCntChange(invenItem);
        }
        
    }

    /// <summary>
    /// �ߺ������� üũ(InvenItem)
    /// </summary>
    /// <param name="item"></param>
    void ItemCheck(InvenItem item)
    {
        InvenItem invenItem = null;
        for (int i = 0; i < invenItems.Count; i++)
        {
            if (invenItems[i].data.itemIdx == item.data.itemIdx)
            {
                invenItem = invenItems[i];
                break;
            }
        }
        invenItem.data.count += item.data.count;
        invenItem.GetComponent<InvenItem>().ItemCntChange(invenItem.data);
        
        if (invenItem.data.inQuickSlot)
        {
            invenItem.data.qItem.ItemCntChange(invenItem);
        }
        
    }


    [SerializeField] QuickInven quickInvenSample;
    /// <summary>
    /// ������ ����(������)
    /// </summary>
    /// <param name="item"></param>
    public void ItemEquip(InvenItem item)
    {
        QuickSlot qSlot = null;
        for(int i =0; i<quickSlots.Length; i++)
        {
            if(!quickSlots[i].GetComponent<QuickSlot>().isFilled)
            {
                qSlot = quickSlots[i].GetComponent<QuickSlot>();
                break;
            }
        }
        QuickInven qItem = Instantiate(quickInvenSample, qSlot.transform);
        item.data.inQuickSlot = true;
        item.data.qItem = qItem;
        qItem.SetData(item);
        qItem.SetInvenItem(item);
        qSlot.GetComponent<QuickSlot>().isFilled = true;

        if(item.data.type.Equals(ItemType.Tool))
        {
            if(item.data.itemTitle.Equals("�콼 ��"))
            {
                Instantiate(item.data.fieldItem, p.swordPos);
            }
        }
    }

}
