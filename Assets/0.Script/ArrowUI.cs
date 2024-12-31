using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : Singleton<ArrowUI>
{
    [SerializeField] Image bar;
    public Arrow arrow;
    public float Power
    {
        set
        {
            bar.fillAmount = ((float)arrow.Power / (float) 130);
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
