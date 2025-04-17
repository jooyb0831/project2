using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPlant_Field : FieldItem
{
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
    }

    public override void ItemGet()
    {
        base.ItemGet();
        Vector3 pos = new Vector3(p.transform.position.x, getUI.transform.position.y, p.transform.position.z);
        getUI.transform.LookAt(pos);
    }
}
