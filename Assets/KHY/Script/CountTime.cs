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
            //���� ��Ʈ����
            //hp ��ũ��Ʈ ������ ü���� 0���� ����� �Լ� ������ ���� 
        }
      
      
            
    }

    private void CountdownText()
    {
        countdownText.text = currentTime.ToString("F0") +" / "+ countdownTime.ToString("F0"); ; 
        //F0 �Ҽ��� 0 �ڸ����� (F1�̸� ���ڸ� ����)
    }
}
