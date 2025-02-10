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
    public int invenOrderNum;
    public bool inQuickSlot = false;
    public int quickSlotIdx;
    public QuickInven qItem;
    public FieldItem fieldItem = null;

    public GameObject inGameobj = null;
}
public enum ItemType
{
    Ore,
    Wood,
    Tool,
    Weapon,
    Potion,
    Arrow
}

public class InventoryData
{
    public int curInvenSlots = 10;
    public int invenCount = 0;
    public List<InvenItem> items = new();
    public bool invenFull = false;
}


public class Inventory : Singleton<Inventory>
{
    private PlayerData pd;
    private Player p;
    [SerializeField] InvenItem invenItem;
    public Transform[] invenSlots;
    public Transform[] quickSlotsInven;
    public Transform weaponSlot;
    public Transform bowSlot;

    public MoveItem moveItem;

    public InventoryData inventoryData = new();
    public List<InvenItem> invenItems = new();
    private List<int> itemIdxList = new();
    public List<InvenData> invenDatas = new();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;
        p = GameManager.Instance.Player;
    }


    /// <summary>
    /// 인벤토리에 아이템 추가하는 함수(ItemData인수)
    /// </summary>
    /// <param name="itemData"></param>
    public void GetItem(ItemData itemData)
    {
        //이미 인벤토리에 중복된 아이템이 있는 경우
        if (itemIdxList.Contains(itemData.itemIdx))
        {
            ItemCheck(itemData);
            if(itemData.type.Equals(ItemType.Ore))
            {
                Pooling.Instance.SetPool(DicKey.stone, itemData.obj);
                return;
            }
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
        inventoryData.invenCount++;
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
            fieldItem = itemData.fItem,
            slotIdx = index
        };
        item.data.invenOrderNum = inventoryData.invenCount;
        item.SetData(data);
        item.SetInventory(this);
        invenItems.Add(item);
        invenDatas.Add(item.data);
        inventoryData.items.Add(item);

        if(itemData.obj!=null)
        {
            if (itemData.type.Equals(ItemType.Ore))
            {
                Pooling.Instance.SetPool(DicKey.stone, itemData.obj);
                return;
            }
            Destroy(itemData.obj);
        }


    }

    /// <summary>
    /// 인벤토리에 아이템 추가(InvenItem 인수)
    /// </summary>
    /// <param name="invenItem"></param>
    public void GetItem(InvenItem invenItem)
    {
        //이미 인벤토리에 중복된 아이템이 있는 경우
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
    /// 슬롯 인덱스
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
    /// 빈 슬롯 체크
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
    /// 중복아이템 체크(ItemData)
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
    /// 중복아이템 체크(InvenItem)
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

    /// <summary>
    /// 아이템 사용
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cnt"></param>
    public void UseItem(InvenItem item, int cnt = -1)
    {
        ItemType type = item.data.type;
        
        switch(type)
        {
            case ItemType.Potion :
            {
                bool canUse = item.data.fieldItem.ItemUseCheck();
                if(!canUse)
                {
                    Debug.Log("더 이상 회복할 수 없습니다.");
                    return;
                }
                item.data.fieldItem.UseItem();
                break;
            }
            default:
            {
                break;
            }
        }
        InvenItemCntChange(item, cnt);
    }

    /// <summary>
    /// 아이템 수량변경
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cnt"></param>
    public void InvenItemCntChange(InvenItem item, int cnt =-1)
    {
        item.data.count += cnt;
        item.ItemCntChange(item.data);
        if (item.data.count == 0)
        {
            DeleteItem(item);
            Destroy(item.gameObject);
        }

    }


    /// <summary>
    /// 아이템 삭제
    /// </summary>
    /// <param name="item"></param>
    public void DeleteItem(InvenItem item)
    {
        if(item.transform.parent.GetComponent<Slot>())
        {
            item.transform.parent.GetComponent<Slot>().isFilled = false;
        }
        else if(item.transform.parent.GetComponent<QuickSlotInven>())
        {
            item.transform.parent.GetComponent<QuickSlotInven>().RemoveItem(item);
        }
        int itemIdx = -1;
        for(int i =0; i<invenItems.Count; i++)
        {
            if(invenItems[i].data.itemIdx == item.data.itemIdx)
            {
                itemIdx = i;
                Debug.Log(itemIdx);
                break;
            }
        }
        itemIdxList.Remove(item.data.itemIdx);
        if(item.data.slotIdx!=-1)
        {

        }
        invenItems.RemoveAt(item.data.invenOrderNum);
        invenDatas.RemoveAt(item.data.invenOrderNum);
    }


    [SerializeField] QuickInven quickInvenSample;
    /// <summary>
    /// 아이템 장착(퀵슬롯)
    /// </summary>
    /// <param name="item"></param>
    public void ItemEquip(InvenItem item)
    {
        QuickSlotInven qSlot = null;
        for(int i =0; i<quickSlotsInven.Length; i++)
        {
            if(!quickSlotsInven[i].GetComponent<QuickSlotInven>().isFilled)
            {
                qSlot = quickSlotsInven[i].GetComponent<QuickSlotInven>();
                break;
            }
        }

        item.transform.parent.GetComponent<Slot>().isFilled = false;
        item.data.slotIdx = -1;
        item.transform.SetParent(qSlot.transform);
        item.transform.position = qSlot.transform.position;
        item.data.quickSlotIdx = qSlot.quickSlotIdx;
        qSlot.SetItem(item);
        qSlot.isFilled = true;

        /*
        if(item.data.type.Equals(ItemType.Tool))
        {
            if(item.data.itemTitle.Equals("곡괭이"))
            {
                Instantiate(item.data.fieldItem, p.swordPos);
            }
        }
        */
    }

    public void WeaponEquip(InvenItem item)
    {
        WeaponSlot wSlot = null;
        WeaponType type = item.data.fieldItem.GetComponent<Weapon>().weaponData.weaponType;
        
        
        switch(type)
        {
            case WeaponType.Sword:
            {
                wSlot = weaponSlot.GetComponent<WeaponSlot>();
                break;
            }

            case WeaponType.Bow:
            {
                wSlot = bowSlot.GetComponent<WeaponSlot>();
                break;
            }
            
        }

        if(wSlot.isFilled)
        {
            //교환
        }
        wSlot.isFilled = true;
        item.transform.parent.GetComponent<Slot>().isFilled = false;
        item.data.slotIdx = -1;
        item.transform.SetParent(wSlot.transform);
        item.transform.position = wSlot.transform.position;
        wSlot.item = item;
        wSlot.Equip();

    }


    public void ItemMove(bool isShow, Vector3 pos, InvenData data = null)
    {
        if(data!=null)
        {
            moveItem.SetData(data);
        }
        moveItem.gameObject.SetActive(isShow);
        moveItem.transform.position = pos;
    }

    public void PointUp(InvenItem invenItem)
    {
        moveItem.MoveSlot(invenItem);
        ItemMove(false, Vector2.zero);
    }


    /// <summary>
    /// 퀵인벤 아이템 장착 코드
    /// </summary>
    /// <param name="item">해당 인벤아이템</param>
    public void QuickSlotItemSet(QuickInven item)
    {
        ItemType type = item.invenItem.data.type;
        QuickSlot quickSlot = item.transform.parent.GetComponent<QuickSlot>();
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }
        
        //아이템의 타입에 따라 세팅 다르게
        switch(type)
        {
            case ItemType.Tool:
            {
                //도구가 장착되어 있지 않는 경우
                if(quickSlot.tool==null)
                {
                    //도구 아이템 생성하고 Player쪽에 넣어주기
                    quickSlot.tool = Instantiate(item.invenItem.data.fieldItem.GetComponent<Tool>(), p.toolPos);
                    p.currentTool = quickSlot.tool.gameObject;
                }
                //도구 세팅
                quickSlot.tool.SetTool();
                break;
            }
        }
    }

    /// <summary>
    /// 퀵 인벤에서 아이템 사용 비활성화
    /// </summary>
    /// <param name="item"></param>
    public void QuickUnequiped(QuickInven item)
    {
        ItemType type = item.invenItem.data.type;
        QuickSlot quickSlot = item.transform.parent.GetComponent<QuickSlot>();
        
        switch(type)
        {
            case ItemType.Tool:
            {
                if(quickSlot.tool==null)
                {
                    return;
                }
                //인게임 도구 오브젝트 끄기
                quickSlot.tool.obj.SetActive(false);

                //Player쪽 현재 사용중인 도구 null로 처리
                p.currentTool = null;
                break;
            }
        }
    }

    /// <summary>
    /// 인벤에 있는 아이템을 리턴
    /// </summary>
    /// <param name="itemIdx">아이템 코드번호</param>
    /// <returns>아이템 개수</returns>
    public InvenItem FindItem(int itemIdx)
    {
        InvenItem item = null;
        for(int i =0; i<invenItems.Count; i++)
        {
            if(invenItems[i].data.itemIdx==itemIdx)
            {
                item = invenItems[i];
                break;
            }
            else
            {
                item = null;
            }
        }
        return item;
    }

    /// <summary>
    /// 인벤에 있는 아이템 개수를 리턴
    /// </summary>
    /// <param name="itemIdx"></param>
    /// <returns></returns>
    public int FindItemCnt(int itemIdx)
    {
        InvenItem item = null;
        int cnt = 0;
        for(int i = 0; i<invenItems.Count; i++)
        {
            if(invenItems[i].data.itemIdx == itemIdx)
            {
                item = invenItems[i];
                cnt = item.data.count;
                break;
            }
            else
            {
                cnt = 0;
            }

        }
        return cnt;
    }

    

    /// <summary>
    /// 아이템 장착 해제(일반슬롯으로 돌아가는코드)
    /// </summary>
    /// <param name="item"></param>
    public void UnequipItem(InvenItem item)
    {
        int x = SlotCheck();
        item.transform.position = invenSlots[x].transform.position;
        
        if (item.transform.parent.GetComponent<QuickSlotInven>())
        {
            item.transform.parent.GetComponent<QuickSlotInven>().RemoveItem(item);

        }
        else if (item.transform.parent.GetComponent<WeaponSlot>())
        {
            item.transform.parent.GetComponent<WeaponSlot>().UnequipWeapon();
        }
        item.transform.SetParent(invenSlots[x].transform);

    }

}
