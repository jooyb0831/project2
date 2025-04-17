using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skill0UI : SkillUISample
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        skill = skillSystem.skills[0];

        if(skill.isSet)
        {
            isEquiped = true;
        }
        SetUI();
    }


    void SetUI()
    {
        skillTitleTxt.text = skill.data.skillData.skilltitle;
        skillExplainTxt.text = skill.data.skillData.skillexplain;
        skillExplainTxt2.text =  $"쿨타임 : {skill.data.skillData.cooltime}초\n공격력 : {skill.data.skillData.damage}";
        icon.sprite = skill.data.SkillIcon;
        unlockTxt.text = $"레벨 필요 : {skill.data.skillData.needlevel}";
    }
}
