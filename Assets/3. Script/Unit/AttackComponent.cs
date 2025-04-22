using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackComponent : MonoBehaviour
{
   public string characterId;
    public float attackPower;
    public float attackPowerMultiplier;
   public abstract void Attack(GameObject target);
}

public class RangedAttackComponent : AttackComponent
{
    public override void Attack(GameObject target)
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
    public override void Attack(GameObject target)
    {
        Debug.Log("마법 공격");
    }
}
