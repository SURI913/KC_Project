using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable

{
    // 근거리 몬스터 1
    public float enemySpeed = 0;
    public Vector2 StartPosition;
    public GameObject enemy_attack_1; // 공격 스타일 (일반 공격)
    public float attackCooldown = 2f;  // 공격 쿨타임
    public float hp = 3.0f;
    public float damage = 1.0f;

    public void OnDamage(double Damage, RaycastHit2D hit)   //데미지를 입힘
    {
        hp -= damage;
        if(hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("몬스터1 처치");
        }
    }

void Start()
    {
        transform.position = StartPosition;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            Debug.Log("충돌");
            enemySpeed = 0;
            StartCoroutine(SpawnWithCooldown()); // Coroutine 시작
        }
    }

    IEnumerator SpawnWithCooldown()
    {
        while (true) // 무한 반복
        {
            Vector3 spawnPosition = transform.position - Vector3.right;         // 공격이 생성되는 위치
            GameObject attackInstance = Instantiate(enemy_attack_1, spawnPosition, Quaternion.identity);  // 공격 생성
            StartCoroutine(DestroyAfterSeconds(attackInstance, 1f));  // 오브젝트 파괴 코루틴 생성

            yield return new WaitForSeconds(attackCooldown); // 쿨타임 동안 대기
        }
    }

    IEnumerator DestroyAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds); // 지정된 시간 동안 대기
        Destroy(obj); // 오브젝트 파괴
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


