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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Debug.Log($"{skill.data.SkillTitle}");
        }
    }


    void SetUI()
    {
        skillTitleTxt.text = skill.data.SkillTitle;
        skillExplainTxt.text = skill.data.SkillExplain;
        skillExplainTxt2.text =  $"쿨타임 : {skill.data.CoolTime}초\n공격력 : {skill.data.Damage}";
        icon.sprite = skill.data.SkillIcon;
        unlockTxt.text = $"레벨 필요 : {skill.data.NeedLv}";
    }
}
