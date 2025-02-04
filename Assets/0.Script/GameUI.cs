using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;

public class GameUI : Singleton<GameUI>
{
    private PlayerData pd;

    public GameObject arrowUI;

    [SerializeField] Image hpBarImg;
    [SerializeField] TMP_Text hpTxt;

    [SerializeField] Image stBarImg;
    [SerializeField] TMP_Text stTxt;

    [SerializeField] Image lvBarImg;
    [SerializeField] TMP_Text lvTxt;

    [SerializeField] Image mpImg;
    [SerializeField] TMP_Text mpTxt;

    public GameObject spUI;
    [SerializeField] Image spBarImg;

    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
        hpBarImg.fillAmount = (float)((float)pd.HP / (float)pd.MAXHP);
        stBarImg.fillAmount = (float)((float)pd.ST / (float)pd.MAXST);
        lvBarImg.fillAmount = (float)((float)pd.EXP / (float)pd.MAXEXP);
        spBarImg.fillAmount = (float)((float)pd.SP / (float)pd.MAXSP);
        mpImg.fillAmount = (float)((float)pd.CURMP/(float)pd.MAXMP);
        hpTxt.text = $"{pd.HP}/{pd.MAXHP}";
        stTxt.text = $"{pd.ST}/{pd.MAXST}";
        lvTxt.text = $"Lv.{pd.Level}";
        mpTxt.text = $"{pd.CURMP}/{pd.MAXMP}";
    }
    void Update()
    {
        
    }

    public int Level
    {
        set
        {
            if(pd == null)
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
            if(pd ==  null)
            {
                pd = GameManager.Instance.PlayerData;
            }
            lvBarImg.fillAmount = ((float)pd.EXP/pd.MAXEXP);
        }
    }

    public int MAXEXP
    {
        set
        {
            if(pd == null)
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
            hpBarImg.DOFillAmount(((float)pd.HP / pd.MAXHP),0.2f);
            //hpBarImg.fillAmount = ((float)pd.HP / pd.MAXHP);
            hpTxt.text = $"{pd.HP}/{pd.MAXHP}";
        }
    }

    public int MAXST
    {
        set
        {
            if(pd == null)
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
            if(pd==null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            stBarImg.fillAmount = ((float)pd.ST/pd.MAXST);
            stTxt.text = $"{pd.ST}/{pd.MAXST}";
        }
    }

    public int MAXSP
    {
        set
        {
            if(pd == null)
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
            if(pd == null)
            {
                pd = GameManager.Instance.PlayerData;
                return;
            }
            spBarImg.DOFillAmount(((float)pd.SP / pd.MAXSP),1f);
            //spBarImg.fillAmount = ((float)pd.SP / pd.MAXSP);
        }
    }
    
    public int CURMP
    {
        set
        {
            if(pd == null)
            {
                pd=GameManager.Instance.PlayerData;
                return;
            }
            mpImg.DOFillAmount(((float)pd.CURMP/pd.MAXMP), 0.2f);
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
            mpImg.fillAmount = Mathf.Lerp(((float)pd.CURMP / pd.MAXMP),1,1);
            mpTxt.text = $"{pd.CURMP}/{pd.MAXMP}";
        }
    }

}
