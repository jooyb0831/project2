using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    [SerializeField] Rock[] normalRockPrefabs;
    [SerializeField] int number;
    [SerializeField] Transform Ground;
    [SerializeField] List<Vector3> positions;
    // Start is called before the first frame update
    void Start()
    {
        SetMaps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetMaps()
    {
        for(int i = 0; i < number; i++)
        {
            int idx = Random.Range(0,2);
            Instantiate(normalRockPrefabs[idx], RandPos(), Quaternion.Euler(RandRotation()));
        }
    }

    Vector3 RandRotation()
    {
        float y = Random.Range(0,359f);
        Vector3 rot = new Vector3(0, y, 0);
        return rot;
    }
    

    Vector3Int RandPos()
    {
        int x = Random.Range(-5, 7);
        int z = Random.Range(-7, 7);
        Vector3Int pos = new Vector3Int (x, 1, z);

        return pos;
        /*
        if(positions.Count == 0)
        {
            positions.Add(pos);
            return pos;
        }

        for(int i = 0; i<positions.Count; i++)
        {
            if(positions[i] == pos)
            {
                RandPos();
            }
            else
            {
                positions.Add(pos);
                
            }
        }
        return pos;
        */
    }
}
