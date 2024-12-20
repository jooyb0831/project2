using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

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

    private int maxSP;
    public int MAXSP
    {
        get { return maxSP; }
        set
        {
            maxSP = value;
            if (GameUI.Instance!=null)
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
            if(GameUI.Instance != null)
            {
                GameUI.Instance.SP = sp;
            }
        }
    }

    //초당 SP 감소량
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
        if(Input.GetKeyDown(KeyCode.F8))
        {
            Debug.Log(SP);
        }
    }
}
