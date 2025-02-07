using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SceneType 사용자 변수
/// </summary>
public enum SceneType
{
    GameStart,
    SampleScene,
    CraftUI
}
public class SceneChanger : Singleton<SceneChanger>
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

    }

    public void GoCraftUI()
    {
        SceneManager.LoadScene("CraftUI", LoadSceneMode.Additive);
        //SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
    }
}
