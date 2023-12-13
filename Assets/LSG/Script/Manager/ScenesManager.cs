using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    //씬 관리
    Stack<int> scenes;

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
        SceneManager.LoadSceneAsync("TowerUI", LoadSceneMode.Additive);  //비동기 씬 로드
        SceneManager.UnloadSceneAsync("MainUI");  //비동기 씬 로드

    }

    public IEnumerator ShowMainUI()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        SceneManager.LoadSceneAsync("MainUI", LoadSceneMode.Additive);  //비동기 씬 로드
        SceneManager.UnloadSceneAsync("TowerUI");  //비동기 씬 로드
    }

    public IEnumerator ShowDungeonUI()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        SceneManager.LoadSceneAsync("Dungeon", LoadSceneMode.Single);  //비동기 씬 로드
        foreach(var go in scenes)
        {
            SceneManager.UnloadSceneAsync(go);  //비동기 씬 로드

        }
        scenes.Clear();
        scenes.Push(99); //던전 번호 체크
    }

    public IEnumerator ShowInventoryUI()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        SceneManager.LoadSceneAsync("Inventory", LoadSceneMode.Single);  //비동기 씬 로드
        foreach (var go in scenes)
        {
            SceneManager.UnloadSceneAsync(go);  //비동기 씬 로드

        }
        scenes.Clear();
        scenes.Push(99); //번호 체크
    }

}
