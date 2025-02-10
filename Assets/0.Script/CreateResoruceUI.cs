using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateResoruceUI : MonoBehaviour
{
    private Inventory inven;
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

    // Start is called before the first frame update
    void Start()
    {
        inven = GameManager.Instance.Inven;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(FieldItem item, ItemRecipeData data)
    {
        if(cover.activeSelf)
        {
            cover.SetActive(false);
            slot.SetActive(true);
        }

        //제작할 아이템의 UI세팅
        this.item = item;
        itemIcon.sprite = item.itemData.invenIcon;
        itemTitle.text = item.itemData.itemTitle;
        int curCnt = inven.FindItemCnt(item.itemData.itemIdx);
        curItemCnt.text = $"보유 : {curCnt}";
        createCnt = 1;
        cntInput.text = createCnt.ToString();

        //재료 리스트 오브젝트 생성
        SetList(data.Resources.Length);

        //리스트 오브젝트의 데이터 세팅
        //아이템 레시피 데이터의 Resources의 데이터를 받아와야 함.
        for(int i = 0; i<needLists.Count; i++)
        {
            needLists[i].SetData(data.Resources[i].itemData, data.RcCnts[i], createCnt);
        }
    }


    void SetList(int count)
    {
        //기존 리스트 및 오브젝트 클리어
        if(needLists.Count !=0 )
        {
            foreach (var item in needLists)
            {
                Destroy(item.gameObject);
            }
            needLists.Clear();
        }


        //리스트에 오브젝트 새로 생성하고 Add
        for(int i = 0; i<count; i++)
        {
            NeedItemUI obj = Instantiate(sampleNeedUI, contentArea);
            needLists.Add(obj);
        }
    }


    public void OnInputField()
    {
        string cntStr = cntInput.text;
        createCnt = int.Parse(cntStr);
        foreach(var item in needLists)
        {
            item.SetCnt(createCnt);
        }
    }

    public void OnPlusBtn()
    {
        int x = int.Parse(cntInput.text);
        x++;
        createCnt = x;
        cntInput.text = $"{createCnt}";
        foreach(var item in needLists)
        {
            item.SetCnt(createCnt);
        }
    }

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

        foreach(var item in needLists)
        {
            item.SetCnt(createCnt);
        }
    }

    public void OnCreateBtn()
    {
        //만약 하나라도 재료가 부족하다면 리턴
        foreach(var item in needLists)
        {
            bool cntCheck = item.CntCheck();
            Debug.Log(cntCheck);
            if(!cntCheck)
            {
                Debug.Log("재료가 부족");
                return;
            }
        }

        //아이템 추가
        inven.GetItem(item.itemData);

        //아이템 재료 소모처리(인벤에서)
        foreach(var item in needLists)
        {
            item.FindUSeItem(createCnt);
        }

        //UI리셋
        int curCnt = inven.FindItemCnt(item.itemData.itemIdx);
        curItemCnt.text = $"보유 : {curCnt}";
        createCnt = 1;
        cntInput.text = createCnt.ToString();
        foreach(var item in needLists)
        {
            item.SetCnt(createCnt);
        }
    }
}
