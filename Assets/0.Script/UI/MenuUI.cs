using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuUI : MonoBehaviour
{

    [SerializeField] GameObject menuObj;
    [SerializeField] Toggle[] menuTabs;
    [SerializeField] GameObject[] menuUIs;
    public Transform questArea;

    private Player p;
    private GameUI gameUI;

    int onIdx = -1;

    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        gameUI = GameManager.Instance.GameUI;        
    }

    // Update is called once per frame
    void Update()
    {
        //메뉴창 열기(Tab키)
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            menuObj.SetActive(true);
            menuUIs[0].GetComponent<CharUI>().SetUI();

            //카메라 움직임, 캐릭터 움직임 정지
            GameManager.Instance.PauseScene(true);
            p.charUICam.gameObject.SetActive(true);
        }

        //ESC를 누르면 메뉴 닫기
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuObj.activeSelf)
            {
                OnExitBtn();
                p.charUICam.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 창 닫기
    /// </summary>
    public void OnExitBtn()
    {
        GameManager.Instance.PauseScene(false);
        menuObj.SetActive(false);
    }

    /// <summary>
    /// 토글에 따라서 UI세팅
    /// </summary>
    /// <param name="idx"></param>
    public void OnToggleChanged(int idx)
    {
        //Toggle.isOn이면 해당하는 UI Active
        if(menuTabs[idx].isOn)
        {
            menuUIs[idx].SetActive(true);
            onIdx = idx;
        }
        else if(!menuTabs[idx].isOn)
        {
            menuUIs[idx].SetActive(false);
            //menuTabs[idx].GetComponent<Image>
        }
    }
}
