using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DamageableImp
{
    void OnDamage(double Damage);   //데미지를 입힘
}

public interface SkillUserImp
{
    float speed { get; set; }   //공격 속도
    float skill_time { get; set; }   //스킬 공격 쿨타임
    bool is_ative_skill { get; set; }   //스킬 활성화 시 공격 멈춤
    double OnSkill(RaycastHit2D hit);
}

public interface AttackableImp
{
    float speed { get; set; }   //공격 속도
    float atk_time { get; set; } //일반공격 쿨타임
    double OnAttack(RaycastHit2D hit);
}

public interface PassiveActiveImp
{
    void OnPassive();
}

interface UsableItemImp
{
    // 아이템 사용 : 성공 여부 리턴
    bool Use(double _amount);
}