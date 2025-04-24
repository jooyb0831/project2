using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : Skill
{
    [SerializeField] GameObject sheildObj;
    public override void Init()
    {
        base.Init();
        SetData(1);
    }


    public override void SkillAct()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }

        if(gameUI == null)
        {
            gameUI = GameManager.Instance.GameUI;
        }

        GameObject obj = Instantiate(sheildObj, p.sheildPos);
        base.SkillAct();
        Destroy(obj, 4.2f);

    }
}
