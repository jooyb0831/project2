using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    private int level;
    public int Level
    {
        get{return level;}
        set
        {
            level = value;
            if(GameUI.Instance !=null)
            {
                GameUI.Instance.Level = level;
            }
        }
    }

    private int maxEXP;
    public int MAXEXP
    {
        get{return maxEXP;}
        set
        {
            maxEXP = value;
            if(GameUI.Instance != null)
            {
                GameUI.Instance.MAXEXP = maxEXP;
            }
        }

    }

    private int exp;
    public int EXP
    {
        get{return exp;}
        set
        {
            exp = value;
            if(GameUI.Instance!=null)
            {
                GameUI.Instance.EXP = exp;
            }
        }

    }
    private int maxHP;
    public int MAXHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
            if (GameUI.Instance != null)
            {
                GameUI.Instance.MAXHP = maxHP;
            }
            /*
            if (StatUI.Instance != null)
            {
                StatUI.Instance.HP = maxHP;
            }
            */

        }
    }

    private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            if (GameUI.Instance != null)
            {
                GameUI.Instance.HP = hp;
            }

        }
    }

    private int maxST;
    public int MAXST
    {
        get { return maxST; }
        set
        {
            maxST = value;
            if(GameUI.Instance!=null)
            {
                GameUI.Instance.MAXST = maxST;
            }
        }
    }

    private int st;
    public int ST
    {
        get{return st;}
        set
        {
            st = value;
            if(GameUI.Instance!=null)
            {
                GameUI.Instance.ST = st;
            }
        }
    }

    private int maxSP;
    public int MAXSP
    {
        get { return maxSP; }
        set
        {
            maxSP = value;
            if (GameUI.Instance != null)
            {
                GameUI.Instance.MAXSP = maxSP;
            }
        }
    }

    private int sp;
    public int SP
    {
        get { return sp; }
        set
        {
            sp = value;
            if (GameUI.Instance != null)
            {
                GameUI.Instance.SP = sp;
            }
        }
    }

    //초당 SP감소량
    public int minSP { get; set; }

    //초당 SP 충전량
    public int plusSP { get; set; }

    //몇초 후부터 다시 충전되는지
    public float delaySP { get; set; }

    private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
            /*
            if (StatUI.Instance != null)
            {
                StatUI.Instance.Speed = (int)speed;
            }
            */
        }
    }

    public float RunSpeed { get; set; } = 6f;

    private int basicAtk;
    public int BasicAtk
    {
        get { return basicAtk; }
        set
        {
            basicAtk = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Level = 1;
        EXP = 0;
        MAXEXP = 15;
        MAXST = 20;
        ST = 20;
        MAXHP = 20;
        HP = MAXHP;
        MAXSP = 20;
        SP = MAXSP;
        minSP = 2;
        delaySP = 2;
        plusSP = 1;
        Speed = 4f;
        BasicAtk = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            Debug.Log(SP);
        }
        LevelUp();
    }

    void LevelUp()
    {
        if(EXP>=MAXEXP)
        {
            Level++;
            EXP = MAXEXP - EXP;
            MAXEXP += 10;
        }
    }
}
