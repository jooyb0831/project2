using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    Player p;
    Inventory inven;
    public bool isFilled = false;

    public bool isToolEquiped;
    public Tool tool = null;
    public GameObject frame;

    public QuickInven item = null;

    Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        isToolEquiped = false;
        toggle = GetComponent<Toggle>();
        p = GameManager.Instance.Player;
        inven = GameManager.Instance.Inven;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFilled)
        {
            item = transform.GetChild(0).GetComponent<QuickInven>();
        }
        else
        {
            item = null;
            if(tool!=null)
            {
                Destroy(tool.gameObject);
            }

        }

        //선택 활성화
        if(toggle.isOn)
        {
            frame.SetActive(true);

            //아이템이 있을 경우
            if(item!=null)
            {
                ItemType type = item.invenItem.data.type;
                switch(type)
                {
                    case ItemType.Tool :
                    {
                        //도구가 있을 경우(이미 세팅되어있는 경우)
                        if (tool != null)
                        {
                            isToolEquiped = true;
                            p.currentTool = tool.gameObject;
                            //Player가 사용중인 무기가 있을 경우
                            if (p.equipedWeapon != null || p.equipedBow != null)  
                            {
                                //도구 안 보이게
                                tool.gameObject.SetActive(false);
                            }
                            //사용중인 무기가 없을 경우
                            else
                            {
                                //도구 보이게
                                tool.gameObject.SetActive(true);
                            }
                        }

                        //도구가 없을 경우(처음 세팅)
                        else
                        {
                            inven.QuickSlotItemSet(item);
                        }
                        break;
                    }
                    case ItemType.Potion:
                    {
                        if(Input.GetKeyDown(KeyCode.G))
                        {
                            inven.UseItem(item.invenItem);
                        }
                        break;
                    }
                }         
            }
        }
        //선택이 비활성화 되었을 경우
        else
        {
            //선택 프레임 비활성화
            frame.SetActive(false);

            //장착중인 아이템이 있을 경우
            if(item!=null)
            {
                if(tool!=null)
                {
                    isToolEquiped = false;
                }
                
                inven.QuickUnequiped(item);
            }

        }
    }    
}
