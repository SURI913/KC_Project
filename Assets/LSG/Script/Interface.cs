using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnDamage(double Damage, RaycastHit2D hit);   //데미지를 입힘
}

public interface IAttack
{
    float speed { get; set; }   //공격 속도
    float atkTime { get; set; } //일반공격 쿨타임
    float skillTime { get; set; }   //스킬 공격 쿨타임
    double OnAttack(RaycastHit2D hit);
}

public interface IPassiveActive
{
    void OnPassive();
}