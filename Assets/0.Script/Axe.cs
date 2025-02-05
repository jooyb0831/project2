using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Tool
{
    GameSystem gameSystem;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        data.toolNmae = "도끼";
        data.useST = 5;
        data.lv = 1;
        gameSystem = GameManager.Instance.GameSystem;
    }
}
