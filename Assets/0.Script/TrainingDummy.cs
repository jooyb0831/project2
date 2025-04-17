using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Hit,
    Dead
}

public class TrainingDummy : MonoBehaviour
{
    [SerializeField] Animator animator;
    private Player p;
    private PlayerData pd;
    private SkillSystem skSystem;
    private Pooling pooling;

    private float recoverTime = 3f;

    [SerializeField] private int curHP;
    private int maxHP = 15;

    [SerializeField] State state = State.Idle;
    void Start()
    {
        p = GameManager.Instance.Player;
        pd = GameManager.Instance.PlayerData;
        skSystem = GameManager.Instance.SkillSystem;
        pooling = GameManager.Instance.Pooling;
        curHP = maxHP;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Punch") && p.state.Equals(Player.State.Attack))
        {
            TakeDamage(pd.BasicAtk); //피격처리
        }

        //플레이어 무기에 맞았을 경우
        if (other.GetComponent<Weapon>())
        {
            if (state.Equals(State.Hit)) return; //이미 피격 상태라면 리턴

            //플레이어가 일반 공격 상태일 때
            if (p.state.Equals(Player.State.Attack)) 
            {
                //피격처리
                TakeDamage(other.GetComponent<Weapon>().weaponData.atkDmg);
            }

            //플레이어가 스킬 공격 상태일 때
            else if (p.state.Equals(Player.State.Skill))
            {   
                //Q스킬일 경우
                if (p.skillState.Equals(Player.SkillState.Qskill))
                {   
                    //피격 처리
                    TakeDamage(skSystem.qSkill.GetComponent<Skill>().data.Damage);
                }
                //R스킬일 경우
                else if (p.skillState.Equals(Player.SkillState.Rskill))
                {   
                    //피격처리
                    TakeDamage(skSystem.rSkill.GetComponent<Skill>().data.Damage);
                }
            }
        }

        //플레이어 화살에 맞았을 경우
        Arrow arrow = other.GetComponent<Arrow>();
        if (arrow)
        {
            if (state.Equals(State.Hit)) return; //이미 피격 상태라면 리턴
            pooling.SetPool(DicKey.arrow, arrow.gameObject); //화살을 Pool로 돌려주기
            TakeDamage(arrow.Damage); //피격처리
        }
    }

    void TakeDamage(int damage)
    {
        curHP -= damage; //체력 감소
        if (curHP <= 0)
        {
            Dead();
            return;
        }
        else
        {
            state = State.Hit;
            animator.SetTrigger("Hit");
        }
    }

    /// <summary>
    /// 사망 처리 함수
    /// </summary>
    void Dead()
    {
        //죽음 처리
        state = State.Dead;
        animator.SetTrigger("Dead");

        Invoke("ResetDummy", recoverTime);
    }

    void ResetDummy()
    {
        curHP = maxHP;
        state = State.Idle;
        animator.SetTrigger("Idle");
    }
}
