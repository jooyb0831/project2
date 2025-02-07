using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }


    private Player p;
    public Player Player
    {
        get
        {
            if (p == null)
            {
                p = FindAnyObjectByType<Player>();
            }
            return p;
        }
    }

    private PlayerData pd;
    public PlayerData PlayerData
    {
        get
        {
            if(pd ==null)
            {
                pd = FindAnyObjectByType<PlayerData>();
            }
            return pd;
        }
    }

    private Inventory inven;
    public Inventory Inven
    {
        get
        {
            if(inven==null)
            {
                inven = FindAnyObjectByType<Inventory>();
            }
            return inven;
        }
    }

    private GameSystem gameSystem;
    public GameSystem GameSystem
    {
        get
        {
            if(gameSystem == null)
            {
                gameSystem = FindAnyObjectByType<GameSystem>();
            }
            return gameSystem;
        }
    }

    private SkillSystem skillSystem;
    public SkillSystem SkillSystem
    {
        get
        {
            if(skillSystem == null)
            {
                skillSystem = FindAnyObjectByType<SkillSystem>();
            }
            return skillSystem;
        }
    }

    private JsonData jd;
    public JsonData JsonData
    {
        get
        {
            if(jd == null)
            {
                jd = FindAnyObjectByType<JsonData>();
            }
            return jd;
        }
    }

    private SceneChanger sc;
    public SceneChanger SceneChanger
    {
        get
        {
            if(sc == null)
            {
                sc = FindAnyObjectByType<SceneChanger>();
            }
            return sc;
        }
    }
    

    public bool isPaused{get;set;}

}
