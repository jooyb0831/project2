using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;

public class GameUI : Singleton<GameUI>
{
    //플레이어 데이터 받아오는 변수값
    private PlayerData pd;

    private Pooling pooling;
    
#region 변수선언
    //화살 UI 연결 변수
    public GameObject arrowUI;

    //QuestUI배치할 Transform
    public Transform questArea;


    [SerializeField] GameObject UI;
    [SerializeField] Image hpBarImg;
    [SerializeField] TMP_Text hpTxt;

    [SerializeField] Image stBarImg;
    [SerializeField] TMP_Text stTxt;

    [SerializeField] Image lvBarImg;
    [SerializeField] TMP_Text lvTxt;

    [SerializeField] Image mpImg;
    [SerializeField] TMP_Text mpTxt;

    [SerializeField] TMP_Text goldTxt;

    public GameObject spUI;
    [SerializeField] Image spBarImg;

    [SerializeField] ItemInfoArea infoArea;
    [SerializeField] ItemGetUI itemGetObj;

    [SerializeField] NoticeInfoArea noticeArea;
    [SerializeField] NoticeUI noticeObj;

    [SerializeField] GameOverUI gameOverUI;

    [SerializeField] InvenItemInfo itemExplainWindow;
    [SerializeField] Transform menuTransform;
#endregion

    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
        pooling = GameManager.Instance.Pooling;
        hpBarImg.fillAmount = (float)((float)pd.HP / (float)pd.MAXHP);
        stBarImg.fillAmount = (float)((float)pd.ST / (float)pd.MAXST);
        lvBarImg.fillAmount = (float)((float)pd.EXP / (float)pd.MAXEXP);
        spBarImg.fillAmount = (float)((float)pd.SP / (float)pd.MAXSP);
        mpImg.fillAmount = (float)((float)pd.CURMP / (float)pd.MAXMP);
        hpTxt.text = $"{pd.HP}/{pd.MAXHP}";
        stTxt.text = $"{pd.ST}/{pd.MAXST}";
        lvTxt.text = $"Lv.{pd.Level}";
        mpTxt.text = $"{pd.CURMP}/{pd.MAXMP}";
        goldTxt.text = $"{pd.Gold}";
    }

    /// <summary>
    /// 게임오버시 호출되는 함수
    /// </summary>
    public void GameOver()
    {
        gameOverUI.gameObject.SetActive(true);
    }

    /// <summary>
    /// QuestQuickUI 스위치하는 함수
    /// </summary>
    /// <param name="isOn"></param>
    public void UISwitch(bool isOn)
    {
        if(isOn)
        {
            UI.SetActive(false);
        }
        else
        {
            UI.SetActive(true);
        }
        
    }

    public int Level
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            lvTxt.text = $"Lv.{pd.Level}";
        }
    }

    public int EXP
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
            }
            lvBarImg.fillAmount = ((float)pd.EXP / pd.MAXEXP);
        }
    }

    public int MAXEXP
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
            }
        }
    }


    public int MAXHP
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            hpBarImg.fillAmount = ((float)pd.HP / pd.MAXHP);
            hpTxt.text = $"{pd.HP}/{pd.MAXHP}";
        }
    }

    public int HP
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            hpBarImg.DOFillAmount(((float)pd.HP / pd.MAXHP), 0.2f);
            //hpBarImg.fillAmount = ((float)pd.HP / pd.MAXHP);
            hpTxt.text = $"{pd.HP}/{pd.MAXHP}";
        }
    }

    public int MAXST
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            stBarImg.fillAmount = ((float)pd.ST / pd.MAXST);
            stTxt.text = $"{pd.ST}/{pd.MAXST}";

        }
    }

    public int ST
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            stBarImg.fillAmount = ((float)pd.ST / pd.MAXST);
            stTxt.text = $"{pd.ST}/{pd.MAXST}";
        }
    }

    public int MAXSP
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            spBarImg.fillAmount = ((float)pd.SP / pd.MAXSP);
        }
    }

    public int SP
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            spBarImg.DOFillAmount(((float)pd.SP / pd.MAXSP), 1f);
            //spBarImg.fillAmount = ((float)pd.SP / pd.MAXSP);
        }
    }

    public int CURMP
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            mpImg.DOFillAmount(((float)pd.CURMP / pd.MAXMP), 0.2f);
            mpTxt.text = $"{pd.CURMP}/{pd.MAXMP}";
        }
    }

    public int MAXMP
    {
        set
        {
            if (pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            mpImg.fillAmount = Mathf.Lerp(((float)pd.CURMP / pd.MAXMP), 1, 1);
            mpTxt.text = $"{pd.CURMP}/{pd.MAXMP}";
        }
    }

    public int Gold
    {
        set
        {
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            goldTxt.text = $"{pd.Gold}";
        }
    }


    /// <summary>
    /// 아이템 획득 시 UI표기
    /// </summary>
    /// <param name="data"></param>
    public void GetItem(ItemData data)
    {
        bool itemCheck = infoArea.ItemCheck(data);
        if (itemCheck)
        {
            infoArea.AddData(data);
        }
        else
        {
            ItemGetUI obj = pooling.GetPool(DicKey.itemGetUI, infoArea.transform).GetComponent<ItemGetUI>();
            obj.SetData(data, data.count);
            obj.transform.SetAsLastSibling();
            obj.FadeIn();
            infoArea.getUIList.Add(obj);
            infoArea.itemTitleList.Add(data.itemTitle);
            obj.isSet = true;
        }
    }

    private string[] noticeTxtList = 
    {"기력이 부족합니다.", "마력이 부족합니다", "골드가 부족합니다.", "재료가 부족합니다.", "아직 스킬을 사용할 수 없습니다.",
    "장착된 활이 없습니다.", "화살이 부족합니다."};

    /// <summary>
    /// UI에 정보 표시하기
    /// </summary>
    public void DisplayInfo(int messageCode)
    {
        NoticeUI obj = pooling.GetPool(DicKey.noticeUI, noticeArea.transform).GetComponent<NoticeUI>();
        obj.SetMessage(noticeTxtList[messageCode]);
        obj.transform.SetAsLastSibling();
        obj.FadeIn();
        obj.isSet = true;
    }

    public void ShowItemExplainWindow(InvenItem item, Transform slot)
    {
        itemExplainWindow.transform.SetParent(slot);
        itemExplainWindow.transform.localPosition = new Vector3(-175f, 0f, 0f);
        itemExplainWindow.transform.SetParent(menuTransform);
        itemExplainWindow.transform.SetAsLastSibling();
        itemExplainWindow.SetData(item);
        itemExplainWindow.gameObject.SetActive(true);
    }

    public void HideItemExplainWindow()
    {
        itemExplainWindow.gameObject.SetActive(false);
    }
}
