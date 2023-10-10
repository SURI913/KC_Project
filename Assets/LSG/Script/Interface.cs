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
    bool AtiveSkill { get; set; }   //��ų Ȱ��ȭ �� ���� ���� ���� �� �Ϲݸ��ʹ� �ش� x Awake���� AiveSkill = false;ó��
    double OnAttack(RaycastHit2D hit);
    double OnSkill(RaycastHit2D hit); //���� �� �Ϲݸ��ʹ� �ش� x return 0;�� ����
}

public interface IPassiveActive
{
    void OnPassive();
}