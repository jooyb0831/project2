using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpike : MonoBehaviour
{
    [SerializeField] int damage = 3;

    [SerializeField] bool isUp;
    private float moveSpeed = 5f;
    private float timer;
    private float restTime = 1.5f;
    private float minY = -0.45f;
    private float maxY = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpDown();
    }

    void MoveUpDown()
    {
        if(isUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        }

        float y = transform.localPosition.y;

        if(y < minY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, minY, transform.localPosition.z);
            timer += Time.deltaTime;
            if(timer>restTime)
            {
                timer = 0;
                isUp = true;
            }
        }
        else if (y >= maxY)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, maxY, transform.localPosition.z);
            timer += Time.deltaTime;
            if(timer > restTime)
            {
                timer = 0;
                isUp = false;
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if(player)
        {
            player.TakeDamage(3);
        }
    }
}
