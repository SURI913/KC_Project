using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IDamage
{
    public string ID_m { get; set; } //몬스터명
    protected float HP { get; set; } //체력
    protected float maxHP { get; set; } //최대체력
    protected float Attack { get; set; } //공격력
    protected int AtcTime { get; set; } //공격쿨타임

    protected float movespeed { get; set; }// 이동
   // void Damage_m(float damage);
        
}

