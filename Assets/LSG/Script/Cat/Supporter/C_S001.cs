using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllUnit;
using System;

public class C_S001 : BaseSupporter, MyHeroesImp
{
    //샌드박스 디자인 패턴이 맞을까?
    //유니티 상속 다시 작업 해야할 듯

    //public string name { get;, private set; }

    //스킬을 위한 함수 일정범위 내의 영웅들을 골라냄
    public GrowingData base_growing_data;
    public CatData base_cat_data;

    //캐릭터 값 초기화
    //DB에서 끌어옴     
    //레벨업 할때마다 저장 호출 + 값 다시 가져오기

    private void Awake()
    {
        //데이터가 없으면
        cat_data = base_cat_data.all_cat_data[2];
        growing_data = base_growing_data;
        //데이터가 있으면
        FindSupportTarget();
        cat_motion = GetComponentInChildren<Animator>();
    }

    public Cat GetMyData()
    {
        return this;
    }
}
