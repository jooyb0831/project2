using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ItemDumpWindow : MonoBehaviour
{
    public Transform slot;
    public Button plusBtn;
    public Button minusBtn;
    public TMP_InputField numInputField;

    private PlayerData pd;

    [SerializeField] InvenItem invenItem;

    // Start is called before the first frame update
    void Start()
    {
        pd = GameManager.Instance.PlayerData;
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
    }

    public void OnInputfield()
    {
        string numStr = numInputField.text;
        int x = int.Parse(numStr);
    }

    public void OnDumpBtn()
    {
        if (int.Parse(numInputField.text) > invenItem.data.count)
        {
            return;
        }

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
        x++;
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

    public void OnExitBtn()
    {
        Destroy(gameObject);
    }
}
