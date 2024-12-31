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
        Inventory.Instance.ItemEquip(item);
    }

    public void OnDumpBtn()
    {

    }
}
