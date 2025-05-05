using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolAble
{
 
    Rigidbody2D myRigdbody2D;
    
    private void Start()
    {
        myRigdbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.right = myRigdbody2D.velocity;
    }

    //바닥에 닿거나 적 피격시 총알 반납
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.Pool != null && (collision.CompareTag("Plane") || collision.CompareTag("Target")))
        {
            ReleaseObject();
        }
    }
}
