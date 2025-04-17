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

#region 컴포넌트 변수    
    protected Player p;
    protected PlayerData pd;
    protected JsonData jd;
    protected GameUI gameUI;
#endregion

    public Data data = new Data();
    public SkillData sData= new SkillData();
    public SkillData data2;

    public bool isSet = false; //스킬이 세팅되었는지 여부 체크
    protected float coolTimer; //쿨타임의 타이머
    public int slotIdx; //스킬이 들어간 슬롯의 인덱스
    public Transform slot; //스킬이 들어간 슬롯의 Transform
    public SkillUISample skillUI; //스킬 UI
    public bool isStart =false; //스킬 작동이 시작되었는지 체크
    public bool isWorking = false; //스킬이 작동중인지 여부


    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        jd = GameManager.Instance.JsonData;
        gameUI = GameManager.Instance.GameUI;
        isSet = false;
    }


    /// <summary>
    /// 스킬의 데이터 세팅
    /// </summary>
    /// <param name="idx">스킬의 인덱스</param>
    public virtual void SetData(int idx)
    {
        //Json 형식의 Skill의 데이터를 받아서 적용
        
        data.SkillTitle = jd.skillData.sData[idx].skilltitle;
        data.SkillExplain = jd.skillData.sData[idx].skillexplain;
        data.SkillIndex = jd.skillData.sData[idx].index;
        data.CoolTime = jd.skillData.sData[idx].cooltime;
        data.MP = jd.skillData.sData[idx].mp;
        data.Damage = jd.skillData.sData[idx].damage;
        data.NeedLv = jd.skillData.sData[idx].needlevel;
        data.SkillLv = jd.skillData.sData[idx].skilllevel;
        
        //data를 인스턴스 할 수 있음
        // sData = Instantiate(jd.skillData.sData[idx]); Monobehaviour가 있어야.
    }

    /// <summary>
    /// 스킬을 작동하는 함수
    /// </summary>
    public virtual void SkillAct()
    {
        //플레이어의 상태를 스킬 사용중인 상태로 변경
        p.state = Player.State.Skill;
        //스킬이 작동중인 상태로 변경
        isWorking = true;
        //스킬의 사용 MP만큼 차감
        pd.CURMP -= data.MP;

        //기타 실질적인 스킬 작동은 상속받는 클래스에서 입력
    }

    void Update()
    {
        //스킬이 해제되지 않았다면면
        if (!data.Unlocked)
        {
            //필요 레벨보다 플레이어 레벨 높으면 해제제
            if (pd.Level >= data.NeedLv)
            {
                data.Unlocked = true;
            }
            return;
        }

        //스킬이 작동중이면 Cooltime체크
        if(isWorking)
        {
            CoolTimeCheck();
        }

    }
    
    /// <summary>
    /// 스킬의 쿨타임 체크
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
