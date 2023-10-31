using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

/*������ �Ŵ������� ��Ʈ�� �����͸� �޾Ұ� 
    ���� ������ �ȿ� ����Ʈ�� �޾ƿ� �����͸� �����ߴ�.
    �̸� M_D01���� ���� �ʱ�ȭ �����ְ� 
    �������� ��ư���� ��ư�� �ε����� �� 
    �ε����� ��Ʈ�� �� ������ ���� ���� �߰�
    ���� ��� �ε��� ���� 3�ϰ�� 3���� �����͸� �����´�.
*/
public class M_D01 : Monster
{
    public MonsterData monsterData; // �ν����Ϳ��� �Ҵ� �� ��ũ���ͺ� ���� ������ �ֱ� 


    private void Awake()//�𸣰���, �ٽ� Ȯ������ ��
    {
       // monsterData ������ null�� �ƴϰ� monsterData.monsterdatas �迭�� ���̰� 1 �̻��� ��쿡�� ����
        if (monsterData != null && monsterData.monsterdatas.Length > 0)
        {
            // ���� ���, ù ��° ���� �����͸� ��������
            MonsterD monsdata = monsterData.monsterdatas[1];
            ID_m = monsdata.stageID;
            HP = monsdata.hp;
            Attack = monsdata.attack;
            AtkTime = monsdata.atktime;      
        }
        else
        {
            Debug.LogError("Monster data is not assigned or empty.");
            //�� �ڵ尡 �����ϸ� ������ ���
        }
       
    }

    

    private float movespeed = 2.0f;
    private bool isAttack = false;
    private float distance = 5.0f;
    private LayerMask Player;

    private void FixedUpdate()
    {
        if (!isAttack)  
        { 
            move_m(); 
        }
    }
    private void move_m()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.left, distance, Player);

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
            Vector2 movement = new Vector2(-movespeed, 0); // ���� ������ x ������ �����Ͽ� �������� �̵��մϴ�.
            GetComponent<Rigidbody2D>().velocity = movement;
        }



    }
    protected override void OnAttack_m(RaycastHit2D hit)
    {
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            isAttack = true;
            Debug.Log("�÷��̾�� ����");
            
        }
    }
   


    protected override void OnDamage_m(float damage, RaycastHit2D hit)
    {
      if(!Dead)
        {
            HP -= damage;
        }
      else if(HP<=0)
        {
            DIe_m();
        }
    }


}
