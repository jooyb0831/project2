using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public string SkillTitle{get;set;}
        public string SkillExplain{get;set;}
        public int SkillIndex{get;set;}
        public float CoolTime{get;set;}
        public int Damage{get;set;}
        public int NeedLv{get;set;}
        public int SkillLv{get;set;}
        public bool Unlocked = false;
        public Sprite SkillIcon;
    }

    protected Player p;
    protected PlayerData pd;
    public Data data = new Data();
    public bool isSet = false;
    public int slotIdx;
    public Transform slot;
    public SkillUISample skillUI;
    public bool isStart =false;
    public bool isWorking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        isSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
