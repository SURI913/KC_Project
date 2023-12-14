using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    //씬 관리
    /// <summary>
    ///  main ~ 1-1~1-6 ~Dungeon~TowerUI~MainUI~ TowerUI~Inventory
    /// 
    /// </summary>
    Stack<int> scenesStage = new Stack<int>();
    Stack<int> scenesUI = new Stack<int>();
    int currentSceneIndex = 0;
    Image FadeObj;
    Color fadeInOutColor;
    AsyncOperation asyncOper;
    

    private void Awake()
    {
        //처음에 생성할 

        FadeObj = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        fadeInOutColor = FadeObj.color;
        StartCoroutine(TransitionToNextStage());
    }

     IEnumerator ShowLodingScene()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);  // 다음 씬으로 전
    }

    IEnumerator FadeOutIn()
    {
        FadeObj.enabled = true;
        while (fadeInOutColor.a < 1)   // 점점 불투명하게
        {
            fadeInOutColor.a += 0.05f; // 페이드아웃 속도 결정
            FadeObj.color = fadeInOutColor;

            yield return null;
        }
        
        while (!asyncOper.isDone)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);  // 3초 대기
        
        while (fadeInOutColor.a > 0)   // 점점 투명하게
        {
            fadeInOutColor.a -= 0.05f;
            FadeObj.color = fadeInOutColor;

            yield return null;
        }

        FadeObj.enabled = false;
        yield return null;
    }

    public IEnumerator TransitionToNextStage()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        if (currentSceneIndex == 6)
        {
            currentSceneIndex = 0;
        }
        if(currentSceneIndex != 0)
        {
            SceneManager.UnloadSceneAsync(currentSceneIndex);  //비동기 씬 로드
        }
        if (scenesStage.Contains(7))
        {
            SceneManager.UnloadSceneAsync(7);  //비동기 씬 로드
        }

        asyncOper = SceneManager.LoadSceneAsync(currentSceneIndex += 1, LoadSceneMode.Additive);  // 다음 씬으로 전환
        yield return StartCoroutine(FadeOutIn());
        yield return StartCoroutine(ShowMainUI());

        scenesStage.Push(currentSceneIndex);
        yield return null;

    }

    public IEnumerator ShowTower()
    {
       
        yield return new WaitForSeconds(3f);  // 3초 대기
        asyncOper = SceneManager.LoadSceneAsync(9, LoadSceneMode.Additive);  //비동기 씬 로드
        if (scenesUI.Count != 0)
        {
            foreach (int go in scenesUI)
            {
                SceneManager.UnloadSceneAsync(go);  //비동기 씬 로드

            }
        }
        scenesUI.Clear();
        scenesUI.Push(9);

        yield return null;

    }

    public IEnumerator ShowMainUI()
    {

        asyncOper = SceneManager.LoadSceneAsync(8, LoadSceneMode.Additive);  //비동기 씬 로드 메인 UI

        if (scenesUI.Count != 0)
        {
            foreach (int go in scenesUI)
            {
                print(go);
                SceneManager.UnloadSceneAsync(go);  //비동기 씬 로드

            }
        }

        scenesUI.Clear();

        scenesUI.Push(8);
        yield return null;

    }

    public IEnumerator ShowDungeonUI()
    {
        SceneManager.UnloadSceneAsync(currentSceneIndex);  //비동기 씬 로드
        currentSceneIndex--;
        if(currentSceneIndex <= 0 && currentSceneIndex>=6)
        {
            currentSceneIndex = 0;
        }
        yield return new WaitForSeconds(3f);  // 3초 대기
        asyncOper = SceneManager.LoadSceneAsync(7, LoadSceneMode.Additive);  //비동기 씬 로드

        if (scenesUI.Count != 0)
        {
            foreach (int go in scenesUI)
            {
                SceneManager.UnloadSceneAsync(go);  //비동기 씬 로드

            }
        }
        scenesUI.Clear();
        scenesUI.Push(7); //던전 번호 체크

        yield return StartCoroutine(FadeOutIn());

        
        yield return null;

    }

}
