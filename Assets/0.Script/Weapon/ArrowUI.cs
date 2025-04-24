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
            bar.DOFillAmount(((float)arrow.Power / (float) 15), 0.5f);
        }
    }


    void Start()
    {
        bar.fillAmount = 0;
    }

    void OnDisable()
    {
        //bar.fillAmount = 0;
    }


    void Update()
    {
        
    }
}
