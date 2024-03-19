using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : PoolAble
{
    private Transform init_transform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == 6) //"Target"레이어에 해당하는 오브젝트라면
        {
            /*IDamageable target = collision.GetComponent<IDamageable>();
            if(target != null)
            {
                
            }*/
            //이펙트 실행 후
            ReleaseObject();
            transform.position = init_transform.position;
        }
    }

    private void Start()
    {
        init_transform = transform;
    }

    private void Update()
    {
        // 총알이 많이 날라가면 삭제 해주기
        if (this.transform.position.y > -10000)
        {
            // 오브젝트 풀에 반환
            ReleaseObject();
            transform.position = init_transform.position;

        }
    }
}
