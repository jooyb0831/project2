using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NeedItemUI : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemTitle;
    [SerializeField] TMP_Text itemCntTxt;
    int needCnt; //필요한 갯수
    int curCnt; //현재 인벤에 있는 갯수
    int createCnt; //제작할 갯수
    int itemIdx = -1; //아이템 인덱스 번호
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

    /// <summary>
    /// 인벤토리에서 아이템 갯수찾기
    /// </summary>
    /// <param name="itemCodeIdx"></param>
    /// <returns></returns>
    int FindInvenCnt(int itemCodeIdx)
    {
        if(inven == null)
        {
            inven = GameManager.Instance.Inven;
        }
        return inven.FindItemCnt(itemCodeIdx);
    }

    /// <summary>
    /// 데이터 세팅
    /// </summary>
    /// <param name="data"></param>
    /// <param name="needCnt"></param>
    /// <param name="createCnt"></param>
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

    /// <summary>
    /// 수량 세팅
    /// </summary>
    /// <param name="createCnt"></param>
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
    /// 인벤토리에서 아이템 찾아서 수량 변경
    /// </summary>
    public void FindUSeItem(int createCnt)
    {
        InvenItem invenItem = inven.FindItem(this.data.itemIdx);
        inven.InvenItemCntChange(invenItem, needCnt * createCnt * (-1));
    }

    /// <summary>
    /// 수량 체크
    /// </summary>
    /// <returns></returns>
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
