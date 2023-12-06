using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Projectile
{
    public GameObject bulletSet;    // 총알 오브젝트 입력
    public Transform bulletPos;

    float Cooltime;
    private string IDset = "Atk";


    void initData() 
    {
        grandParent = this.transform.parent.gameObject;
        //자식 Awake 먼저 시작
        grandParentIAttack = grandParent.GetComponent<IAttack>(); //캐릭터에다가 넣어줘야하는 스크립트
        Cooltime = grandParentIAttack.atkTime;  //쿨타임 초기화
        bullet = bulletSet; //총알 오브젝트 초기화
        fireTransform = bulletPos;
        if (grandParent.GetComponent<Cat>())
        {
            ID = grandParent.GetComponent<Cat>().ID;
        }
        else if(grandParent.GetComponent<Tower>())
        {
            ID = "Tower";

        }
        else
        {
            Debug.Log("id 찾을 수 없음");
        }

    }

    private void Start()
    {
        initData();
    }
    protected void Update()
    {
        //이 부분도 수정 필요
        if (grandParentIAttack.ativeSkill == true)   //스킬 사용 상태면 일시적으로 멈춤
        {
                state = State.Reloading;
        }
        StateCheck();
        if (Cooltime <= 0)
        {
            state = State.Ready;    //쿨타임 종료
            Cooltime = grandParentIAttack.atkTime;
        }
        else
        {
            Cooltime -= Time.deltaTime;
        }
    }
}
