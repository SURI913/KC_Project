using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_02 : MonoBehaviour, IDamageable
{
    // �ٰŸ� ���� 2
    public float enemySpeed = 0;
    public Vector2 StartPosition;
    public GameObject enemy_attack_2;   // attack1���� �� ū ����
    public float attackCooldown = 2f;  // ���� ��Ÿ��
    private double hp;
    private float damage;
    private float originalEnemySpeed;  // �ʱ� enemySpeed ���� �����ϱ� ���� ����


    void Start()
    {
        transform.position = StartPosition;
        originalEnemySpeed = enemySpeed;  // ó�� enemySpeed ���� ����
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //�������� ����
    {
        hp -= Damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("����2 óġ");
        }
    }

    public void SetStats(double health, float dmg)
    {
        hp = health;
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle"))
        {
            Debug.Log("�浹");
            enemySpeed = 0;
            StartCoroutine(SpawnWithCooldown()); // Coroutine ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  // �浹�� ������ ȣ��Ǵ� �Լ�
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            enemySpeed = originalEnemySpeed;  // enemySpeed ���� ���� ������ �缳��
        }
    }

    IEnumerator SpawnWithCooldown()
    {
        while (true) // ���� �ݺ�
        {
            Vector3 spawnPosition = transform.position - Vector3.right + (Vector3.up / 2);
            GameObject attackInstance = Instantiate(enemy_attack_2, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 1f));

            yield return new WaitForSeconds(attackCooldown); // ��Ÿ�� ���� ���
        }
    }

    IEnumerator DestroyAttack(GameObject obj, float seconds)    // ���ݾ��ֱ�
    {
        yield return new WaitForSeconds(seconds); // ������ �ð� ���� ���
        Destroy(obj); // ������Ʈ �ı�
    }


    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * enemySpeed);

        if (transform.position.x < -15)
        {
            gameObject.SetActive(false);
        }
    }
}
