using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class BossEnemyUI : Singleton<BossEnemyUI>
{   
    private Player p;
    public Enemy boss;
    public GameObject bossUI;
    [SerializeField] TMP_Text bossNameTxt;
    [SerializeField] Image hpBar;

    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
    }

    public void SetUI(Enemy boss)
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
            if (boss == null)
            {
                return;
            }
            hpBar.fillAmount = ((float)boss.data.CURHP / boss.data.MAXHP);
        }
    }

    public void HPBarCheck()
    {
        hpBar.DOFillAmount(((float)boss.data.CURHP / boss.data.MAXHP), 0.2f);
    }

    public void TurnOffUI()
    {

        Debug.Log("Turnoffui");
    }
    // Update is called once per frame
    void Update()
    {
        if(boss==null) return;

        if (boss.state.Equals(Enemy.State.Dead) || p.state.Equals(Player.State.Dead))
        {
            if (bossUI.activeSelf)
            {
                bossUI.SetActive(false);
            }
        }
    }
}
