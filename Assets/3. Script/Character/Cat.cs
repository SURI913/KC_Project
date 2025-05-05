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
    public float attackCooltime= 2f;
    private float attackTime = 0f;

    public GameObject targetObject;

    private AttackComponent attackComponent;
    private SkillComponent skillComponent;
    private RecoveryComponent recoveryComponent;
 
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
  

    private void Awake()
    {
        attackComponent = GetComponentInChildren<AttackComponent>();
        //skillComponent = GetComponent<SkillComponent>();
        //recoveryComponent = GetComponent<RecoveryComponent>();

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
    
    private void Update()
    {
        attackTime += Time.deltaTime;

        if(attackTime >= attackCooltime)
        {
            PerformAttack();
            attackTime = 0f;
        }
    }


    public void PerformAttack()
    {
        if (attackComponent != null)
            attackComponent.Attack(targetObject.transform.position);
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