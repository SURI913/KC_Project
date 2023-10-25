using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
   

    public string ID_m { get; set; } //몬스터명
    protected double HP { get; set; } //체력
    protected double maxHP { get; set; } //최대체력

    protected bool Dead = false;
    protected double Attack { get; set; } //공격력
    protected int AtkTime { get; set; } //공격쿨타임
    protected double movespeed { get; set; }// 이동

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
