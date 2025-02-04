using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SkillQuickIcon : MonoBehaviour
{
    public Skill skill;
    public SkillUISample skillUI;

    [SerializeField] float coolTime;
    [SerializeField] Image icon;
    [SerializeField] TMP_Text timeTxt;
    [SerializeField] GameObject coverImg;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetData();
    }

    // Update is called once per frame
    void Update()
    {
        if (skill.isWorking)
        {
            SkillAct();
        }
        if (!skill.isWorking)
        {
            coverImg.SetActive(false);
            timer = 0;
        }
    }

    void SetData()
    {
        icon.sprite = skill.data.SkillIcon;
        coolTime = skill.data.CoolTime;
        timeTxt.text = $"{coolTime}";
    }

    void SkillAct()
    {
        timer += Time.deltaTime;
        coverImg.SetActive(true);
        coverImg.GetComponent<Image>().fillAmount = 1 - ((timer) / (coolTime));
        timeTxt.text = $"{(coolTime - timer).ToString("0.0")}";
    }
}
