using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronRock : Rock
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SetData(1);
    }
}
