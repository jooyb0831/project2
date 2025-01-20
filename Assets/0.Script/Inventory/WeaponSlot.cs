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
    private GameObject weaponObj;

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
        WeaponType type = item.data.fieldItem.GetComponent<Weapon>().weaponData.weaponType;
        switch(type)
        {
            case WeaponType.Sword:
            {
                weaponObj = Instantiate(item.data.fieldItem, p.weapon1Rest).gameObject;
                weaponObj.transform.localPosition = new Vector3(0, -0.3f, 0);
                p.curWeapon = weaponObj.GetComponent<Weapon>();
                break;
            }
            case WeaponType.Bow:
            {
                weaponObj = Instantiate(item.data.fieldItem, p.backWeaponRest).gameObject;
                weaponObj.transform.localRotation = Quaternion.Euler(0,0,180);
                weaponObj.transform.localPosition = new Vector3(0.2f, 0,0);
                p.curBow = weaponObj.GetComponent<Weapon>();
                pd.bowEquiped = true;
                break;
            }
        }

        SetWeapon(weaponObj.GetComponent<Weapon>());
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

    public void UnequipWeapon()
    {
        WeaponType type = item.data.fieldItem.GetComponent<Weapon>().weaponData.weaponType;
        switch(type)
        {
            case WeaponType.Sword :
            {
                p.weaponEquipState = Player.WeaponEquipState.None;
                p.equipedWeapon = null;
                p.curWeapon = null;
                break;
            }

            case WeaponType.Bow :
            {
                p.curBow = null;
                pd.bowEquiped = false;
                break;
            }
        }
        isFilled = false;
        Destroy(weaponObj);
        item = null;
    }
}
