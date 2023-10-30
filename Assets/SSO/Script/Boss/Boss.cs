using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour, IDamageable
{
    // ���� ����
    public float enemySpeed = 0;
    public Vector2 StartPosition;
    public GameObject boss_attack;
    public float attackCooldown = 2f;  // ���� ��Ÿ��
    private double hp;
    private float damage;
    private bool bossSpawned = false;   // ����� ������ �Ͼ�°��� �����ϴ� ����
    private bool isCollidedCastle = false; // 'Castle'�� �浹�ߴ��� Ȯ���ϴ� ����
    private Enemy_Respown respawner;  // Enemy_Respown ��ũ��Ʈ�� ����


    void Start()
    {
        transform.position = StartPosition;
        respawner = Enemy_Respown.Instance;  // �̱��� �ν��Ͻ��� ���� ���� ����
    }

    public void SetStats(double health, float dmg)
    {
        hp = health;
        damage = dmg;
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //�������� ����
    {
        hp -= Damage;
        Debug.Log("���� ���ݴ���");
        if (hp <= 500 && hp > 0 && !bossSpawned && isCollidedCastle)  
            // ü���� ��ƴ����, ������������, �� �ڵ尡 ���� ��������ʾҴٸ�, Castle�� �浹�ߴٸ� 2������ ����
        {
            bossSpawned = true; // �� ������ �߰��Ͽ� BossSecondPage()�� �� ���� ����ǵ��� ��
            StopCoroutine(BossFirstPage());
            Debug.Log("1������ ����, 2������ ����");
            StartCoroutine(BossSecondPage()); // Coroutine ����
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("���� óġ");
            respawner.ShowStageClear();  // ������ �׾��� �� "Stage Clear!!" ǥ��
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle"))
        {
            Debug.Log("���� �浹");
            enemySpeed = 0;
            if (hp > 500)
            {
                Debug.Log("1������ ����");
                StartCoroutine(BossFirstPage()); // Coroutine ����
            }
            isCollidedCastle = true; // Castle�� �浹������ ǥ��
        }
    }

    IEnumerator BossFirstPage()   // 1������
    {
        while (true) // ���� �ݺ�
        {
            Vector3 spawnPosition = transform.position - Vector3.right*3;
            GameObject attackInstance = Instantiate(boss_attack, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 0.5f));

            yield return new WaitForSeconds(attackCooldown); // ��Ÿ�� ���� ���
        }
    }

    IEnumerator BossSecondPage()   // 2������
    {
        while (true) // ���� �ݺ�
        {   // ���� 3������
            Vector3 spawnPosition = transform.position - Vector3.right * 3;                   // �߰�����
            Vector3 spawnPosition2 = spawnPosition - Vector3.right - Vector3.up;       // ��
            Vector3 spawnPosition3 = spawnPosition - Vector3.right - Vector3.down;  // �Ʒ�
            GameObject attackInstance = Instantiate(boss_attack, spawnPosition, Quaternion.identity);
            GameObject attackInstance2 = Instantiate(boss_attack, spawnPosition2, Quaternion.identity);
            GameObject attackInstance3 = Instantiate(boss_attack, spawnPosition3, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 0.5f));
            StartCoroutine(DestroyAttack(attackInstance2, 0.5f));
            StartCoroutine(DestroyAttack(attackInstance3, 0.5f));

            yield return new WaitForSeconds(attackCooldown); // ��Ÿ�� ���� ���
        }
    }

    IEnumerator DestroyAttack(GameObject obj, float seconds)  // ���� ���ֱ�
    {
        yield return new WaitForSeconds(seconds); // ������ �ð� ���� ���
        Destroy(obj); // ������Ʈ �ı�
    }


    void Update()
    {

        transform.Translate(Vector2.right * Time.deltaTime * enemySpeed);

        if (transform.position.x < -15)
        {
            gameObject.SetActive(false);
        }

    }

}
