using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class M_D02 : Monster
{
    //protected int Stage[];
    //스테이지를 어떻게 확인하고 
    //해당 스테이지마다 체력과 공격력을 다르게 할것인지 조금만 고민해보자 
    protected override void HP_m()
    {
        ID_m = "M_D02";
        HP = maxHP;
        maxHP = 30.0f;

    }

    protected override void DIe_m()
    {
        if (HP > 0)
        {
            Dead = true;
        }
    }
    protected override void Attack_m()
    {
        Attack = 10.0f;
        AtkTime = 3;
    }


   /* private float sign = -1.0f;
    private bool iswalk = true;*/
    protected override void move_m()
    {
      /*  movespeed = 3.0f;
        if (Time.time >= 0 && iswalk)
        {
            transform.position += new Vector3(movespeed * Time.deltaTime * sign, 0, 0);
        }*/

        //이전코드

        /* private void OnCollisionEnter2D(Collision2D collision)
         {
             if (collision.gameObject.CompareTag("Player"))
             {
                 Debug.Log("플레이어와 충돌 ");
                 iswalk = false;
                 rb.velocity = Vector2.zero;
                 //플레이어태그와 닿았을때 몬스터 속도 제로로 변경 

             }

         }*/

    }
    private void Awake()
    {
        HP_m();
        Attack_m();
    }
}
