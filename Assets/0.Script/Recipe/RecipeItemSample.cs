using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeItemSample : MonoBehaviour
{
    public GameObject item;
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemTitle;
    [SerializeField] TMP_Text itemExplain;
    private Toggle toggle;

    public CreateResoruceUI resourceBG;
    private ItemRecipeData data;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
