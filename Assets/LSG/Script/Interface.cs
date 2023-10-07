using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnDamage(double Damage, RaycastHit2D hit);   //�������� ����
}

public interface IAttack
{
    float speed { get; set; }   //���� �ӵ�
    float atkTime { get; set; } //�Ϲݰ��� ��Ÿ��
    float skillTime { get; set; }   //��ų ���� ��Ÿ��
    double OnAttack(RaycastHit2D hit);
}

public interface IPassiveActive
{
    void OnPassive();
}