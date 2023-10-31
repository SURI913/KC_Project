using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack_2 : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;

    // ���� �������� �������� ������ ��ũ��Ʈ
    

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Castle").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Castle") || collision.CompareTag("Player"))
        {
            Debug.Log("���ݴ���");
            Destroy(gameObject);  // �߻�ü ����
        }
    }

    private void Update()
    {
        // �߻�ü�� ��ǥ�� ���� �̵��մϴ�.
        Vector2 moveDirection = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;
    }
}
