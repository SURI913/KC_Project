using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Exit : MonoBehaviour
{
    ScenesManager sceneManager;

    private void Awake()
    {
       // sceneManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
    }
    public void OnClick()
    {
        StartCoroutine(sceneManager.TransitionToNextStage());

        //던전끄고 사냥 씬으로 넘어가기
        // 수정 해라 수정 현재 씬으로 넘어오도록 겜 맴니거 ㄱㄱ

    }
}
