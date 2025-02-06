using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : FieldItem
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
    }

    public void Initialize()
    {
        transform.localPosition = Vector3.zero;
        isFind = false;
    }

}
