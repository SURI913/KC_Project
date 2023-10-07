using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

public class C_T001 : Cat, IAttack
{
    float skillEft = 5.0f;
    public float speed { get; set; } //���� �ӵ�
    public float atkTime { get; set; } //�Ϲݰ��� ��Ÿ��
    public float skillTime { get; set; } //��ų ���� ��Ÿ��
    public bool AtiveSkill { get; set; }   //��ų Ȱ��ȭ �� ���� ����

    //ĳ���� �� �ʱ�ȭ
    //DB���� �����
    //������ �Ҷ����� ���� ȣ�� + �� �ٽ� ��������
    private void InitData()
    {
        //ù ������ ������
        ID = "C_D001";
        maxHp = 3000;
        hp = maxHp;
        attack = 10;
        Lv = 1;
        speed = 15f;    //����
        skillTime = 5f;
        atkTime = 2f;
    }

    private void Awake()
    {
        //�����Ͱ� ������
        InitData();
        printData();    //check
        //�����Ͱ� ������
    }

    public double OnSkill(RaycastHit2D hit)
    {
        //5�ʵ��� �޴� ���ط� 0
        StartCoroutine(Skill());
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

    IEnumerator Skill()
    {
        this.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(skillEft);
    }
}
