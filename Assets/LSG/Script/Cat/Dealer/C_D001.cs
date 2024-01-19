using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnit;
using UnityEngine.UI;

public class C_D001 : MonoBehaviour
{
    public GrowingData base_growing_data;
    public GameObject damage_prefab;
    public BaseCatData base_cat_data;

    //-----------------------------------------------------

    BaseDealer c_d001;

    //캐릭터 값 초기화
    //DB에서 끌어옴
    //레벨업 할때마다 저장 호출 + 값 다시 가져오기

    private void Awake()
    {
        //데이터가 없으면
        c_d001 = new BaseDealer(base_cat_data, base_growing_data, damage_prefab);

        //데이터가 있으면
    }
}
