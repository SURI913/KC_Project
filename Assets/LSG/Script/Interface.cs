using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnDamage(double Damage, RaycastHit2D hit);   //데미지를 입힘
}

public interface IAttack //수정필요함 공격이랑 스킬 따로 분리할 것
{
    float speed { get; set; }   //공격 속도
    float atkTime { get; set; } //일반공격 쿨타임
    float skillTime { get; set; }   //스킬 공격 쿨타임
    bool ativeSkill { get; set; }   //스킬 활성화 시 공격 멈춤 보스 및 일반몬스터는 해당 x Awake에서 AiveSkill = false;처리
    double OnAttack(RaycastHit2D hit);
    double OnSkill(RaycastHit2D hit); //보스 및 일반몬스터는 해당 x return 0;로 종료
}

public interface IPassiveActive
{
    void OnPassive();
}

interface IUsableItem
{
    // 아이템 사용 : 성공 여부 리턴
    bool Use(double _amount);
}