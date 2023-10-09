using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Projectile
{
    public GameObject bulletSet;    // 총알 오브젝트 입력
    public Transform bulletPos;
    private string IDset = "Skill";

    float Cooltime;

    void initData()
    {
        GrandParent = this.transform.parent.gameObject;
        GrandParentIAttack = GrandParent.GetComponent<IAttack>(); //캐릭터에다가 넣어줘야하는 스크립트
        Cooltime = GrandParentIAttack.skillTime;  //쿨타임 초기화
        bullet = bulletSet; //총알 오브젝트 초기화
        fireTransform = bulletPos;
        ID = IDset;
    }

    private void Awake()
    {
        initData();
        GrandParentIAttack.AtiveSkill = true;
    }

    protected override void OnBullet() //스킬이면 오버라이드
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.right, 1);
        IDamageable hitDamage = hit.collider.GetComponent<IDamageable>();
        if (hitDamage != null)
        {
            Debug.Log(hit.collider.name);
            hit.collider.GetComponent<IDamageable>().OnDamage(GrandParentIAttack.OnSkill(hit), hit);
            //hit된 오브젝트에 자식 Attack값만큼 데미지입힘
            Destroy(newBullet);   //다 파괴됨
        }
    }
    protected void Update()
    {
        StateCheck();
        
        if (Cooltime <= 0)
        {
            //Debug.Log("스킬 생성 가능");
            state = State.Ready;    //쿨타임 종료
            Cooltime = GrandParentIAttack.skillTime;
        }
        else
        {
            GrandParentIAttack.AtiveSkill = false;
            Cooltime -= Time.deltaTime;
        }
    }
}
