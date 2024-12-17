using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : Singleton<EnemyUI>
{
    [SerializeField] TMP_Text bossNameTxt;
    [SerializeField] Image hpBar;
    Enemy enemy = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUI(string name, int hp, Enemy enemy)
    {
        this.enemy = enemy;
        bossNameTxt.text = name;
        hpBar.fillAmount = ((float)enemy.data.CURHP / enemy.data.MAXHP);
    }

    public int HP
    {
        set
        {
            if(enemy == null)
            {
                return;
            }
            hpBar.fillAmount = ((float)enemy.data.CURHP / enemy.data.MAXHP);
        }
    }
}
