using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTanker : Cat
{
    public GameObject attack_effect;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) //타겟레이어의 경우
        {
            //데미지 스크립트 확인시 공격 시작
            if (collision.collider.GetComponent<IDamageable>() != null && !is_attack)
            {
                //StartCoroutine(AttackEft(collision));
                player_rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

}
