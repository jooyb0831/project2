using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameOverUI : MonoBehaviour
{
    private SceneChanger sceneChanger;
    private PlayerData pd;
    private Player player;

    [SerializeField] Image panel;
    [SerializeField] TMP_Text deadTxt;
    [SerializeField] Button restartBtn;
    [SerializeField] TMP_Text restartTxt;
    [SerializeField] Button exitBtn;
    [SerializeField] TMP_Text exitTxt;
    
    
    void OnEnable()
    {
        panel.DOFade(1f, 1.5f).OnComplete(() =>
        {
            deadTxt.DOFade(1, 1f).OnComplete(() =>
            {
                GameManager.Instance.PauseScene(true);
                restartBtn.GetComponent<Image>().DOFade(1, 1.5f);
                restartTxt.DOFade(1, 1.5f);
                exitBtn.GetComponent<Image>().DOFade(1, 1.5f);
                exitTxt.DOFade(1, 1.5f);
            });
        });
    }

    void OnDisable()
    {
        panel.color = new Color(0, 0, 0, 50);
        deadTxt.alpha = 0f;
        restartBtn.GetComponent<Image>().color = new Color(164, 121, 106, 0f);
        restartTxt.color = new Color(255, 194, 8, 0);
        exitBtn.GetComponent<Image>().color = new Color(84, 84, 84, 0);
        exitTxt.color = new Color(255, 255, 255, 0);
    }

    void Start()
    {
        sceneChanger = GameManager.Instance.SceneChanger;
        pd = GameManager.Instance.PlayerData;
        player = GameManager.Instance.Player;
    }

    public void OnRestartBtnClicked()
    {
        pd.HP = pd.MAXHP;
        sceneChanger.GoLobby();
        player.state = Player.State.Idle;
        gameObject.SetActive(false);
        GameManager.Instance.PauseScene(false);
    }

    public void OnExitBtnClicked()
    {
        pd.HP = pd.MAXHP;
        GameManager.Instance.PauseScene(false);
        sceneChanger.GoGameStart();
    }
}
