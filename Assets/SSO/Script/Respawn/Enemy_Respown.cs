using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Respown : MonoBehaviour
{
    public PoolManager pool;
    public GameObject bossPrefab; // ���� ������
    public GameObject warningUI;  // ��� UI
    public UnityEngine.UI.Image screenOverlay; // ȭ�� �������� ���� UI Image
    float timer;
    bool bossSpawned = false; // ������ �̹� ��ȯ�ƴ��� Ȯ��
    public GameObject stageClearUI;  // Ŭ���� �ؽ�Ʈ
    public static Enemy_Respown Instance;  // �̱��� �ν��Ͻ�
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
        //Enemy_Respown ��ũ��Ʈ�� Spawn �޼��忡�� ���� ������ ������
        //Enemy ��ũ��Ʈ��  SetStats �޼��带 ����Ͽ� hp�� damage ���� ����
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

        if (timer > 3f && pool.enemyCount < 3) // 3���� ������ ���� ���� ����
        {
            timer = 0;
            Spawn();
        }
        else if (pool.enemyCount >= 3 && !bossSpawned) // 3������ �Ǹ� ���� ��ȯ
        {
            SpawnBoss();   // ���� ��ȯ
            StartCoroutine(FlashRedScreen());  // ����ȭ�� �̹��� ����
            ShowWarning();  // warning ui ����
        }
        if (Input.GetKeyDown("space"))
        {
            pool.Get(1);
        }
    }

    void SpawnBoss()
    {
        Instantiate(bossPrefab, transform);
        bossSpawned = true; // ������ ��ȯ�Ǿ����� ǥ��
    }

    IEnumerator FlashRedScreen() // ȭ�� ������ ������
    {
        for (int i = 0; i < 3; i++)
        {
            screenOverlay.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            screenOverlay.color = Color.clear; // �ʱ� ȭ������
            yield return new WaitForSeconds(0.5f);

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
