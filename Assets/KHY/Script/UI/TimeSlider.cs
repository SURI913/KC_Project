using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeSlider : MonoBehaviour
{

    [SerializeField]
    private Slider timeSlider;
    [SerializeField]
    private float maxTime = 10.0f;
    private float currentTime;
    public bool stopTime = false;

    void Start()
    {
        timeSlider.maxValue = maxTime;
        timeSlider.value = currentTime;

        StartTime();

       /* 
        UpdateTime();
        StartCoroutine(Countdown());*/
    }

    void Update()
    {
        // 추가적인 업데이트 로직이 필요한 경우 여기에 작성
    }

  /*  private void UpdateTime()
    {
        timeSlider.value =(float)currentTime / maxTime;
    }*/
    public void StartTime()
    {
        StartCoroutine(Countdown());
    }
    private IEnumerator Countdown()
    {
        while(stopTime == false)
        {
            maxTime -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);

            if(maxTime<=0)
            {
                stopTime = true;
                SceneManager.LoadScene("1-1");
            }
            if(stopTime==false)
            {
                timeSlider.value = maxTime;
            }
        }    

        // 카운트다운이 끝났을 때의 추가 작업을 수행하려면 여기에 작성
        //로드씬 ,  이미지 
    }
}

