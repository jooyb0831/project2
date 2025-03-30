using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSystem : MonoBehaviour
{
    PlayerData pd;
    Player p;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;
        p = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            pd.Level ++;
        }

        if(Input.GetKeyDown(KeyCode.F6))
        {
            pd.EXP += 10;
        }
    }
}
