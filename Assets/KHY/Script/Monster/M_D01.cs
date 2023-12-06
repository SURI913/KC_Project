using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;
using Spine.Unity;


public class M_D01 : Monster
{
    private SkeletonAnimation spine;

    public void Start()
    {
        printData();// 데이터 출력
        spine = GetComponent<SkeletonAnimation>();
        spine.AnimationState.SetAnimation(0, "Idle", true);//Idle 애니메이션 기본으로 설정
        InvokeRepeating("Spine_Animation", 0f, 1.8f);//1.8마다 호출

    }

    public void Update()
    {


    }

    private void printData()
    {
        if (!string.IsNullOrEmpty(stageID))
        {
            Debug.Log("스테이지-" + stageID);
            Debug.Log("체력-" + HP);
            Debug.Log("공격력-" + Attack);
            Debug.Log("공격속도=" + AtkTime);
        }
        else
        {
            Debug.Log("Monster에서 데이터 전달 안됨");
        }
    }

    void Spine_Animation()
    {
        Debug.Log("애니//");
        if (isAtk)
        {
            Debug.Log("공격 애니//");
            spine.AnimationState.SetAnimation(0, "Attack", false);
            //공격중 애니메이션(인덱스,애니메이션 이름, 루프 T/F
        }
        /*  if (isGetdamage)
          {
              spine.AnimationState.SetAnimation(0, "Idle", false);
              //공격받는중 애니메이션(인덱스,애니메이션 이름, 루프 T/F
          }*/
        if (isDead)
        {
            Debug.Log("데드");
            spine.AnimationState.SetAnimation(0, "Dead", false);
            //죽었을때 애니메이션(인덱스,애니메이션 이름, 루프 T/F
            Debug.Log("데드애니메이션 후");
        }
        /*else
        {
            spine.AnimationState.SetAnimation(0, "Idle", true);
        }*/
    }
}


    


