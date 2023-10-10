using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    [SerializeField]
    private float movespeed = 5.0f;
    [SerializeField]
    private float sign = -1.0f;
    private bool iswalk = true;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Time.time >= 0 && iswalk)
        {
            transform.position += new Vector3(movespeed * Time.deltaTime * sign, 0, 0);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� �浹 ");
            iswalk = false;
            rb.velocity = Vector2.zero;
            //�÷��̾��±׿� ������� ���� �ӵ� ���η� ���� 
           
        }
    }
}
