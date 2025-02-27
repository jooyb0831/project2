using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    /// <summary>
    /// Skill�� Data
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

    public bool isSet = false; //��ų�� ���õǾ����� ����
    protected float coolTimer; //��ų CoolTimeüũ�ϴ� Timer
    public int slotIdx; //��ų�� ���� Slot�� Index
    public Transform slot; //��ų�� ������ Slot
    public SkillUISample skillUI; //��ų�� UI
    public bool isStart =false; //��ų�� ���۵Ǿ����� ����
    public bool isWorking = false; //��ų�� �۵������� ����


    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        jd = GameManager.Instance.JsonData;
        isSet = false;
    }

    public virtual void SetData(int idx)
    {
        //JsonData�� SkillData�� �޾Ƽ� ��ų�� idx�ѹ��� ���� ����
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
    /// ��ų �۵�
    /// </summary>
    public virtual void SkillAct()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }
        p.state = Player.State.Skill;
        isWorking = true;
        pd.CURMP -= data.MP;
    }

    // Update is called once per frame
    void Update()
    {
        //��ų�� �������� �ʾ��� ���(��ݻ���)
        if (!data.Unlocked)
        {
            //��ų�� �䱸 ������ �����Ͽ����� ����
            if (pd.Level >= data.NeedLv)
            {
                data.Unlocked = true;
            }
            return;
        }

        //��ų�� �۵����̸� Cooltimeüũ ����
        if(isWorking)
        {
            CoolTimeCheck();
        }

    }
    
    /// <summary>
    /// ��ų ��Ÿ�� üũ
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
