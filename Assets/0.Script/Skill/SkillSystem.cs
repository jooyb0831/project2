using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public Skill[] skills;
    public SkillUISample[] skillUIs;
    public GameObject qSkill;
    public GameObject rSkill;
    public SkillUISample qSkillUI;
    public SkillUISample rSkillUI;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

        //스킬 버튼 누르면 활성화
    }

    public void SetSkill()
    {
        if(qSkill != null)
        {
            if(qSkill.GetComponent<Skill>().isSet)
            {
                //SkillUISystem.In
            }
        }
    }

    public void SetSkillUI()
    {
        for(int i = 0; i<skills.Length; i++)
        {
            skills[i].skillUI = skillUIs[i];
        }
    }
}
