using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Projectile
{
    public GameObject bulletSet;    // �Ѿ� ������Ʈ �Է�
    public Transform bulletPos;

    float Cooltime;
    private string IDset = "Atk";


    void initData() 
    {
        grandParent = this.transform.parent.gameObject;
        //�ڽ� Awake ���� ����
        grandParentIAttack = grandParent.GetComponent<IAttack>(); //ĳ���Ϳ��ٰ� �־�����ϴ� ��ũ��Ʈ
        Cooltime = grandParentIAttack.atkTime;  //��Ÿ�� �ʱ�ȭ
        bullet = bulletSet; //�Ѿ� ������Ʈ �ʱ�ȭ
        fireTransform = bulletPos;
        ID = IDset;
 
    }

    private void Awake()
    {
        initData();
    }
    protected void Update()
    {
        //�� �κе� ���� �ʿ�
        if (grandParentIAttack.ativeSkill == true)   //��ų ��� ���¸� �Ͻ������� ����
        {
                state = State.Reloading;
        }
        StateCheck();
        if (Cooltime <= 0)
        {
            state = State.Ready;    //��Ÿ�� ����
            Cooltime = grandParentIAttack.atkTime;
        }
        else
        {
            Cooltime -= Time.deltaTime;
        }
    }
}
