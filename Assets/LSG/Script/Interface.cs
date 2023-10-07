using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnDamage(double Damage, RaycastHit hit);   //�������� ����
}

public interface ISkillActive
{
    double OnSkill(double Damage, RaycastHit hit);  //��ų Ȱ��ȭ
}

public interface IPassiveActive
{
    void OnPassive();
}