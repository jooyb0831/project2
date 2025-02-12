using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoArea : MonoBehaviour
{
    private Inventory inven;
    public List<ItemGetUI> getUIList;
    public List<string> itemTitleList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        inven = GameManager.Instance.Inven;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ItemCheck(ItemData data)
    {

        if (itemTitleList.Contains(data.itemTitle))
        {
            Debug.Log("Check");
            return true;
        }
        else
        {
            return false;
        }

    }

    public void AddData(ItemData data)
    {
        for (int i = 0; i < getUIList.Count; i++)
        {
            if (getUIList[i].itemTitleStr.Equals(data.itemTitle))
            {
                int x = getUIList[i].itemCnt + data.count;
                getUIList[i].ChangeUI(x);
                getUIList[i].Init();
                getUIList[i].FadeIn();
                //getUIList[i].SetData(data, getUIList[i].itemCnt);
                break;
            }
        }
    }

    public void DeleteObj(ItemGetUI ui)
    {
        itemTitleList.Remove(ui.itemTitleStr);
        getUIList.Remove(ui);
    }
}
