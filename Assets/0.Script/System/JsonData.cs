using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyData
{
    public int maxHP;
    public string enemyName;
    public int index;
    public float speed;
    public int atkPower;
    public int exp;
}

[System.Serializable]
public class SkillData 
{
    public string skilltitle;
    public string skillexplain;
    public int index;
    public float cooltime;
    public int mp;
    public int damage;
    public int needlevel;
    public int skilllevel;
}

[System.Serializable]
public class RockData
{
    public int maxHit;
    public int needHit;
    public int exp;
}

[System.Serializable]
public class EnemyJsonData
{
    public List<EnemyData> eData = new List<EnemyData>();
}

[System.Serializable]
public class SkillJsonData
{
    public List<SkillData> sData = new List<SkillData>();
}

[System.Serializable]
public class QuestDialogueData
{
    [System.Serializable]
    public class QuestDialogue
    {
        public string[] questBasic; //일반적으로 출력되는 퀘스트 대사
        public string[] questY; //"Yes"를 선택했을 때 출력되는 퀘스트 대사
        public string[] questN; //"NO"를 선택했을 때 출력되는 퀘스트 대사
    }
    public List<QuestDialogue> questDialogueData = new List<QuestDialogue>();
}

[System.Serializable]
public class RockJsonData
{
    public List<RockData> rData = new List<RockData>();
}
public class JsonData : Singleton<JsonData>
{
#region 각 Json파일을 받을 TextAsset 변수
    [SerializeField] private TextAsset enemyJson;
    [SerializeField] private TextAsset skillJson;
    [SerializeField] private TextAsset questDialogueJson;
    [SerializeField] private TextAsset rockJson;
#endregion

#region JsonDataClass
    public EnemyJsonData enemyData = new EnemyJsonData();
    public SkillJsonData skillData = new SkillJsonData();
    public QuestDialogueData questDialogueData = new QuestDialogueData();
    public RockJsonData rockData = new RockJsonData();
#endregion

    public SkillData data1;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        //JsonUtility 클래스의 FromJson을 활용하여 입력
        enemyData = JsonUtility.FromJson<EnemyJsonData>(enemyJson.text);
        skillData = JsonUtility.FromJson<SkillJsonData>(skillJson.text);
        questDialogueData = JsonUtility.FromJson<QuestDialogueData>(questDialogueJson.text);
        rockData = JsonUtility.FromJson<RockJsonData>(rockJson.text);

        data1 = skillData.sData[0];
    }
}
