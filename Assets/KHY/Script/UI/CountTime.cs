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
            //40초가 끝나면 던전이 실패하고 다시 던전 메인씬으로 넘어간다
            //실패했다는 이미지는 1.5초정도 보여준후 
            //몬스터 디스트로이
            //hp 스크립트 만든후 체력을 0으로 만들며 함수 가저와 쓸것 
        }
    }
    public void StartCountdwon()
    {
        isCounting = true;
    }
    public void StopCountdwon()
    {
        isCounting = false;
        SceneManager.LoadScene(1 - 1);
       /* DestroyMonsters(); 
        StartCoroutine(ShowFailureImage()); 이미지 로드 
       HP 0으로 만들기 ㅋ*/
    }
    private void CountdownText()
    {
        //카운트 텍스트 로직
        countdownText.text = currentTime.ToString("F0") + " / " + countdownTime.ToString("F0"); ;
        //F0 소수점 0 자리까지 (F1이면 한자리 까지)
    }
}
