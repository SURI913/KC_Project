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

   

    protected virtual void HP_m()
    {
        maxHP = maxHP;
        HP = HP;
    }
    protected virtual void DIe_m()
    {
        Dead = true;
        //���� �״� �ִϸ��̼� �߰� 
    }

    protected virtual void OnAttack_m(RaycastHit2D hit)
    {
        Attack = Attack;
        AtkTime = AtkTime;
    }

    protected virtual void OnDamage_m(float damage, RaycastHit2D hit)
    {
        HP -= damage;
    }



    /*protected virtual void move_m()
    {

        movespeed = movespeed;
    }*/
}
