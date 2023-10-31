using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountTime : MonoBehaviour
{
    [SerializeField]
    private float countdownTime = 40.0f;
   
    private float currentTime;
    [SerializeField]
    private Text countdownText;
    void Start()
    {
        currentTime = countdownTime;
        countdownText = GetComponent<Text>();
       
    }

    // Update is called once per frame
    void Update()
    {
       if(currentTime>0)
        {
            currentTime -= Time.deltaTime; //
            CountdownText();
        }
       else
        {
            //몬스터 디스트로이
            //hp 스크립트 만든후 체력을 0으로 만들며 함수 가저와 쓸것 
        }
      
      
            
    }

    private void CountdownText()
    {
        countdownText.text = currentTime.ToString("F0") +" / "+ countdownTime.ToString("F0"); ; 
        //F0 소수점 0 자리까지 (F1이면 한자리 까지)
    }
}
