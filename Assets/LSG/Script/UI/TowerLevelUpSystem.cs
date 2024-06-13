using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerLevelUpSystem : MonoBehaviour
{
    Slider my_slider;

    private void Awake()
    {
        my_slider = GetComponent<Slider>();
        my_slider.interactable = false;
    }

    //옵저버 패턴 적용 전
    public void UpdateExGauge()
    {
        my_slider.value = Mathf.Lerp(my_slider.value, GameManager.instance.ex, Time.deltaTime * 10);
    }

    //레벨업 세팅 //레벨업 하면 값 세팅하는 방식 아니면 게임매니저에 추가하기
    //서버 데이터는 모아뒀다가 스테이지 클리어시 전송하도록 할 것
    public void SetLv(int _value)
    {
        if (_value <= 10)
        {
            GameManager.instance.consume_ex += 1000;
        }
        else if (_value <= 20 && _value > 10)
        {
            GameManager.instance.consume_ex += 2000;
        }
        else if(_value <= 50&& _value > 20)
        {
            GameManager.instance.consume_ex += 5000;
        }
        else if (_value <= 100 && _value > 50)
        {
            GameManager.instance.consume_ex += 10000;
        }
        else if (_value <= 200 && _value > 100)
        {
            GameManager.instance.consume_ex += 100000;
        }
        else if (_value <= 300 && _value > 200)
        {
            GameManager.instance.consume_ex += 200000;
        }
        else if (_value <= 400 && _value > 300)
        {
            GameManager.instance.consume_ex += 300000;
        }
        else if ( _value > 400)
        {
            GameManager.instance.consume_ex += 400000;
        }
        my_slider.maxValue = GameManager.instance.consume_ex;
    }
}
