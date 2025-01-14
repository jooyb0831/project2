using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoveItem : MonoBehaviour
{
    [SerializeField] Image frameImg;
    [SerializeField] Image bgImg;
    [SerializeField] Image icon;
    [SerializeField] TMP_Text countTxt;
    [SerializeField] GameObject countBG;

    public InvenData data;
    public Collider2D coll;

    private Inventory inven;
    // Start is called before the first frame update
    void Start()
    {
        inven = GameManager.Instance.Inven;
    }

    // Update is called once per frame
    void Update()
    {
        if(Inventory.Instance.moveItem == null)
        {
            Inventory.Instance.moveItem = this;
            return;
        }
    }

    public void SetData(InvenData data)
    {
        this.data = data;
        icon.sprite = data.iconSprite;
        bgImg.sprite = data.bgSprite;
        countTxt.text = $"{data.count}";
        countBG.SetActive(data.count <= 1 ? false : true);
    }

    public void SetItem(InvenItem item)
    {
        this.data = item.data;
        icon.sprite = item.data.iconSprite;
        bgImg.sprite = item.data.bgSprite;
        countTxt.text = $"{item.data.count}";
        countBG.SetActive(item.data.count <= 1 ? false : true);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("인식");
        coll = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //coll = null;
    }
    
    /// <summary>
    /// 아이템 드래그-드롭으로 이동
    /// </summary>
    /// <param name="invenItem"></param>
    public void MoveSlot(InvenItem invenItem)
    {

        //무기 슬롯으로 이동시 : 무기 장착
        if(coll.GetComponent<WeaponSlot>())
        {
            if(invenItem.data.type != ItemType.Weapon)
            {
                return;
            }
            //무기 슬롯이 비어있을 경우
            if(!coll.GetComponent<WeaponSlot>().isFilled)
            {
                //일반 슬롯에서 이동할 경우
                if (invenItem.transform.parent.GetComponent<Slot>())
                {
                    invenItem.transform.parent.GetComponent<Slot>().isFilled = false;
                }
            }

            invenItem.transform.position = coll.transform.position;
            invenItem.transform.SetParent(coll.transform);
            coll.GetComponent<WeaponSlot>().isFilled = true;
            coll.GetComponent<WeaponSlot>().item = invenItem;
            coll.GetComponent<WeaponSlot>().Equip();
        }

        // 아이템을 인벤토리의 일반 슬롯으로 이동할 경우
        if (coll.GetComponent<Slot>()) 
        {
            // 빈 슬롯으로 이동시
            if (!coll.GetComponent<Slot>().isFilled)
            {
                // 아이템이 현재 퀵슬롯에 있을 경우
                if (invenItem.transform.parent.GetComponent<QuickSlotInven>()) 
                {
                    invenItem.transform.parent.GetComponent<QuickSlotInven>().RemoveItem(invenItem);
                    invenItem.data.quickSlotIdx = -1;
                    //invenItem.transform.parent.GetComponent<QuickSlot>().RemoveItem();
                }
                // 아이템이 현재 일반 슬롯에 있을 경우
                if (invenItem.transform.parent.GetComponent<Slot>()) 
                {
                    invenItem.transform.parent.GetComponent<Slot>().isFilled = false;
                }

                //아이템이 무기 슬롯에 있을 경우
                if(invenItem.transform.parent.GetComponent<WeaponSlot>())
                {
                    invenItem.transform.parent.GetComponent<WeaponSlot>().isFilled = false;
                }
                /*
                if (invenItem.transform.parent.GetComponent<BoxSlot>() == true) // �������� �ڽ����� �̵��� ���
                {
                    Inventory.Instance.GetBoxItem(invenItem);
                    invenItem.transform.parent.GetComponent<BoxSlot>().isFilled = false;
                    invenItem.transform.parent.GetComponent<BoxSlot>().RemoveItem();
                }
                */
                invenItem.data.slotIdx = coll.GetComponent<Slot>().indexNum;
                invenItem.transform.position = coll.transform.position;
                invenItem.transform.SetParent(coll.transform);
                coll.GetComponent<Slot>().isFilled = true;
            }

            //비어있지 않은 슬롯으로 이동시
            if (coll.GetComponent<Slot>().isFilled)
            {
                //옮기려는 슬롯에 있는 아이템
                InvenItem itemOrigin = coll.transform.GetChild(0).GetComponent<InvenItem>();

                //중복된 아이템일 경우 수량 증가 처리(합치기)
                if (itemOrigin.data.itemTitle.Equals(invenItem.data.itemTitle) 
                    && itemOrigin.data.itemIdx != invenItem.data.itemIdx)
                {
                    itemOrigin.data.count += invenItem.data.count;
                    //coll.transform.GetChild(0).GetComponent<InvenItem>().AddData(coll.transform.GetChild(0).GetComponent<InvenItem>().data);

                    //퀵슬롯으로 옮길 경우
                    if (invenItem.transform.parent.GetComponent<QuickSlotInven>())
                    {
                        invenItem.transform.parent.GetComponent<QuickSlotInven>().isFilled = false;
                        invenItem.data.inQuickSlot = false;
                        //invenItem.transform.parent.GetComponent<QuickSlot>().RemoveItem();
                    }

                    //일반 슬롯으로 옮길 경우
                    if (invenItem.transform.parent.GetComponent<Slot>())
                    {
                        invenItem.transform.parent.GetComponent<Slot>().isFilled = false;
                    }
                    /*
                    if (invenItem.transform.parent.GetComponent<BoxSlot>() == true)
                    {
                        invenItem.transform.parent.GetComponent<BoxSlot>().isFilled = false;
                        invenItem.transform.parent.GetComponent<BoxSlot>().RemoveItem();
                    }
                    */

                    //옮기려는 invenItem삭제
                    Destroy(invenItem.gameObject);
                }
            }

        }
        // 아이템이 퀵슬롯으로 이동할 경우
        if (coll.GetComponent<QuickSlotInven>())
        {
            //옮기려는 퀵슬롯이 비어있을 경우
            if (!coll.GetComponent<QuickSlotInven>().isFilled)
            {
                //현재 아이템이 퀵슬롯에 있을 경우
                if (invenItem.transform.parent.GetComponent<QuickSlotInven>() )
                {
                    //현재 위치의 퀵슬롯 비움처리
                    invenItem.transform.parent.GetComponent<QuickSlotInven>().isFilled = false;
                    //invenItem.transform.parent.GetComponent<QuickSlotInven>().RemoveItem();
                }

                //현재 아이템이 일반 슬롯에 있을 경우
                if (invenItem.transform.parent.GetComponent<Slot>())
                {
                    invenItem.transform.parent.GetComponent<Slot>().isFilled = false;
                    //invenItem.data.quickIndexNum = coll.GetComponent<QuickSlot>().index;
                }

                /*
                if (invenItem.transform.parent.GetComponent<BoxSlot>() == true) // �������� �ڽ����� �̵��ϴ� ���
                {
                    Inventory.Instance.GetBoxItem(invenItem);
                    invenItem.transform.parent.GetComponent<BoxSlot>().isFilled = false;
                    invenItem.transform.parent.GetComponent<BoxSlot>().RemoveItem();
                }
                */
                invenItem.transform.position = coll.transform.position;
                invenItem.transform.SetParent(coll.transform);
                coll.GetComponent<QuickSlotInven>().isFilled = true;
                coll.GetComponent<QuickSlotInven>().SetItem(invenItem);
                //invenItem.data.isQuickSlot = true;
                //QuickInventory.Instance.GetItem(invenItem, coll.GetComponent<QuickSlot>().lowSlot);
                //Instantiate(invenItem, coll.GetComponent<QuickSlots>().slot);//
            }

            //옮기려는 퀵슬롯이 차있을 경우
            if (coll.transform.GetComponent<QuickSlotInven>().isFilled)
            {
                //옮기려는 위치에 있는 아이템
                InvenItem itemOrigin = coll.transform.GetChild(0).GetComponent<InvenItem>();
                if (itemOrigin.data.itemTitle.Equals(invenItem.data.itemTitle) 
                    && itemOrigin.data.itemIdx != invenItem.data.itemIdx)
                {
                    itemOrigin.data.count += invenItem.data.count;
                    //coll.transform.GetChild(0).GetComponent<InvenItem>().AddData(coll.transform.GetChild(0).GetComponent<InvenItem>().data);
                    //coll.transform.GetComponent<QuickSlot>().lowSlot.GetChild(0).GetComponent<QuickInvenItem>().data.count = coll.transform.GetChild(0).GetComponent<InvenItem>().data.count;
                    //coll.transform.GetComponent<QuickSlot>().lowSlot.GetChild(0).GetComponent<QuickInvenItem>().AddData(coll.transform.GetComponent<QuickSlots>().lowSlot.GetChild(0).GetComponent<QuickInvenItem>().data);
                    
                    //아이템이 퀵슬롯으로부터 이동할 경우
                    if (invenItem.transform.parent.GetComponent<QuickSlotInven>())
                    {
                        invenItem.transform.parent.GetComponent<QuickSlotInven>().isFilled = false;
                        //invenItem.transform.parent.GetComponent<QuickSlot>().RemoveItem();
                    }
                    //아이템이 일반 슬롯으로부터 이동할 경우
                    if (invenItem.transform.parent.GetComponent<Slot>())
                    {
                        invenItem.transform.parent.GetComponent<Slot>().isFilled = false;
                    }
                    /*
                    if (invenItem.transform.parent.GetComponent<BoxSlot>() == true)
                    {
                        invenItem.transform.parent.GetComponent<BoxSlot>().isFilled = false;
                        invenItem.transform.parent.GetComponent<BoxSlot>().RemoveItem();
                    }
                    */
                    Destroy(invenItem.gameObject);
                }
            }


        }
        
    }
}
