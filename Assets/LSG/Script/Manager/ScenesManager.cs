using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    //씬 관리
    /// <summary>
    ///  main ~ 1-1~1-6 ~Dungeon~TowerUI~MainUI~ TowerUI~Inventory
    /// 
    /// </summary>
    Stack<int> scenes;
    int currentSceneIndex = 0;

    private void Awake()
    {
        //처음에 생성할 씬
        StartCoroutine(TransitionToNextStage());
        StartCoroutine(ShowMainUI());

    }

    IEnumerator ShowLodingScene()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);  // 다음 씬으로 전
    }

    IEnumerator TransitionToNextStage()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        int currentSceneIndex = scenes.Pop();
        SceneManager.LoadSceneAsync(currentSceneIndex += 1,LoadSceneMode.Additive);  // 다음 씬으로 전환
        scenes.Push(currentSceneIndex);
    }

    public IEnumerator ShowTower()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        SceneManager.LoadSceneAsync(10, LoadSceneMode.Additive);  //비동기 씬 로드
        int tmp = scenes.Pop();
        if(tmp == 9) { Debug.Log("정상적으로 UI해제"); }
        scenes.Push(10);


    }

    public IEnumerator ShowMainUI()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        SceneManager.LoadSceneAsync(9, LoadSceneMode.Additive);  //비동기 씬 로드 메인 UI
        SceneManager.LoadSceneAsync(11, LoadSceneMode.Single);  //비동기 씬 로드 인벤토리

        SceneManager.UnloadSceneAsync("TowerUI");  //비동기 씬 로드
        int tmp = scenes.Pop();
        if (tmp == 10) { Debug.Log("정상적으로 UI해제"); }
        scenes.Push(9);
        scenes.Push(11);

    }

    public IEnumerator ShowDungeonUI()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        SceneManager.LoadSceneAsync(8, LoadSceneMode.Single);  //비동기 씬 로드
        foreach(var go in scenes)
        {
            SceneManager.UnloadSceneAsync(go);  //비동기 씬 로드

        }
        scenes.Clear();
        scenes.Push(8); //던전 번호 체크
    }

}
