using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemBuyWindow : MonoBehaviour
{
    public Transform slot;
    public Button plusBtn;
    public Button minusBtn;
    public TMP_InputField numInputField;
    public TMP_Text totalPriceTxt;

    private PlayerData pd;
    private GameUI gameUI;
    private Inventory inven;
    [SerializeField] InvenItem invenItem;
    [SerializeField] int price;
    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
        gameUI = GameManager.Instance.GameUI;
        inven = GameManager.Instance.Inven;
        Init();
    }

    void Init()
    {
        numInputField.text = 1.ToString();
    }

    public void SetItem(InvenItem item)
    {
        invenItem = item;
        Instantiate(item.gameObject, slot);
        price = item.data.price;
    }

    [SerializeField] int totalPrice;
    public void OnInputField()
    {
        string numStr = numInputField.text;
        int x = int.Parse(numStr);
        totalPrice = x * price;
        totalPriceTxt.text = $"{totalPrice}";
    }

    public void OnBuyBtn()
    {
        if (totalPrice > pd.Gold)
        {
            //GameUI.Instance.fullInvenObj.SetActive(true);
            //GameUI.Instance.fullInvenObj.GetComponent<FullInvenObj>().Act(4);
            gameUI.DisplayInfo(2);
            return;
        }

        for (int i = 0; i < int.Parse(numInputField.text); i++)
        {
            inven.GetItem(invenItem.data.fieldItem.itemData);
        }
        pd.Gold -= totalPrice;
        ShopUI.Instance.SetShopInven();
        Destroy(gameObject);

    }

    public void OnPlusBtn()
    {
        int x = int.Parse(numInputField.text);
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
