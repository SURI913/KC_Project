using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDealer : Cat, SkillUserImp, AttackableImp
{
    //--------------------------------------------------------------------------------------------------------------공격 : 특수공격이 있으면 override
    //====[TODO]====이후 스킬 방향성 보고 수정 스킬만 따로 캐릭터 별로 분류한다거나
    public override double OnSkill(RaycastHit2D hit)
    {
        is_ative_skill = true;
        cat_motion.SetTrigger("isSkill");

        double skillDamage = GetAttackPower() * skill_effect;
        if (hit.collider.CompareTag("boss")) //보스 공격의 경우
        {
            skillDamage += boss_attack;
        }
        //Debug.Log(ID+"skillDamage: "+Unit.ToUnitString(skillDamage));
        return skillDamage;
    }

    public override double OnAttack(RaycastHit2D hit) 
    {
        //스킬공격중이 아니라면 데미지값 반환, 공격중이라면 0 반환
        if(!is_ative_skill)
        {
            cat_motion.SetTrigger("isAttack");
            if (hit.collider.CompareTag("boss"))
            {
                return GetAttackPower() + boss_attack;
            }
            return GetAttackPower();
        }
        else
        {
            return 0;
        }
    }
}
