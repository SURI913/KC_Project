using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_04 : MonoBehaviour, IDamageable
{
    // ���Ÿ� ���� ����
    public float enemySpeed = 0;
    public Vector2 StartPosition;
    public GameObject enemy_attack_4;  // �߻�ü ������
    public float attackCooldown = 2f;  // ���� ��Ÿ��
    public float speed = 5f; // �߻�ü�� �ӵ�
    private Transform target; // �߻�ü�� ��ǥ
    private double hp;
    private float damage;
    private float originalEnemySpeed;  // �ʱ� enemySpeed ���� �����ϱ� ���� ����
    private Animator enemy_attack_animation;

    void Start()
    {
        enemy_attack_animation = GetComponent<Animator>();

        transform.position = StartPosition;
        target = GameObject.FindGameObjectWithTag("Castle").transform; // "Castle" �±׸� ���� ������Ʈ�� ã���ϴ�.
        originalEnemySpeed = enemySpeed;  // ó�� enemySpeed ���� ����
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //�������� ����
    {
        hp -= Damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("����4 óġ");
        }
    }

    public void SetStats(double health, float dmg)
    {
        hp = health;
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
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
            enemy_attack_animation.SetTrigger("Enemy_attack");
            Vector3 spawnPosition = transform.position - Vector3.right;
            GameObject attackInstance = Instantiate(enemy_attack_4, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(attackCooldown); // ��Ÿ�� ���� ���
        }
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
