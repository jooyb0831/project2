using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill0 : Skill
{
    [SerializeField] float delay;
    [SerializeField] float coolTimer;
    [SerializeField] float timer;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        data.SkillTitle = JsonData.Instance.skillData.sData[0].skilltitle;
        data.SkillExplain = JsonData.Instance.skillData.sData[0].skillexplain;
        data.SkillIndex = JsonData.Instance.skillData.sData[0].index;
        data.CoolTime = JsonData.Instance.skillData.sData[0].cooltime;
        data.Damage = JsonData.Instance.skillData.sData[0].damage;
        data.NeedLv = JsonData.Instance.skillData.sData[0].needlevel;
        data.SkillLv = JsonData.Instance.skillData.sData[0].skilllevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
