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
    public int damage;
    public int needlevel;
    public int skilllevel;
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

public class JsonData : Singleton<JsonData>
{
    [SerializeField] private TextAsset enemyJson;
    [SerializeField] private TextAsset skillJson;

    public EnemyJsonData enemyData = new EnemyJsonData();
    public SkillJsonData skillData = new SkillJsonData();

    private void Awake()
    {
        DontDestroyOnLoad(this);
        enemyData = JsonUtility.FromJson<EnemyJsonData>(enemyJson.text);
        skillData = JsonUtility.FromJson<SkillJsonData>(skillJson.text);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
