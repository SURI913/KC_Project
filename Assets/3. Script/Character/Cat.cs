using System.Collections;
using UnityEngine;
using AllUnit;
using DamageNumbersPro.Demo;
using DamageNumbersPro;
using System;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;

public class Cat : BattleUnit
{
    public float attackCooltime = 2f;
    private float attackTime = 0f;
    
    public GameObject targetObject;
    private bool isActiveSkill;

    private CharacterDataBase characterData; //현재 캐릭터 위치에 들어가는 캐릭터 정보

    [SerializeField] private AttackComponent attackComponent;
    [SerializeField] private SkillBehaviourSO skillBehaviour;
    private RecoveryComponent recoveryComponent;


    //캐릭터 움직임을 위한 변수
    protected Rigidbody2D player_rb;
    private float playerMoveSpeed = 7f;
    private Vector2 vel = Vector2.zero;
    protected bool is_attack = false;

    public float size;
    public LayerMask layer_mask;

    protected RaycastHit2D target;
    protected bool isLookTarget = false;

    new private void Awake()
    {
        //recoveryComponent = GetComponent<RecoveryComponent>();
        if (targetObject == null) targetObject = gameObject;
        isActiveSkill = false;
    }

    private void Start()
    {
        GameManager.instance.RegisterCharacter(this);
    }

    //게임 시작할 때, 캐릭터 변경 시 적용
    public void ApplyCharacter(CharacterDataBase dataBase)
    {
        characterData = dataBase;
        myMotion = GetComponentInChildren<Animator>();
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

    /*
 * 캐릭터 사정거리에 따라 PerformAttack 사용하도록 추가
 * 사정거리 내에 있는 적이라면 attackTime에 따라 주기적으로 공격하도록
 */

    private void Update()
    {
        attackTime += Time.deltaTime;

        if (attackTime >= attackCooltime)
        {
            myMotion.SetTrigger("isAttack"); //자동 공격
            attackTime = 0f;
        }
    }

    //SkillUI 연결 & 자동화[미구현]용 
    public void ActiveSkillAnimation()
    {
        isActiveSkill = true;
        if (isInvincible) isActiveSkill = false; //[임시] 탱커 무적 상태일때 공격이 들어가야해서 일단 이렇게 처리
        myMotion.SetTrigger("isSkill");

    }

    public void DisableSkillAnimation()
    {
        isActiveSkill = false;
    }

    //애니메이션 이벤트에서 호출
    public void PerformAttack()
    {
        if (attackComponent != null)
            attackComponent.Attack(targetObject.transform.position);
    }

    //애니메이션 이벤트에서 호출
    public void PerformSkill()
    {
        if (skillBehaviour != null)
            skillBehaviour.UseSkill(this,targetObject.transform.position);
    }

    public void Recovery()
    {
        if (recoveryComponent != null)
            recoveryComponent.Recover();
    }

    void OnDestroy()
    {
        GameManager.instance.RemoveCharacter(this);
    }
}