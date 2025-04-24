using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] int hitCnt = 12;
    [SerializeField] int curHit = 0;
    [SerializeField] GameObject wood;
    [SerializeField] Transform area;
    [SerializeField] Transform woodPooling;
    [SerializeField] ParticleSystem woodParticle;

    private Pooling pooling;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player;
        pooling = GameManager.Instance.Pooling;
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
        if (other.GetComponent<Axe>() && player.state.Equals(Player.State.Mine))
        {
            curHit++;
            if (curHit > 0)
            {
                if (curHit % 3 == 0)
                {
                    GameObject obj = pooling.GetPool(DicKey.wood, area);
                    obj.transform.SetParent(woodPooling);
                }
            }

        }
    }

}
