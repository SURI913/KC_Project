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

    public string ID_m { get; set; } //몬스터명
    protected float HP { get; set; } //체력
    protected float maxHP { get; set; } //최대체력

    protected bool Dead = false;
    protected float Attack { get; set; } //공격력
    protected int AtcTime { get; set; } //공격쿨타임

    protected float movespeed { get; set; }// 이동

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
