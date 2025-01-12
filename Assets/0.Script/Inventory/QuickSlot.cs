using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    Player p;
    Inventory inven;
    public bool isFilled = false;

    public bool isToolEquiped = false;
    public Tool tool = null;
    public GameObject frame;

    public InvenItem item = null;

    Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {

        toggle = GetComponent<Toggle>();
        p = GameManager.Instance.Player;
        inven = GameManager.Instance.Inven;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFilled)
        {
            item = transform.GetChild(1).GetComponent<InvenItem>();
        }
        else
        {
            item = null;
        }

        if(toggle.isOn)
        {
            frame.SetActive(true);

            if(item!=null)
            {
                if (tool != null)
                {
                    if (p.equipedWeapon != null)
                    {
                        tool.gameObject.SetActive(false);
                    }
                    else
                    {
                        tool.gameObject.SetActive(true);
                    }
                }
                else
                {
                    inven.QuickSlotItemSet(item);
                }
            }
        }
        else
        {
            frame.SetActive(false);
            if(item!=null)
            {
                inven.QuickUnequiped(item);
            }

        }
    }
}
