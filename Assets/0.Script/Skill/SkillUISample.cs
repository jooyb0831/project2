using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO.Pipes;

public class SkillUISample : MonoBehaviour
{
    public Skill skill;
    protected SkillSystem skillSystem;
    protected PlayerData pd;
    [SerializeField] protected GameObject equipWindow;
    [SerializeField] protected TMP_Text skillTitleTxt;
    [SerializeField] protected TMP_Text skillExplainTxt;
    [SerializeField] protected TMP_Text skillExplainTxt2;
    [SerializeField] protected Image icon;
    [SerializeField] protected GameObject unlockCover;
    [SerializeField] protected TMP_Text unlockTxt;
    [SerializeField] protected GameObject usingCover;

    public bool isUnlock = false;
    public bool isEquiped = false;
    public GameObject skillUIicon;
    public GameObject skillQuickIcon;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        pd = GameManager.Instance.PlayerData;
        skillSystem = GameManager.Instance.SkillSystem;
    }

    // Update is called once per frame
    void Update()
    {
        if(skill.data.Unlocked)
        {
            isUnlock = true;
        }

        if(isUnlock)
        {
            unlockCover.SetActive(false);
            GetComponent<Button>().interactable = true;
        }

        if(isEquiped)
        {
            usingCover.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
        else
        {
            usingCover.SetActive(false);
            GetComponent<Button>().interactable = true;
        }
    }

    public void OnClickBtn()
    {
        if(!isUnlock)
        {
            return;
        }
        equipWindow.SetActive(true);
        equipWindow.GetComponent<SkillEquipWindow>().skillIcon = skillUIicon;
        equipWindow.GetComponent<SkillEquipWindow>().temp_skill = this.gameObject;
        //equipWindow.transform.position = transform.position;
    }

}
