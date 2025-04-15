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

    void Start()
    {
        p = GameManager.Instance.Player;
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// SkillUI세팅
    /// </summary>
    public void SetSkillUI()
    {
        //현재 스킬의 배열 크기만큼 돌면서 스킬 UI를 세팅
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].skillUI = skillUIs[i];
        }
    }

    /// <summary>
    /// 스킬 세팅하기
    /// </summary>
    public void SetSkill()
    {
        //스킬 데이터 관련 세부 내용은 상속받는 쪽에서 입력
    }
}
