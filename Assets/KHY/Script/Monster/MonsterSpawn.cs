
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject Monster;

    [SerializeField]
   // private float movespeed = 5.0f;
   // [SerializeField]
    //private float sign = -1.0f;

    void Start()
    {
        //몬스터 복사 
        for (int i = 0; i < 2; i++)
        {
           // Instantiate(Monster, transform.position, Quaternion.identity);
        }
    }


    void Update()
    {
       // Move();
    }

   /* private void Move()
    {
        //몬스터 좌측으로 움직이기 
        if (Time.time >= 0)
        {
            transform.position += new Vector3(movespeed * Time.deltaTime * sign, 0, 0);
        }
    }*/
    /* private void OnCollisionEnter2D(Collision2D collision)
   {
       if(collision.gameObject.CompareTag("Player"))
       {
           moving = false;
       }
   }*/
}
