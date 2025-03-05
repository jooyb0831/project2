using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;

public class PickAxe : Tool
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
        data.toolNmae = "곡괭이";
        data.useST = 1;
        data.lv = 1;
        gameSystem = GameManager.Instance.GameSystem;
    }

    public override void SetTool()
    {
        base.SetTool();
    }

}
