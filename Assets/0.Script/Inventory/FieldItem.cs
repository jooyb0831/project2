using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemData
{
    public string itemTitle;
    public Sprite invenIcon;
    public Sprite bgSprite;
    public ItemType type;
    public int count;
    public int price;
    public int itemIdx;
    public GameObject obj;
    public FieldItem fItem;
}

public class FieldItem : MonoBehaviour
{
    public ItemData itemData;
    protected Player p;

    bool isFind = false;
    bool isFull = false;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        itemData.obj = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(itemData.type.Equals(ItemType.Tool))
        {
            ItemGet();
        }

        else
        {
            ItemMove();
        }
    }
    

    void ItemMove()
    {
        if (p == null)
        {
            p = GameManager.Instance.Player;
        }
        float dist = Vector3.Distance(p.transform.position, transform.position);

        if (Input.GetKeyDown(KeyCode.F3))
        {
            Debug.Log(dist);
        }
        if (dist < 1.5f)
        {
            if (!isFull)
            {
                isFind = true;
            }

        }
        else
        {
            isFind = false;
        }

        speed = isFind ? 5f : 0f;

        transform.position = Vector3.MoveTowards(transform.position, p.transform.position, Time.deltaTime * speed);

        if (dist < 0.2f)
        {
            Inventory.Instance.GetItem(itemData);
        }
    }

    void ItemGet()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }

        float dist = Vector3.Distance(p.transform.position, transform.position);

        if(dist<2f)
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                Inventory.Instance.GetItem(itemData);
            }
        }
           
    }
    public void InvenFull(bool invenFull)
    {
        isFull = invenFull;
        if(invenFull)
        {
            Debug.Log("�κ� ���� ��.");
        }
    }


    public virtual bool ItemUseCheck()
    {
        return true;
    }
    public virtual void UseItem()
    {
        
    }
}
