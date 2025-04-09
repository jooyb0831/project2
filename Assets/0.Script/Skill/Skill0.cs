using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill0 : Skill
{
    
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SetData(0);
    }

    public override void SkillAct()
    {
        //플레이어가 무기 장착하고 있지 않으면 작동X
        if(p.weaponEquipState.Equals(Player.WeaponEquipState.None))
        {
            gameUI.DisplayInfo(8);
            p.skillState = Player.SkillState.None;
            return;
        }
        Debug.Log($"{p.state}, {p.weaponEquipState}");
        p.Weapon();
        p.animator.SetTrigger("Skill0");
        base.SkillAct();
    }
}
