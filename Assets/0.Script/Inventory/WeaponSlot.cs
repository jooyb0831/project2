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
        Instantiate(item.data.fieldItem, p.swordPos);
    }
}
