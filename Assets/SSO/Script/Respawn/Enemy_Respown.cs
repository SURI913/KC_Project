using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Respown : MonoBehaviour
{
    public PoolManager pool;
    public GameObject warningUI;  // 경고 UI
    public GameObject stageClearUI;  // 클리어 텍스트
    public static Enemy_Respown Instance;  // 싱글톤 인스턴스
    public GameObject bossPrefab; // 보스 프리팹
    public double bossHp;
    public float bossDamage;
    public double enemyHp;
    public float enemyDamage;
    public float enemeyStageCount; // 이 스테이지에서 소환할 몬스터의 수
    private bool bossSpawned = false; // 보스가 이미 소환됐는지 확인
    private float timer;

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

    public double GetEnemyDamage()  // 입력받은 enemey의 데미지값을 반환
    {
        return enemyDamage;
    }

    public double GetBossDamage()
    {
        return bossDamage;
    }

    void SpawnBoss()
    {
        GameObject bossInstance = Instantiate(bossPrefab, transform); // 생성된 Boss의 참조를 가져옴
        Boss bossScript = bossInstance.GetComponent<Boss>(); // Boss 스크립트의 참조를 가져옴
        if (bossScript)
        {
            bossScript.SetStats(bossHp, bossDamage); // Boss의 hp와 damage 값을 설정
        }
        bossSpawned = true; // 보스가 소환되었음을 표시
    }

    void Spawn()
    {
        //Enemy_Respown 스크립트의 Spawn 메서드에서 적을 생성할 때마다
        //Enemy 스크립트의  SetStats 메서드를 사용하여 hp와 damage 값을 전달
        GameObject enemyObject = pool.Get(Random.Range(0, 4));
        Enemy enemyScript = enemyObject.GetComponent<Enemy>();
        Enemy_02 enemyScript2 = enemyObject.GetComponent<Enemy_02>();
        Enemy_03 enemyScript3 = enemyObject.GetComponent<Enemy_03>();
        Enemy_04 enemyScript4 = enemyObject.GetComponent<Enemy_04>();
        if (enemyScript)
        {
            enemyScript.SetStats(enemyHp, enemyDamage);
        }
        if (enemyScript2)
        {
            enemyScript2.SetStats(enemyHp, enemyDamage);
        }
        if (enemyScript3)
        {
            enemyScript3.SetStats(enemyHp, enemyDamage);
        }
        if (enemyScript4)
        {
            enemyScript4.SetStats(enemyHp, enemyDamage);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;  // enemy 소환의 쿨타임 역할

        if (timer > 3f && pool.enemyCount < enemeyStageCount) // 3초에 한번씩, n마리 이하일 때만 적을 생성
        {
            timer = 0;
            Spawn();     // enemy 소환하면 타이머 0으로 초기화
        }
        else if (pool.enemyCount >= enemeyStageCount && !bossSpawned) // n마리가 되면 보스 소환
        {
            SpawnBoss();   // 보스 소환
            ShowWarning();  // warning ui 생성
        }
        if (Input.GetKeyDown("space"))  // 실험용
        {
            pool.Get(0);
            pool.Get(1);
            pool.Get(2);
            pool.Get(3);
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
