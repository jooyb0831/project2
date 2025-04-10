using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;


[System.Serializable]
public class ItemData
{
    public string itemTitle;
    public string itemExplain;
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
    protected GameUI gameUI;
    protected Inventory inven;
    protected Player p;
    protected PlayerData pd;

    [SerializeField] protected bool isFind = false;
    [SerializeField] protected GameObject getUI;
    bool isFull = false;
    float speed;

    void Start()
    {
        Init();
    }


    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        inven = GameManager.Instance.Inven;
        gameUI = GameManager.Instance.GameUI;
        itemData.obj = this.gameObject;

    }

    
    void Update()
    {
        if(itemData.type.Equals(ItemType.Plant))
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
            inven.GetItem(itemData);
        }
    }

    public virtual void ItemGet()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }

        float dist = Vector3.Distance(p.transform.position, transform.position);

        if(dist < 2f)
        {
            getUI.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(pd.ST < 2)
                {
                    gameUI.DisplayInfo(0);
                    Debug.Log("기력이 부족합니다.");
                    return;
                }
                p.GatherAnim(true, 2);
                inven.GetItem(itemData);
            }
        }
        else
        {
            getUI.SetActive(false);
        }
           
    }
    public void InvenFull(bool invenFull)
    {
        isFull = invenFull;
        if(invenFull)
        {
            gameUI.DisplayInfo(8);
            Debug.Log("인벤토리가 가득 찼습니다.");
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
