using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : Singleton<EnemyUI>
{
    private Player p;
    [SerializeField] GameObject hpObj;
    [SerializeField] GameObject deadObj;
    [SerializeField] TMP_Text bossNameTxt;
    [SerializeField] Image hpBar;
    Enemy enemy = null;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (enemy == null)
        {
            return;
        }

        if (p == null)
        {
            p = GameManager.Instance.Player;
        }

        if(p.transform.position.z<enemy.transform.position.z)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        */
        Vector3 camPos = Camera.main.transform.position;
        Vector3 targetPos = new Vector3(camPos.x, transform.position.y, camPos.z);
        transform.LookAt(targetPos);
    }

    public void SetUI(string name, int hp, Enemy enemy)
    {
        this.enemy = enemy;
        bossNameTxt.text = name;
        hpBar.fillAmount = ((float)enemy.data.CURHP / enemy.data.MAXHP);
    }

    public void DeadUI()
    {
        hpObj.SetActive(false);
        deadObj.SetActive(true);
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
