using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{
    // ���� �������� �������� ������ ��ũ��Ʈ
    [SerializeField]
    private Enemy_Respown enemyRespawner;

    private void Start()
    {
        // ���� enemyRespawner�� �������� �ʾҴٸ�, ���� ������ Enemy_Respown �ν��Ͻ��� ã�� �����մϴ�.
        // (�̷��� �ϸ� �����Ϳ��� �������� �������� �ʾƵ� �ڵ����� ������ ã���ݴϴ�.)
        if (!enemyRespawner)
        {
            enemyRespawner = FindObjectOfType<Enemy_Respown>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            if (target != null && enemyRespawner)
            {
                double damageValue = enemyRespawner.GetEnemyDamage();
                target.OnDamage(damageValue, new RaycastHit2D());  // ���⼭ RaycastHit2D ������ �ʿ信 ���� ������ ����
                Debug.Log("enemy�� ���ݴ���");
            }
        }
    }
}
