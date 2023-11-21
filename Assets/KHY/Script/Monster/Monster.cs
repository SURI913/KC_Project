using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{


    public string stageID { get; set; } //��
    public double HP { get; set; } //ü��
    public double maxHP { get; set; } //�ִ�ü��

    public bool Dead = false;
    public double Attack { get; set; } //���ݷ�
    public int AtkTime { get; set; } //������Ÿ��

    StageButton s;
   
    public MonsterData monsterData; // �ν����Ϳ��� �Ҵ� �� ��ũ���ͺ� ���� ������ �ֱ� 

    /* ������ �Ŵ������� ��Ʈ�� �����͸� �޾Ұ�
     ���� ������ �ȿ� ����Ʈ�� �޾ƿ� �����͸� �����ߴ�.
     �̸� M_D01���� ���� �ʱ�ȭ �����ְ�
     �������� ��ư���� ��ư�� �ε����� ��
     �ε����� ��Ʈ�� �� ������ ���� ���� �߰�
     ���� ��� �ε��� ���� 3�ϰ�� 3���� �����͸� �����´�.*/

    public void Awake()
    {

      /*  MonsterD monster = monsterData.monsterdatas[0];
        stageID = monster.stageID;
        HP = monster.hp;
        Attack = monster.attack;
        AtkTime = monster.atktime;*/

    }

    public virtual void SetMonsterData(MonsterD monsdata) {
        if (monsdata != null)
        {
            stageID = monsdata.stageID;
            HP = monsdata.hp;
            Attack = monsdata.attack;
            AtkTime = monsdata.atktime;
            Debug.Log("SetMonsterData: " + "StageID: " + stageID + "" +
                ", HP: " + HP + ", Attack: " + Attack + ", AtkTime: " + AtkTime);
            //���⼱ �����Ͱ� �´� !
        }
        else
        {
            Debug.Log("�����Ͱ� ���޵�������");
        }
    }
    

   

    public virtual void OnDamage(double Damage, RaycastHit2D hit) 
    {

        HP -= Damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
            Debug.Log("���� ���� óġ");
        }
    }
    // ���Ϳ��� �������� ������ �Լ�{}

    /* protected virtual void HP_m()
     {
         maxHP = maxHP;
         HP = HP;
     }
     protected virtual void DIe_m()
     {
         Dead = true;
         Destroy(gameObject);
         //���� �״� �ִϸ��̼� �߰� 
     }

     protected virtual void OnAttack_m(RaycastHit2D hit)
     {
         Attack = Attack;
         AtkTime = AtkTime;
     }

     protected virtual void OnDamage(float damage, RaycastHit2D hit)
     {
         HP -= damage;
     }*/



    /*protected virtual void move_m()
    {

        movespeed = movespeed;
    }*/
}
