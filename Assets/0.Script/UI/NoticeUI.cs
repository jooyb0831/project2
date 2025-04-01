using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class NoticeUI : MonoBehaviour
{
    private Pooling pooling;
    [SerializeField] Image bg;
    [SerializeField] TMP_Text messageTxt;

    public string noticeStr;

    private NoticeInfoArea noticeArea;
    private float time = 3f;
    private float timer;
    public bool isSet;


    public void SetMessage(string message)
    {
        messageTxt.text = message;
    }

    public void FadeIn()
    {
        bg.DOFade(1, 0.5f);
        messageTxt.DOFade(1, 0.5f);
    }

    public void FadeOut()
    {
        isSet = false;
        messageTxt.DOFade(0,1f);
        bg.DOFade(0, 1f).OnComplete (() => 
        pooling.SetPool(DicKey.noticeUI, this.gameObject));
    }
    // Start is called before the first frame update
    void Start()
    {
        pooling = GameManager.Instance.Pooling;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSet)
        {
            timer += Time.deltaTime;
            if(timer >- time)
            {
                timer = 0;
            }
        }
    }

    
}
