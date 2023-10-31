using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable

{
    // �ٰŸ� ���� 1
    public float enemySpeed;              // �̵��ӵ�
    public Vector2 StartPosition;         // ��ȯ ��ġ
    public float attackCooldown = 2f;  // ���� ��Ÿ��
    private double hp;                         // ü��
    private float damage;                    // ���� ������
    public GameObject enemy_attack_1;             // ���� ��Ÿ�� (�Ϲ� �ٰŸ� ����)
    private float originalEnemySpeed;                 // �ʱ� enemySpeed ���� �����ϱ� ���� ����
    private Animator enemy_attack_animation;  //�ִϸ��̼�

    public void OnDamage(double Damage, RaycastHit2D hit)   // ���Ϳ��� �������� ������ �Լ�
    {
        hp -= Damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("����1 óġ");
        }
    }

    public void SetStats(double health, float dmg)  // enemy_respown���� ������ ü��, ������ �ҷ�����
    {
        hp = health;
        damage = dmg;
    }

    void Start()
    {
        enemy_attack_animation = GetComponent<Animator>();

        transform.position = StartPosition;
        originalEnemySpeed = enemySpeed;  // ó�� enemySpeed ���� ����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Castle") || collision.collider.CompareTag("Player"))
        {
            Debug.Log("�浹");
            enemySpeed = 0;                                    // �浹�ߴٸ�, �̵��� ����
            StartCoroutine(SpawnWithCooldown()); // Coroutine ����
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Castle") || collision.collider.CompareTag("Player"))
        {
            enemySpeed = originalEnemySpeed;  // �浹�� �������ٸ�, �ٽ� �̵�
        }
    }

    IEnumerator SpawnWithCooldown()
    {
        while (true) // ���� �ݺ�
        {
            enemy_attack_animation.SetTrigger("Enemy_attack");
            Vector3 spawnPosition = transform.position - Vector3.right + (Vector3.up / 2);         // ������ �����Ǵ� ��ġ
            GameObject attackInstance = Instantiate(enemy_attack_1, spawnPosition, Quaternion.identity);  // ���� ����
            StartCoroutine(DestroyAttack(attackInstance, 1f));  // ������Ʈ �ı� �ڷ�ƾ ����

            yield return new WaitForSeconds(attackCooldown); // ��Ÿ�� ���� ���
        }
    }

    IEnumerator DestroyAttack(GameObject obj, float seconds)   // ���� ���ֱ�
    {
        yield return new WaitForSeconds(seconds); // ������ �ð� ���� ���
        Destroy(obj); // ������Ʈ �ı�
    }


    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * enemySpeed);

        if (transform.position.x < -20)    // x������ -20���� ���� (ȭ�� ��)
        {
            gameObject.SetActive(false);
        }
    }
}


