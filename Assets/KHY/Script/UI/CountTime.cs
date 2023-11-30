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
    bool isCounting = false;

    void Start()
    {
        currentTime = countdownTime;
        countdownText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isCounting && currentTime > 0)
        {
            currentTime -= Time.deltaTime; //
            CountdownText();
        }
        else if (isCounting && currentTime <= 0)
        {
            StopCountdwon();
            //SceneManager.LoadScene("StageSelection");
            //40�ʰ� ������ ������ �����ϰ� �ٽ� ���� ���ξ����� �Ѿ��
            //�����ߴٴ� �̹����� 1.5������ �������� 
            //���� ��Ʈ����
            //hp ��ũ��Ʈ ������ ü���� 0���� ����� �Լ� ������ ���� 
        }
    }
    public void StartCountdwon()
    {
        isCounting = true;
    }
    public void StopCountdwon()
    {
        isCounting = false;
       /* DestroyMonsters(); 
        StartCoroutine(ShowFailureImage()); �̹��� �ε� 
       HP 0���� ����� ��*/
    }
    private void CountdownText()
    {
        //ī��Ʈ �ؽ�Ʈ ����
        countdownText.text = currentTime.ToString("F0") + " / " + countdownTime.ToString("F0"); ;
        //F0 �Ҽ��� 0 �ڸ����� (F1�̸� ���ڸ� ����)
    }
}
