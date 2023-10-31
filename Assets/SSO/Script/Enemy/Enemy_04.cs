using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_04 : MonoBehaviour, IDamageable
{
    // 원거리 공중 몬스터
    public float enemySpeed = 0;
    public Vector2 StartPosition;
    public GameObject enemy_attack_4;  // 발사체 날리기
    public float attackCooldown = 2f;  // 공격 쿨타임
    public float speed = 5f; // 발사체의 속도
    private Transform target; // 발사체의 목표
    private double hp;
    private float damage;
    private float originalEnemySpeed;  // 초기 enemySpeed 값을 저장하기 위한 변수
    private Animator enemy_attack_animation;

    void Start()
    {
        enemy_attack_animation = GetComponent<Animator>();

        transform.position = StartPosition;
        target = GameObject.FindGameObjectWithTag("Castle").transform; // "Castle" 태그를 가진 오브젝트를 찾습니다.
        originalEnemySpeed = enemySpeed;  // 처음 enemySpeed 값을 저장
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //데미지를 입힘
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            Debug.Log("충돌");
            enemySpeed = 0;
            StartCoroutine(SpawnWithCooldown()); // Coroutine 시작
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  // 충돌이 끝나면 호출되는 함수
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            enemySpeed = originalEnemySpeed;  // enemySpeed 값을 원래 값으로 재설정
        }
    }

    IEnumerator SpawnWithCooldown()
    {
        while (true) // 무한 반복
        {
            enemy_attack_animation.SetTrigger("Enemy_attack");
            Vector3 spawnPosition = transform.position - Vector3.right;
            GameObject attackInstance = Instantiate(enemy_attack_4, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(attackCooldown); // 쿨타임 동안 대기
        }
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * enemySpeed);

        if (transform.position.x < -15)
        {
            gameObject.SetActive(false);
        }
    }
}
