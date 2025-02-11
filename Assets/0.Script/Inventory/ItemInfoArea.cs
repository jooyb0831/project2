using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoArea : MonoBehaviour
{
    private Inventory inven;
    public List<ItemGetUI> getUILists;
    // Start is called before the first frame update
    void Start()
    {
        inven = GameManager.Instance.Inven;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
