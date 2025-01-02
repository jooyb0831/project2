using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoveItem : MonoBehaviour
{
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
        if(inven.moveItem == null)
        {
            inven.moveItem = this;
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
        Debug.Log("�ν�");
        coll = collision;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        coll = null;
    }


    public void MoveSlot(InvenItem invenItem)
    {
        if (coll.GetComponent<Slot>() == true) // �������� �κ��丮�� �Ϲ� �������� �̵��� ���
        {
            if (coll.GetComponent<Slot>().isFilled == false)
            {
                if (invenItem.transform.parent.GetComponent<QuickSlot>() == true) // �������� ���� �����Կ� ���� ���
                {
                    invenItem.transform.parent.GetComponent<QuickSlot>().isFilled = false;
                    invenItem.data.inQuickSlot = false;
                    invenItem.data.quickSlotIdx = -1;
                    //invenItem.transform.parent.GetComponent<QuickSlot>().RemoveItem();
                }
                if (invenItem.transform.parent.GetComponent<Slot>() == true) // �������� ���� �Ϲ� ���Կ� ���� ���
                {
                    invenItem.transform.parent.GetComponent<Slot>().isFilled = false;

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

            if (coll.GetComponent<Slot>().isFilled == true)
            {
                if (coll.transform.GetChild(0).GetComponent<InvenItem>().data.itemTitle.Equals(invenItem.data.itemTitle) 
                    && coll.transform.GetChild(0).GetComponent<InvenItem>().data.itemIdx != invenItem.data.itemIdx)
                {
                    coll.transform.GetChild(0).GetComponent<InvenItem>().data.count += invenItem.data.count;
                    //coll.transform.GetChild(0).GetComponent<InvenItem>().AddData(coll.transform.GetChild(0).GetComponent<InvenItem>().data);

                    if (invenItem.transform.parent.GetComponent<QuickSlot>() == true)
                    {
                        invenItem.transform.parent.GetComponent<QuickSlot>().isFilled = false;
                        invenItem.data.inQuickSlot = false;
                        //invenItem.transform.parent.GetComponent<QuickSlot>().RemoveItem();
                    }
                    if (invenItem.transform.parent.GetComponent<Slot>() == true)
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
        if (coll.GetComponent<QuickSlot>() == true) // �������� ���������� �̵��� ���
        {
            if (coll.GetComponent<QuickSlot>().isFilled == false)
            {
                if (invenItem.transform.parent.GetComponent<QuickSlot>() == true) // �������� ���� �����Կ� ���� ���
                {
                    invenItem.transform.parent.GetComponent<QuickSlot>().isFilled = false;
                    //invenItem.transform.parent.GetComponent<QuickSlot>().RemoveItem();
                }
                if (invenItem.transform.parent.GetComponent<Slot>() == true) // �������� ���� �Ϲ� ���Կ� ���� ���
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
                coll.GetComponent<QuickSlot>().isFilled = true;
                //invenItem.data.isQuickSlot = true;
                //QuickInventory.Instance.GetItem(invenItem, coll.GetComponent<QuickSlot>().lowSlot);
                //Instantiate(invenItem, coll.GetComponent<QuickSlots>().slot);//
            }

            if (coll.transform.GetComponent<QuickSlot>().isFilled == true)
            {
                if (coll.transform.GetChild(0).GetComponent<InvenItem>().data.itemTitle.Equals(invenItem.data.itemTitle) 
                    && coll.transform.GetChild(0).GetComponent<InvenItem>().data.itemIdx != invenItem.data.itemIdx)
                {
                    coll.transform.GetChild(0).GetComponent<InvenItem>().data.count += invenItem.data.count;
                    //coll.transform.GetChild(0).GetComponent<InvenItem>().AddData(coll.transform.GetChild(0).GetComponent<InvenItem>().data);
                    //coll.transform.GetComponent<QuickSlot>().lowSlot.GetChild(0).GetComponent<QuickInvenItem>().data.count = coll.transform.GetChild(0).GetComponent<InvenItem>().data.count;
                    //coll.transform.GetComponent<QuickSlot>().lowSlot.GetChild(0).GetComponent<QuickInvenItem>().AddData(coll.transform.GetComponent<QuickSlots>().lowSlot.GetChild(0).GetComponent<QuickInvenItem>().data);
                    if (invenItem.transform.parent.GetComponent<QuickSlot>() == true)
                    {
                        invenItem.transform.parent.GetComponent<QuickSlot>().isFilled = false;
                        //invenItem.transform.parent.GetComponent<QuickSlot>().RemoveItem();
                    }

                    if (invenItem.transform.parent.GetComponent<Slot>() == true)
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
