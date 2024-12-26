using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DicKey
{
    arrow
}

public class Pooling : Singleton<Pooling>
{
    private Queue<Arrow> arrowQueue = new Queue<Arrow>();
    [SerializeField] Arrow arrow;

    private Dictionary<DicKey, Queue<GameObject>> pool = new Dictionary<DicKey, Queue<GameObject>>();
    // Start is called before the first frame update
    void Start()
    {
        pool.Add(DicKey.arrow, new Queue<GameObject>());
    }


    public void SetPool(DicKey key, GameObject obj)
    {
        switch(key)
        {
            case DicKey.arrow:
                {
                    obj.GetComponent<Arrow>().Initialize();
                }
                break;
        }

        obj.gameObject.SetActive(false);
        pool[key].Enqueue(obj);
    }


    public GameObject GetPool(DicKey key, Transform trans = null)
    {
        GameObject obj = null;

        if(pool[key].Count ==0)
        {
            switch(key)
            {
                case DicKey.arrow:
                    {
                        obj = Instantiate(arrow, trans).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
            }
        }

        obj = pool[key].Dequeue();
        obj.transform.position = trans.position;
        obj.SetActive(true);
        return obj;
    }
}
