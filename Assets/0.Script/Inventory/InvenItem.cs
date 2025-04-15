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

    [SerializeField] GameObject itemSellWindow;
    [SerializeField] GameObject itemBuyWindow;

    [SerializeField] ItemInvenOption itemOptionWindow;

    private Inventory inventory;
    private GameUI gameUI;
    public GameObject invenOption = null;
    public InvenData data;

    [SerializeField] bool canMove = false;
    
    void Start()
    {
        gameUI = GameManager.Instance.GameUI;
        Slot slot = transform.parent.GetComponent<Slot>();
        canMove = (slot != null) ? slot.isInven : false;
        
    }
    void Update()
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
        //마우스 우클릭 시 인벤옵션창 호출
        if(Input.GetMouseButtonDown(1))
        {
            //인벤옵션창이 없다면 
            if(invenOption == null)
            {   
                //인벤옵션창 생성
                invenOption = Instantiate(itemOptionWindow, transform).gameObject;
                invenOption.GetComponent<ItemInvenOption>().item = this;
                invenOption.GetComponent<ItemInvenOption>().SetInvenButton();

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
            if (transform.parent.GetComponent<Slot>())
            {
                if (transform.parent.GetComponent<Slot>().isInven)
                {
                    inventory.ItemMove(true, eventData.position, data);
                }
                else if (transform.parent.GetComponent<Slot>().isShopSlot)
                {
                    GameObject window = Instantiate(itemSellWindow, transform.parent.parent.parent.parent);
                    window.transform.SetAsLastSibling();
                    window.GetComponent<ItemSellWindow>().SetItem(this);
                }
                else if (transform.parent.GetComponent<Slot>().isMerchantSlot)
                {
                    GameObject window = Instantiate(itemBuyWindow, transform.parent.parent.parent.parent);
                    window.transform.SetAsLastSibling();
                    window.GetComponent<ItemBuyWindow>().SetItem(this);
                }
            }

            else if(transform.parent.GetComponent<QuickSlotInven>())
            {
                if(transform.parent.GetComponent<QuickSlotInven>().isInven)
                {
                    inventory.ItemMove(true, eventData.position, data);
                }
                else if (transform.parent.GetComponent<QuickSlotInven>().isShopSlot)
                {
                    GameObject window = Instantiate(itemSellWindow, transform.parent.parent.parent.parent);
                    window.transform.SetAsLastSibling();
                    window.GetComponent<ItemSellWindow>().SetItem(this);
                }
            }
            else if (transform.parent.GetComponent<WeaponSlot>())
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
            if(transform.parent.GetComponent<Slot>() && transform.parent.GetComponent<Slot>().isInven)
            {
                inventory.PointUp(this);
            }
            else if (transform.parent.GetComponent<QuickSlotInven>() && transform.parent.GetComponent<QuickSlotInven>().isInven)
            {
                inventory.PointUp(this);
            }
            else if (transform.parent.GetComponent<WeaponSlot>())
            {
                inventory.PointUp(this);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        

        if(Input.GetMouseButton(0) && canMove)
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
        gameUI.ShowItemExplainWindow(this, this.transform.parent);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameUI.HideItemExplainWindow();
    }
}
