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

    [SerializeField] int onIdx = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            menuObj.SetActive(true);
            GameManager.Instance.isPaused = true;
            Camera.main.GetComponent<CameraMove>().enabled = false;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuObj.activeSelf)
            {
                OnExitBtn();
            }
        }
    }

    public void OnExitBtn()
    {
        menuObj.SetActive(false);
        GameManager.Instance.isPaused = false;
        Camera.main.GetComponent<CameraMove>().enabled = true;
    }

    public void OnToggleChanged(int idx)
    {
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
