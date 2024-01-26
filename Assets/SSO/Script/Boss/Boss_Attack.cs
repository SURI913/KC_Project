using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{
    // 보스 공격 당했을떄 데미지를 입히는 스크립트
    [SerializeField]
    private Enemy_Respown enemyRespawner;

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
        {
            DamageableImp target = collision.GetComponent<DamageableImp>();
            if (target != null && enemyRespawner)
            {
                double damageValue = enemyRespawner.GetBossDamage();
                target.OnDamage(damageValue);  // 여기서 RaycastHit2D 정보는 필요에 따라 적절히 설정
                Debug.Log("보스가 공격함");
            }
        }
    }
}
