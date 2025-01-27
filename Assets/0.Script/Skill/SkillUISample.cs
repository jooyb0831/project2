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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void Init()
    {
        pd = GameManager.Instance.PlayerData;
        skillSystem = GameManager.Instance.SkillSystem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBtn()
    {
        if(!isUnlock)
        {
            return;
        }
    }

}
