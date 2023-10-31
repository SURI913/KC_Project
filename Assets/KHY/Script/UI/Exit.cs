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

    }
}
