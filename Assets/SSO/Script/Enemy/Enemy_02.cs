using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy_02 : MonoBehaviour, IDamageable
{
    // 근거리 몬스터 2
    public float enemySpeed;              // 이동속도
    public Vector2 StartPosition;         // 소환 위치
    public float attackCooldown = 2f;  // 공격 쿨타임
    private double hp;                         // 체력
    private float damage;                    // 몬스터 데미지
    public GameObject enemy_attack_2;             // 공격 스타일 (일반 근거리 공격)
    private float originalEnemySpeed;                 // 초기 enemySpeed 값을 저장하기 위한 변수
    private Animator enemy_attack_animation;  //애니메이션

    void Start()
    {
        enemy_attack_animation = GetComponent<Animator>();

        transform.position = StartPosition;
        originalEnemySpeed = enemySpeed;  // 처음 enemySpeed 값을 저장
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   // 몬스터에게 데미지를 입힘
    {
        hp -= Damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("몬스터2 처치");
        }
    }

    public void SetStats(double health, float dmg)
    {
        hp = health;
        damage = dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Castle"))
        {
            Debug.Log("충돌");
            enemySpeed = 0;
            StartCoroutine(SpawnWithCooldown()); // Coroutine 시작
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Castle") || collision.collider.CompareTag("Player"))
        {
            enemySpeed = originalEnemySpeed;  // enemySpeed 값을 원래 값으로 재설정
        }
    }

    IEnumerator SpawnWithCooldown()
    {
        while (true) // 무한 반복
        {
            enemy_attack_animation.SetTrigger("Enemy_attack");
            Vector3 spawnPosition = transform.position - Vector3.right + (Vector3.up / 2);
            GameObject attackInstance = Instantiate(enemy_attack_2, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyAttack(attackInstance, 1f));

            yield return new WaitForSeconds(attackCooldown); // 쿨타임 동안 대기
        }
    }

    IEnumerator DestroyAttack(GameObject obj, float seconds)    // 공격없애기
    {
        yield return new WaitForSeconds(seconds); // 지정된 시간 동안 대기
        Destroy(obj); // 오브젝트 파괴
    }


    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * enemySpeed);

        if (transform.position.x < -20)    // x축으로 -20까지 가면 (화면 밖)
        {
            gameObject.SetActive(false);
        }
    }
}
