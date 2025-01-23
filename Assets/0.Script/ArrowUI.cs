using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ArrowUI : Singleton<ArrowUI>
{
    [SerializeField] Image bar;
    public Arrow arrow;
    public float Power
    {
        set
        {
            //bar.fillAmount =(float)arrow.Power / (float) 130;
            bar.DOFillAmount(((float)arrow.Power / (float) 130), 0.5f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
