using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
   

    public string ID_m { get; set; } //명
    public double HP { get; set; } //체력
    public double maxHP { get; set; } //최대체력

    public bool Dead = false;
    public double Attack { get; set; } //공격력
    public int AtkTime { get; set; } //공격쿨타임
    public double movespeed { get; set; }// 이동

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
