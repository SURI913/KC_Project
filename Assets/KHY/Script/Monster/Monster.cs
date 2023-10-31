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

   

    protected virtual void HP_m()
    {
        maxHP = maxHP;
        HP = HP;
    }
    protected virtual void DIe_m()
    {
        Dead = true;
        //몬스터 죽는 애니메이션 추가 
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
