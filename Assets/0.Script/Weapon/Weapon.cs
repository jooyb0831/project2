using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword,
    Bow
}

[System.Serializable]
public class WeaponData
{
    public int atkDmg;
    public int weaponIndex;
    public WeaponType weaponType;
}

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
