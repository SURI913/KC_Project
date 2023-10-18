using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Respown : MonoBehaviour
{
    public PoolManager pool;
    public GameObject bossPrefab; // ���� ������
    public GameObject warningUI;  // ��� UI
    public UnityEngine.UI.Image screenOverlay; // ȭ�� �������� ���� UI Image
    float timer;
    bool bossSpawned = false; // ������ �̹� ��ȯ�ƴ��� Ȯ��

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3f && pool.enemyCount < 3) // 10���� ������ ���� ���� ����
        {
            timer = 0;
            Spawn();
        }
        else if (pool.enemyCount >= 3 && !bossSpawned) // 10������ �Ǹ� ���� ��ȯ
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
        StartCoroutine(HideWarningAfterSeconds(3)); // 3�� �Ŀ� ��� UI �����
    }

    IEnumerator HideWarningAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        warningUI.SetActive(false);
    }

}
