using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class InvenItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image itemFrame;
    [SerializeField] Image itemBG;
    [SerializeField] Image itemIcon;
    [SerializeField] GameObject cntBG;
    [SerializeField] TMP_Text cntTxt;

    [SerializeField] ItemInvenOption itemOptionWindow;

    private Inventory inventory;
    public GameObject invenOption = null;
    public InvenData data;

    public void Update()
    {
        if(transform.parent.GetComponent<QuickSlot>())
        {
            itemFrame.color = Color.clear;
        }
        else
        {
            itemFrame.color = Color.white;
        }
    }

    public void SetData(InvenData data)
    {
        this.data = data;
        itemIcon.sprite = data.iconSprite;
        itemBG.sprite = data.bgSprite;
        cntTxt.text = $"{data.count}";
        cntBG.SetActive(data.count <= 1 ? false : true);
    }

    public void SetInventory(Inventory inven)
    {
        inventory = inven;
    }

    public void ItemCntChange(InvenData data)
    {

        cntTxt.text = $"{data.count}";
        cntBG.SetActive(data.count <= 1 ? false : true);
        
        if (data.inQuickSlot)
        {
            data.qItem.ItemCntChange(this);
        }
        
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetMouseButtonDown(1))
        {
            /*
            if(transform.parent.GetComponent<Slot>().isMerchantInven
                || transform.parent.GetComponent<Slot>().isSellInven)
            {
                return;
            }
            */

            if(invenOption == null)
            {
                invenOption = Instantiate(itemOptionWindow, transform).gameObject;
                invenOption.GetComponent<ItemInvenOption>().item = this;

                // 이 사이에 아이템 종류에 따라 목록 다르게 수정하는 코드 들어감

                invenOption.transform.SetParent(transform.parent.parent.parent.parent);
                invenOption.transform.SetAsLastSibling();

            }

            else if (invenOption != null)
            {
                Destroy(invenOption);
            }
        }

        else if(Input.GetMouseButton(0))
        {
            if(transform.parent.GetComponent<Slot>() 
            || transform.GetComponent<QuickSlotInven>()
            || transform.GetComponent<WeaponSlot>())
            {
                inventory.ItemMove(true, eventData.position, data);
            }

            //박스슬롯
            
        }


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(Input.GetMouseButtonUp(0))
        {
            itemFrame.color = Color.white;
            itemBG.color = Color.white;
            itemIcon.color = Color.white;
            if (data.count >1)
            {
                cntBG.SetActive(true);
            }
            if(transform.parent.GetComponent<Slot>() 
            || transform.parent.GetComponent<QuickSlotInven>()
            || transform.parent.GetComponent<WeaponSlot>())
            {
                inventory.PointUp(this);
            }


        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Input.GetMouseButton(0))
        {
            itemFrame.color = Color.clear;
            itemBG.color = Color.clear;
            itemIcon.color = Color.clear;
            cntBG.SetActive(false);

            if(transform.parent.GetComponent<Slot>() 
            || transform.parent.GetComponent<QuickSlotInven>()
            || transform.parent.GetComponent<WeaponSlot>())
            {
                inventory.ItemMove(true, eventData.position);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
