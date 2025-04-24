using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishResultUI : MonoBehaviour
{
    private PlayerData pd;
    private SceneChanger sceneChanger;
    private GameUI gameUI;

    [SerializeField] FishSpawn fishSpawn;
    [SerializeField] TMP_Text resultTxt;
    

    void Start()
    {
        pd = GameManager.Instance.PlayerData;
        sceneChanger = GameManager.Instance.SceneChanger;
        gameUI = GameManager.Instance.GameUI;
    }

    /// <summary>
    /// 결과Txt 표시
    /// </summary>
    /// <param name="cnt"></param>
    public void SetUpResult(int cnt)
    {
        resultTxt.text = $"물고기 {cnt}마리를 잡았습니다!";
    }

    /// <summary>
    /// 재시작 버튼 클릭 시 호출
    /// </summary>
    public void OnRestartBtn()
    {
        //기력이 부족할 경우 LobbyScene으로 돌아가기
        if (pd.ST < 5)
        {
            gameUI.DisplayInfo(0);
            sceneChanger.GoLobby();
            return;
        }

        //스태미너 감소
        pd.ST -= 5;

        gameObject.SetActive(false);
        fishSpawn.Initialize();
    }

    /// <summary>
    /// 나가기 버튼 클릭 시 호출
    /// </summary>
    public void OnExitBtn()
    {
        sceneChanger.GoLobby();
    }

}
