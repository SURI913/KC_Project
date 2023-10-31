using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack_2 : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;
    [SerializeField]
    private Enemy_Respown enemyRespawner;

    // ���� �������� �������� ������ ��ũ��Ʈ


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Castle").transform;
        // ���� enemyRespawner�� �������� �ʾҴٸ�, ���� ������ Enemy_Respown �ν��Ͻ��� ã�� �����մϴ�.
        // (�̷��� �ϸ� �����Ϳ��� �������� �������� �ʾƵ� �ڵ����� ������ ã���ݴϴ�.)
        if (!enemyRespawner)
        {
            enemyRespawner = FindObjectOfType<Enemy_Respown>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable target = collision.GetComponent<IDamageable>();
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
        // �߻�ü�� ��ǥ�� ���� �̵��մϴ�.
        Vector2 moveDirection = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }
}
