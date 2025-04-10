using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CircleTest : MonoBehaviour
{
    private float timer;
    [SerializeField] float time; // 기준시간
    [SerializeField] bool isStop = true;
    [SerializeField] bool isDone;
    [SerializeField] bool isFailed;
    [SerializeField] int clickCnt = 0;
    [SerializeField] List<GameObject> objs;

    void OnMouseDown()
    {
        clickCnt++;
        if(clickCnt >= 2)
        {
            isDone = true;
            return;
        }
        if(isStop)
        {
            isStop = false;
        }
        else
        {
            isStop = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;

        if (x<=1.5f)
        {
            isDone = true;
            isFailed = true;
        }

        if (isDone) 
        {
            if(isFailed)
            {
                GetComponent<SphereCollider>().enabled = false;
                return;
            }
            Debug.Log(objs.Count);
            return;
        }

        if (!isStop && !isDone)
        {
            timer += Time.deltaTime;

            if (timer >= time)
            {
                timer = 0;
                x -= 0.2f;
                y -= 0.2f;
                z -= 0.2f;
                transform.localScale = new Vector3(x, y, z);
            }
        }

    }


    public void OnTriggerEnter(Collider coll)
    {
        if(coll.GetComponent<FishMove>())
        {
            objs.Add(coll.gameObject);
        }
    }

    public void OnTriggerExit(Collider coll)
    {
        if(coll.GetComponent<FishMove>())
        {
            objs.Remove(coll.gameObject);
        }
    }

}
