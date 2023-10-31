using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack_2 : MonoBehaviour
{
    // ���� �������� �������� ������ ��ũ��Ʈ
    public float speed = 5f;     // �߻�ü�� �ӵ�
    private Transform target;
    [SerializeField]
    private Enemy_Respown enemyRespawner;  // ����

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Castle").transform;
        // ���� enemyRespawner�� �������� �ʾҴٸ�, ���� ������ Enemy_Respown �ν��Ͻ��� ã�� ������
        // �̷��� �ϸ� �����Ϳ��� �������� �������� �ʾƵ� �ڵ����� ������ ã����
        if (!enemyRespawner)
        {
            enemyRespawner = FindObjectOfType<Enemy_Respown>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable target = collision.collider.GetComponent<IDamageable>();
        if (target != null && enemyRespawner)
        {
            double damageValue = enemyRespawner.GetEnemyDamage();
            target.OnDamage(damageValue, new RaycastHit2D());  // ���⼭ RaycastHit2D ������ �ʿ信 ���� ������ ����
            Debug.Log("enemy�� ���ݴ���");
            Destroy(gameObject);  // �߻�ü ����
        }
    }

    private void Update()
    {
        // �߻�ü�� ��ǥ�� ���� �̵�
        Vector2 moveDirection = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }
}
