using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public class Data
    {
        public int HitCnt;
        public int CurHit = 0;
        public int NeedHitCnt;
        public int EXP;
    }
    protected Player p;
    protected JsonData jsonData;
    public Data data = new Data();
    [SerializeField] protected GameObject stone;
    [SerializeField] protected Transform area;
    [SerializeField] protected Transform stonePooling;
    [SerializeField] protected GameObject effect;
    // Start is called before the first frame update
    void Start()
    {

    }

    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        jsonData = GameManager.Instance.JsonData;
    }

    public virtual void SetData(int idx)
    {
        data.HitCnt = jsonData.rockData.rData[idx].maxHit;
        data.NeedHitCnt = jsonData.rockData.rData[idx].needHit;
        data.EXP = jsonData.rockData.rData[idx].exp;
    }
    // Update is called once per frame
    void Update()
    {
        if (data.CurHit >= data.HitCnt)
        {
            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickAxe>() && p.state.Equals(Player.State.Mine))
        {
            data.CurHit++;

            effect.SetActive(true);
            Invoke(nameof(ResetEffect), 0.2f);
            if (data.CurHit > 0)
            {
                if (data.CurHit % data.NeedHitCnt == 0)
                {
                    //Pooling으로 처리할것
                    GameObject obj = Pooling.Instance.GetPool(DicKey.stone, area);
                    obj.transform.SetParent(stonePooling);
                }
            }
        }
    }



    void ResetEffect()
    {
        effect.SetActive(false);
        effect.GetComponent<ParticleSystem>().Clear();
    }
}
