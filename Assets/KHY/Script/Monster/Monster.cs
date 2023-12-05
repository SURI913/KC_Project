using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{


    public string stageID { get; set; } //명
    public double HP { get; set; } //체력
    public double maxHP { get; set; } //최대체력
    public double Attack { get; set; } //공격력
    public int AtkTime { get; set; } //공격쿨타임

    StageButton s;

    public MonsterData monsterData; // 인스펙터에서 할당 즉 스크립터블 몬스터 데이터 넣기 

    /* 데이터 매니저에서 시트의 데이터를 받았고
     몬스터 데이터 안에 리스트에 받아온 데이터를 저장했다.
     이를 M_D01에서 전부 초기화 시켜주고
     스테이지 버튼에서 버튼에 인덱스를 줌
     인덱스는 시트의 행 순서랑 값을 같게 했고
     예를 들어 인덱스 값이 3일경우 3행의 데이터를 가저온다.*/


   
    
    public void FixedUpdate()
    {
       
       // Move();
        OnAttack();
    }

    public void SetMonsterDataByIndex(int index)
    {
        if (index >= 1 && index <= monsterData.monsterdatas.Length)
        {
            MonsterD stageData = monsterData.monsterdatas[index - 1];
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
            stageID = monsdata.stageID;
            HP = monsdata.hp;
            Attack = monsdata.attack;
            AtkTime = monsdata.atktime;
            Debug.Log("SetMonsterData: " + "StageID: " + stageID + "" +
                ", HP: " + HP + ", Attack: " + Attack + ", AtkTime: " + AtkTime);
            //여기선 데이터가 온다 !
        }
        else
        {
            Debug.Log("데이터가 전달되지않음");
        }
    }






    private float rayLen=10f;// 레이캐스트의 길이 
    private LayerMask layerMask; //레이어 플레이어 
    private bool isatk = false; // 공격중이 아닐때
   // private bool ismove = true;
    RaycastHit2D hit;
    private float moveSpeed = 4f;

    public void Move()
    {
        Debug.Log(" HP:" + HP);
        /*if (!isatk)
        {
            //hit에 저장되어있는 Player 레이마스크 즉 null이 아니면 이동
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * Vector2.zero * Time.deltaTime);
        }*/

    }
    public void OnDamage(double Damage, RaycastHit2D hit)
    {
        Debug.Log(" 데지미HP:" + HP);
        Debug.Log(" 데지미HP:" + Attack);

        HP -= Damage;
        Debug.Log("몬스터가 공격받는다  HP:" + HP);
        if (HP <= 0)
        {
            Destroy(gameObject);
            Debug.Log("던전 몬스터 처치");
        }
    }
    public void OnAttack()
    {
        // 찾을 레이어 저장
        layerMask = LayerMask.GetMask("Player");
        //레이를 표시할 포지션
        Vector2 MonsterPosition = new Vector2(transform.position.x, transform.position.y + 2);
        //hit에 저장
        hit = Physics2D.Raycast(MonsterPosition, Vector2.left, rayLen, layerMask);
        //레이 색 줘서 표시
        Debug.DrawRay(MonsterPosition, Vector2.left * rayLen, Color.red);//

        if (hit.collider != null)
        {
            if(hit.collider.CompareTag("Player"))
            {
                isatk = true;
            }
        }
    }

        
}
 /*   public void SpawnMonster(Vector2 spawnPosition)
    {
        // 몬스터를 생성하고 스폰 위치로 이동
        Instantiate(gameObject, spawnPosition, Quaternion.identity);

        // 생성된 몬스터에 대한 초기화
       // SetMonsterData(monsterData);
        // 추가적인 초기화 작업이 필요하다면 여기에 추가
    }*/
    // 몬스터에게 데미지를 입히는 함수{}

 

     



