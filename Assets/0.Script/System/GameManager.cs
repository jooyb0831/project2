using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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
            if (pd == null)
            {
                pd = FindAnyObjectByType<PlayerData>();
            }
            return pd;
        }
    }

    private GameUI gui;
    public GameUI GameUI
    {
        get
        {
            if(gui == null)
            {
                gui = FindAnyObjectByType<GameUI>();
            }
            return gui;
        }
    }

    private MenuUI mui;
    public MenuUI MenuUI
    {
        get
        {
            if(mui == null)
            {
                mui = FindAnyObjectByType<MenuUI>();
            }
            return mui;
        }
    }
    
    private Inventory inven;
    public Inventory Inven
    {
        get
        {
            if (inven == null)
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
            if (gameSystem == null)
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
            if (skillSystem == null)
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
            if (jd == null)
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
            if (sc == null)
            {
                sc = FindAnyObjectByType<SceneChanger>();
            }
            return sc;
        }
    }

    private CraftSystem craftSystem;
    public CraftSystem CraftSystem
    {
        get
        {
            if (craftSystem == null)
            {
                craftSystem = FindAnyObjectByType<CraftSystem>();
            }
            return craftSystem;
        }
    }

    private QuestManager questManager;
    public QuestManager QuestManager
    {
        get
        {
            if(questManager == null)
            {
                questManager = FindAnyObjectByType<QuestManager>();
            }
            return questManager;
        }
    }

    private Pooling pooling;
    public Pooling Pooling
    {
        get
        {
            if(pooling == null)
            {
                pooling = FindAnyObjectByType<Pooling>();
            }
            return pooling;
        }
    }

    private EnchantSystem enchantSystem;
    public EnchantSystem EnchantSystem
    {
        get
        {
            if(enchantSystem == null)
            {
                enchantSystem = FindAnyObjectByType<EnchantSystem>();
            }
            return enchantSystem;
        }
    }

    


    /// <summary>
    /// 게임 일시정지 체크여부
    /// </summary>
    public bool isPaused { get; set; }

    public void PauseScene(bool isPause)
    {
        isPaused = isPause;
        CameraMove camMove = Camera.main.GetComponent<CameraMove>();

        //일시정지 상태일 경우
        if(isPause)
        {
            camMove.enabled = false;
            Cursor.visible = true;
        }
        else
        {
            camMove.enabled = true;
            Cursor.visible = false;
        }

    }

}
