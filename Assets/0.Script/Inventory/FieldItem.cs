using UnityEngine;


[System.Serializable]
public class ItemData
{
    public string itemTitle;
    public string itemExplain;
    public Sprite invenIcon;
    public Sprite bgSprite;
    public ItemType type;
    public int count;
    public int price;
    public int itemIdx;
    public GameObject obj;
    public FieldItem fItem;
}

public class FieldItem : MonoBehaviour
{
    public ItemData itemData;
    protected GameUI gameUI;
    protected Inventory inven;
    protected Player p;
    protected PlayerData pd;

    [SerializeField] protected bool isFind = false;
    [SerializeField] protected GameObject getUI;

    //인벤이 가득 찼는지 변수
    private bool isFull = false;
    
    //아이템 이동 속도를 받을 변수
    private float speed;

    //아이템이 움직이는 속도
    private const float SPEED = 5f;
    //찾았다고 인식되는 최소거리
    private const float FIND_DIST = 1.5f;
    //획득되는 거리
    private const float GET_DIST = 0.2f;
    //수동으로 획득할 때 최소거리
    private const float GATHER_DIST = 2.0f;
    //수집시 ST 소모하는 아이템의 ST 사용치
    private const int ST_USE = 2;
    void Start()
    {
        Init();
    }


    public virtual void Init()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        inven = GameManager.Instance.Inven;
        gameUI = GameManager.Instance.GameUI;
        itemData.obj = this.gameObject;
    }

    
    void Update()
    {
        if(itemData.type.Equals(ItemType.Plant))
        {
            ItemGet();
        }

        else
        {
            ItemMove();
        }
    }
    

    /// <summary>
    /// 실물 아이템이 게임에서 움직이는 코드
    /// </summary>
    void ItemMove()
    {
        if (p == null)
        {
            p = GameManager.Instance.Player;
        }

        //거리계산
        float dist = Vector3.Distance(p.transform.position, transform.position);

        //거리가 FIND_DIST보다 가까우면
        if (dist < FIND_DIST)
        {   
            //인벤이 가득 차지 않았을 경우에만 찾아진 것으로 체크
            isFind = !isFull;
        }
        //거리가 FIND_DIST보다 멀면 안 찾아진 것으로 인식
        else
        {
            isFind = false;
        }

        //아이템 움직이는 속도가 Find상태이면 SPEED, 아닐 경우 0(움직이지 않음)
        speed = isFind ? SPEED : 0f;

        //아이템 이동
        transform.position = Vector3.MoveTowards(transform.position, 
                                                p.transform.position, 
                                                Time.deltaTime * speed);

        //거리가 GET_DIST미만이면 인벤토리에 추가
        if (dist < GET_DIST)
        {
            inven.GetItem(itemData);
        }
    }

    /// <summary>
    /// 수동으로 획득하는 아이템의 코드
    /// </summary>
    public virtual void ItemGet()
    {
        if(p == null)
        {
            p = GameManager.Instance.Player;
        }

        //거리계산
        float dist = Vector3.Distance(p.transform.position, transform.position);

        //거리가 GATHER_DIST미만일 경우
        if(dist < GATHER_DIST)
        {   
            //획득 표기 UI 활성화
            getUI.SetActive(true);
            //E키를 눌렀을 때 
            if(Input.GetKeyDown(KeyCode.E))
            {   
                //플레이어의 ST가 ST_USE(소모되는 ST)미만이면 리턴
                if(pd.ST < ST_USE)
                {
                    //기력이 부족함을 UI에 표시
                    gameUI.DisplayInfo(0);
                    return;
                }
                //아이템 수집 애니메이션 호출
                p.GatherAnim(true, 2);
                //인벤토리에 아이템 추가
                inven.GetItem(itemData);
            }
        }
        //거리가 GATHER_DIST보다 멀면
        else
        {   
            //아이템 획득 표기 UI 비활성화
            getUI.SetActive(false);
        }
           
    }
    
    /// <summary>
    /// 인벤토리가 Full일때 처리하는 함수
    /// </summary>
    /// <param name="invenFull"></param>
    public void InvenFull(bool invenFull)
    {
        isFull = invenFull;
        if(invenFull)
        {
            //인벤토리가 가득 찼음을 UI에 표기
            gameUI.DisplayInfo(8);
        }
    }


    public virtual bool ItemUseCheck()
    {
        return true;
    }
    
    public virtual void UseItem()
    {
        
    }
}
