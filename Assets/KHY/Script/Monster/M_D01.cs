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
            Debug.Log("��������-" + stageID);
            Debug.Log("ü��-" + HP);
            Debug.Log("���ݷ�-" + Attack);
            Debug.Log("���ݼӵ�=" + AtkTime);
        }
        else
        {
            Debug.Log("Monster���� ������ ���� �ȵ�");
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
            // ����ĳ��Ʈ�� �÷��̾ �浹 �����ϸ� ���Ͱ� ����
            isAttack = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
           
        }
        else
        {
            // ����ĳ��Ʈ�� �÷��̾ �������� ������ ������ �̵� ���·� ����
            isAttack = false;
            // �ִϸ��̼�?
           *//* Vector2 movement = new Vector2(-movespeed, 0); // ���� ������ x ������ �����Ͽ� �������� �̵��մϴ�.
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
                Debug.Log("�÷��̾�� ����");

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

                //�����Լ�, ������Ʈ ��Ʈ���� 
            }
        }

    }*/



