using UnityEngine;

public class RangedAttack : AttackComponent
{
    /*
     * parabola :   speed = 20;
     * straight :   speed = 15~17, 중력X
     * 
     * 포물선 타겟은 사거리 맞춰서 고정할 것
     */
    public float speed;

    public override void Attack(Vector2 targetPostion)
    {
        var my_bullet_obj = ObjectPoolManager.instance.GetGo(characterId + "_AttackObject");
       
        Vector2 currentPostion = new Vector2(transform.position.x, transform.position.y);

        //바라볼 방향 = (도착할 위치 - 시작 위치).정규화
        Vector2 currentDirection = new Vector2(targetPostion.x - currentPostion.x, targetPostion.y - currentPostion.y).normalized;
        my_bullet_obj.transform.right = currentDirection;
        my_bullet_obj.transform.position = currentPostion;

        //== 타겟 방향으로 회전값 ==// 아크탄젠트로 각 구하고 라디언값 전달하면 자동으로 z 축에 디그리 값으로 변경됨
        float radian = Mathf.Atan2(currentDirection.y, currentDirection.x);
        var myBullet = my_bullet_obj.GetComponent<Bullet>();
        myBullet.SetVelocity(my_bullet_obj.transform.right * speed);
        myBullet.SetParticleRotate(-radian);
    }

   
}