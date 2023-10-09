using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

public class C_S001 : Cat, IAttack
{
    float skillEft = 0.05f;
    public float speed { get; set; } //���� �ӵ�
    public float atkTime { get; set; } //�Ϲݰ��� ��Ÿ��
    public float skillTime { get; set; } //��ų ���� ��Ÿ��
    public bool AtiveSkill { get; set; }   //��ų Ȱ��ȭ �� ���� ����

    //��ų�� ���� �Լ� �������� ���� �������� ���
    List<Cat> catsHealing = new List<Cat>();

    //ĳ���� �� �ʱ�ȭ
    //DB���� �����     
    //������ �Ҷ����� ���� ȣ�� + �� �ٽ� ��������
    private void InitData()
    {
        // Ȱ��ȭ �� ����� CatŬ������ ù ������ ������
        ID = "C_S001";
        maxHp = 1500;
        hp = maxHp;
        attack = 10;
        Lv = 1;
        speed = 20f;    //����
        skillTime = 10f;
        atkTime = 2f;

        Vector2 skillpos = this.transform.position;
        //��ųƯ��
        Collider2D[] cats = Physics2D.OverlapCircleAll(skillpos, 1000.0f);
        foreach (var Cats in cats)
        {
            if (Cats.CompareTag("Player"))
            {
                catsHealing.Add(Cats.GetComponent<Cat>());
            }
        }

    }

    private void Awake()
    {
        //�����Ͱ� ������
        InitData();
        //�����Ͱ� ������
    }

    public double OnSkill(RaycastHit2D hit)
    {
        Debug.Log( "������ų �ߵ�");
        Debug.Log(catsHealing.Count);
        //��� ���� �ȿ��ִ� ������׸� ���� ��
        foreach (var Cats in catsHealing)
        {
            Cats.hp += Cats.maxHp * skillEft;
            Debug.Log(Cats.ID+ "����");
            if(hp > maxHp) //maxHp�� �����ʰ� ó��
            {
                hp = maxHp;
            }
        }
        return 0;
    }

    public double OnAttack(RaycastHit2D hit) //���� üũ
    {
        if (hit.collider.CompareTag("Respawn")) //���� ������ ���
        {
            return attackApply() + bossAttack;
        }
        return attackApply();
    }

    public void levelUP()
    {
        LevelUP();
    }
}
