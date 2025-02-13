using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{

    public Skill[] skills; //스킬을 담을 배열
    public SkillUISample[] skillUIs; //스킬 UI를 담을 배열
    public GameObject qSkill; //현재 장착된 qSkill
    public GameObject rSkill; //현재 장착된 rSkill
    public SkillUISample qSkillUI; //현재 장착된 qSkill의 UI
    public SkillUISample rSkillUI; //현재 장착된 rSkill의 UI

    private Player p;

    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    /// <summary>
    /// 스킬 세팅하기
    /// </summary>
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

    /// <summary>
    /// SkillUI세팅
    /// </summary>
    public void SetSkillUI()
    {
        for(int i = 0; i<skills.Length; i++)
        {
            skills[i].skillUI = skillUIs[i];
        }
    }
}
