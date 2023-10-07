using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

public class C_T001 : Cat, IAttack
{
    float skillEft = 5.0f;
    public float speed { get; set; } //공격 속도
    public float atkTime { get; set; } //일반공격 쿨타임
    public float skillTime { get; set; } //스킬 공격 쿨타임
    public bool AtiveSkill { get; set; }   //스킬 활성화 시 공격 멈춤

    //캐릭터 값 초기화
    //DB에서 끌어옴
    //레벨업 할때마다 저장 호출 + 값 다시 가져오기
    private void InitData()
    {
        //첫 데이터 보내기
        ID = "C_D001";
        maxHp = 3000;
        hp = maxHp;
        attack = 10;
        Lv = 1;
        speed = 15f;    //임의
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

    public double OnSkill(RaycastHit2D hit)
    {
        //5초동안 받는 피해량 0
        StartCoroutine(Skill());
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

    IEnumerator Skill()
    {
        this.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(skillEft);
    }
}
