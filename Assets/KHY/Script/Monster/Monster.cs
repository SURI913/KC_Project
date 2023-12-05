using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{


    public string stageID { get; set; } //��
    public double HP { get; set; } //ü��
    public double maxHP { get; set; } //�ִ�ü��
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


   
    
    public void FixedUpdate()
    {
       
        Move();
        OnAttack();
    }

    public void SetMonsterDataByIndex(int index)
    {
        if (index >= 1 && index <= monsterData.monsterdatas.Length)
        {
            MonsterD stageData = monsterData.monsterdatas[index - 1];
            SetMonsterData(stageData);
        }
        else
        {
            Debug.LogError("Invalid stage index: " + index);
        }
    }
    public void SetMonsterData(MonsterD monsdata) {
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






    private float rayLen=10f;// ����ĳ��Ʈ�� ���� 
    private LayerMask layerMask; //���̾� �÷��̾� 
    private bool isatk = false; // �������� �ƴҶ�
   // private bool ismove = true;
    RaycastHit2D hit;
    private float moveSpeed = 4f;

    public void Move()
    {
        Debug.Log(" HP:" + HP);
        /*if (!isatk)
        {
            //hit�� ����Ǿ��ִ� Player ���̸���ũ �� null�� �ƴϸ� �̵�
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * Vector2.zero * Time.deltaTime);
        }*/

    }
    public void OnDamage(double Damage, RaycastHit2D hit)
    {
        Debug.Log(" ������HP:" + HP);
        HP -= Damage;
        Debug.Log("���Ͱ� ���ݹ޴´�  HP:" + HP);
        if (HP <= 0)
        {
            Destroy(gameObject);
            Debug.Log("���� ���� óġ");
        }
    }
    public void OnAttack()
    {
        // ã�� ���̾� ����
        layerMask = LayerMask.GetMask("Player");
        //���̸� ǥ���� ������
        Vector2 MonsterPosition = new Vector2(transform.position.x, transform.position.y + 2);
        //hit�� ����
        hit = Physics2D.Raycast(MonsterPosition, Vector2.left, rayLen, layerMask);
        //���� �� �༭ ǥ��
        Debug.DrawRay(MonsterPosition, Vector2.left * rayLen, Color.red);//

        if (hit.collider != null)
        {
            if(hit.collider.CompareTag("Player"))
            {
                isatk = true;
            }
        }
    }

        
}
 /*   public void SpawnMonster(Vector2 spawnPosition)
    {
        // ���͸� �����ϰ� ���� ��ġ�� �̵�
        Instantiate(gameObject, spawnPosition, Quaternion.identity);

        // ������ ���Ϳ� ���� �ʱ�ȭ
       // SetMonsterData(monsterData);
        // �߰����� �ʱ�ȭ �۾��� �ʿ��ϴٸ� ���⿡ �߰�
    }*/
    // ���Ϳ��� �������� ������ �Լ�{}

 

     



