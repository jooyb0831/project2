using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CraftSystem : MonoBehaviour
{
    private GameUI gameUI;
    private Inventory inven;
    public List<ItemRecipeData> itemRecipeDatas;
    public List<ItemRecipeData> foodRecipeDatas;

    void Start()
    {
        DontDestroyOnLoad(this);
        Init();
    }

    void Init()
    {
        gameUI = GameManager.Instance.GameUI;
        inven = GameManager.Instance.Inven;
        foreach(var item in itemRecipeDatas)
        {
            item.SetData();
        }

        foreach(var item in foodRecipeDatas)
        {
            item.SetData();
        }
    }

    /// <summary>
    /// 아이템 제작시 처리 코드
    /// </summary>
    /// <param name="needList"></param>
    /// <param name="item"></param>
    /// <param name="createCnt"></param>
    public void CreateItem(List<NeedItemUI> needList, FieldItem item, int createCnt)
    {
        //만약 하나라도 재료가 부족하다면 리턴
        foreach(var obj in needList)
        {
            if(!obj.CntCheck())
            {
                //UI에 재료가 부족함을 표시
                gameUI.DisplayInfo(3);
                return;
            }
        }
        //추가될 아이템의 갯수 데이터 설정
        item.itemData.count = createCnt;
        //인벤토리에 아이템 추가
        inven.GetItem(item.itemData);
        //아이템 재료 소모처리(인벤에서)
        foreach(var obj in needList)
        {
            obj.FindUseItem(createCnt);
        }
    }
}
