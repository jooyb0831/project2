using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Player p;
    [SerializeField] int hitCnt = 12;
    [SerializeField] int curHit = 0;
    [SerializeField] GameObject stone;
    [SerializeField] Transform area;
    [SerializeField] Transform stonePooling;
    [SerializeField] GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHit >= hitCnt)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickAxe>() && p.state.Equals(Player.State.Mine))
        {
            curHit += 1;

            effect.SetActive(true);
            Invoke(nameof(ResetEffect), 0.2f);
            if (curHit > 0)
            {
                if (curHit % 3 == 0)
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
