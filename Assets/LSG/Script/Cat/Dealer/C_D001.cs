using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

public class C_D001 : Cat, IAttack
{
    double skillEft= 2.0;
    public float speed { get; set; }
    public float atkTime { get; set; }
    public float skillTime { get; set; }

    //캐릭터 값 초기화
    //DB에서 끌어옴
    //레벨업 할때마다 저장 호출 + 값 다시 가져오기
    private void InitData()
    {
        //첫 데이터 보내기
        ID = "C_D001";
        maxHp = 1500;
        hp = maxHp;
        attack = 15;
        Lv = 1;
        speed = 5;    //임의
        skillTime = 5f;
        atkTime = 2f;
    }

    private void Awake()
    {
        //데이터가 없으면
        InitData();
        printData();    //check
        //데이터가 있으면
    }

    public double OnSkill(double Damage, RaycastHit2D hit)
    {
        double skillDamage = attackApply() * (double)skillEft;
        if (hit.collider.CompareTag("Boss")) //보스 공격의 경우
        {
            skillDamage += bossAttack;
        }
        //공격력의 2배인 발사포 투척
        Debug.Log(ID+"skillDamage: "+Unit.ToUnitString(skillDamage));
        return skillDamage;
    }

    public double OnAttack(RaycastHit2D hit) //공격 체크
    {
        if (hit.collider.CompareTag("Boss")) //보스 공격의 경우
        {
            return attackApply() + bossAttack;
        }
        return attackApply();
    }

    public void levelUP()
    {
        levelUP();
    }

}
