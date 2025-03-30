using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : Singleton<ShopUI>
{
    public GameObject window;

    [SerializeField] Transform[] buySlots;
    [SerializeField] List<Transform> invenSlots;
    [SerializeField] Transform[] quickSlots;
    [SerializeField] InvenItem invenItem;
    [SerializeField] Transform[] merchInvenSlots;
    [SerializeField] GameObject merchInvenUI;
    [SerializeField] TMP_Text goldTxt;


    private InventoryData invenData;
    private Inventory inven;
    private PlayerData pd;

    // Start is called before the first frame update
    void Start()
    {
        inven = GameManager.Instance.Inven;
        invenData = inven.inventoryData;
        pd = GameManager.Instance.PlayerData;
        Init();
    }

    void Init()
    {
        int index = inven.invenSlots.Length;
        for(int i =0; i<index; i++)
        {
            invenSlots.Add(inven.invenSlots[i]);
        }
    }

    public void SetShopInven()
    {
        for (int i = 0; i < merchInvenSlots.Length; i++)
        {
            if (merchInvenSlots[i].transform.childCount >= 1)
            {
                Destroy(merchInvenSlots[i].GetChild(0).gameObject);
            }
        }

        for (int i = 0; i < inven.invenSlots.Length; i++)
        {
            if (inven.invenSlots[i] == null)
            {
                return;
            }
            if (inven.invenSlots[i].GetComponent<Slot>().isFilled)
            {
                merchInvenUI = inven.invenSlots[i].GetChild(0).gameObject;
                Instantiate(merchInvenUI, merchInvenSlots[i]);
            }
        }

        for (int i = 0; i < inven.quickSlotsInven.Length; i++)
        {
            if(inven.quickSlotsInven[i].GetComponent<QuickSlotInven>().isFilled)
            {
                merchInvenUI = inven.quickSlotsInven[i].GetChild(0).gameObject;
                Instantiate(merchInvenUI, quickSlots[i]);
            }
        }
        goldTxt.text = $"{pd.Gold}";
    }

    public void  SellItemCheck(InvenItem item, int count)
    {

        if(item.transform.parent.GetComponent<Slot>())
        {
            int index = item.transform.parent.GetSiblingIndex();
            //전체 판매할 경우
            if (merchInvenSlots[index].GetChild(0).GetComponent<InvenItem>().data.count == count)
            {
                //인벤토리에서 아이템 삭제
                Destroy(inven.invenSlots[index].GetChild(0).gameObject);
                //인벤토리 빈 것으로 체크
                inven.invenSlots[index].GetComponent<Slot>().isFilled = false;
            }

            //일부만 판매할 경우
            else if (merchInvenSlots[index].GetChild(0).GetComponent<InvenItem>().data.count > count)
            {
                //인벤토리에서 수량만 변경
                inven.InvenItemCntChange(inven.invenSlots[index].GetChild(0).GetComponent<InvenItem>(), -count);
            }
        }
        else if (item.transform.parent.GetComponent<QuickSlotInven>())
        {
            int index = item.data.quickSlotIdx;
            if (quickSlots[index].GetChild(0).GetComponent<InvenItem>().data.count == count)
            {
                Destroy(inven.quickSlotsInven[index].GetChild(0).gameObject);
                inven.quickSlotsInven[index].GetComponent<QuickSlotInven>().isFilled = false;
                Destroy(inven.quickSlotsInven[index].GetComponent<InvenItem>().data.qItem);
            }
            else if (quickSlots[index].GetChild(0).GetComponent<InvenItem>().data.count > count)
            {
                inven.InvenItemCntChange(inven.quickSlotsInven[index].GetChild(0).GetComponent<InvenItem>(), -count);
            }
        }

        goldTxt.text = $"{pd.Gold}";
    }

    public void OnExitBtn()
    {
        window.SetActive(false);
        //카메라 움직임, 캐릭터 움직임 정지
        GameManager.Instance.isPaused = false;
        Camera.main.GetComponent<CameraMove>().enabled = true;
    }
}
