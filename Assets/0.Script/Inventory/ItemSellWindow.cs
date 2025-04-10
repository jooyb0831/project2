using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemSellWindow : MonoBehaviour
{
    public Transform slot;
    public Button plusBtn;
    public Button minusBtn;
    public TMP_InputField numInputField;
    public TMP_Text totalPriceTxt;

    private Player p;
    private PlayerData pd;
    [SerializeField] InvenItem invenItem;
    [SerializeField] int price;

    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
    }

    public void SetItem(InvenItem item)
    {
        invenItem = item;
        Debug.Log($"{item.data.price}");
        Instantiate(item.gameObject, slot);
        price = item.data.price;

    }

    [SerializeField] int totalPrice;
    public void OnInputField()
    {
        Debug.Log($"{numInputField.text}");
        string numStr = numInputField.text;
        int x = int.Parse(numStr);
        totalPrice = x * price;
        totalPriceTxt.text = $"{totalPrice}";
    }
    
    public void OnSellBtn()
    {
        if (int.Parse(numInputField.text) > invenItem.data.count)
        {
            //GameUI.Instance.fullInvenObj.SetActive(true);
            //GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(7);
            return;
        }
        if (pd == null)
        {
            pd = GameManager.Instance.PlayerData;
        }
        pd.Gold += totalPrice;
        ShopUI.Instance.SellItemCheck(invenItem, int.Parse(numInputField.text));
        Inventory.Instance.InvenItemCntChange(invenItem, -int.Parse(numInputField.text));
        Destroy(gameObject);
    }

    public void OnPlusBtn()
    {
        int x = int.Parse(numInputField.text);
        if (x >= invenItem.data.count)
        {
            return;
        }
        x += 1;
        numInputField.text = $"{x}";
    }

    public void OnMinusBtn()
    {
        int x = int.Parse(numInputField.text);
        if (x <= 0)
        {
            return;
        }
        x -= 1;
        numInputField.text = $"{x}";
    }

    public void OnValueChange()
    {
        string numStr = numInputField.text;
        int x = int.Parse(numStr);
        totalPrice = x * price;
        totalPriceTxt.text = $"{totalPrice}";
    }
}
