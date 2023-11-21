using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_04 : MonoBehaviour, IDamageable
{
    // ���Ÿ� ���� ����
    public float enemySpeed;              // �̵��ӵ�
    public Vector2 StartPosition;         // ��ȯ ��ġ
    public float attackCooldown;  // ���� ��Ÿ��
    private double hp;                         // ü��
    private float damage;                    // ���� ������
    public GameObject enemy_attack_4;             // ���� ��Ÿ�� (���Ÿ� ����)
    public float speed; // �߻�ü�� �ӵ�
    private Transform target; // �߻�ü�� ��ǥ
    private float originalEnemySpeed;                 // �ʱ� enemySpeed ���� �����ϱ� ���� ����
    private Animator enemy_attack_animation;  //�ִϸ��̼�
    private Coroutine attackCoroutine;  // Attack �ڷ�ƾ�� �����ϱ� ���� ����
    private bool isAttack = true; // �ڷ�ƾ�� ������ �� ������ ����ϴ� �÷���


    void Start()
    {
        enemy_attack_animation = GetComponent<Animator>();

        transform.position = StartPosition;
        target = GameObject.FindGameObjectWithTag("Castle").transform; // "Castle" �±׸� ���� ������Ʈ�� ã���ϴ�.
        originalEnemySpeed = enemySpeed;  // ó�� enemySpeed ���� ����
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //���Ϳ��� �������� ����
    {
        /*hp -= Damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("����4 óġ");
        }*/
    }

    public void SetStats(double health, float dmg)
    {
        hp = health;
       damage = dmg;
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
            GameObject attackInstance = Instantiate(enemy_attack_4, spawnPosition, Quaternion.identity);

            // ���
            yield return new WaitForSeconds(attackCooldown);

            // �߻�ü ������ ������ ����
            Destroy(attackInstance);
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
