using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable

{
    // �ٰŸ� ���� 1
    public float enemySpeed = 0;
    public Vector2 StartPosition;
    public GameObject enemy_attack_1; // ���� ��Ÿ�� (�Ϲ� ����)
    public float attackCooldown = 2f;  // ���� ��Ÿ��
    private double hp;
    private float damage;

    public void OnDamage(double Damage, RaycastHit2D hit)   //�������� ����
    {
        hp -= Damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("����1 óġ");
        }
    }

    public void SetStats(double health, float dmg)
    {
        hp = health;
        damage = dmg;
    }

    void Start()
    {
        transform.position = StartPosition;
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

    IEnumerator SpawnWithCooldown()
    {
        while (true) // ���� �ݺ�
        {
            Vector3 spawnPosition = transform.position - Vector3.right;         // ������ �����Ǵ� ��ġ
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
        transform.Translate(Vector2.right * Time.deltaTime * enemySpeed);

        if (transform.position.x < -15)
        {
            gameObject.SetActive(false);
        }
    }
}


