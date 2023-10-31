using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

/*������ �Ŵ������� ��Ʈ�� �����͸� �޾Ұ� 
    ���� ������ �ȿ� ����Ʈ�� �����͸� �����ߴ�.
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
   
   

    private void FixedUpdate()
    {
        move_m();
    }
    protected override void move_m()
    {
       
        
        
       

    }
  

}
