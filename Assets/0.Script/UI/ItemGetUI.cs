using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ItemGetUI : MonoBehaviour
{
    private Pooling pooling;
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemTitleTxt;
    [SerializeField] TMP_Text itemCntTxt;

    public string itemTitleStr;
    public int itemCnt;

    private ItemInfoArea itemInfoArea;
    public bool isSet;
    float time = 3f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        pooling = GameManager.Instance.Pooling;
    }

    // Update is called once per frame
    void Update()
    {
        //세팅 되었다면
        if (isSet)
        {
            //디스플레이 시간체크하여 투명하게 만들고 화면에서 안 보이게 처리
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                FadeOut();
            }
        }
    }

    public void Init()
    {
        timer = 0;
        transform.SetAsLastSibling();
    }

    /// <summary>
    /// 데이터를 UI에 세팅
    /// </summary>
    /// <param name="data"></param>
    /// <param name="cnt"></param>
    public void SetData(ItemData data, int cnt = 1)
    {

        itemIcon.sprite = data.invenIcon;
        itemTitleStr = data.itemTitle;
        itemTitleTxt.text = itemTitleStr;
        itemCnt = cnt;
        if (cnt > 0)
        {
            itemCntTxt.text = $"+{cnt}";
        }
        else if (cnt < 0)
        {
            itemCntTxt.text = $"-{cnt}";
        }
        itemInfoArea = transform.parent.GetComponent<ItemInfoArea>();
    }

    /// <summary>
    /// 점차 진하게 하여 보이게
    /// </summary>
    public void FadeIn()
    {
        itemIcon.DOFade(1, 0.5f);
        itemTitleTxt.DOFade(1, 0.5f);
        itemCntTxt.DOFade(1, 0.5f);
        GetComponent<Image>().DOFade(0.95f, 0.5f);
    }

    /// <summary>
    /// 점차 흐리게 하여 사라지게함.
    /// </summary>
    private void FadeOut()
    {
        isSet = false;
        itemInfoArea.DeleteObj(this);
        itemIcon.DOFade(0, 1f);
        itemTitleTxt.DOFade(0, 1f);
        itemCntTxt.DOFade(0, 1f);
        GetComponent<Image>().DOFade(0, 1f).OnComplete(() =>
        pooling.SetPool(DicKey.itemGetUI, this.gameObject));
    }

    /// <summary>
    /// UI에 표시되는 아이템 숫자 표기 변환
    /// </summary>
    /// <param name="cnt"></param>
    public void ChangeUI(int cnt)
    {
        itemCntTxt.text = $"+{cnt}";
    }
}
