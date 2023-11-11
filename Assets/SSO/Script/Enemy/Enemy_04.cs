using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_04 : MonoBehaviour, IDamageable
{
    // ���Ÿ� ���� ����
    public float enemySpeed;              // �̵��ӵ�
    public Vector2 StartPosition;         // ��ȯ ��ġ
    public float attackCooldown = 5f;  // ���� ��Ÿ��
    private double hp;                         // ü��
    private float damage;                    // ���� ������
    public GameObject enemy_attack_4;             // ���� ��Ÿ�� (���Ÿ� ����)
    public float speed = 5f; // �߻�ü�� �ӵ�
    private Transform target; // �߻�ü�� ��ǥ
    private float originalEnemySpeed;                 // �ʱ� enemySpeed ���� �����ϱ� ���� ����
    private Animator enemy_attack_animation;  //�ִϸ��̼�

    void Start()
    {
        enemy_attack_animation = GetComponent<Animator>();

        transform.position = StartPosition;
        target = GameObject.FindGameObjectWithTag("Castle").transform; // "Castle" �±׸� ���� ������Ʈ�� ã���ϴ�.
        originalEnemySpeed = enemySpeed;  // ó�� enemySpeed ���� ����
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //���Ϳ��� �������� ����
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Castle") || collision.collider.CompareTag("Player"))
        {
            Debug.Log("�浹");
            enemySpeed = 0;
            StartCoroutine(SpawnWithCooldown()); // Coroutine ����
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Castle") || collision.collider.CompareTag("Player"))
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

        if (transform.position.x < -20)      // x������ -20���� ���� (ȭ�� ��)
        {
            gameObject.SetActive(false);
        }
    }
}
