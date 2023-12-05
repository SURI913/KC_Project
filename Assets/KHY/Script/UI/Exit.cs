using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("1-1");
        //던전끄고 사냥 씬으로 넘어가기
        // 수정 해라 수정 현재 씬으로 넘어오도록 겜 맴니거 ㄱㄱ

    }
}
