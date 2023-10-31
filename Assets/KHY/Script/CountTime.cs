using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountTime : MonoBehaviour
{
    [SerializeField]
    private float countdownTime = 20.0f;
   
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
       else if (currentTime<=0)
        {
            SceneManager.LoadScene("StageSelection");
            //40�ʰ� ������ ������ �����ϰ� �ٽ� ���� ���ξ����� �Ѿ��
            //�����ߴٴ� �̹����� 1.5������ �������� 
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
