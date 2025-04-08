using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRockCtrl : MonoBehaviour
{
    [SerializeField] Transform[] spawnPos;
    [SerializeField] float delay;
    private float timer;

    private Pooling pooling;
    // Start is called before the first frame update
    void Start()
    {
        pooling = GameManager.Instance.Pooling;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            int pos = Random.Range(0, spawnPos.Length);
            pooling.GetPool(DicKey.fallRock, spawnPos[pos]);
            timer = 0;
        }
    }
}
