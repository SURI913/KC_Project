using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public string ID_m { get; set; } //���͸�
    protected float HP { get; set; } //ü��
    protected float maxHP { get; set; } //�ִ�ü��

    protected bool Dead = false;
    protected float Attack { get; set; } //���ݷ�
    protected int AtcTime { get; set; } //������Ÿ��

    protected float movespeed { get; set; }// �̵�

    protected virtual void HP_m()
    {
        HP = maxHP;
    }
    protected virtual void DIe_m()
    {
        Dead = true;
    }

    protected virtual void Attack_m()
    {
        Attack = Attack;
        AtcTime = AtcTime;
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
