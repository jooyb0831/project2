using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUISystem : MonoBehaviour
{
    private SkillSystem sksystem;
    [SerializeField] Transform Qslot;
    [SerializeField] Transform Rslot;
    public GameObject skillIcon;
    [SerializeField] GameObject skill_Q;
    [SerializeField] GameObject skill_R;
    [SerializeField] Transform qSlot_inGame;
    [SerializeField] Transform rSlot_inGame;
    [SerializeField] GameObject skillQuickIcon;
    [SerializeField] SkillUISample[] skillUIs;
    [SerializeField] SkillEquipWindow skillEquipWindow;
    //[SerializeField] SkillEquipWindow skEqwindow;

    // Start is called before the first frame update
    void Start()
    {
        sksystem = GameManager.Instance.SkillSystem;
        SetSkillUIs();
        sksystem.SetSkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSkillUIs()
    {
        for(int i = 0; i< skillUIs.Length; i++)
        {
            sksystem.skillUIs[i] = skillUIs[i];
        }
        sksystem.SetSkillUI();
    }

    public void SetQSkill(Skill skill)
    {
        GameObject obj = Instantiate(skillIcon, Qslot);
        obj.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = skill.data.SkillIcon;
        obj.GetComponent<SkillIcon>().skill = skill.gameObject;
        obj.transform.SetAsFirstSibling();
        skillEquipWindow.skill_Q = skill.skillUI.gameObject;
        Qslot.GetComponent<ActiveSkillSlot>().isFilled = true;
        GameObject obj2 = Instantiate(skillQuickIcon, qSlot_inGame);
        obj2.GetComponent<SkillQuickIcon>().skill = skill;
        obj2.GetComponent<SkillQuickIcon>().skillUI = skill.skillUI;
    }

    public void SetRSkill(Skill skill)
    {

    }
}
