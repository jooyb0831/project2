using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{   
    //물고기 잡는 원
    [SerializeField] CatchZone catchZone;

    //물고기 프리팹
    [SerializeField] FishMove fishPrefab;

    //물고기를 관리하는 리스트
    [SerializeField] List<FishMove> fishList;

    //스폰할 물고기의 갯수를 받는 정수 변수
    [SerializeField] int fishCnt;

    //최대로 스폰 가능한 물고기 상수
    private const int MAX_FISH_CNT = 5;

    //X와 Z좌표의 제한값
    private const float X_POS = 13f;
    private const float Z_POS = 7f;

    //Y좌표 고정값
    private const float Y_POS = -0.4f;


    void Start()
    {
        SpawnFish();
    }

    /// <summary>
    /// 물고기 스폰을 초기화하는 함수
    /// </summary>
    public void Initialize()
    {
        //물고기 잡는 원 초기화
        catchZone.Init();

        //씬에 있는 물고기 모두 삭제
        if(fishList.Count != 0)
        {
            foreach (var item in fishList)
            {
                if (item != null)
                {
                    Destroy(item.gameObject);
                }
            }
        }

        fishList.Clear();
        
        // 물고기 다시 스폰
        SpawnFish();
    }


    /// <summary>
    /// 물고기 스폰하는 함수
    /// </summary>
    void SpawnFish()
    {
        // 1~MAX_FISH_CNT까지의 랜덤한 숫자 뽑아서 그만큼 물고기 만들기
        fishCnt = Random.Range(1, MAX_FISH_CNT + 1);
        for (int i = 0; i < fishCnt; i++)
        {
            FishMove fish = Instantiate(fishPrefab, RandomPos()
                                        , Quaternion.identity);
            fishList.Add(fish);
        }
    }

    /// <summary>
    /// 랜덤한 포지션을 반환하는 함수
    /// </summary>
    /// <returns></returns>
    Vector3 RandomPos()
    {
        float x = Random.Range(-X_POS, X_POS);
        float z = Random.Range(-Z_POS, Z_POS);

        return new Vector3(x, Y_POS, z);
    }
}
