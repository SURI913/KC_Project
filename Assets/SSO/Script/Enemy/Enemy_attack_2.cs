using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack_2 : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;

    // 공격 당했을떄 데미지를 입히는 스크립트
    

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Castle").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            Debug.Log("공격당함");
            Destroy(gameObject);  // 발사체 삭제
        }
    }

    private void Update()
    {
        // 발사체가 목표를 향해 이동합니다.
        Vector2 moveDirection = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }
}
