using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTanker : Cat
{
    public GameObject attack_effect;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) //타겟레이어의 경우
        {
            //데미지 스크립트 확인시 공격 시작
            if (collision.collider.GetComponent<DamageableImp>() != null && !is_attack)
            {
                //StartCoroutine(AttackEft(collision));
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

/*    IEnumerator AttackEft(Collision2D collision)
    {
        is_attack = true;
        cat_motion.SetTrigger("isAttack");

        var my_Effect_obj = ObjectPoolManager.instance.GetGo(cat_data._id + "_Atk_Obj").transform.position = this.transform.position;

        //Destroy(Instantiate(attack_effect, transform.position + new Vector3(2f, 0, 0), Quaternion.identity), atk_time);
        collision.collider.GetComponent<DamageableImp>().OnDamage(OnAttack(collision.collider)); //데미지 주는 스크립트
        yield return new WaitForSeconds(atk_time);
        is_attack = false;

    }*/

    public override double OnAttack(Collider2D collision) //공격값 계산
    {
        cat_motion.SetTrigger("isAttack");

        if (collision.CompareTag("boss")) //보스라면
        {
            return GetAttackPower() + boss_attack;
        }
        return GetAttackPower();
    }

    public IEnumerator Skill()
    {
        cat_motion.SetTrigger("isSkill");
        var my_Effect_obj = ObjectPoolManager.instance.GetGo(cat_data._id + "_Skill_Obj");
        my_Effect_obj.transform.position = transform.position + new Vector3(0, 4, 0);
        my_Effect_obj.GetComponent<MeleeImpact>().init_transform = transform;

        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(skill_time);
        GetComponent<Collider2D>().enabled = true;
        //Debug.Log(ID+"스킬 사용중");
    }

    public override double OnSkill(Collider2D collision)
    {
        StartCoroutine(Skill());
        return 0;
    }

}
