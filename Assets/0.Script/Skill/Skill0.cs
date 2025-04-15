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
        SetData(0); //스킬의 데이터 세팅
    }

    /// <summary>
    /// 스킬 활용
    /// </summary>
    public override void SkillAct()
    {
        //플레이어가 무기 장착하고 있지 않으면 작동X
        if(p.weaponEquipState.Equals(Player.WeaponEquipState.None))
        {
            //무기가 없음을 UI에 표시
            gameUI.DisplayInfo(8);
            p.skillState = Player.SkillState.None;
            return;
        }
        
        p.Weapon(); //손에 무기 장착

        //애니메이션 트리거
        p.animator.SetTrigger("Skill0");
        base.SkillAct();
    }
}
