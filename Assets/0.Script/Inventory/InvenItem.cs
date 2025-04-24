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
        windowPos = transform.parent.parent.parent.parent;

    }
    void Update()
    {
        if (transform.parent.GetComponent<QuickSlot>())
        {
            itemFrame.color = Color.clear;
        }
        else
        {
            itemFrame.color = Color.white;
        }
    }


    /// <summary>
    /// 아이템의 데이터로 UI 세팅
    /// </summary>
    /// <param name="data"></param>
    public void SetData(InvenData data)
    {
        this.data = data;
        itemIcon.sprite = data.iconSprite;
        itemBG.sprite = data.bgSprite;
        cntTxt.text = $"{data.count}";
        cntBG.SetActive(data.count <= 1 ? false : true);
    }

    /// <summary>
    /// 인벤토리 세팅
    /// </summary>
    /// <param name="inven"></param>
    public void SetInventory(Inventory inven)
    {
        inventory = inven;
    }

    /// <summary>
    /// 인벤아이템 수량 변경 코드
    /// </summary>
    /// <param name="data"></param>
    public void ItemCntChange(InvenData data)
    {
        cntTxt.text = $"{data.count}";
        cntBG.SetActive(data.count <= 1 ? false : true);

        if (data.inQuickSlot)
        {
            data.qItem.ItemCntChange(this);
        }
    }

    Transform windowPos;

    /// <summary>
    /// 마우스 클릭시 호출되는 함수
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //마우스 우클릭 시 인벤옵션창 호출
        if (Input.GetMouseButtonDown(1))
        {
            //인벤옵션창이 없다면 
            if (invenOption == null)
            {
                //인벤옵션창 생성
                invenOption = Instantiate(itemOptionWindow, transform).gameObject;
                invenOption.GetComponent<ItemInvenOption>().item = this;
                invenOption.GetComponent<ItemInvenOption>().SetInvenButton();

                invenOption.transform.SetParent(windowPos);
                invenOption.transform.SetAsLastSibling();

            }
            //인벤옵션창이 있다면 기존의 옵션창 삭제
            else if (invenOption != null)
            {
                Destroy(invenOption);
            }
        }

        //마우스 좌클릭 시 아이템 이동 활성화
        else if (Input.GetMouseButton(0))
        {   
            //아이템이 일반슬롯에 있을 경우
            if (transform.parent.GetComponent<Slot>())
            {
                //인벤아이템이 인벤토리리 슬롯에 있을 경우
                if (transform.parent.GetComponent<Slot>().isInven)
                {
                    //아이템 움직이기
                    inventory.ItemMove(true, eventData.position, data);
                }

                //아이템이 상점의 내 인벤토리 슬롯에 있을 경우
                else if (transform.parent.GetComponent<Slot>().isShopSlot)
                {
                    //아이템 판매 창 생성
                    GameObject window = Instantiate(itemSellWindow, windowPos);
                    window.transform.SetAsLastSibling();
                    window.GetComponent<ItemSellWindow>().SetItem(this);
                }
                //아이템이 상점의 상인 슬롯에 있을 경우
                else if (transform.parent.GetComponent<Slot>().isMerchantSlot)
                {
                    //아이템 구매 창 생성
                    GameObject window = Instantiate(itemBuyWindow, windowPos);
                    window.transform.SetAsLastSibling();
                    window.GetComponent<ItemBuyWindow>().SetItem(this);
                }
            }
            //아이템이 퀵슬롯에 있을 경우
            else if (transform.parent.GetComponent<QuickSlotInven>())
            {
                if (transform.parent.GetComponent<QuickSlotInven>().isInven)
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

        }


    }

    /// <summary>
    /// 클릭을 해제했을 때 호출
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //좌클릭을 뗐을 때
        if (Input.GetMouseButtonUp(0))
        {
            //이미지 보이게
            itemFrame.color = Color.white;
            itemBG.color = Color.white;
            itemIcon.color = Color.white;
            if (data.count > 1)
            {
                cntBG.SetActive(true);
            }

            //만약 일반 슬롯에 있었을 경우
            if (transform.parent.GetComponent<Slot>() 
               && transform.parent.GetComponent<Slot>().isInven)
            {
                inventory.PointUp(this);
            }
            else if (transform.parent.GetComponent<QuickSlotInven>() 
                    && transform.parent.GetComponent<QuickSlotInven>().isInven)
            {
                inventory.PointUp(this);
            }
            else if (transform.parent.GetComponent<WeaponSlot>())
            {
                inventory.PointUp(this);
            }
        }
    }

    /// <summary>
    /// 마우스로 드래그 시 호출
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //아이템을 클릭한 상태 + 아이템이 움직일 수 있는 상태일 때
        if (Input.GetMouseButton(0) && canMove)
        {
            //현재의 인벤 아이템 투명처리(MoveItem활성화)
            itemFrame.color = Color.clear;
            itemBG.color = Color.clear;
            itemIcon.color = Color.clear;
            cntBG.SetActive(false);

            //아이템 움직이기
            if (transform.parent.GetComponent<Slot>()
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
