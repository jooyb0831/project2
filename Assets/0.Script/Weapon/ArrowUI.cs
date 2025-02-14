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

    // Start is called before the first frame update
    void Start()
    {
        bar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
