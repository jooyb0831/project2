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

}
