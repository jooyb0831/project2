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
        int curCnt = inven.FindItem(item.itemData.itemIdx).data.count;
        curItemCnt.text = $"보유 : {curCnt}";

        //재료 리스트 오브젝트 생성
        SetList(data.Resources.Length);

        //리스트 오브젝트의 데이터 세팅
        //아이템 레시피 데이터의 Resources의 데이터를 받아와야 함.
        for(int i = 0; i<needLists.Count; i++)
        {
            needLists[i].SetData(data.Resources[i].itemData, data.RcCnts[i]);
        }
    }


    void SetList(int count)
    {
        //우선 기존의 리스트 클리어
        needLists.Clear();

        //리스트에 오브젝트 새로 생성하고 Add
        for(int i = 0; i<count; i++)
        {
            NeedItemUI obj = Instantiate(sampleNeedUI, contentArea);
            needLists.Add(obj);
        }
    }
}
