using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllUnit;

public class C_S001 : Cat, IAttack
{
    //public string name { get;, private set; }
    float skillEft = 0.05f; //힐 들어가는 퍼센트
    public float speed { get; set; } //공격 속도
    public float atkTime { get; set; } //일반공격 쿨타임
    public float skillTime { get; set; } //스킬 공격 쿨타임
    public bool ativeSkill { get; set; }   //스킬 활성화 시 공격 멈춤

    //스킬을 위한 함수 일정범위 내의 영웅들을 골라냄
    List<Cat> catsHealing = new List<Cat>();
    [SerializeField] GrowingData growingdata;

    //캐릭터 값 초기화
    //DB에서 끌어옴     
    //레벨업 할때마다 저장 호출 + 값 다시 가져오기
    private void InitData() //대신에 생성자로 값 넣는방식으로 변경해 볼 것
    {
        // 활성화 된 고양이 Cat클래스에 첫 데이터 보내기
        ID = "C_S001";
        Lv = 1;

        xhp = 2f;
        hpIncrease = 0.1f;
        maxHp = growingdata.Hp*xhp;
        hp = maxHp;

        xattack = 1.5f;
        attackIncrease = 0.1f;

        speed = 20f;    //임의
        skillTime = 10f;
        atkTime = 2f;

        growingData = growingdata;
        Debug.Log(ID + "growingData 저장 완료");

        catMotion = GetComponentInChildren<Animator>();


        Vector2 skillpos = this.transform.position;
        //스킬특기
        Collider2D[] cats = Physics2D.OverlapCircleAll(skillpos, 1000.0f);
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
        //데이터가 있으면
    }

    public double OnSkill(RaycastHit2D hit)
    {
        catMotion.SetTrigger("AttackAnim");
        Debug.Log( "힐링스킬 발동");
        Debug.Log(catsHealing.Count);
        //요새 범위 안에있는 사람한테만 힐이 들어감
        foreach (var Cats in catsHealing)
        {
            Cats.hp += Cats.maxHp * skillEft;
            Debug.Log(Cats.ID+ "힐링");
            if(hp > maxHp) //maxHp를 넘지않게 처리
            {
                hp = maxHp;
            }
        }
        return 0;
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
