using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Respown : MonoBehaviour
{
    public PoolManager pool;
    public GameObject bossPrefab; // 보스 프리팹
    public GameObject warningUI;  // 경고 UI
    public UnityEngine.UI.Image screenOverlay; // 화면 깜빡임을 위한 UI Image
    float timer;
    bool bossSpawned = false; // 보스가 이미 소환됐는지 확인

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3f && pool.enemyCount < 3) // 10마리 이하일 때만 적을 생성
        {
            timer = 0;
            Spawn();
        }
        else if (pool.enemyCount >= 3 && !bossSpawned) // 10마리가 되면 보스 소환
        {
            SpawnBoss();
            StartCoroutine(FlashRedScreen());
            ShowWarning();
        }
        if (Input.GetKeyDown("space"))
        {
            pool.Get(1);
        }
    }

    void Spawn()
    {
        pool.Get(Random.Range(0, 4));
    }

    void SpawnBoss()
    {
        Instantiate(bossPrefab, transform);
        bossSpawned = true; // 보스가 소환되었음을 표시
    }

    IEnumerator FlashRedScreen() // 화면 빨간색 깜빡임
    {
        for (int i = 0; i < 3; i++)
        {
            screenOverlay.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            screenOverlay.color = Color.clear; // 초기 화면으로
            yield return new WaitForSeconds(0.5f);

        }
    }

    // 경고 메시지 표시 및 숨기기를 위한 함수 추가
    void ShowWarning()
    {
        warningUI.SetActive(true);  // 경고 UI 활성화
        StartCoroutine(HideWarningAfterSeconds(3)); // 3초 후에 경고 UI 숨기기
    }

    IEnumerator HideWarningAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        warningUI.SetActive(false);
    }

}
