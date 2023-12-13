using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enterstage : MonoBehaviour
{
    public GameObject UI;
    public GameObject UI2;
    public CountTime time;
    public GameObject image;
    public GameObject sliderbar;
    public void OnClick()
    {
        // SceneManager.LoadScene("Dungeon");
        //스테이지 입장

        //지금은 클릭했을때 setactive로 UI를 가린다.
        
        //UI 스테이지랑 enter,eixt 버튼 
        UI.SetActive(false);
        UI2.SetActive(false);
        time.StartCountdwon();//enter버튼이 눌리면 카운트 시작 
                              // image.SetActive(false);

        sliderbar.SetActive(true);
        
    }
}

