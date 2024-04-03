using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Enemy_Respown : MonoBehaviour
{
    public PoolManager pool;
    //public GameObject warningUI;  // 경고 UI
    //public GameObject stageClearUI;  // 클리어 텍스트
    public static Enemy_Respown Instance;  // 싱글톤 인스턴스

    public double bossHp;
    public float bossDamage;
    private double enemyHp;
    private float enemyDamage;
    private double enemy2Hp;
    private float enemy2Damage;
    private double enemy3Hp;
    private float enemy3Damage;
    private double enemy4Hp;
    private float enemy4Damage;

    public GameObject bossPrefab; // 보스 프리팹
    public float enemeyStageCount; // 이 스테이지에서 소환할 몬스터의 수
    private bool bossSpawned = false; // 보스가 이미 소환됐는지 확인
    private float timer;
    public Vector2 ground_enemy_position;
    public Vector2 fly_enemy_position;
    public Vector2 boss_position;
    public float enemy_speed;
    public Enemy_Data enemyData;
    private int enemySetValaue;

    private void Start()
    {
        // Data_Manager 오브젝트의 Data_Manager 스크립트를 찾아서 dataManager에 할당합니다.
        enemyData = Resources.Load<Enemy_Data>("Enemy Data");

    }

    private void Awake()
    {
        enemySetValaue = 0;
        SetEnemyData(enemyData);

        //PrintEnemyData();


        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEnemyData(Enemy_Data enemyData)
    {
        if(enemyData != null)
        {
            enemyHp = enemyData.enemy1[enemySetValaue].hp;
            enemyDamage = enemyData.enemy1[enemySetValaue].damage;

            enemy2Hp = enemyData.enemy2[enemySetValaue].hp;
            enemy2Damage = enemyData.enemy2[enemySetValaue].damage;

            enemy3Hp = enemyData.enemy3[enemySetValaue].hp;
            enemy3Damage = enemyData.enemy3[enemySetValaue].damage;

            enemy4Hp = enemyData.enemy4[enemySetValaue].hp;
            enemy4Damage = enemyData.enemy4[enemySetValaue].damage;
        }
        else
        {
            Debug.Log("에너미 데이터 전달안됨");
        }
    }

    public void PrintEnemyData()
    {
        Debug.Log("enemy1 체력 = " + enemyHp);
        Debug.Log("enemy1 데미지 = " + enemyDamage);
        Debug.Log("enemy2 체력 = " + enemy2Hp);
        Debug.Log("enemy2 데미지 = " + enemy2Damage);
        Debug.Log("enemy3 체력 = " + enemy3Hp);
        Debug.Log("enemy3 데미지 = " + enemy3Damage);
        Debug.Log("enemy4 체력 = " + enemy4Hp);
        Debug.Log("enemy4 데미지 = " + enemy4Damage);
    }

    public Vector2 GetBossPosition()
    {
        return boss_position;
    }

    public Vector2 GetGroundEnemyPosition()
    {
        return ground_enemy_position;
    }

    public Vector2 GetFlyEnemyPosition()
    {
        return fly_enemy_position;
    }

    public float GetEnemySpeed()
    {
        return enemy_speed;
    }

    public double GetEnemyDamage()  // 입력받은 enemey1의 데미지값을 반환
    {
        return enemyDamage;
    }

    public double GetEnemy2Damage()
    {
        return enemy2Damage;
    }

    public double GetEnemy3Damage()
    {
        return enemy3Damage;
    }

    public double GetEnemy4Damage()
    {
        return enemy4Damage;
    }

    public double GetBossDamage()
    {
        return bossDamage;
    }

    public double GetEnemyHp()
    {
        return enemyHp;
    }

    public double GetEnemy2Hp()
    {
        return enemy2Hp;
    }

    public double GetEnemy3Hp()
    {
        return enemy3Hp;
    }

    public double GetEnemy4Hp()
    {
        return enemy4Hp;
    }

    public double GetBossHp()
    {
        return bossHp;
    }

    void SpawnBoss()
    {
        enemySetValaue += 1;
        GameObject bossInstance = Instantiate(bossPrefab, transform); // 생성된 Boss의 참조를 가져옴
        Boss bossScript = bossInstance.GetComponent<Boss>(); // Boss 스크립트의 참조를 가져옴
        bossSpawned = true; // 보스가 소환되었음을 표시
    }

    void Spawn()
    {
        //Enemy_Respown 스크립트의 Spawn 메서드에서 적을 생성할 때마다
        //Enemy 스크립트의  SetStats 메서드를 사용하여 hp와 damage 값을 전달
        GameObject enemyObject = pool.Get(Random.Range(0, 4));
        Enemy_001 enemyScript1 = enemyObject.GetComponent<Enemy_001>();
        Enemy_002 enemyScript2 = enemyObject.GetComponent<Enemy_002>();
        Enemy_003 enemyScript3 = enemyObject.GetComponent<Enemy_003>();
        Enemy_004 enemyScript4 = enemyObject.GetComponent<Enemy_004>();
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
        //warningUI.SetActive(true);  // 경고 UI 활성화
        StartCoroutine(UIWait(3)); // 3초 후에 경고 UI 숨기기
    }

    IEnumerator UIWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //warningUI.SetActive(false);
    }

    public void ShowStageClear()
    {
        ScenesManager sceneManager = GameObject.Find("SceneManager").GetComponent<ScenesManager>();
        //stageClearUI.SetActive(true);  // "Stage Clear!!" 텍스트 활성화
        StartCoroutine(sceneManager.TransitionToNextStage());
    }

    /*IEnumerator TransitionToNextStage()
    {
        yield return new WaitForSeconds(3f);  // 3초 대기
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);  // 다음 씬으로 전환
    }*/
}
