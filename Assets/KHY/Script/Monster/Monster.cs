using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
   

    public string ID_m { get; set; } //��
    public double HP { get; set; } //ü��
    public double maxHP { get; set; } //�ִ�ü��

    public bool Dead = false;
    public double Attack { get; set; } //���ݷ�
    public int AtkTime { get; set; } //������Ÿ��
    public double movespeed { get; set; }// �̵�

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
