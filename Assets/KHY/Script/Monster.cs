using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
   

    public string ID_m { get; set; } //���͸�
    protected double HP { get; set; } //ü��
    protected double maxHP { get; set; } //�ִ�ü��

    protected bool Dead = false;
    protected double Attack { get; set; } //���ݷ�
    protected int AtkTime { get; set; } //������Ÿ��
    protected double movespeed { get; set; }// �̵�

    protected virtual void HP_m()
    {
        maxHP = maxHP;
        HP = HP;
    }
    protected virtual void DIe_m()
    {
        Dead = true;
    }

    protected virtual void Attack_m()
    {
        Attack = Attack;
        AtkTime = AtkTime;
    }

    protected virtual void Damage_m(float damage)
    {
        HP -= damage;
    }



    protected virtual void move_m()
    {

        movespeed = movespeed;
    }
}
