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
        grandParent = this.transform.parent.gameObject;
        grandParentIAttack = grandParent.GetComponent<IAttack>(); //ĳ���Ϳ��ٰ� �־�����ϴ� ��ũ��Ʈ
        Cooltime = grandParentIAttack.skillTime;  //��Ÿ�� �ʱ�ȭ
        bullet = bulletSet; //�Ѿ� ������Ʈ �ʱ�ȭ
        fireTransform = bulletPos;
        ID = IDset;
    }

    private void Awake()
    {
        initData();
        grandParentIAttack.ativeSkill = true;
    }

    protected override void OnBullet() //��ų�̸� �������̵�
    {
        IDamageable hitDamage = target.collider.GetComponent<IDamageable>();
        if (hitDamage != null)
        {
            Debug.Log(target.collider.name);
            Debug.Log(grandParent.transform.GetComponent<Cat>().ID+"�� ��ų ��� ��");
            hitDamage.OnDamage(grandParentIAttack.OnSkill(target), target);
            Debug.Log(grandParent.transform.tag + "�� " + grandParentIAttack.OnAttack(target) + "��ŭ�� �������� �������ϴ�");
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
            Cooltime = grandParentIAttack.skillTime;
        }
        else
        {
            grandParentIAttack.ativeSkill = false;
            Cooltime -= Time.deltaTime;
        }
    }
}
