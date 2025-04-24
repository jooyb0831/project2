using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchZone : MonoBehaviour
{
    private float timer;

    //Collider을 담을 변수 
    private SphereCollider coll;
    //Inventory를 받을 변수
    private Inventory inven;

    //UI를 연결할 변수
    [SerializeField] FishResultUI fishResultUI;

    //원의 최대/최소 사이즈의 상수값
    private const float MAX_SIZE = 10f;
    private const float MIN_SIZE = 1.5f;

    [SerializeField] float time; // 기준시간
    [SerializeField] bool isStop = true; //원의 움직임 여부를 체크하는 Bool
    [SerializeField] bool isDone; // 끝났는지를 체크하는 Bool
    [SerializeField] int clickCnt = 0; //클릭 횟수를 받는 변수
    [SerializeField] List<GameObject> objs; //잡은 물고기를 담는 리스트

    void Start()
    {
        coll = GetComponent<SphereCollider>();
        inven = GameManager.Instance.Inven;
    }

    /// <summary>
    /// 오브젝트에 마우스 클릭 시 호출되는 함수
    /// </summary>
    void OnMouseDown()
    {
        //클릭 카운트
        clickCnt++;

        //2번째 클릭 시
        if (clickCnt >= 2)
        {
            ShowResult();
            return;
        }

        //원이 정지한 상태일때 클릭하면 움직이게, 반대의 경우 안 움직이게
        isStop = !isStop;
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public void Init()
    {
        //최대 사이즈로 만들기
        transform.localScale = Vector3.one * MAX_SIZE;
        //멈춰있는 상태, 끝나지 않은 상태로 만들기
        isStop = true; isDone = false;
        //Collider 활성화
        coll.enabled = true;
        //타이머 초기화
        timer = 0;
        //리스트 초기화
        objs.Clear();
        //클릭 횟수 초기화
        clickCnt = 0;
    }

    void Update()
    {
        ScaleChange();
    }

    /// <summary>
    /// 원 크기 줄어드는 함수
    /// </summary>
    void ScaleChange()
    {
        // 크기 받아오기
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;

        // 최소 사이즈보다 작아지면 종료로 체크
        if (x <= MIN_SIZE)
        {
            ShowResult();
        }

        //종료되었다면 Colldier 끄기(더 이상 체크 되지 않게)
        if (isDone)
        {
            coll.enabled = false;
        }

        //원이 움직이거나 종료된 상태가 아닐 경우
        if (!isStop && !isDone)
        {
            //타이머 활성화
            timer += Time.deltaTime;
            //타이머를 체크하면서 크기 줄이기
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


    /// <summary>
    /// 결과 로드
    /// </summary>
    void ShowResult()
    {
        //끝남 체크
        isDone = true;

        //리스트에 담겨있는 아이템을 돌면서
        foreach (var item in objs)
        {
            //물고기 움직임 중지 
            item.GetComponent<FishMove>().isMoving = false;
            //인벤토리에 아이템 추가
            inven.GetItem(item.GetComponent<FieldItem>().itemData);
        }

        //UI활성화 및 표시
        fishResultUI.SetUpResult(objs.Count);
        fishResultUI.gameObject.SetActive(true);
    }

    public void OnTriggerEnter(Collider coll)
    {
        //물고기와 원이 충돌하면
        if (coll.GetComponent<FishMove>())
        {
            //리스트에 물고기 추가
            objs.Add(coll.gameObject);
        }
    }

    public void OnTriggerExit(Collider coll)
    {
        //물고기가 원 밖으로 나가면
        if (coll.GetComponent<FishMove>())
        {
            //리스트에서 물고기 제거
            objs.Remove(coll.gameObject);
        }
    }

}
