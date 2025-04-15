using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftSystem : MonoBehaviour
{
    public List<ItemRecipeData> itemRecipeDatas;
    public List<ItemRecipeData> foodRecipeDatas;

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

        foreach(var item in foodRecipeDatas)
        {
            item.SetData();
        }
    }
}
