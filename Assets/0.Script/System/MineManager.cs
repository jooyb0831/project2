using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    private SceneChanger sc;
    private Player p;
    [SerializeField] GameObject sign;
    [SerializeField] Rock[] normalRockPrefabs;
    [SerializeField] Rock ironRockPrefab;
    private int number;
    [SerializeField] List<Transform> spawnPos;
    [SerializeField] List<Vector3> positions;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.Player;
        sc = GameManager.Instance.SceneChanger;
        SetMaps();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(sign.transform.position, p.transform.position);
        if(dist<2f)
        {
            sign.transform.GetChild(0).gameObject.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                sc.GoLobby();
            }
        }
        else
        {
            sign.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void SetMaps()
    {

        for (int i = 0; i < 2; i++)
        {
            int rand = Random.Range(0, spawnPos.Count);
            Instantiate(ironRockPrefab, spawnPos[rand].position, Quaternion.Euler(RandRotation()));
            spawnPos.RemoveAt(rand);
        }
        for (int i = 0; i < spawnPos.Count; i++)
        {
            int idx = Random.Range(0, 2);
            Instantiate(normalRockPrefabs[idx], spawnPos[i].position, Quaternion.Euler(RandRotation()));
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
