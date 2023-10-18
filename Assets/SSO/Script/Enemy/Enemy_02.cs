using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_02 : MonoBehaviour, IDamageable
{
    // 근거리 몬스터 2
    public float enemySpeed = 0;
    public Vector2 StartPosition;
    public GameObject enemy_attack_2;   // attack1보다 더 큰 공격
    public float attackCooldown = 2f;  // 공격 쿨타임
    public float hp = 3.0f;
    public float damage = 1.0f;


    void Start()
    {
        transform.position = StartPosition;
    }

    public void OnDamage(double Damage, RaycastHit2D hit)   //데미지를 입힘
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            Debug.Log("몬스터2 처치");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle"))
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
            Vector3 spawnPosition = transform.position - Vector3.right;
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
        transform.Translate(Vector2.right * Time.deltaTime * enemySpeed);

        if (transform.position.x < -15)
        {
            gameObject.SetActive(false);
        }
    }
}
