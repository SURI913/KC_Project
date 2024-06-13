using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack_1 : PoolAble
{
    // 근거리 몬스터 공격
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
            DamageableImp target = collision.GetComponent<DamageableImp>();
            if (target != null && enemyRespawner)
            {
                double damageValue = enemyRespawner.GetEnemyDamage();
                target.OnDamage(damageValue);  // 여기서 RaycastHit2D 정보는 필요에 따라 적절히 설정
                //Debug.Log("enemy가 공격함");
                StartCoroutine(DestroyAttack());
            }
        }
    }

    IEnumerator DestroyAttack()   // 공격 없애기
    {
        yield return new WaitForSeconds(1.5f); // 지정된 시간 동안 대기
        ReleaseObject();
    }
}
