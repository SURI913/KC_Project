using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackComponent
{
    public override void Attack(Vector2 targetPostion)
    {
        var my_bullet_obj = ObjectPoolManager.instance.GetGo(characterId + "_AttackObject");
        my_bullet_obj.transform.position = targetPostion;
        //애니메이션 끝나면 사라지도록 작업하자 > 유니티 애니메이션으로 작업할 것
    }

}
