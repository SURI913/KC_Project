using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Respown : MonoBehaviour
{
    public PoolManager pool;
    public GameObject warningUI;  // ��� UI
    public GameObject stageClearUI;  // Ŭ���� �ؽ�Ʈ
    public static Enemy_Respown Instance;  // �̱��� �ν��Ͻ�
    public GameObject bossPrefab; // ���� ������
    public double bossHp;
    public float bossDamage;
    public double enemyHp;
    public float enemyDamage;
    public float enemeyStageCount; // �� ������������ ��ȯ�� ������ ��
    private bool bossSpawned = false; // ������ �̹� ��ȯ�ƴ��� Ȯ��
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

    public double GetEnemyDamage()  // �Է¹��� enemey�� ���������� ��ȯ
    {
        return enemyDamage;
    }

    public double GetBossDamage()
    {
        return bossDamage;
    }

    void SpawnBoss()
    {
        GameObject bossInstance = Instantiate(bossPrefab, transform); // ������ Boss�� ������ ������
        Boss bossScript = bossInstance.GetComponent<Boss>(); // Boss ��ũ��Ʈ�� ������ ������
        if (bossScript)
        {
            bossScript.SetStats(bossHp, bossDamage); // Boss�� hp�� damage ���� ����
        }
        bossSpawned = true; // ������ ��ȯ�Ǿ����� ǥ��
    }

    void Spawn()
    {
        //Enemy_Respown ��ũ��Ʈ�� Spawn �޼��忡�� ���� ������ ������
        //Enemy ��ũ��Ʈ��  SetStats �޼��带 ����Ͽ� hp�� damage ���� ����
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
        timer += Time.deltaTime;  // enemy ��ȯ�� ��Ÿ�� ����

        if (timer > 3f && pool.enemyCount < enemeyStageCount) // 3�ʿ� �ѹ���, n���� ������ ���� ���� ����
        {
            timer = 0;
            Spawn();     // enemy ��ȯ�ϸ� Ÿ�̸� 0���� �ʱ�ȭ
        }
        else if (pool.enemyCount >= enemeyStageCount && !bossSpawned) // n������ �Ǹ� ���� ��ȯ
        {
            SpawnBoss();   // ���� ��ȯ
            ShowWarning();  // warning ui ����
        }
        if (Input.GetKeyDown("space"))  // �����
        {
            pool.Get(0);
            pool.Get(1);
            pool.Get(2);
            pool.Get(3);
        }
    }

    // ��� �޽��� ǥ�� �� ����⸦ ���� �Լ� �߰�
    void ShowWarning()
    {
        warningUI.SetActive(true);  // ��� UI Ȱ��ȭ
        StartCoroutine(UIWait(3)); // 3�� �Ŀ� ��� UI �����
    }

    IEnumerator UIWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        warningUI.SetActive(false);
    }

    public void ShowStageClear()
    {
        stageClearUI.SetActive(true);  // "Stage Clear!!" �ؽ�Ʈ Ȱ��ȭ
        StartCoroutine(TransitionToNextStage());
    }

    IEnumerator TransitionToNextStage()
    {
        yield return new WaitForSeconds(3f);  // 3�� ���
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);  // ���� ������ ��ȯ
    }
}
