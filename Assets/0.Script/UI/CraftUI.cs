using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI : Singleton<CraftUI>
{
    private CraftSystem craftSystem;

    [SerializeField] GameObject craftWindow;

    [SerializeField] CreateItemIndexUI sample;

    [SerializeField] List<CreateItemIndexUI> itemLists;

    [SerializeField] Transform area;

    [SerializeField] ToggleGroup recipeToggleGroup;

    [SerializeField] CreateResoruceUI resourceBG;

    // Start is called before the first frame update
    void Start()
    {
        craftSystem = GameManager.Instance.CraftSystem;
        Init();
    }

    void Init()
    {
        SetListUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ������ ������ ������Ʈ ���� �� ����Ʈ�� �ֱ�
    /// </summary>
    void SetListUp()
    {
        int count = craftSystem.itemRecipeDatas.Count;
        for(int i = 0; i<count; i++)
        {
            CreateItemIndexUI obj = Instantiate(sample, area);
            itemLists.Add(obj);
        }
    }

    /// <summary>
    /// ������ �������� ������ ����
    /// </summary>
    void SetItemListData()
    {
        for(int i = 0; i<craftSystem.itemRecipeDatas.Count; i++)
        {
            itemLists[i].SetData(craftSystem.itemRecipeDatas[i]);
            itemLists[i].GetComponent<Toggle>().group = recipeToggleGroup;
            itemLists[i].resourceBG = this.resourceBG;
        }
    }

    public void EnableWindow()
    {
        craftWindow.SetActive(true);
        SetItemListData();
    }

    public void OnExitBtn()
    {
        craftWindow.SetActive(false);
    }

}
