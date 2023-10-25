using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Skill : Projectile
{
    public GameObject bulletSet;    // �Ѿ� ������Ʈ �Է�
    public Transform bulletPos;
    private string IDset = "Skill";

    float Cooltime;

    void initData()
    {
        GrandParent = this.transform.parent.gameObject;
        GrandParentIAttack = GrandParent.GetComponent<IAttack>(); //ĳ���Ϳ��ٰ� �־�����ϴ� ��ũ��Ʈ
        Cooltime = GrandParentIAttack.skillTime;  //��Ÿ�� �ʱ�ȭ
        bullet = bulletSet; //�Ѿ� ������Ʈ �ʱ�ȭ
        fireTransform = bulletPos;
        ID = IDset;
    }

    private void Awake()
    {
        initData();
        GrandParentIAttack.ativeSkill = true;
    }

    protected override void OnBullet() //��ų�̸� �������̵�
    {
        IDamageable hitDamage = target.collider.GetComponent<IDamageable>();
        if (hitDamage != null)
        {
            Debug.Log(target.collider.name);
            Debug.Log(GrandParent.transform.GetComponent<Cat>().ID+"�� ��ų ��� ��");
            hitDamage.OnDamage(GrandParentIAttack.OnSkill(target), target);
            Debug.Log(GrandParent.transform.tag + "�� " + GrandParentIAttack.OnAttack(target) + "��ŭ�� �������� �������ϴ�");
            //hit�� ������Ʈ�� �ڽ� Attack����ŭ ����������
            Destroy(newBullet, 2f);   //2�� �� �ı�
        }
    }
    protected void Update()
    {
        StateCheck();
        
        if (Cooltime <= 0)
        {
            //Debug.Log("��ų ���� ����");
            state = State.Ready;    //��Ÿ�� ����
            Cooltime = GrandParentIAttack.skillTime;
        }
        else
        {
            GrandParentIAttack.ativeSkill = false;
            Cooltime -= Time.deltaTime;
        }
    }
}
