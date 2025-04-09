using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    [SerializeField] Transform[] slots;
    [SerializeField] Transform[] quickSlots;
    [SerializeField] Transform[] quickSlotsInven;
    [SerializeField] Transform weaponslot;
    [SerializeField] Transform bowSlot;
    [SerializeField] Inventory inventory;
    [SerializeField] QuickInven quickItem;
    [SerializeField] InvenItem sampleInvenItem;
    [SerializeField] PlayerData pd;
    [SerializeField] MoveItem moveitem;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("check");
        Init();
        SetInventory();
    }

    public void Update()
    {
        WheelScroll();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectItem(3);
        }

    }


    /// <summary>
    /// 휠스크롤 아이템 선택 변경 코드
    /// </summary>
    [SerializeField] int selectedIdx = 0;
    void WheelScroll()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");

        //휠 인풋값이 0보다 클경우 : 위로 올릴 때
        if (wheelInput > 0)
        {
            selectedIdx++;
            if (selectedIdx > 3)
            {
                selectedIdx = 0;
            }
            SelectItem(selectedIdx);
        }

        //휠 아래로 내릴 때
        else if (wheelInput < 0)
        {
            selectedIdx--;
            if (selectedIdx < 0)
            {
                selectedIdx = 3;
            }
            SelectItem(selectedIdx);
        }
    }

    /// <summary>
    /// 아이템 선택-토글활성화
    /// </summary>
    /// <param name="idx">퀵슬롯 인덱스</param>
    void SelectItem(int idx)
    {
        //모든 퀵슬롯의 토글 isOn 다 끄기
        foreach (var item in quickSlots)
        {
            item.GetComponent<Toggle>().isOn = false;
        }
        
        //현재 선택된 인덱스의 토글만 isOn을 True로
        quickSlots[idx].GetComponent<Toggle>().isOn = true;
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


    /// <summary>
    /// 인벤토리 세팅(InvenUi-Inventory연결)
    /// </summary>
    void SetInvenSlot()
    {
        //현재 해제된 인벤 슬롯의 개수
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

        //숫자 4 : 퀵슬롯 인덱스
        for (int i = 0; i < 4; i++)
        {
            inventory.quickSlotsInven[i] = quickSlotsInven[i];
        }

        inventory.weaponSlot = this.weaponslot;
        inventory.bowSlot = this.bowSlot;

    }

    /// <summary>
    /// 인벤토리 세팅
    /// </summary>
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

    /// <summary>
    /// 인벤아이템 세팅, 데이터 세팅
    /// </summary>
    /// <param name="data"></param>
    void SetData(InvenData data)
    {
        InvenItem item = null;
        //만약 item이 퀵슬롯에 있다면
        if (data.inQuickSlot)
        {
            item = Instantiate(sampleInvenItem, quickSlotsInven[data.quickSlotIdx]);
            item.transform.parent.GetComponent<QuickSlotInven>().isFilled = true;
            item.SetData(data);
            item.SetInventory(inventory);
            inventory.invenItems.Add(item);
            QuickInven quickInvenItem = Instantiate(quickItem, quickSlots[data.quickSlotIdx]);
            quickInvenItem.transform.SetAsFirstSibling();
            quickInvenItem.SetData(item);
            quickInvenItem.SetInvenItem(item);
            quickSlots[data.quickSlotIdx].GetComponent<QuickSlot>().isFilled = true;
            item.data.fieldItem = data.fieldItem;
            data.qItem = quickItem;
        }

        //무기슬롯에 있을 경우
        else if (data.inWeaponSlot)
        {
            WeaponType type = data.fieldItem.GetComponent<Weapon>().weaponData.weaponType;
            switch (type)
            {
                case WeaponType.Sword:
                    {
                        item = Instantiate(sampleInvenItem, weaponslot);
                        item.data.fieldItem = data.fieldItem;
                        item.SetData(data);
                        item.SetInventory(inventory);
                        inventory.invenItems.Add(item);
                        weaponslot.GetComponent<WeaponSlot>().item = item;
                        weaponslot.GetComponent<WeaponSlot>().Equip();
                        break;
                    }
                case WeaponType.Bow:
                    {
                        item = Instantiate(sampleInvenItem, bowSlot);
                        item.data.fieldItem = data.fieldItem;
                        item.SetData(data);
                        item.SetInventory(inventory);
                        inventory.invenItems.Add(item);
                        weaponslot.GetComponent<WeaponSlot>().item = item;
                        weaponslot.GetComponent<WeaponSlot>().Equip();
                        break;
                    }
            }

        }

        //나머지 경우(슬롯인덱스 값이 -1이 아니어야 오류 발생 X)
        else if (data.slotIdx != -1)
        {
            item = Instantiate(sampleInvenItem, slots[data.slotIdx]);
            item.transform.parent.GetComponent<Slot>().isFilled = true;
            item.data.fieldItem = data.fieldItem;
            item.SetData(data);
            item.SetInventory(inventory);
            inventory.invenItems.Add(item);
        }
    }


    /// <summary>
    /// 슬롯의 fill여부 체크
    /// </summary>
    void InventoryCheck()
    {
        foreach (var item in slots)
        {
            //Slot의 하위에 오브젝트가 있다면
            if (item.childCount >= 1)
            {   
                //Slot이 filled 인것으로 판단
                item.GetComponent<Slot>().isFilled = true;
            }
        }
    }
}
