using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantUI : Singleton<EnchantUI>
{
    [SerializeField] SpeedEnchant speedEnchant;
    [SerializeField] AttackEnchant atkEnchat;
    [SerializeField] InvenEnchant invEnchat;
    public GameObject window;

    public void EnableWindow()
    {
        speedEnchant.SetData();
        atkEnchat.SetData();
        invEnchat.SetData();
        window.SetActive(true);
    }

    public void OnExitBtn()
    {
        window.SetActive(false);
        GameManager.Instance.PauseScene(false);
    }
}
