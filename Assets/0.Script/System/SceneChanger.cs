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
    Lobby,
    SampleScene,
    CraftUI,
    NPC,
    Loading,
    Shop,
    Stage1,
    Stage2,
    Stage3,
    Mine,
    Forest,
    Fishing
}

public class SceneChanger : Singleton<SceneChanger>
{
    public SceneType sceneType = SceneType.GameStart;
    
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    //각 씬별로 이동할 때 참조할 수 있도록 함수 생성

    public void GoCraftUI()
    {
        SceneManager.LoadScene("CraftUI", LoadSceneMode.Additive);
    }

    public void GoStage1()
    {
        LoadingSceneManger.LoadScene("Stage1");
        sceneType = SceneType.Stage1;
    }

    public void GoStage2()
    {
        LoadingSceneManger.LoadScene("Stage2");
        sceneType = SceneType.Stage2;
    }

    public void GoStage3()
    {
        LoadingSceneManger.LoadScene("Stage3");
        sceneType = SceneType.Stage3;
    }

    public void GoMine()
    {
        LoadingSceneManger.LoadScene("Mine");
        sceneType = SceneType.Mine;
    }

    public void GoForest()
    {
        LoadingSceneManger.LoadScene("Forest");
        sceneType = SceneType.Forest;
    }

    public void GoGameStart()
    {
        LoadingSceneManger.LoadScene("GameStart");
        sceneType = SceneType.GameStart;
    }

    public void GoLobby()
    {
        LoadingSceneManger.LoadScene("Lobby");
        sceneType = SceneType.Lobby;
        Cursor.visible = false;
    }

    public void GoNPC(bool isLoad)
    {
        if(isLoad)
        {
            SceneManager.LoadScene("NPC", LoadSceneMode.Additive);
            sceneType = SceneType.NPC;
        }
        else
        {
            SceneManager.UnloadSceneAsync("NPC");
            sceneType = SceneType.Lobby;
        }
    }

    public void GoFishing()
    {
        SceneManager.LoadScene("GameUI");
        SceneManager.LoadScene("Fishing", LoadSceneMode.Additive);
    }
}
