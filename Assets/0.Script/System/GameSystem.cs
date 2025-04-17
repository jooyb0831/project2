using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSystem : MonoBehaviour
{
    private PlayerData pd;
    private Player p;
    void Start()
    {
        DontDestroyOnLoad(this);
        pd = GameManager.Instance.PlayerData;
        p = GameManager.Instance.Player;
    }
    

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.F6))
        {
            pd.EXP += 10;
        }
    }
}
