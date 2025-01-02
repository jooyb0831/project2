using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class InvenItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image itemBG;
    [SerializeField] Image itemIcon;
    [SerializeField] GameObject cntBG;
    [SerializeField] TMP_Text cntTxt;

    [SerializeField] ItemInvenOption itemOptionWindow;

    private Inventory inventory;
    public GameObject invenOption = null;
    public InvenData data;

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

                // �� ���̿� ������ ������ ���� ��� �ٸ��� �����ϴ� �ڵ� ��

                invenOption.transform.SetParent(transform.parent.parent.parent.parent);
                invenOption.transform.SetAsLastSibling();

            }

            else if (invenOption != null)
            {
                Destroy(invenOption);
            }
        }

        if(Input.GetMouseButton(0))
        {
            if(transform.parent.GetComponent<Slot>() || transform.GetComponent<QuickSlot>())
            {
                inventory.ItemMove(true, eventData.position, data);
            }

            //�ڽ�����
            
        }


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(Input.GetMouseButtonUp(0))
        {
            itemBG.color = Color.white;
            itemIcon.color = Color.white;
            if (data.count >1)
            {
                cntBG.SetActive(true);
            }
            if(transform.parent.GetComponent<Slot>() || transform.parent.GetComponent<QuickSlot>())
            {
                inventory.PointUp(this);
            }


        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Input.GetMouseButton(0))
        {
            itemBG.color = Color.clear;
            itemIcon.color = Color.clear;
            cntBG.SetActive(false);

            if(transform.parent.GetComponent<Slot>() || transform.parent.GetComponent<QuickSlot>())
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
