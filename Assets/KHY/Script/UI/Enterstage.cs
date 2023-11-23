using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enterstage : MonoBehaviour
{
    public GameObject UI;
    public GameObject UI2;
    public void OnClick()
    {
        /*  테스트 끝난후 데이터매니저를 통해 씬을 넘어가도 데이터를 넘길수 있도록 한다
          SceneManager.LoadScene("Dungeon");
          //스테이지 입장*/

        //지금은 클릭했을때 setactive로 UI를 가린다.
        //카운트 수정하기
        UI.SetActive(false);
        UI2.SetActive(false);
    }
}

// 씬 구분해사 하기 