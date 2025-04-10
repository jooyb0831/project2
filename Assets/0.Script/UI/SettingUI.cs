using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
    private SceneChanger sceneChanger;
    [SerializeField] GameObject helpWindow;

    void Start()
    {
        sceneChanger = GameManager.Instance.SceneChanger;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackBtn();
        }
    }

    public void OnTitleBtn()
    {
        sceneChanger.GoGameStart();
    }

    public void OnHelpBtn()
    {
        helpWindow.SetActive(true);
    }

    public void OnBackBtn()
    {
        gameObject.SetActive(false);
    }
}
