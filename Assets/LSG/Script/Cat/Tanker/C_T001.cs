using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllUnit;
using System.Globalization;
using TMPro;

public class C_T001 : Cat, IAttack
{
    float skillEft = 5.0f;
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
        ID = "C_T001";
        Lv = 1;

        xhp = 3f;
        hpIncrease = 0.3f;
        maxHp = growingdata.Hp * xhp;
        hp = maxHp;

        xattack = 0.7f;
        attackIncrease = 0.05f;

        speed = 0f;    //임의
        skillTime = 5f;
        atkTime = 2f;

        growingData = growingdata;
        Debug.Log(ID+"growingData 저장 완료");


    }

    private void Awake()
    {
        //데이터가 없으면
        InitData();
        //데이터가 있으면

        //이동 을 위한 선언
        playerRb = GetComponent<Rigidbody2D>();
        //myAnim = GetComponent<Animator>();
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

    IEnumerator Skill()
    {
        this.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(skillEft);
    }

    private bool isAttack;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isAttack)
        {
            isAttack = true;
            //다시 작업 필요 탱커용 스크립트 수정 필요
            IDamageable damageable = collision.collider.GetComponent<IDamageable>();
            //적과 부딪치는 동안 적의 피를 깎음
            if (damageable != null)
            {
                RaycastHit2D EnemyRayHit = collision.collider.GetComponent<RaycastHit2D>();
                collision.collider.GetComponent<IDamageable>().OnDamage(OnAttack(EnemyRayHit), EnemyRayHit);
            }
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isAttack = false;
    }

    //캐릭터 이동 관련 변수
    private Rigidbody2D playerRb;
    //private Animator myAnim;
    private float playerMoveSpeed =1.0f;
    private Transform targetPosition;
    private Vector2 vel = Vector2.zero;
    

    private void FixedUpdate()
    {
        if (!isAttack) { Move(); }
    }
    private void Move()
    {
        //한 캐릭터 공격중이면 더이상 움직임 x
        int layerMask = 1 << LayerMask.NameToLayer("Target");
        RaycastHit2D target = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 5f, layerMask);

       if(target)
        {
            //타겟이 잡히는 경우 타겟쪽으로 이동
            float delta = Mathf.SmoothDamp(gameObject.transform.position.x, target.transform.position.x, ref vel.x, playerMoveSpeed);
            this.transform.position = new Vector2(delta, this.transform.position.y);
        }
        
        /*myAnim.SetFloat("MoveX", playerRb.velocity.x);
        myAnim.SetFloat("MoveY", playerRb.velocity.y);*/
    }
}
