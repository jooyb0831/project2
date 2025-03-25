using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossEnemyUI : Singleton<BossEnemyUI>
{
    public BossEnemy boss;
    public GameObject bossUI;
    [SerializeField] TMP_Text bossNameTxt;
    [SerializeField] Image hpBar;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetUI(BossEnemy boss)
    {
        this.boss = boss;
        boss.bossUI = this;
        bossNameTxt.text = boss.data.EnemyName;
        hpBar.fillAmount = ((float)boss.data.CURHP / boss.data.MAXHP);
        bossUI.SetActive(true);
    }

    public int HP
    {
        set
        {
            if(boss == null)
            {
                return;
            }
            hpBar.fillAmount = ((float) boss.data.CURHP / boss.data.MAXHP);
        }
    }

    public void HPBarCheck()
    {
        hpBar.fillAmount = ((float)boss.data.CURHP / boss.data.MAXHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
