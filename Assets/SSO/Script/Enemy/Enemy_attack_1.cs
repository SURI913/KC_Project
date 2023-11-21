using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack_1 : MonoBehaviour
{
    // �ٰŸ� ���� ����
    [SerializeField]
    private Enemy_Respown enemyRespawner;  // ����

    private void Start()
    {
        // ���� enemyRespawner�� �������� �ʾҴٸ�, ���� ������ Enemy_Respown �ν��Ͻ��� ã�� ����
        // �̷��� �ϸ� �����Ϳ��� �������� �������� �ʾƵ� �ڵ����� ������ ã����
        if (!enemyRespawner)
        {
            enemyRespawner = FindObjectOfType<Enemy_Respown>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {   // ������ �÷��̾ ĳ���� �浹�ߴٸ�
            IDamageable target = collision.GetComponent<IDamageable>();
            if (target != null && enemyRespawner)
            {
                double damageValue = enemyRespawner.GetEnemyDamage();
                target.OnDamage(damageValue, new RaycastHit2D());  // ���⼭ RaycastHit2D ������ �ʿ信 ���� ������ ����
                Debug.Log("enemy�� ������");
            }
        }
    }
}
