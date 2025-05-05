using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackComponent : MonoBehaviour
{
   public string characterId;
   public abstract void Attack(Vector2 targetPostion);
}

public class RangedAttackComponent : AttackComponent
{
    public override void Attack(Vector2 targetPostion)
    {

        //들어가야할 부분
        //-> ObectPool 생성 및 반환
        //-> 공격 Draw
        //-> 데미지?
        Debug.Log("원거리 공격");
    }
}
public class MagicAttackComponent : AttackComponent
{
    public override void Attack(Vector2 targetPostion)
    {
        Debug.Log("마법 공격");
    }
}
