using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    /// <summary>
    /// Skill의 Data
    /// </summary>
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

    public bool isSet = false; //스킬이 세팅되었는지 여부
    protected float coolTimer; //스킬 CoolTime체크하는 Timer
    public int slotIdx; //스킬의 장착 Slot의 Index
    public Transform slot; //스킬이 장착된 Slot
    public SkillUISample skillUI; //스킬의 UI
    public bool isStart =false; //스킬이 시작되었는지 여부
    public bool isWorking = false; //스킬이 작동중인지 여부


    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        jd = GameManager.Instance.JsonData;
        isSet = false;
    }

    public virtual void SetData(int idx)
    {
        //JsonData의 SkillData를 받아서 스킬의 idx넘버에 따라 세팅
        data.SkillTitle = jd.skillData.sData[idx].skilltitle;
        data.SkillExplain = jd.skillData.sData[idx].skillexplain;
        data.SkillIndex = jd.skillData.sData[idx].index;
        data.CoolTime = jd.skillData.sData[idx].cooltime;
        data.MP = jd.skillData.sData[idx].mp;
        data.Damage = jd.skillData.sData[idx].damage;
        data.NeedLv = jd.skillData.sData[idx].needlevel;
        data.SkillLv = jd.skillData.sData[idx].skilllevel;
    }

    /// <summary>
    /// 스킬 작동
    /// </summary>
    public virtual void SkillAct()
    {
        p.state = Player.State.Skill;
        isWorking = true;
        pd.CURMP -= data.MP;
    }

    // Update is called once per frame
    void Update()
    {
        //스킬이 해제되지 않았을 경우(잠금상태)
        if (!data.Unlocked)
        {
            //스킬의 요구 레벨에 도달하였으면 해제
            if (pd.Level >= data.NeedLv)
            {
                data.Unlocked = true;
            }
            return;
        }

        //스킬이 작동중이면 Cooltime체크 시작
        if(isWorking)
        {
            CoolTimeCheck();
        }

    }
    
    /// <summary>
    /// 스킬 쿨타임 체크
    /// </summary>
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
