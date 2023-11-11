using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllUnit;

public class C_S001 : Cat, IAttack
{
    //public string name { get;, private set; }
    float skillEft = 0.05f; //�� ���� �ۼ�Ʈ
    public float speed { get; set; } //���� �ӵ�
    public float atkTime { get; set; } //�Ϲݰ��� ��Ÿ��
    public float skillTime { get; set; } //��ų ���� ��Ÿ��
    public bool ativeSkill { get; set; }   //��ų Ȱ��ȭ �� ���� ����

    //��ų�� ���� �Լ� �������� ���� �������� ���
    List<Cat> catsHealing = new List<Cat>();
    [SerializeField] GrowingData growingdata;

    //ĳ���� �� �ʱ�ȭ
    //DB���� �����     
    //������ �Ҷ����� ���� ȣ�� + �� �ٽ� ��������
    private void InitData() //��ſ� �����ڷ� �� �ִ¹������ ������ �� ��
    {
        // Ȱ��ȭ �� ����� CatŬ������ ù ������ ������
        ID = "C_S001";
        Lv = 1;

        xhp = 2f;
        hpIncrease = 0.1f;
        maxHp = growingdata.Hp*xhp;
        hp = maxHp;

        xattack = 1.5f;
        attackIncrease = 0.1f;

        speed = 20f;    //����
        skillTime = 10f;
        atkTime = 2f;

        growingData = growingdata;
        Debug.Log(ID + "growingData ���� �Ϸ�");

        catMotion = GetComponentInChildren<Animator>();


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
        catMotion.SetTrigger("AttackAnim");
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
        if (hit.collider.CompareTag("boss")) //���� ������ ���
        {
            return attackApply() + bossAttack;
        }
        return attackApply();
    }

}
