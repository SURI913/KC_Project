using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour, DamageableImp
{

    public string stageID { get; set; } //명
    public double HP { get; set; } //체력
    public double maxHP { get; set; } //최대체력
    public double Attack { get; set; } //공격력
    public int AtkTime { get; set; } //공격쿨타임
    public  double curHP;

    
    StageButton s;

    public MonsterData monsterData; // 인스펙터에서 할당 즉 스크립터블 몬스터 데이터 넣기 

    ScenesManager sceneManager; //씬매니저 변수

    /* 데이터 매니저에서 시트의 데이터를 받았고
     몬스터 데이터 안에 리스트에 받아온 데이터를 저장했다.
     이를 M_D01에서 전부 초기화 시켜주고
     스테이지 버튼에서 버튼에 인덱스를 줌
     인덱스는 시트의 행 순서랑 값을 같게 했고
     예를 들어 인덱스 값이 3일경우 3행의 데이터를 가저온다.*/

    public void Awake()
    {
        //몬스터 스탯들 처음 초기화 해주기 

        HP = 100000000000;
        Attack = 10;
        AtkTime = 3;

        //sceneManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();

    }

    public void FixedUpdate()
    {
       
        Move();
        OnAttack();
    }

    public void SetMonsterDataByIndex(int index)
    {
        if (index >= 1 && index <= monsterData.dungeon_monsterdatas.Length)
        {
            MonsterD stageData = monsterData.dungeon_monsterdatas[index - 1];
            SetMonsterData(stageData);
        }
        else
        {
            Debug.LogError("Invalid stage index: " + index);
        }   
    }

    public void SetMonsterData(MonsterD monsdata) {
        if (monsdata != null)
        {
            stageID = monsdata.dungeon_monster_stageID;
            HP = monsdata.dungeon_monster_hp;
            Attack = monsdata.dungeon_monster_attack;
            AtkTime = monsdata.dungeon_monster_atktime;
           /* Debug.Log("SetMonsterData: " + "StageID: " + stageID + "" +
                ", HP: " + HP + ", Attack: " + Attack + ", AtkTime: " + AtkTime);*/
            //여기선 데이터가 온다 !
        }
        else
        {
            Debug.Log("데이터가 전달되지않음");
        }

    }



    private float rayLen=10f;// 레이캐스트의 길이 
    private LayerMask layerMask; //레이어 플레이어
    private LayerMask layerMask1;
    public bool isAtk = false; // 공격중 확인
    public bool isDead = false;
   // private bool ismove = true;
    RaycastHit2D hit;
    RaycastHit2D hit1;
    private float moveSpeed = 3f;
   
   

    public void Move()
    {
       
        if (!isAtk)
        {
            //hit에 저장되어있는 Player 레이마스크 즉 null이 아니면 이동
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            isDead = false;
        }
        else
        {
            transform.Translate(Vector2.left * Vector2.zero * Time.deltaTime);
        }

    }
    public void OnDamage(double Damage)
    {
 
        curHP = HP - Damage;

        Debug.Log("몬스터 HP:" + curHP);
        if (HP <= 0)
        {
            //체력이 0 이하일때 아래 코드 실행
            isDead = true;
            Destroy(gameObject, 2f);//오브젝트 2초후 삭제 
           /* Debug.Log("던전 몬스터 처치");*/
            //로드씬
            StartCoroutine(sceneManager.TransitionToNextStage());


        }
    }
    
    public void OnAttack()
    {
        // 찾을 레이어 저장
        layerMask = LayerMask.GetMask("Player");
        layerMask1 = LayerMask.GetMask("Tower");
        //레이를 표시할 포지션
        Vector2 MonsterPosition = new Vector2(transform.position.x, transform.position.y + 5);
        Vector2 MonsterPosition1 = new Vector2(transform.position.x, transform.position.y + 7);
        //hit에 저장
        hit = Physics2D.Raycast(MonsterPosition, Vector2.left, rayLen, layerMask);
        hit1 = Physics2D.Raycast(MonsterPosition, Vector2.left, rayLen, layerMask);
        //레이 색 줘서 표시
        Debug.DrawRay(MonsterPosition, Vector2.left * rayLen, Color.red);//


        MyHeroesImp cat = GameObject.FindWithTag("Player").GetComponent<MyHeroesImp>();
        Tower tower = GameObject.FindWithTag("Castle").GetComponent<Tower>();
        //Debug.Log("플레이어 HP:" + catHP);
        if (hit.collider != null)
        {
            if(hit.collider.CompareTag("Player")||hit1.collider.CompareTag("Castle") )
            {

                //플레이어 태그를 찾고 공격
                isAtk = true;
                /*  Debug.Log("hit 이 플레이어 태그 찾음");*/
                DamageableImp target = cat.GetMyData().GetComponent<DamageableImp>();
                target.OnDamage(Attack);

                // Debug.Log("플레이어 HP:" + catHP);
                //  Debug.Log("몬스터가 플레이어에게 공격 " + Attack);

            }
        }
    }
  

}


 

     



