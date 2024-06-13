using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;
using UnityEngine.UI;

public class C_D001 : BaseDealer, MyHeroesImp
{
    public GrowingData base_growing_data;
    public CatData base_cat_data;

    //-----------------------------------------------------
    //캐릭터 값 초기화
    //DB에서 끌어옴
    //레벨업 할때마다 저장 호출 + 값 다시 가져오기

    private void Awake()
    {
        atk_distance = 10; //원거리
        skill_distance = 10;
        is_parabola_attack = true;
        is_parabola_skill = false;

        //데이터가 없으면
        cat_data = base_cat_data.all_cat_data[0];
        initAttackData();//임시 위치

        growing_data = base_growing_data;
        //데이터가 있으면
        cat_motion = GetComponentInChildren<Animator>();
    }

    public Cat GetMyData()
    {
        
        return this;
    }
}
