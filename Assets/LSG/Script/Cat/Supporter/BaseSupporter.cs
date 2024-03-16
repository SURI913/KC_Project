using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSupporter : Cat, AttackableImp, SkillUserImp
{
    //---------------------------------------------------------------------------FindTarget
    List<Cat> support_target = new List<Cat>();
    //--------------------------------------------------------------------------------------------------------------공격 : 특수공격이 있으면 override

    public void FindSupportTarget() //대신에 생성자로 값 넣는방식으로 변경해 볼 것
    {
        //이거 왜 가지고 오지 못하는거?
        Vector2 skillpos = gameObject.transform.position;
        //스킬특기
        Collider2D[] cats = Physics2D.OverlapCircleAll(skillpos, 1000.0f);
        foreach (var Cats in cats)
        {
            if (Cats.CompareTag("Player"))
            {
                support_target.Add(Cats.GetComponent<Cat>());
            }
        }

    }

    public override double OnSkill(RaycastHit2D hit)
    {
        cat_motion.SetTrigger("isSkill");

        //Debug.Log( "힐링스킬 발동");
        //Debug.Log(catsHealing.Count);
        //요새 범위 안에있는 사람한테만 힐이 들어감
        foreach (var Cats in support_target)
        {
            Cats.hp += Cats.growing_data.Hp * cat_data._hp_multipler * skill_effect;
            // Debug.Log(Cats.ID+ "힐링");
            if (hp > growing_data.Hp * cat_data._hp_multipler) //maxHp를 넘지않게 처리
            {
                hp = growing_data.Hp * cat_data._hp_multipler;
            }
        }
        return 0;
    }

    public override double OnAttack(RaycastHit2D hit) //공격 체크
    {

        if (hit.collider.CompareTag("boss")) //보스 공격의 경우
        {
            return GetAttackPower() + boss_attack;
        }
        return GetAttackPower();
    }

}
