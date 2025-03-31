using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : Singleton<StageSelect>
{
    private PlayerData pd;
    private SceneChanger sc;

    [SerializeField] GameObject mapObj;
    [SerializeField] Button stage2Btn;
    [SerializeField] GameObject stage2Cover;
    [SerializeField] GameObject stage2Label;
    [SerializeField] Button stage3Btn;
    [SerializeField] GameObject stage3Cover;
    [SerializeField] GameObject stage3Label;
    // Start is called before the first frame update
    void Start()
    {
        sc = GameManager.Instance.SceneChanger;
        pd = GameManager.Instance.PlayerData;
    }

    public void TurnOnMap()
    {
        mapObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //스테이지 1이 클리어 되었으면 2오픈
        if (pd.StageCleared[0])
        {
            stage2Btn.interactable = true;
            stage2Cover.SetActive(false);
            stage2Label.SetActive(true);
        }

        if (pd.StageCleared[1])
        {
            stage3Btn.interactable = true;
            stage3Cover.SetActive(false);
            stage3Label.SetActive(true);
        }
    }

    public void StageBtnClicked(int stageNum)
    {
        switch(stageNum)
        {
            case 0 :
            {
                sc.GoStage1();
                break;
            }
            case 1 :
            {
                sc.GoStage2();
                break;
            }
            case 2 :
            {
                sc.GoStage3();
                break;
            }
        }
    }
}
