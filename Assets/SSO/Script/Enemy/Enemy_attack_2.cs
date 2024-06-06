using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack_2 : PoolAble
{
    // 원거리 몬스터의 공격
    // 공격 당했을떄 데미지를 입히는 스크립트
    public float speed;     // 발사체의 속도
    private Transform target;
    [SerializeField]
    private Enemy_Respown enemyRespawner;  // 참조

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Castle").transform;
        // 만약 enemyRespawner가 설정되지 않았다면, 현재 씬에서 Enemy_Respown 인스턴스를 찾아 설정합
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
                double damageValue = enemyRespawner.GetEnemyDamage();
                target.OnDamage(damageValue);  // 여기서 RaycastHit2D 정보는 필요에 따라 적절히 설정
                                               //Debug.Log("enemy가 공격함");
                Destroy(gameObject);  // 발사체 삭제
                //ReleaseObject();
            }
        }
    }

    private void Update()
    {
        // 발사체가 목표를 향해 이동
        Vector2 moveDirection = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }
}
