using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;
using UnityEngine.UI;

public class C_D001 : Cat, IAttack
{
    float skillEft= 2.0f;
    public float speed { get; set; } //공격 속도
    public float atkTime { get; set; } //일반공격 쿨타임
    public float skillTime { get; set; } //스킬 공격 쿨타임
    public bool ativeSkill { get; set; }   //스킬 활성화 시 공격 멈춤

    [SerializeField] GrowingData growingdata;

    //캐릭터 값 초기화
    //DB에서 끌어옴
    //레벨업 할때마다 저장 호출 + 값 다시 가져오기
    private void InitData()
    {
        //첫 데이터 보내기
        ID = "C_D001";
        Lv = 1;

        xhp = 0.7f;
        hpIncrease = 0.08f;
        maxHp = growingdata.Hp * xhp;
        hp = maxHp;

        xattack = 1.8f;
        attackIncrease = 0.17f;

        speed = 15f;    //임의
        skillTime = 5f;
        atkTime = 2f;

        growingData = growingdata;
        Debug.Log(ID + "growingData 저장 완료");

        catMotion = GetComponentInChildren<Animator>();
    }

    private void Awake()
    {
        //데이터가 없으면
        InitData();
        //데이터가 있으면
    }

    public double OnSkill(RaycastHit2D hit)
    {
        double skillDamage = attackApply() * (double)skillEft;
        if (hit.collider.CompareTag("boss")) //보스 공격의 경우
        {
            skillDamage += bossAttack;
        }
        //공격력의 2배인 발사포 투척
        Debug.Log(ID+"skillDamage: "+Unit.ToUnitString(skillDamage));
        return skillDamage;
    }

    public double OnAttack(RaycastHit2D hit) //공격 체크
    {
        if (hit.collider.CompareTag("boss")) //보스 공격의 경우
        {
            return attackApply() + bossAttack;
        }
        return attackApply();
    }

}
