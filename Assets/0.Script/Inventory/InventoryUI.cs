using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] Transform[] slots;
    [SerializeField] Transform[] quickSlots;
    [SerializeField] Inventory inventory;
    [SerializeField] QuickInven quickItem;
    [SerializeField] InvenItem sampleInvenItem;
    [SerializeField] PlayerData pd;
    [SerializeField] MoveItem moveitem;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        SetInventory();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            quickSlots[0].GetComponent<Toggle>().isOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            quickSlots[1].GetComponent<Toggle>().isOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            quickSlots[2].GetComponent<Toggle>().isOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            quickSlots[3].GetComponent<Toggle>().isOn = true;
        }
    }

    public void Init()
    {
        if (inventory == null)
        {
            inventory = GameManager.Instance.Inven;
        }

        if (pd == null)
        {
            pd = GameManager.Instance.PlayerData;
        }
        inventory.moveItem = this.moveitem;
        SetInvenSlot();
        InventoryCheck();

    }


    void SetInvenSlot()
    {
        int curSlotNum = inventory.inventoryData.curInvenSlots;

        for (int i = 0; i < curSlotNum; i++)
        {
            inventory.invenSlots[i] = slots[i];
        }

        if (curSlotNum > 10)
        {
            int gap = curSlotNum - 10;

            for (int i = 1; i < gap + 1; i++)
            {
                slots[i + 9].GetComponent<Slot>().isLocked = false;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            inventory.quickSlots[i] = quickSlots[i];
        }
    }
    void SetInventory()
    {
        if (inventory == null)
        {
            inventory = GameManager.Instance.Inven;
        }
        inventory.invenItems.Clear();
        List<InvenData> invenData = inventory.invenDatas;
        if (invenData.Count == 0)
        {
            return;
        }
        for (int i = 0; i < invenData.Count; i++)
        {
            SetData(invenData[i]);
        }

        /*
        SceneType type = SceneChanger.Instance.sceneType;
        if (type.Equals(SceneType.Ship) || type.Equals(SceneType.Stage1)
            || type.Equals(SceneType.Stage2) || type.Equals(SceneType.Stage3))
        {
            Inventory.Instance.quickSlot = quickSlot;
        }
        else
        {
            return;
        }
        */
    }

    void SetData(InvenData data)
    {
        InvenItem item = null;
        item = Instantiate(sampleInvenItem, slots[data.slotIdx]);
        item.SetData(data);
        item.SetInventory(inventory);
        item.transform.parent.gameObject.GetComponent<Slot>().isFilled = true;
        inventory.invenItems.Add(item);


        if (data.inQuickSlot)
        {
            QuickInven quickItem = null;
            quickItem = Instantiate(quickItem, quickSlots[data.quickSlotIdx]);
            quickItem.SetData(item);
            quickItem.SetInvenItem(item);
            quickSlots[data.quickSlotIdx].GetComponent<QuickSlot>().isFilled = true;
            data.qItem = quickItem;
        }

    }


    void InventoryCheck()
    {
        foreach (var item in slots)
        {
            if (item.childCount >= 1)
            {
                item.GetComponent<Slot>().isFilled = true;
            }
        }
    }
}
