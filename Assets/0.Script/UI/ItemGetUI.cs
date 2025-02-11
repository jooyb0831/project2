using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemGetUI : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemTitleTxt;
    [SerializeField] TMP_Text itemCntTxt;

    private string itemTitleStr;


    float time = 3f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=time)
        {
            timer = 0;
        }
    }

    public void SetData(ItemData data, int cnt = 1)
    {
        itemIcon.sprite = data.invenIcon;
        itemTitleStr = data.itemTitle;
        itemTitleTxt.text = itemTitleStr;
        if(cnt>0)
        {
            itemCntTxt.text = $"+{cnt}";
        }
        else if(cnt<0)
        {
            itemCntTxt.text = $"-{cnt}";
        }

    }
}
