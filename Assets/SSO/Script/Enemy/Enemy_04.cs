using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_04 : MonoBehaviour, IDamageable
{
    // 원거리 공중 몬스터
    public float enemySpeed;              // 이동속도
    public Vector2 StartPosition;         // 소환 위치
    public float attackCooldown;  // 공격 쿨타임
    private double hp;                         // 체력
    private float damage;                    // 몬스터 데미지
    public GameObject enemy_attack_4;             // 공격 스타일 (원거리 공격)
    public float speed; // 발사체의 속도
    private Transform target; // 발사체의 목표
    private float originalEnemySpeed;                 // 초기 enemySpeed 값을 저장하기 위한 변수
    private Animator enemy_attack_animation;  //애니메이션
    private Coroutine attackCoroutine;  // Attack 코루틴을 저장하기 위한 변수
    private bool isAttack = true; // 코루틴을 시작할 때 공격을 허용하는 플래그


    void Start()
    {
        enemy_attack_animation = GetComponent<Animator>();

        transform.position = StartPosition;
        target = GameObject.FindGameObjectWithTag("Castle").transform; // "Castle" 태그를 가진 오브젝트를 찾습니다.
        originalEnemySpeed = enemySpeed;  // 처음 enemySpeed 값을 저장
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //몬스터에게 데미지를 입힘
    {
        hp -= Damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("몬스터4 처치");
        }
    }

    public void SetStats(double health, float dmg)
    {
        hp = health;
       damage = dmg;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            Debug.Log("enemy_04의 충돌");
            enemySpeed = 0;

            // 공격 플래그가 true인 경우에만 공격 코루틴을 시작
            if (isAttack)
            {
                // 이전에 실행 중이던 Attack 코루틴을 중지
                if (attackCoroutine != null)
                {
                    StopCoroutine(attackCoroutine);
                }

                // Attack 코루틴을 시작
                attackCoroutine = StartCoroutine(Attack());
                isAttack = false; // 공격 코루틴을 한 번 시작하면 플래그를 false로 변경
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            enemySpeed = originalEnemySpeed;  // enemySpeed 값을 원래 값으로 재설정
            isAttack = true; // 플래그를 true로 변경하여 다시 공격을 시작할 수 있도록 함
        }
    }

    IEnumerator Attack()
    {
        while (true) // 무한 반복
        {
            enemy_attack_animation.SetTrigger("Enemy_attack");
            Vector3 spawnPosition = transform.position - Vector3.right + (Vector3.up / 2);
            GameObject attackInstance = Instantiate(enemy_attack_4, spawnPosition, Quaternion.identity);

            // 대기
            yield return new WaitForSeconds(attackCooldown);

            // 발사체 수명이 끝나면 제거
            Destroy(attackInstance);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            Debug.Log("enemy_04의 충돌");
            enemySpeed = 0;

            // 이전에 실행 중이던 Attack 코루틴을 중지
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }

            // Attack 코루틴을 시작
            attackCoroutine = StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            enemySpeed = originalEnemySpeed;  // enemySpeed 값을 원래 값으로 재설정
        }
    }

    IEnumerator Attack()
    {
        while (true) // 무한 반복
        {
            enemy_attack_animation.SetTrigger("Enemy_attack");
            Vector3 spawnPosition = transform.position - Vector3.right + (Vector3.up / 2);
            GameObject attackInstance = Instantiate(enemy_attack_4, spawnPosition, Quaternion.identity);

            // 대기
            yield return new WaitForSeconds(attackCooldown);

            // 발사체 수명이 끝나면 제거
            Destroy(attackInstance);
        }
    }*/

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * enemySpeed);

        if (transform.position.x < -20)      // x축으로 -20까지 가면 (화면 밖)
        {
            gameObject.SetActive(false);
        }
    }
}
