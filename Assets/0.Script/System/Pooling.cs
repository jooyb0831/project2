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
    //오브젝트의 큐 생성
    private Queue<Arrow> arrowQueue = new Queue<Arrow>();
    private Queue<Stone> stoneQueue = new Queue<Stone>();

    //프리팹 오브젝트
    [SerializeField] Arrow arrow;
    [SerializeField] Stone stone;


    //pool 딕셔너리 생성
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

        obj.SetActive(false);
        pool[key].Enqueue(obj);
    }


    public GameObject GetPool(DicKey key, Transform trans = null)
    {
        GameObject obj = null;

        //Pool에 오브젝트가 없을 경우 새로 생성하고 큐에 집어넣기
        if(pool[key].Count == 0)
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

        //Queue에서 오브젝트 꺼내기
        obj = pool[key].Dequeue();

        switch (key)
        {
            case DicKey.arrow:
                {
                    obj.transform.SetParent(trans);
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
