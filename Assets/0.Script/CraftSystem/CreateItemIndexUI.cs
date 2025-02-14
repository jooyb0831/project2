using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CreateItemIndexUI : MonoBehaviour
{
    public FieldItem item;
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

    public void SetData(ItemRecipeData data)
    {
        this.data = data;
        itemIcon.sprite = data.ItemIcon;
        itemTitle.text = data.ItemTitle;
        itemExplain.text = data.ItemExplain;
        item = data.gameObject.GetComponent<FieldItem>();
    }

    public void OnToggleSelected()
    {
        resourceBG.SetData(item, data);
    }
}
