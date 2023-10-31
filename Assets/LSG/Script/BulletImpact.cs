using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : PoolAble
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

    /*private void Update()
    {
        // �Ѿ��� ���� ���󰡸� ���� ���ֱ�
        if (this.transform.position.y > 5)
        {
            // ������Ʈ Ǯ�� ��ȯ
            ReleaseObject();
        }
    }*/
}
