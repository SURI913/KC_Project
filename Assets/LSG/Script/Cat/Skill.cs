using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Skill : Projectile
{
    public GameObject bulletSet;    // 총알 오브젝트 입력
    public Transform bulletPos;
    private string IDset = "Skill";

    float Cooltime;

    void initData()
    {
        grandParent = this.transform.parent.gameObject;
        grandParentIAttack = grandParent.GetComponent<IAttack>(); //캐릭터에다가 넣어줘야하는 스크립트
        Cooltime = grandParentIAttack.skillTime;  //쿨타임 초기화
        bullet = bulletSet; //총알 오브젝트 초기화
        fireTransform = bulletPos;
        ID = IDset;
    }

    private void Awake()
    {
        initData();
        grandParentIAttack.ativeSkill = true;
    }

    protected override void OnBullet() //스킬이면 오버라이드
    {
        IDamageable hitDamage = target.collider.GetComponent<IDamageable>();
        if (hitDamage != null)
        {
            Debug.Log(target.collider.name);
            Debug.Log(grandParent.transform.GetComponent<Cat>().ID+"가 스킬 사용 중");
            hitDamage.OnDamage(grandParentIAttack.OnSkill(target), target);
            Debug.Log(grandParent.transform.tag + "가 " + grandParentIAttack.OnAttack(target) + "만큼의 데미지를 입혔습니다");
            //hit된 오브젝트에 자식 Attack값만큼 데미지입힘
            Destroy(newBullet, 2f);   //2초 뒤 파괴
        }
    }
    protected void Update()
    {
        StateCheck();
        
        if (Cooltime <= 0)
        {
            //Debug.Log("스킬 생성 가능");
            state = State.Ready;    //쿨타임 종료
            Cooltime = grandParentIAttack.skillTime;
        }
        else
        {
            grandParentIAttack.ativeSkill = false;
            Cooltime -= Time.deltaTime;
        }
    }
}
