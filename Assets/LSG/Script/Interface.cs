using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnDamage(double Damage, RaycastHit hit);   //데미지를 입힘
}

public interface ISkillActive
{
    double OnSkill(double Damage, RaycastHit hit);  //스킬 활성화
}

public interface IPassiveActive
{
    void OnPassive();
}