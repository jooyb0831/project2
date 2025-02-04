using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public string SkillTitle { get; set; }
        public string SkillExplain { get; set; }
        public int SkillIndex { get; set; }
        public float CoolTime { get; set; }
        public int MP { get; set; }
        public int Damage { get; set; }
        public int NeedLv { get; set; }
        public int SkillLv { get; set; }
        public bool Unlocked = false;
        public Sprite SkillIcon;
    }

    protected Player p;
    protected PlayerData pd;
    protected JsonData jd;
    public Data data = new Data();
    public bool isSet = false;

    protected float coolTimer;
    public int slotIdx;
    public Transform slot;
    public SkillUISample skillUI;
    public bool isStart =false;
    public bool isWorking = false;

    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        jd = GameManager.Instance.JsonData;
        isSet = false;
    }

    public virtual void SetData(int idx)
    {
        data.SkillTitle = jd.skillData.sData[idx].skilltitle;
        data.SkillExplain = jd.skillData.sData[idx].skillexplain;
        data.SkillIndex = jd.skillData.sData[idx].index;
        data.CoolTime = jd.skillData.sData[idx].cooltime;
        data.MP = jd.skillData.sData[idx].mp;
        data.Damage = jd.skillData.sData[idx].damage;
        data.NeedLv = jd.skillData.sData[idx].needlevel;
        data.SkillLv = jd.skillData.sData[idx].skilllevel;
    }

    public virtual void SkillAct()
    {
        p.state = Player.State.Skill;
        isWorking = true;
        pd.CURMP -= data.MP;
    }

    // Update is called once per frame
    void Update()
    {
        if (!data.Unlocked)
        {
            if (pd.Level >= data.NeedLv)
            {
                data.Unlocked = true;
            }
            return;
        }

        if(isWorking)
        {
            CoolTimeCheck();
        }

    }

    void CoolTimeCheck()
    {
        coolTimer += Time.deltaTime;
        if(coolTimer>=data.CoolTime)
        {
            coolTimer = 0;
            isWorking = false;
        }
    }


}
