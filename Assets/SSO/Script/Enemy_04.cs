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
    public float hp = 3.0f;
    public float damage = 1.0f;

    void Start()
    {
        transform.position = StartPosition;
        target = GameObject.FindGameObjectWithTag("Castle").transform; // "Castle" �±׸� ���� ������Ʈ�� ã���ϴ�.
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //�������� ����
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("����4 óġ");
        }
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
            Vector3 spawnPosition = transform.position - Vector3.right;
            GameObject attackInstance = Instantiate(enemy_attack_4, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(attackCooldown); // ��Ÿ�� ���� ���
        }
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
