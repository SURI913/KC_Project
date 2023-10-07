using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

public class C_S001 : Cat, IAttack
{
    float skillEft = 0.05f;
    public float speed { get; set; } //공격 속도
    public float atkTime { get; set; } //일반공격 쿨타임
    public float skillTime { get; set; } //스킬 공격 쿨타임
    public bool AtiveSkill { get; set; }   //스킬 활성화 시 공격 멈춤

    //스킬을 위한 함수 일정범위 내의 영웅들을 골라냄
    public Vector2 SkillPos;
    List<Cat> catsHealing = new List<Cat>();
    Collider[] cats;   

    //캐릭터 값 초기화
    //DB에서 끌어옴
    //레벨업 할때마다 저장 호출 + 값 다시 가져오기
    private void InitData()
    {
        // 활성화 된 고양이 Cat클래스에 첫 데이터 보내기
        ID = "C_D001";
        maxHp = 1500;
        hp = maxHp;
        attack = 10;
        Lv = 1;
        speed = 15f;    //임의
        skillTime = 10f;
        atkTime = 2f;

        //스킬특기
        cats = Physics.OverlapSphere(SkillPos, 100.0f);
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
        //데이터가 없으면
        InitData();
        printData();    //check
        //데이터가 있으면
    }

    public double OnSkill(RaycastHit2D hit)
    {
        //요새 범위 안에있는 사람한테만 힐이 들어감
        foreach (var Cats in catsHealing)
        {
            Cats.hp += Cats.maxHp * skillEft;
            if(hp > maxHp) //maxHp를 넘지않게 처리
            {
                hp = maxHp;
            }
        }
        return 0;
    }

    public double OnAttack(RaycastHit2D hit) //공격 체크
    {
        if (hit.collider.CompareTag("Respawn")) //보스 공격의 경우
        {
            return attackApply() + bossAttack;
        }
        return attackApply();
    }

    public void levelUP()
    {
        LevelUP();
    }
}
