using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public bool isFilled;
    public bool isBowSlot;
    public InvenItem item;
    private Player p;
    private PlayerData pd;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }
        if(pd == null)
        {
            pd = GameManager.Instance.PlayerData;
        }
        GameObject obj = null;
        WeaponType type = item.data.fieldItem.GetComponent<Weapon>().weaponData.weaponType;
        switch(type)
        {
            case WeaponType.Sword:
            {
                obj = Instantiate(item.data.fieldItem, p.weapon1Rest).gameObject;
                obj.transform.localPosition = new Vector3(0, -0.3f, 0);
                p.curWeapon = obj.GetComponent<Weapon>();
                break;
            }
            case WeaponType.Bow:
            {
                obj = Instantiate(item.data.fieldItem, p.backWeaponRest).gameObject;
                obj.transform.localRotation = Quaternion.Euler(0,0,180);
                obj.transform.localPosition = new Vector3(0.2f, 0,0);
                p.curBow = obj.GetComponent<Weapon>();
                pd.bowEquiped = true;
                break;
            }
        }

        SetWeapon(obj.GetComponent<Weapon>());
    }

    public void SetWeapon(Weapon weapon)
    {
        WeaponType type = weapon.weaponData.weaponType;

        switch(type)
        {
            case WeaponType.Sword:
                {
                    p.weaponEquipState = Player.WeaponEquipState.Sword;
                    p.curWeapon = weapon;
                    break;
                }
        }
    }
}
