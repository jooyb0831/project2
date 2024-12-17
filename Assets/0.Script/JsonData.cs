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
public class EnemyJsonData
{
    public List<EnemyData> eData = new List<EnemyData>();
}

public class JsonData : Singleton<JsonData>
{
    [SerializeField] private TextAsset enemyJson;

    public EnemyJsonData enemyData = new EnemyJsonData();

    private void Awake()
    {
        DontDestroyOnLoad(this);
        enemyData = JsonUtility.FromJson<EnemyJsonData>(enemyJson.text);
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
