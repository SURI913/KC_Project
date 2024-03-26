using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;
using Spine.Unity;
using UnityEngine.UI;

public class M_D01 : Monster
{
    private SkeletonAnimation spine;
    private Animator anim;
    public Slider healthSlider;
    



    public void Start()
    {
       // printData();// 데이터 출력
        //스파인
        /*spine = GetComponent<SkeletonAnimation>();
        spine.AnimationState.SetAnimation(0, "Idle", true);//Idle 애니메이션 기본으로 설정

        InvokeRepeating("Spine_Animation", 0f, 1.8f);//1.8마다 호출*/

        

        //애니메이션
        anim = GetComponent<Animator>();


        // InvokeRepeating("Animation()", 0f, 1f);


        if (healthSlider != null)
        {
            //  healthSlider = healthBar.slider; // 예시. healthBar에 따라 코드 수정 필요할 수 있음
            healthSlider.maxValue = (float)HP;
            healthSlider.value = (float)curHP;
        }

        // setData();


    }

   /* void setData()
    {
        GameManager.instance.DmonsterHP = HP; //====>null값 에러
        Debug.Log("HP");
        GameManager.instance.DmonsterATK = Attack;
        GameManager.instance.DmonsterAtime = AtkTime;
    }*/

    public void Update()
    {
        Animation();

       
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

    void Animation()
    {
       // Debug.Log("애니//");
        if (isAtk)
        {
           // Debug.Log("공격 애니//");
           /* //스파인 
            spine.AnimationState.SetAnimation(0, "Attack", false);*/
            //공격중 애니메이션(인덱스,애니메이션 이름, 루프 T/F

            //애니메이션
            anim.SetBool("isattack", true);
        }
        if (isDead)
        {
           // Debug.Log("데드");
            //스파인 
         /*   spine.AnimationState.SetAnimation(0, "Dead", false);
            //죽었을때 애니메이션(인덱스,애니메이션 이름, 루프 T/F*/
           // Debug.Log("데드애니메이션 후");


            //애니메이션
            anim.SetBool("isdead", true);
            Debug.Log("죽음 ");

        }
       
    }
}


    


