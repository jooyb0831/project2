using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NeedItemUI : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemTitle;
    [SerializeField] TMP_Text itemCntTxt;
    int needCnt;
    int curCnt;
    int createCnt;
    int itemIdx = -1;
    private ItemData data;
    private Inventory inven;
    // Start is called before the first frame update
    void Start()
    {
        inven = GameManager.Instance.Inven;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int FindInvenCnt(int itemCodeIdx)
    {
        if(inven == null)
        {
            inven = GameManager.Instance.Inven;
        }
        return inven.FindItemCnt(itemCodeIdx);
    }

    public void SetData(ItemData data, int needCnt, int createCnt)
    {
        this.data = data;
        this.needCnt = needCnt;
        this.createCnt = createCnt;
        int totalCnt = needCnt * createCnt;
        this.itemIdx = data.itemIdx;
        curCnt = FindInvenCnt(data.itemIdx);
        itemIcon.sprite = data.invenIcon;
        itemTitle.text = data.itemTitle;
        itemCntTxt.text = $"{curCnt} / {totalCnt}";
        if(curCnt < totalCnt)
        {
            itemCntTxt.color = Color.red;
        }
        else
        {
            itemCntTxt.color = Color.white;
        }
    }

    public void SetCnt(int createCnt)
    {
        this.createCnt = createCnt;
        int totalCnt = needCnt * createCnt;
        curCnt = FindInvenCnt(itemIdx);
        itemCntTxt.text = $"{curCnt} / {totalCnt}";
        if(curCnt<totalCnt)
        {
            itemCntTxt.color = Color.red;
        }
        else
        {
            itemCntTxt.color = Color.white;
        }
    }

    /// <summary>
    /// 인벤토리에서 아이템 찾아서 수량 감소
    /// </summary>
    public void FindUSeItem(int createCnt)
    {
        InvenItem invenItem = inven.FindItem(this.data.itemIdx);
        inven.InvenItemCntChange(invenItem, needCnt * createCnt * (-1));
    }

    public bool CntCheck()
    {
        if (curCnt < needCnt * createCnt)
        {
            return false;
        }

        else
        {
            return true;
        }

    }
}
