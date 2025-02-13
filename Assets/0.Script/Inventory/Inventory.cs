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
    public int count; //아이템 갯수
    public int itemIdx; //아이템의 고유 idx(코드번호)
    public string itemTitle; 
    public int price; //아이템 가격
    public int slotIdx; //아이템의 인벤토리 슬롯 인덱스
    public int invenOrderNum; //아이템이 인벤토리에 들어간 순서
    public bool inQuickSlot = false; //아이템이 퀵인벤에 있는지 여부
    public int quickSlotIdx; //퀵 인벤 슬롯 인덱스
    public QuickInven qItem;
    public FieldItem fieldItem = null;

    public GameObject inGameobj = null;
}
/// <summary>
/// 아이템 종류
/// </summary>
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
    public Transform[] invenSlots; //인벤토리 슬롯 리스트
    public Transform[] quickSlotsInven; //퀵 인벤 슬롯 리스트
    public Transform weaponSlot; //무기 슬롯
    public Transform bowSlot; //활 슬롯

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
            GameUI.Instance.GetItem(itemData);

            // 아이템이 물리적으로 존재할 경우 오브젝트 비활성화 및 삭제
            if (itemData.obj != null)
            {
                //아이템 종류에 따라 Pooling작업
                ItemType type = itemData.type;
                switch(type)
                {
                    case ItemType.Ore :
                    {
                        Pooling.Instance.SetPool(DicKey.stone, itemData.obj);
                        return;
                    }
                    case ItemType.Wood:
                    {
                        Pooling.Instance.SetPool(DicKey.wood, itemData.obj);
                        return;
                    }
                    default:
                        break;
                }
                
                //필드의 아이템 게임 오브젝트 삭제
                Destroy(itemData.obj);
            }
            return;
        }

        //인벤토리가 가득 찼는지 체크
        bool isFull = EmptySlotCheck();
        //가득 찼다면 아이템 습득 못하게 막기
        if (isFull)
        {
            itemData.obj.GetComponent<FieldItem>().InvenFull(isFull);
            return;
        }

        //리스트에 아이템 데이터 추가
        itemIdxList.Add(itemData.itemIdx);
        //인벤토리 데이터의 인벤 카운트 증가
        inventoryData.invenCount++;
        //아이템이 들어갈 인벤토리 인덱스 번호 부여
        int index = SlotCheck();
        //인벤아이템 생성
        InvenItem item = Instantiate(invenItem, invenSlots[index]);
        //인벤아이템이 위치한 슬롯의 Filled True로 변경
        invenSlots[index].GetComponent<Slot>().isFilled = true;

        //인벤데이터 생성하고 데이터 집어넣기
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

        //인벤아이템의 데이터(인벤데이터)가 인벤에 들어간 순서 
        item.data.invenOrderNum = inventoryData.invenCount;
        //인벤아이템의 데이터 세팅
        item.SetData(data);
        item.SetInventory(this);
        
        //인벤아이템 리스트, 데이터 리스트에추가
        invenItems.Add(item);
        invenDatas.Add(item.data);
        inventoryData.items.Add(item);

        //UI쪽에서 아이템 획득하였음을 알려주는 UI표기
        GameUI.Instance.GetItem(itemData);

        //아이템 획득시 실물 게임 오브젝트가 있는 경우 오브젝트 제거
        if(itemData.obj!=null)
        {
            ItemType type = itemData.type;
            switch(type)
            {
                case ItemType.Ore :
                    Pooling.Instance.SetPool(DicKey.stone, itemData.obj);
                    return;
                case ItemType.Wood:
                    Pooling.Instance.SetPool(DicKey.wood, itemData.obj);
                    return;
                default :
                    break;
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
    /// <returns>비어있는 가장 작은 슬롯의 인덱스 넘버</returns>
    int SlotCheck()
    {
        int number = -1;
        for (int i = 0; i < invenSlots.Length; i++)
        {
            Slot slot = invenSlots[i].GetComponent<Slot>();
            if (!slot.isFilled && !slot.isLocked)
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
        if (item.data.count <= 0)
        {
            DeleteItem(item);
            Destroy(item.gameObject);
        }
        item.ItemCntChange(item.data);
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

    /// <summary>
    /// 인벤토리에 있는 무기 장착하는 함수
    /// </summary>
    /// <param name="item"></param>
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
            //교환코드(추후 작성)
        }
        wSlot.isFilled = true;
        item.transform.parent.GetComponent<Slot>().isFilled = false;
        item.data.slotIdx = -1;
        item.transform.SetParent(wSlot.transform);
        item.transform.position = wSlot.transform.position;
        wSlot.item = item;
        wSlot.Equip();

    }


    /// <summary>
    /// 인벤토리에서 아이템 옮기는 코드
    /// </summary>
    /// <param name="isShow"></param>
    /// <param name="pos"></param>
    /// <param name="data"></param>
    public void ItemMove(bool isShow, Vector3 pos, InvenData data = null)
    {
        if(data!=null)
        {
            moveItem.SetData(data);
        }
        moveItem.gameObject.SetActive(isShow);
        moveItem.transform.position = pos;
    }

    /// <summary>
    /// 마우스 포인트를 뗐을 때 작동
    /// </summary>
    /// <param name="invenItem"></param>
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
    /// 아이템 장착 해제(퀵 슬롯/무기슬롯 -> 일반슬롯으로 돌아가는코드)
    /// </summary>
    /// <param name="item"></param>
    public void UnequipItem(InvenItem item)
    {
        int x = SlotCheck();
        item.transform.position = invenSlots[x].transform.position;
        invenSlots[x].GetComponent<Slot>().isFilled = true;
        
        //퀵슬롯에 있었을 경우
        if (item.transform.parent.GetComponent<QuickSlotInven>())
        {
            item.transform.parent.GetComponent<QuickSlotInven>().RemoveItem(item);
        }

        //무기 슬롯에 있었을 경우
        else if (item.transform.parent.GetComponent<WeaponSlot>())
        {
            item.transform.parent.GetComponent<WeaponSlot>().UnequipWeapon();
        }

        item.transform.SetParent(invenSlots[x].transform);

    }

}
