using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{


    public string stageID { get; set; } //명
    public double HP { get; set; } //체력
    public double maxHP { get; set; } //최대체력

    public bool Dead = false;
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

    public void Awake()
    {

      /*  MonsterD monster = monsterData.monsterdatas[0];
        stageID = monster.stageID;
        HP = monster.hp;
        Attack = monster.attack;
        AtkTime = monster.atktime;*/

    }

    public virtual void SetMonsterData(MonsterD monsdata) {
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
    

   

    public virtual void OnDamage(double Damage, RaycastHit2D hit) 
    {

        HP -= Damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
            Debug.Log("던전 몬스터 처치");
        }
    }
    // 몬스터에게 데미지를 입히는 함수{}

    /* protected virtual void HP_m()
     {
         maxHP = maxHP;
         HP = HP;
     }
     protected virtual void DIe_m()
     {
         Dead = true;
         Destroy(gameObject);
         //몬스터 죽는 애니메이션 추가 
     }

     protected virtual void OnAttack_m(RaycastHit2D hit)
     {
         Attack = Attack;
         AtkTime = AtkTime;
     }

     protected virtual void OnDamage(float damage, RaycastHit2D hit)
     {
         HP -= damage;
     }*/



    /*protected virtual void move_m()
    {

        movespeed = movespeed;
    }*/
}
