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
    public void OnClick()
    {
        // SceneManager.LoadScene("Dungeon");
        //스테이지 입장

        //지금은 클릭했을때 setactive로 UI를 가린다.
        //카운트 수정하기
        UI.SetActive(false);
        UI2.SetActive(false);
        time.StartCountdwon();
       // image.SetActive(false);
    }
}

