using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_03 : MonoBehaviour
{
    // 원거리 몬스터
    public float enemySpeed = 0;
    public Vector2 StartPosition;
    public GameObject enemy_attack_3;  // 발사체 날리기
    public float attackCooldown = 2f;  // 공격 쿨타임
    public float speed = 5f; // 발사체의 속도
    private Transform target; // 발사체의 목표

    void Start()
    {
        transform.position = StartPosition;
        target = GameObject.FindGameObjectWithTag("Castle").transform; // "Castle" 태그를 가진 오브젝트를 찾습니다.
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
            Vector3 spawnPosition = transform.position - Vector3.right;
            GameObject attackInstance = Instantiate(enemy_attack_3, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(attackCooldown); // 쿨타임 동안 대기
        }
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
