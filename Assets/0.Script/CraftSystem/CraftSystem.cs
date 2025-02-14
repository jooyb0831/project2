using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftSystem : MonoBehaviour
{
    public List<ItemRecipeData> itemRecipeDatas;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Init();
    }

    void Init()
    {
        foreach(var item in itemRecipeDatas)
        {
            item.SetData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
