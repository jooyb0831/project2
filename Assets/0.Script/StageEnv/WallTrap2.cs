using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap2 : MonoBehaviour
{
    [SerializeField] Transform firePos;
    [SerializeField] float fireCoolTime;
    private float timer;
    private bool isTriggered;

    private Pooling pooling;
    // Start is called before the first frame update
    void Start()
    {
        pooling = GameManager.Instance.Pooling;
        timer = fireCoolTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTriggered)
        {
            timer += Time.deltaTime;
            if(timer>=fireCoolTime)
            {
                ArrowTrap arrowTrap = pooling.GetPool(DicKey.arrowTrap, firePos).GetComponent<ArrowTrap>();
                arrowTrap.Fire();
                timer = 0;
            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>()&&!isTriggered)
        {
            isTriggered = true;
        }
    }
}
