using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDealer : Cat, SkillUserImp, AttackableImp
{
    public float speed { get; set; }   //공격 속도
    public float atk_time { get; set; } //일반공격 쿨타임
    public float skill_time { get; set; }   //스킬 공격 쿨타임
    public bool is_ative_skill { get; set; } = false;   //스킬 활성화 시 공격 멈춤
    public BaseDealer(BaseCatData _cat_data, GrowingData _growing_data, GameObject _damage_prefab) 
        : base(_cat_data, _growing_data, _damage_prefab) 
    { speed = _cat_data._attack_speed; atk_time = _cat_data._atk_time; skill_time = _cat_data._skl_time; skill_effect = _cat_data._skl_effect; }

    //--------------------------------------------------------------------------------------------------------------공격 : 특수공격이 있으면 override
    //====[TODO]====이후 스킬 방향성 보고 수정 스킬만 따로 캐릭터 별로 분류한다거나
    public virtual double OnSkill(RaycastHit2D hit)
    {
        is_ative_skill = true;
        catMotion.SetTrigger("isSkill");

        double skillDamage = GetAttackPower() * skill_effect;
        if (hit.collider.CompareTag("boss")) //보스 공격의 경우
        {
            skillDamage += boss_attack;
        }
        //Debug.Log(ID+"skillDamage: "+Unit.ToUnitString(skillDamage));
        return skillDamage;
    }

    public virtual double OnAttack(RaycastHit2D hit) 
    {
        //스킬공격중이 아니라면 데미지값 반환, 공격중이라면 0 반환
        if(!is_ative_skill)
        {
            catMotion.SetTrigger("isAttack");
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
