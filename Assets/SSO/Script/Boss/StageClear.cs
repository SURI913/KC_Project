using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class StageClear : MonoBehaviour
{
    public GameObject stageClearText;  // Stage Clear 텍스트

    public void OnBossClear()  // 보스를 쓰러트릴때 호출할 함수
    {
        stageClearText.SetActive(true);  // Stage Clear 텍스트를 활성화
        StartCoroutine(LoadNextScene());  // 다음 Scene 로드 코루틴 호출
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2f);  // 2초 대기 (Stage Clear 표시 시간)

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  // 현재 Scene 인덱스 가져오기
        int nextSceneIndex = currentSceneIndex + 1;  // 다음 Scene 인덱스

        // 마지막 Scene이면 게임 종료 또는 메인 메뉴로 돌아감(옵션)
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            // Application.Quit();  // 게임 종료 (편집기에서는 작동하지 않음) 또는
            // SceneManager.LoadScene(0);  // 메인 메뉴로 돌아가기 (메인 메뉴가 첫 번째 Scene인 경우)
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);  // 다음 Scene 로드
        }
    }
}
