using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDealer : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------공격 : 특수공격이 있으면 override
    //====[TODO]====이후 스킬 방향성 보고 수정 스킬만 따로 캐릭터 별로 분류한다거나
    /*public double OnSkill(Collider2D collision)
    {
        is_ative_skill = true;
        myMotion.SetTrigger("isSkill");

        double skillDamage = GetAttackPower() * skill_effect;
        if (collision.CompareTag("boss")) //보스 공격의 경우
        {
            //skillDamage += boss_attack;
        }
        //Debug.Log(ID+"skillDamage: "+Unit.ToUnitString(skillDamage));
        return skillDamage;
    }

    public double OnAttack(Collider2D collision) 
    {
        //스킬공격중이 아니라면 데미지값 반환, 공격중이라면 0 반환
        if(!is_ative_skill)
        {
            myMotion.SetTrigger("isAttack");
            if (collision.CompareTag("boss"))
            {
                return GetAttackPower();
            }
            return GetAttackPower();
        }
        else
        {
            return 0;
        }
    }*/
}
