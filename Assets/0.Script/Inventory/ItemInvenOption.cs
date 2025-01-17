using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemInvenOption : MonoBehaviour
{
    public InvenItem item;
    [SerializeField] private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnUeseBtn()
    {

    }

    public void OnInfoBtn()
    {

    }

    public void OnEquipBtn()
    {
        if(item.data.type.Equals(ItemType.Weapon))
        {
            Inventory.Instance.WeaponEquip(item);
        }
        else
        {
            Inventory.Instance.ItemEquip(item);
        }
        Destroy(gameObject);
        
    }

    public void OnDumpBtn()
    {

    }

    public void OnExitBtn()
    {
        Destroy(gameObject);
    }

    public void OnUnEquipBtn()
    {
        
    }
}
