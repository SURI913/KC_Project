using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllUnit;
using System;

public class C_S001 : MonoBehaviour
{
    //샌드박스 디자인 패턴

    //public string name { get;, private set; }
    float skillEft = 0.05f; //힐 들어가는 퍼센트


    //스킬을 위한 함수 일정범위 내의 영웅들을 골라냄
    public GrowingData base_growing_data;
    public GameObject damage_prefab;
    public BaseCatData base_cat_data;

    BaseSupporter c_s001;

    //캐릭터 값 초기화
    //DB에서 끌어옴     
    //레벨업 할때마다 저장 호출 + 값 다시 가져오기

    private void Awake()
    {
        //데이터가 없으면
        c_s001 = new BaseSupporter(base_cat_data, base_growing_data, damage_prefab);
        //데이터가 있으면
    }
}
