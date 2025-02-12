using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ItemGetUI : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSet)
        {
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                isSet = false;
                itemInfoArea.DeleteObj(this);
                itemIcon.DOFade(0,1f);
                itemTitleTxt.DOFade(0,1f);
                itemCntTxt.DOFade(0,1f);
                GetComponent<Image>().DOFade(0,1f).OnComplete(()=>
                Pooling.Instance.SetPool(DicKey.itemGetUI, this.gameObject));
            }
        }

    }

    public void Init()
    {
        timer = 0;
        transform.SetAsLastSibling();
    }

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

    public void FadeIn()
    {
        itemIcon.DOFade(1, 0.5f);
        itemTitleTxt.DOFade(1, 0.5f);
        itemCntTxt.DOFade(1, 0.5f);
        GetComponent<Image>().DOFade(0.95f, 0.5f);
    }

    public void ChangeUI(int cnt)
    {
        itemCntTxt.text = $"+{cnt}";
    }
}
