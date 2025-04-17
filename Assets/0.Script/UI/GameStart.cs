using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    private SceneChanger sc;

    [SerializeField] Button gameStartBtn;
    [SerializeField] Button helpBtn;
    [SerializeField] Button exitBtn;

    [SerializeField] GameObject helpObj;
    // Start is called before the first frame update
    void Start()
    {
        sc = GameManager.Instance.SceneChanger;
    }

    public void OnGameStartBtn()
    {
        Cursor.visible = false;
        sc.GoLobby();
    }

    public void OnHelpBtn()
    {
        helpObj.SetActive(true);
    }

    public void OnExitBtn()
    {
        Application.Quit();
    }

}
