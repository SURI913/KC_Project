using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.gameObject.layer == 6) //"Target"레이어에 해당하는 오브젝트라면
        {
            //이펙트 실행 후
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == 6) //"Target"레이어에 해당하는 오브젝트라면
        {
            //이펙트 실행 후
            Destroy(gameObject);
        }
    }
}
