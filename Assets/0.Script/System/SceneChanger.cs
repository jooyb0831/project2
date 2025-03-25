using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    NPC
}
public class SceneChanger : Singleton<SceneChanger>
{
    public SceneType sceneType = SceneType.Lobby;
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

    public void GoStage1()
    {
        SceneManager.LoadScene("Stage1");
        SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
        
    }

    public void GoMine()
    {
        SceneManager.LoadScene("Mine");
        SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
    }

    public void GoLobby()
    {
        SceneManager.LoadScene("Lobby");
        SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
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
}
