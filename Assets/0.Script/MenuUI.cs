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

    int onIdx = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�޴�â ����(TabŰ)
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            menuObj.SetActive(true);

            //ī�޶� ������, ĳ���� ������ ����
            GameManager.Instance.isPaused = true;
            Camera.main.GetComponent<CameraMove>().enabled = false;
        }

        //ESC�� ������ �޴� �ݱ�
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuObj.activeSelf)
            {
                OnExitBtn();
            }
        }
    }

    /// <summary>
    /// â �ݱ�
    /// </summary>
    public void OnExitBtn()
    {
        menuObj.SetActive(false);
        GameManager.Instance.isPaused = false;
        Camera.main.GetComponent<CameraMove>().enabled = true;
    }

    /// <summary>
    /// ��ۿ� ���� UI����
    /// </summary>
    /// <param name="idx"></param>
    public void OnToggleChanged(int idx)
    {
        //Toggle.isOn�̸� �ش��ϴ� UI Active
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
