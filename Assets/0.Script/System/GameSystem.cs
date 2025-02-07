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
    }
}
