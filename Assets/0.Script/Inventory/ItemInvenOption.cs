using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemInvenOption : MonoBehaviour
{
    private Inventory inven;
    public InvenItem item;
    [SerializeField] private GameObject canvas;
    [SerializeField] GameObject[] buttonList;
    [SerializeField] GameObject useBtn;
    [SerializeField] GameObject equipBtn;
    [SerializeField] GameObject unequipBtn;
    [SerializeField] GameObject dumpBtn;
    [SerializeField] ItemDumpWindow itemDumpWindow;


    void Start()
    {
        inven = GameManager.Instance.Inven;   
    }

    public void OnUseBtn()
    {
        inven.UseItem(item);
        Destroy(gameObject);
    }

    public void OnEquipBtn()
    {
        if(item.data.type.Equals(ItemType.Weapon))
        {
            inven.WeaponEquip(item);
        }
        else
        {
            inven.ItemEquip(item);
        }
        Destroy(gameObject);
        
    }

    public void OnDumpBtn()
    {
        ItemDumpWindow window = Instantiate(itemDumpWindow, transform.parent.parent);
        window.SetItem(item);
        window.transform.SetAsLastSibling();
        Destroy(gameObject);
    }

    public void OnExitBtn()
    {
        Destroy(gameObject);
    }

    public void OnUnEquipBtn()
    {
        inven.UnequipItem(item);
        Destroy(gameObject);
    }

    public void SetInvenButton()
    {
        foreach(var button in buttonList)
        {
            button.SetActive(false);
        }

        //아이템이 퀵슬롯이나 무기 슬롯에 있을 경우 Unequip만 보이게 설정정
        if(item.transform.parent.GetComponent<QuickSlotInven>()
        || item.transform.parent.GetComponent<WeaponSlot>())
        {
            buttonList[2].SetActive(true);
        }

        if(item.transform.parent.GetComponent<Slot>())
        {
            ItemType type = item.data.type;

            switch(type)
            {
                case ItemType.Weapon :
                case ItemType.Tool:
                {
                    buttonList[1].SetActive(true);
                    break;
                }
                case ItemType.Potion :
                case ItemType.Food :
                {
                    buttonList[0].SetActive(true);
                    break;
                }
                default :
                {
                    break;
                }
            }
            buttonList[3].SetActive(true);
        }
    }
}
