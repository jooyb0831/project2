using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateResoruceUI : MonoBehaviour
{
    private Inventory inven;
    private GameUI gameUI;
    private CraftSystem craftSystem;

    private FieldItem item;

    [SerializeField] GameObject cover;
    [SerializeField] GameObject slot;

    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemTitle;
    [SerializeField] TMP_Text curItemCnt;

    [SerializeField] Transform contentArea;
    [SerializeField] NeedItemUI sampleNeedUI;
    [SerializeField] List<NeedItemUI> needLists;

    [SerializeField] TMP_InputField cntInput;
    [SerializeField] Button plusBtn;
    [SerializeField] Button minusBtn;

    [SerializeField] Button createBtn;

    [SerializeField] int createCnt = 1;

    void Start()
    {
        inven = GameManager.Instance.Inven;
        gameUI = GameManager.Instance.GameUI;
        craftSystem = GameManager.Instance.CraftSystem;
    }


    /// <summary>
    /// 제작 아이템의 데이터를 받아와서 세팅
    /// </summary>
    /// <param name="item"></param>
    /// <param name="data"></param>
    public void SetData(FieldItem item, ItemRecipeData data)
    {
        if (cover.activeSelf)
        {
            cover.SetActive(false);
            slot.SetActive(true);
        }

        //제작할 아이템의 UI세팅
        this.item = item;

        //재료 리스트 오브젝트 생성
        SetList(data.Resources.Length);

        //리스트 오브젝트의 데이터 세팅
        //아이템 레시피 데이터의 Resources의 데이터를 받아와야 함.
        for (int i = 0; i < needLists.Count; i++)
        {
            needLists[i].SetData(data.Resources[i].itemData, data.RcCnts[i], createCnt);
        }
    }

    /// <summary>
    /// UI세팅
    /// </summary>
    void SetUI()
    {
        itemIcon.sprite = item.itemData.invenIcon;
        itemTitle.text = item.itemData.itemTitle;
        int curCnt = inven.FindItemCnt(item.itemData.itemIdx);
        curItemCnt.text = $"보유 : {curCnt}";
        createCnt = 1;
        cntInput.text = createCnt.ToString();
        SetCnt(createCnt);
    }

    /// <summary>
    /// 수량세팅
    /// </summary>
    void SetCnt(int num)
    {
        foreach (var item in needLists)
        {
            item.SetCnt(num);
        }
    }


    /// <summary>
    /// 리스트 생성 및 세팅
    /// </summary>
    /// <param name="count"></param>
    void SetList(int count)
    {
        //기존 리스트 및 오브젝트 클리어
        if (needLists.Count != 0)
        {
            foreach (var item in needLists)
            {
                Destroy(item.gameObject);
            }
            needLists.Clear();
        }

        //리스트에 오브젝트 새로 생성하고 Add
        for (int i = 0; i < count; i++)
        {
            NeedItemUI obj = Instantiate(sampleNeedUI, contentArea);
            needLists.Add(obj);
        }
    }

    /// <summary>
    /// 인풋필드
    /// </summary>
    public void OnInputField()
    {
        string cntStr = cntInput.text;
        createCnt = int.Parse(cntStr);
        SetCnt(createCnt);
    }

    /// <summary>
    /// PlusBtn
    /// </summary>
    public void OnPlusBtn()
    {
        int x = int.Parse(cntInput.text);
        x++;
        createCnt = x;
        cntInput.text = $"{createCnt}";
        SetCnt(createCnt);
    }

    /// <summary>
    /// MinusBtn
    /// </summary>
    public void OnMinusBtn()
    {
        int x = int.Parse(cntInput.text);
        if (x <= 1)
        {
            return;
        }
        x--;
        createCnt = x;
        cntInput.text = $"{createCnt}";
        SetCnt(createCnt);
    }

    /// <summary>
    /// CreateBtn
    /// </summary>
    public void OnCreateBtn()
    {
        if(craftSystem == null)
        {
            craftSystem = GameManager.Instance.CraftSystem;
        }
        
        craftSystem.CreateItem(needLists, item, createCnt);

        //UI 세팅
        SetUI();
    }
}
