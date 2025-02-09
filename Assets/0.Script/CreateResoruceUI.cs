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

        //������ �������� UI����
        this.item = item;
        itemIcon.sprite = item.itemData.invenIcon;
        itemTitle.text = item.itemData.itemTitle;
        int curCnt = inven.FindItem(item.itemData.itemIdx).data.count;
        curItemCnt.text = $"���� : {curCnt}";

        //��� ����Ʈ ������Ʈ ����
        SetList(data.Resources.Length);

        //����Ʈ ������Ʈ�� ������ ����
        //������ ������ �������� Resources�� �����͸� �޾ƿ;� ��.
        for(int i = 0; i<needLists.Count; i++)
        {
            needLists[i].SetData(data.Resources[i].itemData, data.RcCnts[i]);
        }
    }


    void SetList(int count)
    {
        //�켱 ������ ����Ʈ Ŭ����
        needLists.Clear();

        //����Ʈ�� ������Ʈ ���� �����ϰ� Add
        for(int i = 0; i<count; i++)
        {
            NeedItemUI obj = Instantiate(sampleNeedUI, contentArea);
            needLists.Add(obj);
        }
    }
}
