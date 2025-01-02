using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : Singleton<GameUI>
{
    private PlayerData pd;

    public GameObject arrowUI;

    [SerializeField] Image hpBarImg;
    [SerializeField] TMP_Text hpTxt;

    public GameObject spUI;
    [SerializeField] Image spBarImg;

    [SerializeField] GameObject menuObj;
    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
        hpBarImg.fillAmount = (float)((float)pd.HP / (float)pd.MAXHP);
        spBarImg.fillAmount = (float)((float)pd.SP / (float)pd.MAXSP);
        hpTxt.text = $"{pd.HP}/{pd.MAXHP}";
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            menuObj.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuObj.activeSelf)
            {
                menuObj.SetActive(false);
            }
        }

        if(menuObj.activeSelf)
        {
            //Time.timeScale = 0;
            Camera.main.GetComponent<CameraMove>().enabled = false;

        }
        else
        {
            //Time.timeScale = 1;
            Camera.main.GetComponent<CameraMove>().enabled = true;
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
            hpBarImg.fillAmount = ((float)pd.HP / pd.MAXHP);
            hpTxt.text = $"{pd.HP}/{pd.MAXHP}";
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
            spBarImg.fillAmount = ((float)pd.SP / pd.MAXSP);
        }
    }
}
