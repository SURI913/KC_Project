using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

public class StatsUp : MonoBehaviour, IAttackDamage
{
    public void think()
    {
        //��������  �� ����,��� ������ ����Ʈ �ʿ� ?
        //��Į�� ��õ�� ���� 
        // 2���� �迭�� �������� �̾Ƴ�? �ε�����.. ���Ƿ� �� �����ؾ���?
        //����üũ�ؾ���
        //���� ����,������ �ٽ� �����
        /*for (int i = 0; i < 2; i++)
        {
            //Instantiate(Monster, transform.position, Quaternion.identity);
        }*/
    }

    public string M_ID { get; set; } //���� �ѹ�
    public float M_HP { get; set; } //ü��
    public float M_MaxHP { get; set; } //�ִ�ü��
    //��õ ���� ������ /10
    public float M_Attack; //���ݷ�
                         

   //virtual �����Լ� ������
    public virtual void GetDamage()
    {
        //��õ ��� ������/100
    }
    public virtual void GetHP()
    {
        //��õ ���� ������ /10
    }
}
