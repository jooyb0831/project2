using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public bool isFilled;
    public InvenItem item;
    private Player p;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        
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
        GameObject obj = Instantiate(item.data.fieldItem, p.weapon1Rest).gameObject;
        obj.transform.localPosition = new Vector3(0, -0.3f, 0);
        p.curWeapon = obj.GetComponent<Weapon>();
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
