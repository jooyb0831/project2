using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DicKey
{
    arrow,
    stone
}

public class Pooling : Singleton<Pooling>
{
    private Queue<Arrow> arrowQueue = new Queue<Arrow>();
    private Queue<Stone> stoneQueue = new Queue<Stone>();
    [SerializeField] Arrow arrow;
    [SerializeField] Stone stone;

    private Dictionary<DicKey, Queue<GameObject>> pool = new Dictionary<DicKey, Queue<GameObject>>();
    // Start is called before the first frame update
    void Start()
    {
        pool.Add(DicKey.arrow, new Queue<GameObject>());
        pool.Add(DicKey.stone, new Queue<GameObject>());
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

            case DicKey.stone:
                {
                    obj.GetComponent<Stone>().Initialize();
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

                case DicKey.stone:
                    {
                        obj = Instantiate(stone, trans).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
            }
        }

        obj = pool[key].Dequeue();
        obj.transform.SetParent(trans);
        switch (key)
        {
            case DicKey.arrow:
                {
                    obj.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    obj.transform.localPosition = Vector3.zero;
                    break;
                }
            case DicKey.stone:
                {
                    obj.transform.position = trans.position;
                    break;
                }
        }
        obj.SetActive(true);
        return obj;
    }
}
