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
    private Coroutine attackCoroutine;  // Attack �ڷ�ƾ�� �����ϱ� ���� ����
    private bool isAttack = true; // �ڷ�ƾ�� ������ �� ������ ����ϴ� �÷���

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            Debug.Log("enemy_04�� �浹");
            enemySpeed = 0;

            // ���� �÷��װ� true�� ��쿡�� ���� �ڷ�ƾ�� ����
            if (isAttack)
            {
                // ������ ���� ���̴� Attack �ڷ�ƾ�� ����
                if (attackCoroutine != null)
                {
                    StopCoroutine(attackCoroutine);
                }

                // Attack �ڷ�ƾ�� ����
                attackCoroutine = StartCoroutine(Attack());
                isAttack = false; // ���� �ڷ�ƾ�� �� �� �����ϸ� �÷��׸� false�� ����
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            enemySpeed = originalEnemySpeed;  // enemySpeed ���� ���� ������ �缳��
            isAttack = true; // �÷��׸� true�� �����Ͽ� �ٽ� ������ ������ �� �ֵ��� ��
        }
    }

    IEnumerator Attack()
    {
        while (true) // ���� �ݺ�
        {
            enemy_attack_animation.SetTrigger("Enemy_attack");
            Vector3 spawnPosition = transform.position - Vector3.right + (Vector3.up / 2);
            GameObject attackInstance = Instantiate(enemy_attack_1, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 1f));

            // ���
            yield return new WaitForSeconds(attackCooldown);

            // �߻�ü ������ ������ ����
            Destroy(attackInstance);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
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
            Destroy(attackInstance);
        }
    }*/

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


