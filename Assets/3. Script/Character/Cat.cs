using System.Collections;
using UnityEngine;
using AllUnit;
using DamageNumbersPro.Demo;
using DamageNumbersPro;
using System;

public class Cat : BattleUnit
{
    protected int level = 0;
    //일시적으로 제한
    private float cooltime = 10f;
    private bool ativelevelup = true;
    private GameObject targetObject;

    bool is_passive_skill = false;
    protected float skill_effect;

    private AttackComponent attackComponent;
    private SkillComponent skillComponent;
    private RecoveryComponent recoveryComponent;

    //------------------------------------------------------초기값 저장
    public BaseCatData cat_data { get; set; }
    public GrowingData growing_data { get; set; }
    //장비 멀로 처리하냐

    [SerializeField]
    private SkillData my_skill_data;
    //---------------------------------------------------------------Skill
    public float skill_distance { get; set; }

    public float speed { get; set; }   //공격 속도
    public float skill_time { get; set; }   //스킬 공격 쿨타임
    public bool is_ative_skill { get; set; } = false;   //스킬 활성화 시 공격 멈춤
 //--------------------------------------------------------------AttackableImp

    //캐릭터 움직임을 위한 변수
    protected Rigidbody2D player_rb;
    private float playerMoveSpeed = 7f;
    private Vector2 vel = Vector2.zero;
    protected bool is_attack = false;

    public float size;
    public LayerMask layer_mask;

    protected RaycastHit2D target;
    protected bool isLookTarget = false;
    public float atk_time
    {
        get { return cat_data._atk_time; }
        set {  }
    } //일반공격 쿨타임 값 초기화 가 안된다면 이렇게 구현
    public float atk_distance { get; set; } // 공격범위
    public bool is_parabola_skill { get; set; }
    public bool is_parabola_attack { get; set; }

    public Transform my_tool_pos;
    public Transform my_attack_transform { get { return my_tool_pos; } }
    private void Start()
    {
        attackComponent = GetComponent<AttackComponent>();
        skillComponent = GetComponent<SkillComponent>();
        //recoveryComponent = GetComponent<RecoveryComponent>();
    }
    protected void initAttackData()
    {
        speed = cat_data._attack_speed;
        atk_time = cat_data._atk_time;
        skill_time = cat_data._skl_time;
        skill_effect = cat_data._skl_effect;
    }

    public double GetAttackPower(){
        //일반 공격값 반환
        if (cat_data._attack_multipler == 0){
            Debug.Log(cat_data._id);
            Debug.Log("attack error!");
            return 0;
        }
        if(growing_data == null)
        {
            Debug.Log("공격 중 growingDataError!");

        }

        double AllAttack = growing_data.Attack*cat_data._attack_multipler;
        //Debug.Log(ID+(int)AllAttack);
        //영웅 공격력*공격력(보유효과)*성급효과*장비장착효과*패시브스킬*별자리
        //패시브 스킬은 어떻게 짤건지 고민 + 크리티컬 데미지 작업도 필요함
        //-----------------------------------------------------------------------------------------------------------애니메이션 추가
        return AllAttack;
    }


   /* public void LevelUP()
    {
        if(ativelevelup)
        {
            Debug.Log(cat_data._id + "레벨업!");
            if (growing_data == null)
            {
                Debug.Log("레벨업 중 growingData Error!");

            }
            level++;
            cat_data._attack_multipler += cat_data._increase_attack;
            cat_data._hp_multipler += cat_data._increase_hp;
            ativelevelup = false;
        }
    }*/

    /*private void Update()
    {
        //레벨업 제한하는 부분 수정해야함
        if (cooltime >= 0 && !ativelevelup)
        {
            cooltime -= Time.deltaTime;

        }
        else
        {
            ativelevelup = true;
            cooltime = 10f;
        }

    }*/


/*    public void Move()
    {
        //레이캐스트로 타겟 체크 후 움직임
        if (!isLookTarget && is_attack)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, size, layer_mask);
            Array.Sort(colliders, new DistanceComparer(transform));
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
        if (target)
        {
            //물리로 움직이는 방향 변경
            float delta = Mathf.SmoothDamp(gameObject.transform.position.x, target.transform.position.x, ref vel.x, playerMoveSpeed);
            transform.position = new Vector2(delta, transform.position.y);
        }
        *//*myAnim.SetFloat("MoveX", playerRb.velocity.x); //나중에 맞춰서 수정
        myAnim.SetFloat("MoveY", playerRb.velocity.y);*//*
    }*/

    public void PerformAttack()
    {
        if (attackComponent != null)
            attackComponent.Attack(targetObject);
    }

    public void PerformSkill()
    {
        if (skillComponent != null)
            skillComponent.UseSkill();
    }

    public void Recovery()
    {
        if (recoveryComponent != null)
            recoveryComponent.Recover();
    }
}