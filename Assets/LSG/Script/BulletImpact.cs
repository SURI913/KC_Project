using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.gameObject.layer == 6) //"Target"���̾ �ش��ϴ� ������Ʈ���
        {
            //����Ʈ ���� ��
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == 6) //"Target"���̾ �ش��ϴ� ������Ʈ���
        {
            //����Ʈ ���� ��
            Destroy(gameObject);
        }
    }
}
