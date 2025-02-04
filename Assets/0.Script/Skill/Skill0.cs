using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill0 : Skill
{
    
    // Start is called before the first frame update
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
        base.SkillAct();
        if(p.weaponEquipState.Equals(Player.WeaponEquipState.None))
        {
            Debug.Log("무기");
            p.skillState = Player.SkillState.None;
            return;
        }
        Debug.Log($"{p.state}, {p.weaponEquipState}");
        p.Weapon();
        p.animator.SetTrigger("Skill0");
    }
}
