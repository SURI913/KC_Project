using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Enemy_Respown : MonoBehaviour
{
    public PoolManager pool;
    public static Enemy_Respown Instance;  // 싱글톤 인스턴스

    private double bossHp;
    private float bossDamage;
    private float bossSkill;
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
    public bool bossSpawned = false; // 보스가 이미 소환됐는지 확인
    private float timer;
    public Vector2 ground_enemy_position;
    public Vector2 fly_enemy_position;
    public Vector2 boss_position;
    public float enemy_speed;
    public Enemy_Data enemyData;
    private int enemySetValaue;
    public int gold;

    private void Start()
    {
        enemySetValaue = 0;

        // 데이터 다운로드 완료 이벤트에 대한 핸들러 등록
        DBManager.Instance.OnEnemyDataDownloaded += HandleEnemyDataDownloaded;
    }

    private void OnDestroy()
    {
        // 반드시 이벤트 핸들러 등록 해제
        if (DBManager.Instance != null)
        {
            DBManager.Instance.OnEnemyDataDownloaded -= HandleEnemyDataDownloaded;
        }
    }

    // 데이터 다운로드 완료 시 호출될 핸들러
    private void HandleEnemyDataDownloaded(Enemy_Data downloadedData)
    {
        // 다운로드 받은 데이터로 적 정보 설정
        SetEnemyData(downloadedData);
        PrintEnemyData();
        GameManager.instance.boss_gauge = enemeyStageCount;

        // 필요한 초기화나 시작 동작 수행
        // 예: 적 생성 등
    }

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

            bossHp = enemyData.boss[enemySetValaue].hp;
            bossDamage = enemyData.boss[enemySetValaue].damage;
            bossSkill = enemyData.boss[enemySetValaue].skill;

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
        Debug.Log("boss 체력 = " + bossHp);
        Debug.Log("boss 데미지 = " + bossDamage);
        Debug.Log("boss 스킬 = " + bossSkill);
    }

    public void ShowGold()
    {
        if (Input.GetKeyDown("g"))
        {
            Debug.Log("gold is " + gold);
        }
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

        if (timer > 3f && GameManager.instance.monster_clear_count < enemeyStageCount) // 3초에 한번씩, n마리 이하일 때만 적을 생성
        {
            timer = 0;
            Spawn();     // enemy 소환하면 타이머 0으로 초기화
        }
        else if(GameManager.instance.monster_clear_count >= enemeyStageCount && !bossSpawned)
        {
            SpawnBoss();   // 보스 소환
        }

        ShowGold();
    }
}
