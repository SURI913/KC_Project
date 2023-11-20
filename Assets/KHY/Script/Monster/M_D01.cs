using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;


public class M_D01 : Monster
{
     Monster monster;
    public void Awake()
    {

        printData();
    }

    void printData()
    {
        if (!string.IsNullOrEmpty(stageID))
        {
            Debug.Log("스테이지-" + stageID);
            Debug.Log("체력-" + HP);
            Debug.Log("공격력-" + Attack);
            Debug.Log("공격속도=" + AtkTime);
        }
        else
        {
            Debug.Log("Monster에서 데이터 전달 안됨");
        }
    }
   

}
    //  private float movespeed = 2.0f;
    /*private bool isAttack = false;
    private float distance = 5.0f;
    private LayerMask LayerMask;
   
 

    private void FixedUpdate()
    {
        if (!isAttack)  
        { 
            move_m(); 
        }
    }


    private void move_m()
       
    {
        
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.left, distance, LayerMask);


        if (ray.collider != null && ray.collider.CompareTag("Player"))
        {
            // 레이캐스트가 플레이어를 충돌 감지하면 몬스터가 공격
            isAttack = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
           
        }
        else
        {
            // 레이캐스트가 플레이어를 감지하지 않으면 몬스터의 이동 상태로 유지
            isAttack = false;
            // 애니메이션?
           *//* Vector2 movement = new Vector2(-movespeed, 0); // 음수 값으로 x 성분을 설정하여 왼쪽으로 이동합니다.
            GetComponent<Rigidbody2D>().velocity = movement;*//*
        }



    }*/
  /*  protected override void OnAttack_m(RaycastHit2D hit)
    {
        if (isAttack)
        {

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                isAttack = true;
                Debug.Log("플레이어에게 공격");

            }

        }
    }*/




  /*  protected override void OnDamage(float damage, RaycastHit2D hit)
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.left, distance, LayerMask);

        if (ray.collider != null && ray.collider.CompareTag("Player"))
        {
            IDamageable target = ray.collider.GetComponent<IDamageable>();
            if (target != null&& HP > 0)
            {

            }
            else
            {
                DIe_m();

                //다이함수, 오브젝트 디스트로이 
            }
        }

    }*/



