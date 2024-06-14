using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    private Slider my_slider;
    TMP_Text stage;

    private void Start()
    {
        my_slider = GetComponent<Slider>();
        stage = transform.parent.GetChild(0).GetComponent<TMP_Text>();
        stage.text = GameManager.instance.current_stage;
    }
    // Update is called once per frame
    void Update()
    {
        //위치 옮겨야함 스테이지 바뀔때 마다 변경해주기
        my_slider.maxValue = GameManager.instance.boss_gauge;
        //Debug.Log(my_slider.maxValue);
        my_slider.value = GameManager.instance.monster_clear_count;
        //몬스터 처치 수 카운트
    }
}
