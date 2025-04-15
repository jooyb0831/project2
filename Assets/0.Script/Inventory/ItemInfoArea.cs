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

    /// <summary>
    /// 표시중인 UI에 중복된 아이템이 있는지 체크
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool ItemCheck(ItemData data)
    {
        //중복된 아이템이 있다면 true를, 없다면 false를 리턴
        return itemTitleList.Contains(data.itemTitle);

    }

    /// <summary>
    /// 데이터 추가
    /// </summary>
    /// <param name="data"></param>
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
