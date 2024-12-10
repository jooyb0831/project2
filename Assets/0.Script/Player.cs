using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk,
        Run,
        Jump,
        Attack,
        Hit,
        Dead
    }

    private Rigidbody rigid;
    private Animator animator;
    private PlayerData pd;
    public State state = State.Idle;

    [SerializeField] Transform foot;
    // Start is called before the first frame update
    void Start()
    {

        pd = GameManager.Instance.PlayerData;
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(foot.position, Vector3.down * 0.1f, Color.red);
        Move();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * pd.Speed);
        
        RaycastHit hit;
        if(Physics.Raycast(foot.position, Vector3.down * 0.1f, out hit))
        {
            if(hit.collider.gameObject == null)
            {
                state = State.Jump;
            }
        }
        /*
        RaycastHit hit;

        if (Physics.Raycast(foot.position, foot.transform.forward, out hit))
        {
            
            state = State.Jump;
        }
        */
        if (Input.GetButtonDown("Jump"))
        {
            if (state != State.Jump)
            {
                Jump();
            }
                
        }


        if (x != 0 || z != 0)
        {
            animator.SetTrigger("WalkForward");
            state = State.Walk;
        }
        else
        {
            animator.SetTrigger("Idle");
            state = State.Idle;
        }
    }

    void Jump()
    {
        rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        animator.SetTrigger("Jump");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ãæµ¹");
        }
    }
}
