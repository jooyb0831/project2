using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public enum DicKey
{
    arrow,
    stone,
    ironOre,
    wood,
    itemGetUI,
    slimeBall,
    fallRock,
    enemyRock,
    arrowTrap
}

public class Pooling : Singleton<Pooling>
{
    //오브젝트의 큐 생성
    private Queue<Arrow> arrowQueue = new Queue<Arrow>();
    private Queue<Stone> stoneQueue = new Queue<Stone>();
    private Queue<Wood> woodQueue = new Queue<Wood>();
    private Queue<ItemGetUI> itemGetUIQueue = new Queue<ItemGetUI>();
    private Queue<IronOre> ironOreQueue = new Queue<IronOre>();
    private Queue<SlimeBall> slimeBallQueue = new Queue<SlimeBall>();
    private Queue<GameObject> fallRockQueue = new Queue<GameObject>();
    private Queue<EnemyRock> enemyRockQueue = new Queue<EnemyRock>();
    private Queue<ArrowTrap> arrowTrapQueue = new Queue<ArrowTrap>();

    //프리팹 오브젝트 할당
    [SerializeField] Arrow arrow;
    [SerializeField] Stone stone;
    [SerializeField] Wood wood;
    [SerializeField] ItemGetUI itemGetUI;
    [SerializeField] IronOre ironOre;
    [SerializeField] SlimeBall slimeBall;
    [SerializeField] GameObject fallRock;
    [SerializeField] EnemyRock enemyRock;
    [SerializeField] ArrowTrap arrowTrap;


    //Pool 딕셔너리 생성
    private Dictionary<DicKey, Queue<GameObject>> pool = new Dictionary<DicKey, Queue<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        pool.Add(DicKey.arrow, new Queue<GameObject>());
        pool.Add(DicKey.stone, new Queue<GameObject>());
        pool.Add(DicKey.wood, new Queue<GameObject>());
        pool.Add(DicKey.itemGetUI, new Queue<GameObject>());
        pool.Add(DicKey.ironOre, new Queue<GameObject>());
        pool.Add(DicKey.slimeBall, new Queue<GameObject>());
        pool.Add(DicKey.fallRock, new Queue<GameObject>());
        pool.Add(DicKey.enemyRock, new Queue<GameObject>());
        pool.Add(DicKey.arrowTrap, new Queue<GameObject>());
    }


    public void SetPool(DicKey key, GameObject obj)
    {
        switch (key)
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

            case DicKey.ironOre:
                {
                    obj.GetComponent<IronOre>().Initialize();
                }
                break;
            case DicKey.slimeBall:
                {
                    obj.GetComponent<SlimeBall>().Initialize();
                }
                break;
            case DicKey.fallRock:
                {
                    obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                break;
            case DicKey.enemyRock:
                {
                    obj.GetComponent<EnemyRock>().Initialize();
                }
                break;
            case DicKey.arrowTrap:
                {
                    obj.GetComponent<ArrowTrap>().Initialize();
                    break;
                }

            default:
                break;

        }

        obj.SetActive(false);
        pool[key].Enqueue(obj);
    }

    public GameObject GetPool(DicKey key, Transform trans = null)
    {
        GameObject obj = null;

        //Pool에 오브젝트가 없을 경우 새로 생성하고 큐에 집어넣음
        if (pool[key].Count == 0)
        {
            switch (key)
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

                case DicKey.wood:
                    {
                        obj = Instantiate(wood, trans).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;

                case DicKey.itemGetUI:
                    {
                        obj = Instantiate(itemGetUI, trans).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
                case DicKey.ironOre:
                    {
                        obj = Instantiate(ironOre, trans).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
                case DicKey.slimeBall:
                    {
                        obj = Instantiate(slimeBall, trans).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
                case DicKey.enemyRock:
                    {
                        obj = Instantiate(enemyRock, trans).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
                case DicKey.fallRock:
                    {
                        obj = Instantiate(fallRock, trans).gameObject;
                        pool[key].Enqueue(obj);
                    }
                    break;
                case DicKey.arrowTrap:
                    {
                        obj = Instantiate(arrowTrap, trans).gameObject;
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
            case DicKey.wood:
                {
                    obj.transform.position = trans.position;
                    break;
                }
            case DicKey.ironOre:
                {
                    obj.transform.position = trans.position;
                    break;
                }
            case DicKey.slimeBall:
                {
                    obj.transform.SetParent(trans);
                    obj.transform.localPosition = Vector3.zero;
                    //obj.transform.SetParent(null);
                    break;
                }
            case DicKey.enemyRock:
                {
                    obj.transform.SetParent(trans);
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.SetParent(null);
                    break;
                }
            case DicKey.fallRock:
                {
                    obj.transform.position = trans.position;
                    break;
                }
            case DicKey.arrowTrap:
                {
                    obj.transform.SetParent(trans);
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.SetParent(null);
                    break;
                }
            default:
                break;
        }
        obj.SetActive(true);
        return obj;
    }
}
