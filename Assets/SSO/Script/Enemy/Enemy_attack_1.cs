using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack_1 : MonoBehaviour
{
    [SerializeField]
    private Enemy_Respown enemyRespawner;  // 참조

    private void Start()
    {
        // 만약 enemyRespawner가 설정되지 않았다면, 현재 씬에서 Enemy_Respown 인스턴스를 찾아 설정
        // 이렇게 하면 에디터에서 수동으로 설정하지 않아도 자동으로 참조를 찾아줌
        if (!enemyRespawner)
        {
            enemyRespawner = FindObjectOfType<Enemy_Respown>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {   // 공격이 플레이어나 캐슬에 충돌했다면
            IDamageable target = collision.GetComponent<IDamageable>();
            if (target != null && enemyRespawner)
            {
                double damageValue = enemyRespawner.GetEnemyDamage();
                target.OnDamage(damageValue, new RaycastHit2D());  // 여기서 RaycastHit2D 정보는 필요에 따라 적절히 설정
                Debug.Log("enemy의 공격당함");
            }
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)  
    {
        if (collision.collider.CompareTag("Castle") || collision.collider.CompareTag("Player"))
        {   // 공격이 플레이어나 캐슬에 충돌했다면
            IDamageable target = collision.collider.GetComponent<IDamageable>();
            if (target != null && enemyRespawner)
            {
                double damageValue = enemyRespawner.GetEnemyDamage();
                target.OnDamage(damageValue, new RaycastHit2D());  // 여기서 RaycastHit2D 정보는 필요에 따라 적절히 설정
                Debug.Log("enemy의 공격당함");
            }
        }
    }*/
}
