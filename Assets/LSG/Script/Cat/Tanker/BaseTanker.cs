using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTanker : Cat, SkillUserImp, AttackableImp
{
    public float speed { get; set; }   //공격 속도
    public float atk_time { get; set; } //일반공격 쿨타임
    public float skill_time { get; set; }   //스킬 공격 쿨타임
    public bool is_ative_skill { get; set; } = false;   //스킬 활성화 시 공격 멈춤

    public GameObject attack_effect;

    public BaseTanker(BaseCatData _cat_data, GrowingData _growing_data, GameObject _damage_prefab)
        : base(_cat_data, _growing_data, _damage_prefab)
    { speed = _cat_data._attack_speed; atk_time = _cat_data._atk_time; skill_time = _cat_data._skl_time; skill_effect = _cat_data._skl_effect; }


    public double OnSkill(RaycastHit2D hit)
    {
        //스킬 사용
        StartCoroutine(Skill());
        return 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {


        if (collision.gameObject.layer == 6) //타겟레이어의 경우
        {
            Debug.Log(collision.collider.name);
            //데미지 스크립트 확인시 공격 시작
            if (collision.collider.GetComponent<DamageableImp>() != null && !is_attack)
            {
                StartCoroutine(AttackEft(collision));
                player_rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Target"))
        {
            //Debug.Log("탱커 공격 대기중");
            isLookTarget = false;
            player_rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //이전위치로 돌아가게 수정해야함
        }

    }

    //공격 효과 및 적용

    private GameObject targetEvent;
    IEnumerator AttackEft(Collision2D collision)
    {
        is_attack = true;
        catMotion.SetTrigger("isAttack");
        Destroy(Instantiate(attack_effect, transform.position + new Vector3(2f, 0, 0), Quaternion.identity), atk_time);
        collision.collider.GetComponent<DamageableImp>().OnDamage(OnAttack(target)); //데미지 주는 스크립트
        yield return new WaitForSeconds(atk_time);
        is_attack = false;

    }

    public double OnAttack(RaycastHit2D hit) //공격값 계산
    {
        catMotion.SetTrigger("isAttack");

        if (hit.collider.CompareTag("boss")) //보스라면
        {
            return GetAttackPower() + boss_attack;
        }
        return GetAttackPower();
    }

    IEnumerator Skill()
    {
        catMotion.SetTrigger("isSkill");

        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(skill_time);
        GetComponent<Collider2D>().enabled = true;
        //Debug.Log(ID+"스킬 사용중");
    }
}
