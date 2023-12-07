using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

public class C_T001 : Cat, IAttack
{
    float skillEft = 5.0f;
    public float speed { get; set; } //공격 속도
    public float atkTime { get; set; } //공격 쿨타임
    public float skillTime { get; set; } //스킬 쿨타임
    public bool ativeSkill { get; set; }   //공격 활성화

    //스폰 값 필요

    [SerializeField] GrowingData growingdata;

    [SerializeField] GameObject attackEffect;


    private void InitData()
    {

        //ù ������ ������
        ID = "C_T001";
        Lv = 1;

        xhp = 3f;
        hpIncrease = 0.3f;
        maxHp = growingdata.Hp * xhp;
        hp = maxHp;

        xattack = 0.7f;
        attackIncrease = 0.05f;

        speed = 0f;    //����
        skillTime = 5f;
        atkTime = 2f;

        growingData = growingdata;
        Debug.Log(ID+"growingData 적용완료");

        catMotion = GetComponentInChildren<Animator>();


    }

    private void Awake()
    {
        //초기화
        InitData();

        playerRb = GetComponent<Rigidbody2D>();
    }

    public double OnSkill(RaycastHit2D hit)
    {
        //스킬 사용
        StartCoroutine(Skill());
        return 0;
    }

    public double OnAttack(RaycastHit2D hit) //공격값 계산
    {
        catMotion.SetTrigger("isAttack");

        if (hit.collider.CompareTag("boss")) //보스라면
        {
            return attackApply() + bossAttack;
        }
        return attackApply();
    }

    //공격 효과 및 적용

    private GameObject targetEvent;
    IEnumerator AttackEft(Collision2D collision)
    {
        isAttack = true;
        catMotion.SetTrigger("isAttack");
        Destroy(Instantiate(attackEffect, transform.position + new Vector3(2f, 0, 0), Quaternion.identity), atkTime);
        collision.collider.GetComponent<IDamageable>().OnDamage(OnAttack(target), target); //데미지 주는 스크립트
        yield return new WaitForSeconds(atkTime);
        isAttack = false;

    }

    IEnumerator Skill()
    {
        catMotion.SetTrigger("isSkill");

        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(skillEft); 
        GetComponent<Collider2D>().enabled = true;
        Debug.Log(ID+"스킬 사용중");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        

        if (collision.gameObject.layer == 6) //타겟레이어의 경우
        {
            Debug.Log(collision.collider.name);
            //데미지 스크립트 확인시 공격 시작
            if (collision.collider.GetComponent<IDamageable>() != null && !isAttack)
            {
                StartCoroutine(AttackEft(collision));
                playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    private bool isAttack = false;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.GetMask("Target"))
        {
            Debug.Log("탱커 공격 대기중");
            isLookTarget = false;
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //이전위치로 돌아가게 수정해야함
        }

    }

    //캐릭터 움직임을 위한 변수
    private Rigidbody2D playerRb;
    private float playerMoveSpeed =7f;
    private Vector2 vel = Vector2.zero;
    

    private void FixedUpdate()
    {
        Move();
    }

    RaycastHit2D target;
    private bool isLookTarget = false;
    private void OnDrawGizmos()
    {
        if (target)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + new Vector3(target.point.x, 0, 0), transform.lossyScale * 20);

        }
    }
    private void Move()
    {
        //레이캐스트로 타겟 체크 후 움직임
        if(!isLookTarget && !isAttack) {
            //타겟 방향으로 이동을 시키나
            target = Physics2D.BoxCast(gameObject.transform.position, transform.lossyScale * 20, 0f, Vector2.right, 0f, LayerMask.GetMask("Target"));
            //target = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 1f, layerMask);
            if (target && !target.collider.GetComponent<Enemy_004>())//위 원거리 딜러
            {
                isLookTarget = true;
            }
            else
            {
                isLookTarget = false;

            }

        }
        if(target)
        {
            //물리로 움직이는 방향 변경
            float delta = Mathf.SmoothDamp(gameObject.transform.position.x, target.transform.position.x, ref vel.x, playerMoveSpeed);
            transform.position = new Vector2(delta, transform.position.y);
        }

        /*myAnim.SetFloat("MoveX", playerRb.velocity.x); //나중에 맞춰서 수정
        myAnim.SetFloat("MoveY", playerRb.velocity.y);*/
    }
}
