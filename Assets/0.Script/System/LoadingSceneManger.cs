using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class LoadingSceneManger : MonoBehaviour
{
    public static string nextScene;

    private Inventory inven;
    private SceneChanger sc;

    [SerializeField] Image progressBar;
    [SerializeField] Image fadeScreen;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] TMP_Text tipTxt;

    [SerializeField] string[] tipList;

    void Start()
    {
        inven = GameManager.Instance.Inven;
        sc = GameManager.Instance.SceneChanger;
        SetUpTipText();
        fadeScreen.color = new Color(0, 0, 0, 1);
        fadeScreen.DOFade(0, 0.5f)
        .OnComplete(() =>
        {
            //Fade가 끝나면 로딩 씬 호출
            StartCoroutine(LoadScene());
        });
    }

    void SetUpTipText()
    {
        int idx = Random.Range(0, tipList.Length);
        tipTxt.text = $"Tip.{tipList[idx]}";
    }

    /// <summary>
    /// SceneName을 인수로 받아서 씬을 로드하는 함수
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    /// <summary>
    /// SceneLoad Progress에 동기화되는 ProgressBar
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        //로딩씬 오브젝트 활성화
        loadingScreen.SetActive(true);
        //씬 타입을 Loading으로 변경
        sc.sceneType = SceneType.Loading;
        yield return null;

        //nextScene의 진행도 받아오기
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;

        //Progress가 끝나지 않았을 경우
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            // progress가 90%미만일 경우
            if (op.progress < 0.9f)
            {
                //progressBar의 fillAmount 진행.
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                //Progress보다 fillamount가 커지면 시간 = 0
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }

            //Progress가 끝났을 경우
            else
            {
                //fillAmount를 1로(완료된 상태)
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    //다음 씬 보여주기(호출)
                    fadeScreen.DOFade(1, 0.5f)
                        .OnComplete(() =>
                        {
                            op.allowSceneActivation = true;

                            //씬 타입에 따라 추가되어야 할 명령어 입력
                            if (nextScene.Equals("GameStart"))
                            {
                                inven.gameObject.SetActive(false);
                                sc.sceneType = SceneType.GameStart;
                            }
                            if (nextScene.Equals("Lobby"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
                                SceneManager.LoadScene("CraftUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("CookingUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("EnchantUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("StageSelect", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                sc.sceneType = SceneType.Lobby;
                            }
                            if (nextScene.Equals("Mine"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                sc.sceneType = SceneType.Mine;
                            }
                            if (nextScene.Equals("Forest"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                sc.sceneType = SceneType.Forest;
                            }
                            if (nextScene.Equals("Stage1"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                sc.sceneType = SceneType.Stage1;
                            }
                            if (nextScene.Equals("Stage2"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("BossEnemyUI", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                sc.sceneType = SceneType.Stage2;
                            }
                            if (nextScene.Equals("Stage3"))
                            {
                                SceneManager.LoadScene("GameUI", LoadSceneMode.Additive);
                                SceneManager.LoadScene("BossEnemyUI", LoadSceneMode.Additive);
                                inven.gameObject.SetActive(true);
                                sc.sceneType = SceneType.Stage3;
                            }
                        });
                    yield break;
                }
            }
        }

        //로딩 스크린 비활성화
        loadingScreen.SetActive(false);
    }
}
