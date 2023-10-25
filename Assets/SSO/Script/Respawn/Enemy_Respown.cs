using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Respown : MonoBehaviour
{
    public PoolManager pool;
    public GameObject bossPrefab; // 보스 프리팹
    public GameObject warningUI;  // 경고 UI
    public UnityEngine.UI.Image screenOverlay; // 화면 깜빡임을 위한 UI Image
    float timer;
    bool bossSpawned = false; // 보스가 이미 소환됐는지 확인
    public GameObject stageClearUI;  // 클리어 텍스트
    public static Enemy_Respown Instance;  // 싱글톤 인스턴스
    public double enemyHp;
    public float enemyDamage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Spawn()
    {
        //Enemy_Respown 스크립트의 Spawn 메서드에서 적을 생성할 때마다
        //Enemy 스크립트의  SetStats 메서드를 사용하여 hp와 damage 값을 전달
        GameObject enemyObject = pool.Get(Random.Range(0, 4));
        Enemy enemyScript = enemyObject.GetComponent<Enemy>();
        if (enemyScript)
        {
            enemyScript.SetStats(enemyHp, enemyDamage);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3f && pool.enemyCount < 3) // 3마리 이하일 때만 적을 생성
        {
            timer = 0;
            Spawn();
        }
        else if (pool.enemyCount >= 3 && !bossSpawned) // 3마리가 되면 보스 소환
        {
            SpawnBoss();   // 보스 소환
            StartCoroutine(FlashRedScreen());  // 빨간화면 이미지 생성
            ShowWarning();  // warning ui 생성
        }
        if (Input.GetKeyDown("space"))
        {
            pool.Get(1);
        }
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
        StartCoroutine(UIWait(3)); // 3초 후에 경고 UI 숨기기
    }

    IEnumerator UIWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        warningUI.SetActive(false);
    }

    public void ShowStageClear()
    {
        stageClearUI.SetActive(true);  // "Stage Clear!!" 텍스트 활성화
        StartCoroutine(TransitionToNextStage());
    }

    IEnumerator TransitionToNextStage()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);  // 다음 씬으로 전환
    }
}
