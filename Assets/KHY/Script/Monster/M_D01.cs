using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

public class M_D01 : Monster
{
    

   /* private void Start()
    {
        HP_m();
    }
    protected override void HP_m()
    {
        ID_m = "M_D01";
        HP = maxHP;
      //  maxHP = data.HP;
       // Debug.Log("ü��" + Unit.ToUnitString(maxHP));



    }

    protected override void DIe_m()
    {
        if(HP >0)
        {
            Dead = true;
        }
    }
    protected override void Attack_m()
    {
        Attack = 10.0f;
        AtkTime = 3;
    }*/


    /*private float sign = -1.0f;
    private bool iswalk = true;*/

    private void FixedUpdate()
    {
        move_m();
    }
    protected override void move_m()
    {
       //�̵����� �ڵ� �ֱ�
       // �÷��̾�� �������� ���ߵ��� 
        /*// private float movespeed = 3.0f;
       
        if (Time.time >= 0 && iswalk)
        {
            transform.position += new Vector3(base.movespeed * Time.deltaTime * sign, 0, 0);
        }*/

        //�����ڵ�

        /* private void OnCollisionEnter2D(Collision2D collision)
         {
             if (collision.gameObject.CompareTag("Player"))
             {
                 Debug.Log("�÷��̾�� �浹 ");
                 iswalk = false;
                 rb.velocity = Vector2.zero;
                 //�÷��̾��±׿� ������� ���� �ӵ� ���η� ���� 

             }

         }*/

    }
   /* private void Awake()
    {
        HP_m();
        Attack_m();
    }*/

}
