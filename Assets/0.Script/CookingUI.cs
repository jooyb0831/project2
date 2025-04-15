using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingUI : Singleton<CookingUI>
{
    private CraftSystem craftSystem;

    #region UI관련 변수
    [SerializeField] GameObject craftWindow;

    [SerializeField] CreateItemIndexUI sample;

    [SerializeField] List<CreateItemIndexUI> itemLists;

    [SerializeField] Transform area;

    [SerializeField] ToggleGroup recipeToggleGroup;

    [SerializeField] CreateResoruceUI resourceBG;
    #endregion



    void Start()
    {
        craftSystem = GameManager.Instance.CraftSystem;
        Init();
    }

    void Init()
    {
        SetListUp();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (craftWindow.activeSelf)
            {
                OnExitBtn();
            }
        }
    }

    /// <summary>
    /// 아이템 레시피 오브젝트 생성 및 리스트에 넣기
    /// </summary>
    void SetListUp()
    {
        int count = craftSystem.foodRecipeDatas.Count;
        for (int i = 0; i < count; i++)
        {
            CreateItemIndexUI obj = Instantiate(sample, area);
            itemLists.Add(obj);
        }
    }

    /// <summary>
    /// 아이템 레시피의 데이터 세팅
    /// </summary>
    void SetItemListData()
    {
        for (int i = 0; i < craftSystem.foodRecipeDatas.Count; i++)
        {
            itemLists[i].SetData(craftSystem.foodRecipeDatas[i]);
            itemLists[i].GetComponent<Toggle>().group = recipeToggleGroup;
            itemLists[i].GetComponent<Toggle>().isOn = false;
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
        GameManager.Instance.isPaused = false;
        Camera.main.GetComponent<CameraMove>().enabled = true;
        craftWindow.SetActive(false);
    }
}
