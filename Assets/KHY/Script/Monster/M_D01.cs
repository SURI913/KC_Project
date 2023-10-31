using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;

/*데이터 매니저에서 시트의 데이터를 받았고 
    몬스터 데이터 안에 리스트에 받아온 데이터를 저장했다.
    이를 M_D01에서 전부 초기화 시켜주고 
    스테이지 버튼에서 버튼에 인덱스를 줌 
    인덱스는 시트의 행 순서랑 값을 같게 했고
    예를 들어 인덱스 값이 3일경우 3행의 데이터를 가저온다.
*/
public class M_D01 : Monster
{
    public MonsterData monsterData; // 인스펙터에서 할당 즉 스크립터블 몬스터 데이터 넣기 


    private void Awake()//모르겟음, 다시 확인하자 ㅋ
    {
       // monsterData 변수가 null이 아니고 monsterData.monsterdatas 배열의 길이가 1 이상인 경우에만 실행
        if (monsterData != null && monsterData.monsterdatas.Length > 0)
        {
            // 예를 들어, 첫 번째 몬스터 데이터를 가져오기
            MonsterD monsdata = monsterData.monsterdatas[1];
            ID_m = monsdata.stageID;
            HP = monsdata.hp;
            Attack = monsdata.attack;
            AtkTime = monsdata.atktime;      
        }
        else
        {
            Debug.LogError("Monster data is not assigned or empty.");
            //위 코드가 실패하면 나오눈 출력
        }
       
    }

    

    private float movespeed = 2.0f;
    private bool isAttack = false;
    private float distance = 5.0f;
    private LayerMask Player;

    private void FixedUpdate()
    {
        if (!isAttack)  
        { 
            move_m(); 
        }
    }
    private void move_m()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.left, distance, Player);

        if (ray.collider != null && ray.collider.CompareTag("Player"))
        {
            // 레이캐스트가 플레이어를 충돌 감지하면 몬스터가 공격
            isAttack = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
            // 레이캐스트가 플레이어를 감지하지 않으면 몬스터의 이동 상태로 유지
            isAttack = false;
            // 애니메이션?
            Vector2 movement = new Vector2(-movespeed, 0); // 음수 값으로 x 성분을 설정하여 왼쪽으로 이동합니다.
            GetComponent<Rigidbody2D>().velocity = movement;
        }



    }
    protected override void OnAttack_m(RaycastHit2D hit)
    {
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            isAttack = true;
            Debug.Log("플레이어에게 공격");
            
        }
    }
   


    protected override void OnDamage_m(float damage, RaycastHit2D hit)
    {
      if(!Dead)
        {
            HP -= damage;
        }
      else if(HP<=0)
        {
            DIe_m();
        }
    }


}
