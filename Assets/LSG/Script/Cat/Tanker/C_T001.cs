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
        Destroy(Instantiate(attackEffect, collision.transform.position, Quaternion.identity), atkTime-1);
        collision.collider.GetComponent<IDamageable>().OnDamage(OnAttack(target), target); //데미지 주는 스크립트
        yield return new WaitForSeconds(atkTime);
        isAttack = false;

    }

    IEnumerator Skill()
    {// �ʱ⿡ �� �ٲ���� �ɷ� ����  �ʿ�
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(skillEft); //�Ͻ������� ������ ���� ����
        GetComponent<Collider2D>().enabled = true;
        Debug.Log("��Ŀ ��ų �����");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider == target.collider) //같은 콜라이더 일 경우
        {
            playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
            IDamageable damageable = collision.collider.GetComponent<IDamageable>();
            //데미지 스크립트 확인시 공격 시작
            if (damageable != null && !isAttack && !collision.collider.GetComponent<Enemy_04>()) //=>Target Layer
            {
                StartCoroutine(AttackEft(collision));
            }
        }
    }

    private bool isAttack = false;
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider == target.collider)
        {
            Debug.Log("탱커 공격 대기중");
            isLookTarget = false;
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; //이전위치로 돌아가게 수정해야함
        }

    }

    //캐릭터 움직임을 위한 변수
    private Rigidbody2D playerRb;
    private float playerMoveSpeed =1.0f;
    private Vector2 vel = Vector2.zero;
    

    private void FixedUpdate()
    {
        //catMotion.SetBool("isLookTarget", !isAttack);
        catMotion.SetBool("isLookTarget", isLookTarget);

        Move();
    }

    RaycastHit2D target;
    private bool isLookTarget = false;
    private void Move()
    {
        //레이캐스트로 타겟 체크 후 움직임
        if(!isLookTarget && !isAttack) {
            int layerMask = 1 << LayerMask.NameToLayer("Target");
            target = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 5f, layerMask);
        }
        if (target)
        {
            float delta = Mathf.SmoothDamp(gameObject.transform.position.x, target.transform.position.x, ref vel.x, playerMoveSpeed);
            transform.position = new Vector2(delta, transform.position.y);
        }
        
        /*myAnim.SetFloat("MoveX", playerRb.velocity.x); //나중에 맞춰서 수정
        myAnim.SetFloat("MoveY", playerRb.velocity.y);*/
    }
}
