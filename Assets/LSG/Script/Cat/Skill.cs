using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GrandParentIAttack.AtiveSkill = true;
    }

    protected override void OnBullet() //��ų�̸� �������̵�
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.right, 1);
        IDamageable hitDamage = hit.collider.GetComponent<IDamageable>();
        if (hitDamage != null)
        {
            Debug.Log(hit.collider.name);
            hit.collider.GetComponent<IDamageable>().OnDamage(GrandParentIAttack.OnSkill(hit), hit);
            //hit�� ������Ʈ�� �ڽ� Attack����ŭ ����������
            Destroy(newBullet);   //�� �ı���
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
            GrandParentIAttack.AtiveSkill = false;
            Cooltime -= Time.deltaTime;
        }
    }
}
