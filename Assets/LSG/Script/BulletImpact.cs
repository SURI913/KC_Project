using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : PoolAble
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.layer == 6) //"Target"���̾ �ش��ϴ� ������Ʈ���
        {
            /*IDamageable target = collision.GetComponent<IDamageable>();
            if(target != null)
            {
                
            }*/
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
