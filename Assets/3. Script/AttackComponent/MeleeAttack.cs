using UnityEngine;

public class MeleeAttack : AttackComponent
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


        my_bullet_obj.GetComponent<Rigidbody2D>().velocity = my_bullet_obj.transform.right * speed;

    }

   
}