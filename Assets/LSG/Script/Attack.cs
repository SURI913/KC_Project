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
        GrandParent = this.transform.parent.gameObject;
        //자식 Awake 먼저 시작
        GrandParentIAttack = GrandParent.GetComponent<IAttack>(); //캐릭터에다가 넣어줘야하는 스크립트
        Cooltime = GrandParentIAttack.atkTime;  //쿨타임 초기화
        bullet = bulletSet; //총알 오브젝트 초기화
        fireTransform = bulletPos;
        ID = IDset;
 
    }

    private void Awake()
    {
        initData();
    }
    protected void Update()
    {
        if (GrandParentIAttack.AtiveSkill == true)   //스킬 사용 상태면 일시적으로 멈춤
        {
            state = State.Reloading;
        }
        StateCheck();
        if (Cooltime <= 0)
        {
            state = State.Ready;    //쿨타임 종료
            Cooltime = GrandParentIAttack.atkTime;
        }
        else
        {
            Cooltime -= Time.deltaTime;
        }
    }
}
